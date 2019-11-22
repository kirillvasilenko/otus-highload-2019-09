CREATE DATABASE IF NOT EXISTS users
CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci; 

CREATE USER IF NOT EXISTS 'UsersApp'@'localhost' IDENTIFIED BY 'UsersApp';
CREATE USER IF NOT EXISTS 'UsersApp'@'%' IDENTIFIED BY 'UsersApp';

GRANT All ON users.* TO 'UsersApp'@'localhost';
GRANT ALL ON users.* TO 'UsersApp'@'%';