CREATE TABLE IF NOT EXISTS user (
  id BIGINT NOT NULL AUTO_INCREMENT,
  email varchar(100) not null UNIQUE,
  email_verified bit not null default 0,
  password tinytext not null,
  given_name tinytext not null,
  family_name tinytext default null,
  age tinyint unsigned not NULL,
  city tinytext default null,
  interests text default null,
  is_active bit not null default 1,
  PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS refresh_token (
  id BIGINT NOT NULL AUTO_INCREMENT,
  user_id BIGINT NOT NULL,
  token tinytext not null,
  expiration_time bigint not null,
  PRIMARY KEY (id),
  constraint fk_refresh_token_user_user_id foreign key (user_id)
    references user(id)
    on delete cascade             
);