-- Удалить все таблицы
DROP TABLE IF EXISTS grid;
DROP TABLE IF EXISTS deal;
DROP TABLE IF EXISTS symbol;
DROP TABLE IF EXISTS buy_sell;

CREATE TABLE IF NOT EXISTS grid (
    id           INTEGER PRIMARY KEY AUTOINCREMENT,
    grid_number  INTEGER UNIQUE ON CONFLICT IGNORE,
    grid_type_id INTEGER,
    symbol_id    INTEGER,
    FOREIGN KEY (grid_type_id) REFERENCES buy_sell (id),
    FOREIGN KEY (symbol_id) REFERENCES symbol (id) 
);

CREATE TABLE IF NOT EXISTS deal (
    id           INTEGER  PRIMARY KEY AUTOINCREMENT,
    order_number INTEGER,
    open_date    DATETIME,
    close_date   DATETIME,
    lot          REAL,
    profit       REAL,
    balance      REAL,
    magic        INTEGER  DEFAULT (0),
    comment      TEXT,
    grid_id      INTEGER  REFERENCES grid (id) ON DELETE NO ACTION
                                               ON UPDATE NO ACTION,
    symbol_id    INTEGER  REFERENCES symbol (id) ON DELETE NO ACTION
                                                 ON UPDATE NO ACTION,
    buy_sell_id  INTEGER  REFERENCES buy_sell (id) ON DELETE NO ACTION
                                                   ON UPDATE NO ACTION
);

CREATE TABLE IF NOT EXISTS symbol (
    id          INTEGER PRIMARY KEY AUTOINCREMENT,
    symbol_name TEXT    UNIQUE ON CONFLICT IGNORE
);

CREATE TABLE IF NOT EXISTS buy_sell (
    id   INTEGER PRIMARY KEY AUTOINCREMENT,
    type TEXT
);

-- Добавить в таблицу постоянные значения
INSERT INTO buy_sell (type) VALUES  ('buy'), ('sell');




-- Добавляем символ - если символ существует то игнор
INSERT OR IGNORE  INTO symbol (symbol_name) VALUES ("GBPUSD");




-- Записываем новый символ, если тоакой уже существует то ignore
INSERT OR IGNORE  INTO symbol (symbol_name) VALUES ("EURUSD");
-- Записываем номер сетки
-- INSERT INTO grid (grid_number) VALUES (1);

-- Записываем информацию о сетке
INSERT INTO grid (grid_number, grid_type_id, symbol_id)
VALUES
(
1,
(SELECT id FROM buy_sell WHERE type = "buy"),
(SELECT id FROM symbol WHERE symbol_name = "EURUSD")
);


-- Записываем сделку
INSERT INTO deal (
    order_number,
    open_date,
    close_date,
    lot,
    profit,
    balance,
    magic,
    comment,
    grid_id,
    symbol_id,
    buy_sell_id
    )
VALUES(
    1,
    "10.02.20",
    "11.02.20",
    0.01,
    5,
    12,
    0,
    NULL,
    (SELECT id FROM grid WHERE grid_number = 1),
    (SELECT id FROM symbol WHERE symbol_name = "EURUSD"),
    (SELECT id FROM buy_sell WHERE type = "buy")
    );

-- **************************************************************************************************
-- Заполним данными
INSERT OR IGNORE  INTO symbol (symbol_name) VALUES ("EURUSD");

INSERT INTO grid (grid_number, grid_type_id, symbol_id)
VALUES
(
1,
(SELECT id FROM buy_sell WHERE type = "buy"),
(SELECT id FROM symbol WHERE symbol_name = "EURUSD")
);

INSERT INTO deal (
    order_number,
    open_date,
    close_date,
    lot,
    profit,
    balance,
    magic,
    comment,
    grid_id,
    symbol_id,
    buy_sell_id
    )
VALUES(
    2,
    "10.02.20",
    "11.02.20",
    0.02,
    3,
    15,
    0,
    NULL,
    (SELECT id FROM grid WHERE grid_number = 1),
    (SELECT id FROM symbol WHERE symbol_name = "EURUSD"),
    (SELECT id FROM buy_sell WHERE type = "buy")
    );

-- Second GRID
INSERT OR IGNORE  INTO symbol (symbol_name) VALUES ("AUDJPY");

INSERT INTO grid (grid_number, grid_type_id, symbol_id)
VALUES
(
2,
(SELECT id FROM buy_sell WHERE type = "sell"),
(SELECT id FROM symbol WHERE symbol_name = "AUDJPY")
);

INSERT INTO deal (
    order_number,
    open_date,
    close_date,
    lot,
    profit,
    balance,
    magic,
    comment,
    grid_id,
    symbol_id,
    buy_sell_id
    )
VALUES(
    1,
    "13.02.20",
    "15.02.20",
    0.01,
    3,
    25,
    0,
    NULL,
    (SELECT id FROM grid WHERE grid_number = 2),
    (SELECT id FROM symbol WHERE symbol_name = "AUDJPY"),
    (SELECT id FROM buy_sell WHERE type = "sell")
    );

INSERT OR IGNORE  INTO symbol (symbol_name) VALUES ("AUDJPY");

INSERT INTO grid (grid_number, grid_type_id, symbol_id)
VALUES
(
2,
(SELECT id FROM buy_sell WHERE type = "sell"),
(SELECT id FROM symbol WHERE symbol_name = "AUDJPY")
);

INSERT INTO deal (
    order_number,
    open_date,
    close_date,
    lot,
    profit,
    balance,
    magic,
    comment,
    grid_id,
    symbol_id,
    buy_sell_id
    )
VALUES(
    2,
    "10.02.20",
    "11.02.20",
    0.03,
    15,
    50,
    0,
    NULL,
    (SELECT id FROM grid WHERE grid_number = 2),
    (SELECT id FROM symbol WHERE symbol_name = "AUDJPY"),
    (SELECT id FROM buy_sell WHERE type = "sell")
    );




-- Найдем id последней записанной сделки
SELECT id FROM deal ORDER BY id DESC LIMIT 1;
