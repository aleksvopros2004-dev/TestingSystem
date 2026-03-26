-- Таблица сессий тестирования
CREATE TABLE IF NOT EXISTS test_sessions (
    id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    test_id INTEGER NOT NULL REFERENCES tests(id) ON DELETE CASCADE,
    start_time TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    end_time TIMESTAMP WITH TIME ZONE,
    duration INTERVAL,
    earned_points INTEGER DEFAULT 0,
    total_points INTEGER DEFAULT 0,
    is_completed BOOLEAN DEFAULT FALSE
);

-- Таблица ответов пользователей
CREATE TABLE IF NOT EXISTS user_answers (
    id SERIAL PRIMARY KEY,
    session_id INTEGER NOT NULL REFERENCES test_sessions(id) ON DELETE CASCADE,
    question_id INTEGER NOT NULL REFERENCES questions(id) ON DELETE CASCADE,
    answer_text TEXT,
    selected_options_json TEXT, -- JSON массив выбранных ID вариантов
    is_correct BOOLEAN DEFAULT FALSE,
    points_earned INTEGER DEFAULT 0
);

-- Индексы для производительности
CREATE INDEX IF NOT EXISTS idx_test_sessions_user_id ON test_sessions(user_id);
CREATE INDEX IF NOT EXISTS idx_test_sessions_test_id ON test_sessions(test_id);
CREATE INDEX IF NOT EXISTS idx_user_answers_session_id ON user_answers(session_id);
CREATE INDEX IF NOT EXISTS idx_user_answers_question_id ON user_answers(question_id);