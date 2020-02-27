#!/bin/bash 

# Настравиваем async replication

docker network create semisync

# run master
docker run -d --name=master --net=semisync --hostname=master \
  -p 3306:3306 \
  -e MYSQL_ROOT_PASSWORD=admin \
  mysql:8.0.19 \
  --server-id=1 \
  --log-bin='mysql-bin-1.log' \
  --gtid_mode=ON \
  --enforce-gtid-consistency=ON

# user for replication
docker exec -it master mysql -uroot -padmin \
  -e "CREATE USER 'repl'@'%' IDENTIFIED WITH mysql_native_password BY 'repl';" \
  -e "GRANT REPLICATION SLAVE ON *.* TO 'repl'@'%';" \
  -e "FLUSH PRIVILEGES;"

# run slave1, slave2
for N in 1 2
do docker run -d --name=slave$N --net=semisync --hostname=slave$N \
  -e MYSQL_ROOT_PASSWORD=admin \
  mysql:8.0.19 \
  --server-id=1$N \
  --log-bin='mysql-bin-1.log' \
  --gtid_mode=ON \
  --enforce-gtid-consistency=ON \
  --skip-slave-start
done

for N in 1 2
do docker exec -it slave$N mysql -uroot -padmin\
  -e "set global read_only = 1;" \
  -e "CHANGE MASTER TO" \
  -e "MASTER_HOST = 'master'," \
  -e "MASTER_PORT = 3306," \
  -e "MASTER_USER = 'repl'," \
  -e "MASTER_PASSWORD = 'repl'," \
  -e "MASTER_AUTO_POSITION = 1;" \
  -e "START SLAVE;"
done

# Накатываем миграции из приложения.
# Генерируем немного пользователей.
# Проверяем, что async replication работает.

# Включаем semisync replication on master and slave1

# master
docker exec -it master mysql -uroot -padmin \
  -e "INSTALL PLUGIN rpl_semi_sync_master SONAME 'semisync_master.so';" \
  -e "SET GLOBAL rpl_semi_sync_master_enabled = 1;" \
  -e "SET GLOBAL rpl_semi_sync_master_timeout = 10000;" # default ms

# slave1
docker exec -it slave1 mysql -uroot -padmin \
  -e "INSTALL PLUGIN rpl_semi_sync_slave SONAME 'semisync_slave.so';" \
  -e "SET GLOBAL rpl_semi_sync_slave_enabled = 1;" \
  -e "STOP SLAVE IO_THREAD; # need to stop async replication" \
  -e "START SLAVE IO_THREAD;" 

# команды для мониторинга:
# SHOW VARIABLES LIKE 'rpl_semi_sync%';
# SHOW STATUS LIKE 'Rpl_semi_sync%';
 
# Генерируем немного пользователей.
# Проверяем, что semisync replication работает

# Проверка отсутствия потери транзакций при неожиданном выходе из строя master
# 1. Запоминаем сколько сейчас пользователей в БД. (в моем случае 25781)
# 2. Запускаем генератор пользователей в 10 потоках и потокобезопасно
#    считаем, сколько транзакций выполнилось успешно.
# 3. Убиваем master: docker kill --signal=9 master
# 4. Смотрим, сколько транзакций выполнилось успешно (генератор пишет в лог). (в моем случае 5907)
# 5. Т.о. в slave1 должно быть 25781+5907=31688 пользователей. Смотрим 
# mysql> select count(*) from socialnetwork.user;
# +----------+
# | count(*) |
# +----------+
# |    31688 |
# +----------+
# 1 row in set (0.01 sec)

# Выбор нового мастера.

# В общем случае, надо идти на все слейвы и смотреть у кого последний gtid,
# select * from mysql.gtid_executed order by interval_end desc limit 10;
# У кого последний, того и промоутим до мастера.

# Слепо промоутить до мастера тот слейв, который был синхронным нельзя т.к. 
# из bin-log мастера транзакция могла доехать до другого слейва, а до синхронного - нет.

# В моем случае все транзакции доехали до всех слейвов и до синхронного и до асинхронного.
# Что объяснимо, т.к. пока мастер ждет подтверждения от синхронного слейва, асинхронные с большей
# вероятностью могут успеть получить изменения.

# Сделаем мастером slave1

docker exec -it slave1 mysql -uroot -padmin\
  -e "STOP SLAVE;" \
  -e "set global read_only = 0;" \
  -e "INSTALL PLUGIN rpl_semi_sync_master SONAME 'semisync_master.so';" \
  -e "SET GLOBAL rpl_semi_sync_master_enabled = 1;" \
  -e "SET GLOBAL rpl_semi_sync_master_timeout = 10000;"
  
# Переключим репликацию slave2 на slave1
docker exec -it slave2 mysql -uroot -padmin\
  -e "set global read_only = 1;" \
  -e "STOP SLAVE;" \
  -e "CHANGE MASTER TO" \
  -e "MASTER_HOST = 'slave1'," \
  -e "MASTER_PORT = 3306," \
  -e "MASTER_USER = 'repl'," \
  -e "MASTER_PASSWORD = 'repl'," \
  -e "MASTER_AUTO_POSITION = 1;" \
  -e "START SLAVE;"
  
# Включим асинхронную репликацию на slave2
docker exec -it slave2 mysql -uroot -padmin \
  -e "INSTALL PLUGIN rpl_semi_sync_slave SONAME 'semisync_slave.so';" \
  -e "SET GLOBAL rpl_semi_sync_slave_enabled = 1;" \
  -e "STOP SLAVE IO_THREAD; # need to stop async replication" \
  -e "START SLAVE IO_THREAD;" 





