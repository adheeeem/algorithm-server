-- +goose Up
-- +goose StatementBegin
alter table week alter column questions_download_url drop not null;
insert into week (number, grade, unit_number) values (1, 3, 1), (2, 3, 1), (3, 3, 1), (4, 3, 1),
                                    (1, 3, 2), (2, 3, 2), (3, 3, 2), (4, 3, 2),
                                    (1, 3, 3), (2, 3, 3), (3, 3, 3), (4, 3, 3),
                                    (1, 3, 4), (2, 3, 4), (3, 3, 4), (4, 3, 4),
                                    (1, 3, 5), (2, 3, 5), (3, 3, 5), (4, 3, 5),
                                    (1, 3, 6), (2, 3, 6), (3, 3, 6), (4, 3, 6),
                                    (1, 3, 7), (2, 3, 7), (3, 3, 7), (4, 3, 7),
                                    (1, 3, 8), (2, 3, 8), (3, 3, 8), (4, 3, 8),
                                    (1, 4, 1), (2, 4, 1), (3, 4, 1), (4, 4, 1),
                                    (1, 4, 2), (2, 4, 2), (3, 4, 2), (4, 4, 2),
                                    (1, 4, 3), (2, 4, 3), (3, 4, 3), (4, 4, 3),
                                    (1, 4, 4), (2, 4, 4), (3, 4, 4), (4, 4, 4),
                                    (1, 4, 5), (2, 4, 5), (3, 4, 5), (4, 4, 5),
                                    (1, 4, 6), (2, 4, 6), (3, 4, 6), (4, 4, 6),
                                    (1, 4, 7), (2, 4, 7), (3, 4, 7), (4, 4, 7),
                                    (1, 4, 8), (2, 4, 8), (3, 4, 8), (4, 4, 8);
alter table app_user_weekly_activity alter column start_date set default now();
-- +goose StatementEnd

-- +goose Down
-- +goose StatementBegin
delete from week where grade = 3 or grade = 4;
alter table week alter column questions_download_url set constraint not null;
alter table app_user_weekly_activity alter column start_date drop default;
-- +goose StatementEnd
