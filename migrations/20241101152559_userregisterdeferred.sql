-- +goose Up
-- +goose StatementBegin
alter table app_user_enrollment drop constraint app_user_enrollment_app_user_id_fkey;
alter table app_user_enrollment add constraint app_user_enrollment_app_user_id_fkey foreign key (app_user_id) references app_user(id) deferred initially deferred;
-- +goose StatementEnd

-- +goose Down
-- +goose StatementBegin
ALTER TABLE app_user_enrollment DROP CONSTRAINT app_user_enrollment_app_user_id_fkey;
ALTER TABLE app_user_enrollment ADD CONSTRAINT app_user_enrollment_app_user_id_fkey FOREIGN KEY (app_user_id) REFERENCES app_user(id);
-- +goose StatementEnd
