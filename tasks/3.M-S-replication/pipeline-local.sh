#!/bin/bash 

docker network create msreplication

# run master
docker run -d --name=master --net=msreplication --hostname=master \
  -e MYSQL_ROOT_PASSWORD=admin \
  mysql:8.0.19 \
  --server-id=1 \
  --log-bin='mysql-bin-1.log' \
  --gtid_mode=ON \
  --enforce-gtid-consistency=ON \
  --default-authentication-plugin=mysql_native_password \

docker run -d --name=slave --net=msreplication --hostname=slave \
  -e MYSQL_ROOT_PASSWORD=admin \
  mysql:8.0.19 \
  --server-id=2 \
  --log-bin='mysql-bin-1.log' \
  --gtid_mode=ON \
  --enforce-gtid-consistency=ON \
  --skip-slave-start \
  --default-authentication-plugin=mysql_native_password \
  
#on master  
CREATE USER 'repl'@'%' IDENTIFIED WITH mysql_native_password BY 'repl';
GRANT REPLICATION SLAVE ON *.* TO 'repl'@'%';
FLUSH PRIVILEGES;

#on slave
set global read_only = 1;
CHANGE MASTER TO
MASTER_HOST = 'master',
MASTER_PORT = 3306,
MASTER_USER = 'repl',
MASTER_PASSWORD = 'repl',
MASTER_AUTO_POSITION = 1;
START SLAVE;

#proxy
docker pull proxysql/proxysql:2.0.9

docker run -d --name=proxysql --hostname=proxysql --net=msreplication \
-p 16032:6032 -p 16033:6033 \
-v /$(pwd)/proxysql.cnf:/etc/proxysql.cnf \
proxysql/proxysql:2.0.9

#on proxysql

#add mysql servers
INSERT INTO mysql_servers(hostgroup_id,hostname,port) VALUES (1,'master',3306);
INSERT INTO mysql_servers(hostgroup_id,hostname,port) VALUES (1,'slave',3306);
#setup monitoring
UPDATE global_variables SET variable_value='user' WHERE variable_name='mysql-monitor_username';
UPDATE global_variables SET variable_value='password' WHERE variable_name='mysql-monitor_password';
UPDATE global_variables SET variable_value='2000' WHERE variable_name IN ('mysql-monitor_connect_interval','mysql-monitor_ping_interval','mysql-monitor_read_only_interval');
#apply and save config
LOAD MYSQL VARIABLES TO RUNTIME;
SAVE MYSQL VARIABLES TO DISK;
#health check
SELECT * FROM monitor.mysql_server_connect_log ORDER BY time_start_us DESC LIMIT 10;
SELECT * FROM monitor.mysql_server_ping_log ORDER BY time_start_us DESC LIMIT 10;
#enable mysql servers
LOAD MYSQL SERVERS TO RUNTIME;

#replication hostgroup
#чтобы proxysql проверял read_only статус серверов (и на основе этого мог маршрутизировать траффик)
#нужно добавить сервера в mysql_replication_hostgroup
#этой команда указывает read_only сервера помещать во 2 группу, writes в 1-ю.
INSERT INTO mysql_replication_hostgroups VALUES (1,2,'read_only','cluster1');
#apply config
LOAD MYSQL SERVERS TO RUNTIME;
#check who is read_only
SELECT * FROM monitor.mysql_server_read_only_log ORDER BY time_start_us DESC LIMIT 10;
#save config to the disk
SAVE MYSQL SERVERS TO DISK;
SAVE MYSQL VARIABLES TO DISK;

#mysql users
INSERT INTO mysql_users(username,password,default_hostgroup) VALUES ('root','password',1);
LOAD MYSQL USERS TO RUNTIME;
#hash passwords
SAVE MYSQL USERS FROM RUNTIME;
SAVE MYSQL USERS TO DISK;
  
CREATE USER 'huy1'@'%' IDENTIFIED WITH mysql_native_password BY 'huy1';
GRANT ALL ON *.* TO 'huy1'@'%';
FLUSH PRIVILEGES;

#query rules
INSERT INTO mysql_query_rules (rule_id,active,match_digest,destination_hostgroup,apply)
VALUES
(1,1,'^SELECT.*FOR UPDATE$',1,1),
(2,1,'^SELECT',2,1);
LOAD MYSQL QUERY RULES TO RUNTIME;
SAVE MYSQL QUERY RULES TO DISK; # if you want this change to be permanent