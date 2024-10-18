-- +goose Up
-- +goose StatementBegin
alter table app_user add column username text unique not null;
alter table app_user add column salt text not null;
insert into school (id, name, region, city, country) 
values (7, 'aoa', 'JR', 'gulakandoz', 'Tajikistan');
insert into app_user (firstname, lastname, username, phone, grade, school_id, total_score, dob, gender, password_hash, salt, role) 
values ('azimjon', 'fayzulloev', 'iamuser', '992985570302', 3, 7, 0, '2002-03-07', 1, 'n5Yt/sirNdlL+sirQvwmCz/iPcwhijT2TOz7g+wXQk4=', 'N07G2qGOgHA8w2+7FOCcGQ==', 1);
-- +goose StatementEnd

-- +goose Down
-- +goose StatementBegin
alter table app_user drop column username;
alter table app_user drop column salt;
delete from app_user where username = 'iamuser';
delete from school where id = 7;
-- +goose StatementEnd
