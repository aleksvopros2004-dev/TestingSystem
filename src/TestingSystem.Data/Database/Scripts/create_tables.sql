-- Таблица пользователей
CREATE TABLE IF NOT EXISTS users (
    id SERIAL PRIMARY KEY,
    login VARCHAR(100) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    full_name VARCHAR(255) NOT NULL,
    role VARCHAR(50) NOT NULL CHECK (role IN ('Admin', 'User')),
    created_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT TRUE
);

-- Таблица тестов
CREATE TABLE IF NOT EXISTS tests (
    id SERIAL PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    author_id INTEGER NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    created_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    time_limit INTERVAL NULL,
    is_active BOOLEAN DEFAULT FALSE,
    questions_order_random BOOLEAN DEFAULT FALSE,
    answer_options_random BOOLEAN DEFAULT FALSE
);

-- Таблица вопросов
CREATE TABLE IF NOT EXISTS questions (
    id SERIAL PRIMARY KEY,
    test_id INTEGER NOT NULL REFERENCES tests(id) ON DELETE CASCADE,
    question_text TEXT NOT NULL,
    question_type VARCHAR(50) NOT NULL CHECK (question_type IN ('SingleChoice', 'MultipleChoice', 'TextAnswer')),
    order_index INTEGER NOT NULL DEFAULT 0,
    image_data BYTEA NULL,                -- Новое поле для хранения изображения
    image_content_type VARCHAR(50) NULL   -- Новое поле для типа контента (image/jpeg, image/png и т.д.)
);

-- Таблица вариантов ответов
CREATE TABLE IF NOT EXISTS answer_options (
    id SERIAL PRIMARY KEY,
    question_id INTEGER NOT NULL REFERENCES questions(id) ON DELETE CASCADE,
    option_text TEXT NOT NULL,
    is_correct BOOLEAN DEFAULT FALSE
);

-- Индексы для улучшения производительности
CREATE INDEX IF NOT EXISTS idx_tests_author_id ON tests(author_id);
CREATE INDEX IF NOT EXISTS idx_tests_is_active ON tests(is_active);
CREATE INDEX IF NOT EXISTS idx_questions_test_id ON questions(test_id);
CREATE INDEX IF NOT EXISTS idx_answer_options_question_id ON answer_options(question_id);
CREATE INDEX IF NOT EXISTS idx_questions_order_index ON questions(order_index);