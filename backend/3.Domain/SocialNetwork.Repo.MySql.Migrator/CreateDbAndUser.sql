CREATE DATABASE IF NOT EXISTS social_network
CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci; 

CREATE USER IF NOT EXISTS 'SocialNetworkApp'@'localhost' IDENTIFIED BY 'SocialNetworkApp';
CREATE USER IF NOT EXISTS 'SocialNetworkApp'@'%' IDENTIFIED BY 'SocialNetworkApp';

GRANT ALL, SELECT, UPDATE, INSERT, DELETE ON social_network.* TO 'SocialNetworkApp'@'localhost';
GRANT ALL, SELECT, UPDATE, INSERT, DELETE ON social_network.* TO 'SocialNetworkApp'@'%';