-- Start new grid
-- Add symbol
INSERT OR IGNORE  INTO symbol (symbol_name) VALUES ("GBPUSD");
-- Добавим информацию по новой сетке
-- Нужно знать:
-- номер сетки - 3
-- символ - GBPUSD
-- тип ордера и сетки соответственно buy/sell - buy

INSERT INTO grid (grid_number, grid_type_id, symbol_id)
VALUES
(
3,
(SELECT id FROM buy_sell WHERE type = "buy"),
(SELECT id FROM symbol WHERE symbol_name = "GBPUSD")
);

INSERT INTO deal (
                     order_number,
                     open_date,
                     lot,
                     grid_id,
                     symbol_id,
                     buy_sell_id
                 )
                 VALUES (
                     3,
                     '10.02.21',
                     0.01,
                    (SELECT id FROM grid WHERE grid_number = 3),
                    (SELECT id FROM symbol WHERE symbol_name = "GBPUSD"),
                    (SELECT id FROM buy_sell WHERE type = "buy")
                 );

-- Добавить информацию о закрытии позиции
-- Нужно знать номер ордера который закрывается - 3
UPDATE deal
SET close_date = "15.10.21",
    profit = 6,
    balance = 75
WHERE order_number = 3;


-- Открыт новый ордер в уже строящейся сетке
-- Нужно знать:
-- символ(хотя для тестера он один)
-- тип ордера


