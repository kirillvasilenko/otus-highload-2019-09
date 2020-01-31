CREATE USER if not exists 'users_service_migrator'@'localhost' IDENTIFIED BY 'users_service_migrator';
CREATE USER if not exists 'users_service_migrator'@'%' IDENTIFIED BY 'users_service_migrator';

GRANT ALL ON users.* TO 'users_service_migrator'@'localhost';
GRANT ALL ON users.* TO 'users_service_migrator'@'%';

CREATE USER if not exists 'users_service_app'@'localhost' IDENTIFIED BY 'users_service_app';
CREATE USER if not exists 'users_service_app'@'%' IDENTIFIED BY 'users_service_app';

GRANT SELECT, UPDATE, INSERT, DELETE ON users.* TO 'users_service_app'@'localhost';
GRANT SELECT, UPDATE, INSERT, DELETE ON users.* TO 'users_service_app'@'%';