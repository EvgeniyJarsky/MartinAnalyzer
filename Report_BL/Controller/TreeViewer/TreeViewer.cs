using Report_BL.ReportModel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.Controller.TreeViewer
{
    /// <summary>
    /// Создаем коллекцию которая биндиться с TreeView WPF
    /// </summary>
    public static class TreeViewer
    {
        /// <summary>
        /// Метод создания коллекции
        /// </summary>
        /// <param name="report">Модель отчета</param>
        public static void CreteTreeView(NewReport report)
        {
            // Путь к файлу БД
            string pathToBD = $"database/{Path.GetFileNameWithoutExtension(report.FileName) + ".db"}";

            using (var connection = new SQLiteConnection(string.Format("Data Source={0};", pathToBD)))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;

                #region Определим сколько всего сеток было построено
                command.CommandText = "SELECT COUNT(id) FROM grid";
                var countGrid = command.ExecuteScalar();
                // переведем значение в int32
                int count = Convert.ToInt32(countGrid);
                #endregion

                for (int i = 1; i <= count; i++)
                {
                    var grid = new Report_BL.ReportModel.TreeViewClass();
                    grid.NumberGrid = i;

                    command.Parameters.AddWithValue("$item", i);
                    command.Parameters.AddWithValue("$reportSymbol", report.Symbol);

                    

                    #region  Получим информацию о сетке
                        command.CommandText = @$"SELECT
                                                grid_number,
                                                symbol_name,
                                                type
                                            FROM grid
                                            JOIN  symbol ON symbol.id  = symbol_id
                                            JOIN buy_sell ON buy_sell.id = grid_type_id
                                            WHERE grid_number = $item;";
                        string symbol = "";
                        string gridType = "";
                        using(SQLiteDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            symbol = reader["symbol_name"].ToString() ?? "";
                            grid.Symbol = reader["symbol_name"].ToString() ?? "";

                            gridType = reader["type"].ToString() ?? "";
                            grid.Sell_Buy = reader["type"].ToString() ?? "";
                        }
                        #endregion

                    #region  Получим все сделки входящие в эту сетку
                    command.CommandText = @$"SELECT open_date,
                                                        close_date,
                                                        open_price,
                                                        close_price,
                                                        lot,
                                                        profit
                                                FROM deal
                                                JOIN grid ON grid.id = grid_id
                                                JOIN symbol ON symbol.id = deal.symbol_id
                                                WHERE grid_number = $item
                                                AND symbol.symbol_name = $reportSymbol;";
                    // подключаемся к БД
                    using(SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // перебираем БД пока есть строки в БД 
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                #region Добавим новый ордер
                                grid.Orders.Add(new Order{
                                    OpenDate = Convert.ToDateTime(reader["open_date"]),
                                    CloseDate = Convert.ToDateTime(reader["close_date"]),
                                    OpenPrice = Convert.ToDouble(reader["open_price"]),
                                    ClosePrice = Convert.ToDouble(reader["close_price"]),
                                    Lot = Convert.ToDouble(reader["lot"]),
                                    Profit = Convert.ToDouble(reader["profit"])
                                    });
                                #endregion

                                #region  Определяем даты начала и конца сетки
                                if(grid.StartDate == DateTime.MinValue)
                                    grid.StartDate = Convert.ToDateTime(reader["open_date"]);
                                grid.EndDate = Convert.ToDateTime(reader["close_date"]);
                                #endregion
                            }
                        }
                    }
                    #endregion

                    #region  Получим колличество колен в сетке
                    command.CommandText = @$"SELECT   COUNT(lot)
                                            FROM deal
                                            JOIN grid ON grid.id = grid_id
                                            JOIN symbol ON symbol.id = deal.symbol_id
                                            WHERE grid_number = $item
                                            AND symbol.symbol_name = $reportSymbol;"; 
                    grid.CountOrders = Convert.ToInt32(command.ExecuteScalar());
                    #endregion
                
                    #region  Получим сумарную лотность
                    command.CommandText = @$"SELECT   SUM(lot)
                                            FROM deal
                                            JOIN grid ON grid.id = grid_id
                                            JOIN symbol ON symbol.id = deal.symbol_id
                                            WHERE grid_number = $item
                                            AND symbol.symbol_name = $reportSymbol;"; 
                    grid.Lot = Math.Round(Convert.ToDouble(command.ExecuteScalar()), 2);
                    #endregion

                    #region  Получим сумарную прибыль
                    command.CommandText = @$"SELECT   SUM(profit)
                                            FROM deal
                                            JOIN grid ON grid.id = grid_id
                                            JOIN symbol ON symbol.id = deal.symbol_id
                                            WHERE grid_number = $item
                                            AND symbol.symbol_name = $reportSymbol;"; 
                    // grid.Profit = Math.Round(Convert.ToDouble(command.ExecuteScalar()), 2);
                    #endregion

                    #region определим размер сетки
                    command.CommandText = @$"SELECT 
                                        MAX(deal.open_price)
                                    FROM deal
                                    JOIN grid ON grid.id = grid_id
                                    JOIN symbol ON symbol.id = deal.symbol_id
                                    JOIN buy_sell ON buy_sell.id = buy_sell_id
                                    WHERE grid_number = $item;";
                    double maxPrice = Convert.ToDouble(command.ExecuteScalar());
                    
                    command.CommandText = @$"SELECT 
                                        MIN(deal.open_price)
                                    FROM deal
                                    JOIN grid ON grid.id = grid_id
                                    JOIN symbol ON symbol.id = deal.symbol_id
                                    JOIN buy_sell ON buy_sell.id = buy_sell_id
                                    WHERE grid_number = $item;";
                    double minPrice = Convert.ToDouble(command.ExecuteScalar());

                    int digit = 100000;
                    if(report.Digits == 3)
                        digit = 1000;

                    grid.GridLenght = Convert.ToInt32((maxPrice - minPrice)*digit);
                    #endregion

                    #region Определяем кол-во пунктов до профита от крайнего колена
                    // Получем среднее значение цены закрытия сетки
                    command.CommandText = @$"SELECT 
                                        close_price
                                    FROM deal
                                    JOIN grid ON grid.id = grid_id
                                    JOIN symbol ON symbol.id = deal.symbol_id
                                    JOIN buy_sell ON buy_sell.id = buy_sell_id
                                    WHERE grid_number = $item;";
                    int count1 = 0;
                    double sum = 0;
                    // подключаемся к БД
                    using(SQLiteDataReader reader = command.ExecuteReader())
                    {
                        
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                // reader["open_price"]
                                sum += Convert.ToDouble(reader["close_price"]);
                                count1++;
                            }
                        }
                    }

                    double averagePrice = sum/count1;
                    
                    if(grid.Sell_Buy == "sell")
                    {
                        grid.PointsToTP =Convert.ToInt32((maxPrice - averagePrice)*digit);
                    }
                    else
                    {
                        grid.PointsToTP = Convert.ToInt32((averagePrice - minPrice)*digit);
                    }

                    #endregion

                    Report_BL.DataCollection.TreeCollection.grid.Add(grid);

                }
                    
                connection.Close();

            }

        }
    }
}
