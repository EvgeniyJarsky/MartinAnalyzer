namespace Report_BL.SQL_Work
{
    /// <summary>
    /// Команды для создания базы данных
    /// </summary>
    public static class CreateMainTables
    {
        public readonly static string buySellTable = 
            "CREATE TABLE buy_sell (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                " type TEXT);";

        public readonly static string deals = "CREATE TABLE deal (" +
            "id INTEGER  PRIMARY KEY AUTOINCREMENT," +
            "order_number INTEGER," +
            "open_date DATETIME," +
            "close_date DATETIME," +
            "lot REAL," +
            "profit REAL," +
            "balance REAL," +
            "magic INTEGER  DEFAULT (0)," +
            "comment TEXT," +
            "grid_id INTEGER  REFERENCES grid (id) ON DELETE NO ACTION ON UPDATE NO ACTION," +
            "symbol_id INTEGER  REFERENCES symbol (id) ON DELETE NO ACTION ON UPDATE NO ACTION," +
            "buy_sell_id INTEGER REFERENCES buy_sell (id) ON DELETE NO ACTION ON UPDATE NO ACTION);";

        public readonly static string grid = "CREATE TABLE grid (" +
            "id INTEGER PRIMARY KEY AUTOINCREMENT," +
            "grid_number INTEGER UNIQUE ON CONFLICT IGNORE, grid_type_id INTEGER," +
            "symbol_id INTEGER," +
            "FOREIGN KEY (grid_type_id)" +
            "REFERENCES buy_sell (id)," +
            "FOREIGN KEY (symbol_id) REFERENCES symbol (id));";

        public readonly static string symbol = "CREATE TABLE symbol (" +
            "id INTEGER PRIMARY KEY AUTOINCREMENT," +
            "symbol_name TEXT UNIQUE ON CONFLICT IGNORE);";

        public readonly static string addBuySell = "INSERT INTO buy_sell (type) VALUES  ('buy'), ('sell');";

        public static string AddSymbol(string symbol)
        { return $"INSERT OR IGNORE  INTO symbol (symbol_name) VALUES (\"{symbol}\");"; }

        public static string AddFilePath(string filePath)
        { return $"INSERT INTO file (file_path) VALUES (\"{filePath}\");"; }

        public static string CreateNewGrid(
            int gridCount,
            string gridType,
            string symbol,
            string filePath)
        {
            return $"INSERT INTO grid (" +
                $"grid_number," +
                $"grid_type_id," +
                $"symbol_id)" +
                $"VALUES(" +
                $"{gridCount}," +
                $"(SELECT id FROM buy_sell WHERE type = \"{gridType}\")," +
                $"(SELECT id FROM symbol WHERE symbol_name = \"{symbol}\"));";
        }

        public static string CreateNewDeal(
            int order_number,
            int gridCount,
            DateTime dateTime,
            double lot,
            string symbol,
            string orderType
            )
        {
            string lotStr = lot.ToString().Replace(',','.');
            return "INSERT INTO deal (" +
                "order_number," +
                "open_date," +
                "lot," +
                "grid_id," +
                "symbol_id," +
                "buy_sell_id)" +
                "VALUES (" +
                $"{order_number}," +
                $"\"{dateTime}\"," +
                $"{lotStr}," +
                $"(SELECT id FROM grid WHERE grid_number = {gridCount})," +
                $"(SELECT id FROM symbol WHERE symbol_name = \"{symbol}\")," +
                $"(SELECT id FROM buy_sell WHERE type = \"{orderType}\"));";
        }

        public static string UpdateDeal(
            DateTime dateTime,
            string profit,
            string balance,
            int orderNumber
            )
        {
            return "UPDATE deal " +
                "SET " +
                    $"close_date = \"{dateTime}\", " +
                    $"profit = \"{profit}\", " +
                    $"balance = \"{balance}\" " +
               $"WHERE order_number = {orderNumber};"; 
        }
    }
}
