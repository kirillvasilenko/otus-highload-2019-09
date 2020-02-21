ALTER TABLE user ADD FULLTEXT KEY ix_user_family_name_given_name_ft (family_name,given_name);

ALTER TABLE user ADD index ix_user_city (city(63)); -- 63 because 255/4 (bytes)
ALTER TABLE user ADD index ix_user_age (age);
ALTER TABLE user ADD index ix_user_city_age (city(63),age);

