-- +goose Up
-- +goose StatementBegin
create table if not exists attempt_result (
    id BIGSERIAL PRIMARY KEY,
    attempt_group_id uuid not null,
    correct_answers int default 0
);
-- +goose StatementEnd

-- +goose Down
-- +goose StatementBegin
drop table if exists attempt_result;
-- +goose StatementEnd
