CREATE TABLE IF NOT EXISTS `user` (
  `id` BIGINT NOT NULL AUTO_INCREMENT,
  `email` tinytext not null,
  `password` tinytext not null,
  `name` tinytext not null,
  `lastname` tinytext default null,
  `age` tinyint unsigned not NULL,
  `city` tinytext default null,
  `interests` text default null,
  PRIMARY KEY (`id`)
);