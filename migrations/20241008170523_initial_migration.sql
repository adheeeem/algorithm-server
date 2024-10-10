-- +goose Up
-- +goose StatementBegin
create table if not exists student (
    id BIGSERIAL PRIMARY KEY,
    firstname VARCHAR(255) NOT NULL,
    lastname VARCHAR(255) NOT NULL,
    phone VARCHAR(255) NOT NULL UNIQUE,
    grade SMALLINT NOT NULL,
    school VARCHAR(255) NOT NULL,
    region VARCHAR(255) NOT NULL,
    country VARCHAR(255) NOT NULL,
    score INT NOT NULL,
    email VARCHAR(255) UNIQUE,
    dob DATE NOT NULL,
    gender VARCHAR(255) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT NOW()
);

create table if not exists question (
    id BIGSERIAL PRIMARY KEY,
    question_tj VARCHAR(255) NOT NULL,
    question_en VARCHAR(255) NOT NULL,
    question_ru VARCHAR(255) NOT NULL,
    options_tj VARCHAR(255)[] NOT NULL,
    options_en VARCHAR(255)[] NOT NULL,
    options_ru VARCHAR(255)[] NOT NULL,
    answer_id BIGINT NOT NULL
);

create table if not exists week (
    id BIGSERIAL PRIMARY KEY,
    number SMALLINT NOT NULL,
    grade SMALLINT NOT NULL,
    quiz_download_url VARCHAR(255) NOT NULL,
    unit_number SMALLINT NOT NULL
);

create table if not exists week_question (
    id BIGSERIAL PRIMARY KEY,
    week_id BIGINT NOT NULL,
    question_id BIGINT NOT NULL,

    FOREIGN KEY (week_id) REFERENCES week(id),
    FOREIGN KEY (question_id) REFERENCES question(id)
);

create table if not exists student_weekly_activity (
    id BIGSERIAL PRIMARY KEY,
    student_id BIGINT NOT NULL,
    start_date DATE NOT NULL,
    is_completed BOOLEAN NOT NULL,
    week_id BIGINT NOT NULL,
    
    FOREIGN KEY (student_id) REFERENCES student(id),
    FOREIGN KEY (week_id) REFERENCES week(id)
);

create table if not exists student_question_attempt(
    id BIGSERIAL PRIMARY KEY,
    student_id BIGINT NOT NULL,
    week_id BIGINT NOT NULL,
    date DATE NOT NULL,
    question_id BIGINT NOT NULL,
    selected_option_id BIGINT NOT NULL,

    FOREIGN KEY (student_id) REFERENCES student(id),
    FOREIGN KEY (week_id) REFERENCES week(id)
);

create table if not exists student_enrollment(
    id BIGSERIAL PRIMARY KEY,
    student_id BIGINT NOT NULL,
    unit_number SMALLINT NOT NULL,
    is_completed BOOLEAN NOT NULL,
    fully_paid BOOLEAN NOT NULL,
    enrollment_date DATE NOT NULL DEFAULT NOW(),

    FOREIGN KEY (student_id) REFERENCES student(id)
);
-- +goose StatementEnd

-- +goose Down
-- +goose StatementBegin
DROP TABLE IF EXISTS student;
DROP TABLE IF EXISTS question;
DROP TABLE IF EXISTS week;
DROP TABLE IF EXISTS week_question;
DROP TABLE IF EXISTS student_weekly_activity;
DROP TABLE IF EXISTS student_question_attempt;
DROP TABLE IF EXISTS student_enrollment;
-- +goose StatementEnd
