-- +goose Up
-- +goose StatementBegin
alter table app_user add column username text unique not null;
alter table app_user add column salt text not null;
alter table app_user alter column total_score set default 0;
alter table app_user alter column role set default 4;

with new_school as (
    insert into school (name, region, city, country) 
    values ('aoa', 'JR', 'gulakandoz', 'Tajikistan')
    returning id
)
insert into app_user (firstname, lastname, username, phone, grade, school_id, dob, gender, password_hash, salt, role) 
    values ('azimjon', 'fayzulloev', 'iamuser', '992985570302', 3, (select id from new_school), '2002-03-07', 0, 'n5Yt/sirNdlL+sirQvwmCz/iPcwhijT2TOz7g+wXQk4=', 'N07G2qGOgHA8w2+7FOCcGQ==', 1),
    ('editor', 'editor', 'iameditor', '992110002306', 3, (select id from new_school), '2002-03-07', 0, 'E5+a3/YapB6e/ZwVe62TxXW1FvTIG44WB/JIgcEnBGs=', '04N/ep3XqzVhedyZZsC5wQ==', 3),
    ('administrator', 'administrator', 'iamadmin', '992112223300', 3, (select id from new_school), '2002-03-07', 0, 'BAA6cdnSlVkkZatt3buQC92uFD3MLjchxSp0uq0aVXg=', 'p6ANuato7PC83XBvKTzT3w==', 2);
-- +goose StatementEnd

-- +goose Down
-- +goose StatementBegin
delete from app_user where username = 'iamuser';
delete from app_user where username = 'iameditor';
delete from app_user where username = 'iamadmin';
alter table app_user drop column username;
alter table app_user drop column salt;
alter table app_user alter column total_score drop default;
alter table app_user alter column role drop default;
delete from school cascade where name = 'aoa';
-- +goose StatementEnd
