CREATE USER if not exists 'socialnetwork_migrator'@'localhost' IDENTIFIED BY 'socialnetwork_migrator';
CREATE USER if not exists 'socialnetwork_migrator'@'%' IDENTIFIED BY 'socialnetwork_migrator';

GRANT ALL ON socialnetwork.* TO 'socialnetwork_migrator'@'localhost';
GRANT ALL ON socialnetwork.* TO 'socialnetwork_migrator'@'%';

CREATE USER if not exists 'socialnetwork_app'@'localhost' IDENTIFIED BY 'socialnetwork_app';
CREATE USER if not exists 'socialnetwork_app'@'%' IDENTIFIED BY 'socialnetwork_app';

GRANT SELECT, UPDATE, INSERT, DELETE ON socialnetwork.* TO 'socialnetwork_app'@'localhost';
GRANT SELECT, UPDATE, INSERT, DELETE ON socialnetwork.* TO 'socialnetwork_app'@'%';