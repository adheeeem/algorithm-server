-- +goose Up
-- +goose StatementBegin
alter table app_user_question_attempt rename column selected_option_id to selected_option_index;
alter table app_user_question_attempt add column group_id BIGINT;
-- +goose StatementEnd

-- +goose Down
-- +goose StatementBegin
alter table app_user_question_attempt rename column selected_option_index to selected_option_id;
alter table app_user_question_attempt drop column group_id;
-- +goose StatementEnd
