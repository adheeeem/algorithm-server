-- +goose Up
-- +goose StatementBegin
alter table question add image_id text;
-- +goose StatementEnd

-- +goose Down
-- +goose StatementBegin
alter table question drop column image_id;
-- +goose StatementEnd
