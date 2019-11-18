CREATE TABLE IF NOT EXISTS `user` (
  `id` BIGINT NOT NULL AUTO_INCREMENT,
  `email` varchar(100) not null UNIQUE,
  `email_verified` bit not null default 0,
  `password` tinytext not null,
  `given_name` tinytext not null,
  `family_name` tinytext default null,
  `age` tinyint unsigned not NULL,
  `city` tinytext default null,
  `interests` text default null,
  `is_active` bit not null default 1,
  PRIMARY KEY (`id`)
);