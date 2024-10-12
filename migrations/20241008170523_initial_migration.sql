-- +goose Up
-- +goose StatementBegin
create table if not exists school (
    id BIGSERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    region TEXT NOT NULL,
    city TEXT NOT NULL,
    country TEXT NOT NULL
);

create table if not exists app_user (
    id BIGSERIAL PRIMARY KEY,
    firstname TEXT NOT NULL,
    lastname TEXT NOT NULL,
    phone TEXT NOT NULL UNIQUE,
    grade SMALLINT NOT NULL,
    school_id BIGINT NOT NULL,  
    total_score INT NOT NULL,
    email TEXT UNIQUE,
    dob DATE NOT NULL,
    gender int2 NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    password_hash TEXT NOT NULL,
    role int2 NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,

    FOREIGN KEY (school_id) REFERENCES school(id)
);

create table if not exists week (
    id BIGSERIAL PRIMARY KEY,
    number SMALLINT NOT NULL,
    grade SMALLINT NOT NULL,
    questions_download_url TEXT NOT NULL,
    unit_number SMALLINT NOT NULL
);

create table if not exists question (
    id BIGSERIAL PRIMARY KEY,
    question_tj TEXT NOT NULL,
    question_en TEXT NOT NULL,
    question_ru TEXT NOT NULL,
    options_tj TEXT[] NOT NULL,
    options_en TEXT[] NOT NULL,
    options_ru TEXT[] NOT NULL,
    week_id BIGINT NOT NULL,
    answer_id BIGINT NOT NULL,

    FOREIGN KEY (week_id) REFERENCES week(id)
);

create table if not exists app_user_weekly_activity (
    id BIGSERIAL PRIMARY KEY,
    app_user_id BIGINT NOT NULL,
    start_date DATE NOT NULL,
    is_completed BOOLEAN NOT NULL,
    week_id BIGINT NOT NULL,

    FOREIGN KEY (app_user_id) REFERENCES app_user(id),
    FOREIGN KEY (week_id) REFERENCES week(id)
);

create table if not exists app_user_question_attempt(
    id BIGSERIAL PRIMARY KEY,
    app_user_id BIGINT NOT NULL,
    date DATE NOT NULL,
    question_id BIGINT NOT NULL,
    selected_option_id BIGINT NOT NULL,

    FOREIGN KEY (app_user_id) REFERENCES app_user(id),
    FOREIGN KEY (question_id) REFERENCES question(id)
);

create table if not exists app_user_enrollment(
    id BIGSERIAL PRIMARY KEY,
    app_user_id BIGINT NOT NULL,
    unit_number SMALLINT NOT NULL,
    is_completed BOOLEAN NOT NULL,
    fully_paid BOOLEAN NOT NULL,
    date DATE NOT NULL DEFAULT NOW(),

    FOREIGN KEY (app_user_id) REFERENCES app_user(id)
);

-- +goose StatementEnd

-- +goose Down
-- +goose StatementBegin
DROP TABLE IF EXISTS app_user;
DROP TABLE IF EXISTS question;
DROP TABLE IF EXISTS week;
DROP TABLE IF EXISTS app_user_weekly_activity;
DROP TABLE IF EXISTS app_user_question_attempt;
DROP TABLE IF EXISTS app_user_enrollment;
DROP TABLE IF EXISTS school;
-- +goose StatementEnd
