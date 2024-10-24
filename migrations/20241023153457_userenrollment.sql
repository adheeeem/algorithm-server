-- +goose Up
-- +goose StatementBegin
alter table app_user_enrollment add column enrolled boolean not null default false;
alter table app_user_enrollment rename column fully_paid to paid;
alter table app_user_enrollment alter column paid set default false;
alter table app_user_enrollment alter column is_completed set default false;
alter table app_user_question_attempt add column attempt_id uuid not null;
-- +goose StatementEnd

-- +goose Down
-- +goose StatementBegin
alter table app_user_enrollment drop column enrolled;
alter table app_user_enrollment rename column paid to fully_paid;
alter table app_user_enrollment alter column fully_paid drop default;
alter table app_user_enrollment alter column is_completed drop default;
alter table app_user_question_attempt drop column attempt_id;
-- +goose StatementEnd
