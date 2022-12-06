using Report_BL.ReportModel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Collections.Specialized;

namespace Report_BL.SQL_Work
{
    
    public static class CreateDB_MT4Tester
    {
        public static void Create_DB(NewReport report)
        {
            /// Список сделок
            var dealsList = Report_BL.DataCollection.DealsCollection.dealsCollection;

            // Создаем виртуальную БД
            string connectionString = "Data Source = :memory:;";
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            
            connection.Open();

            #region Создаем таблицы
            SQLiteCommand command;
            command = new SQLiteCommand(CreateMainTables.buySellTable, connection);
            var rez = command.ExecuteNonQuery();

            command = new SQLiteCommand(CreateMainTables.addBuySell, connection);
            rez = command.ExecuteNonQuery();

            command = new SQLiteCommand(CreateMainTables.deals, connection);
            rez = command.ExecuteNonQuery();

            command = new SQLiteCommand(CreateMainTables.grid, connection);
            rez = command.ExecuteNonQuery();

            command = new SQLiteCommand(CreateMainTables.symbol, connection);
            rez = command.ExecuteNonQuery();
            #endregion

            #region заполнение БД

            var sell = new List<double>();
            var buy  = new List<double>();

            int gridCountBuy = 0;
            int gridCountSell = 0;

            // Добавим нужный символ
            command = new SQLiteCommand(CreateMainTables.AddSymbol(report.Symbol), connection);
            rez = command.ExecuteNonQuery();
            // Добавим путь к файлу
            // command = new SQLiteCommand(CreateMainTables.AddFilePath(report.FilePath), connection);
            // rez = command.ExecuteNonQuery();

            foreach (var deal in dealsList)
            {
                // Если это сделка buy
                if(deal.Buy_Sell == "buy")
                {
                    // Если это открытие позиции
                    if (deal.Direct == "open")
                    {
                        // Если открытых позиций buy нет - первое калено
                        if(buy.Count == 0)
                        {
                            gridCountBuy = (Math.Max(gridCountBuy, gridCountSell)) + 1;

                            #region Записываем в БД новую сетку
                            command = new SQLiteCommand(
                                CreateMainTables.CreateNewGrid(
                                    gridCountBuy,
                                    deal.Buy_Sell,
                                    report.Symbol,
                                    report.FilePath),
                                connection);
                            rez = command.ExecuteNonQuery();
                            #endregion

                            #region Записываем сделку в БД
                            command = new SQLiteCommand(
                                CreateMainTables.CreateNewDeal(
                                    deal.Number,
                                    gridCountBuy,
                                    deal.Date,// 02.01.2020 3:22:00
                                    deal.Lot,
                                    deal.Symbol,
                                    deal.Buy_Sell),
                                connection);
                            rez = command.ExecuteNonQuery();
                            #endregion

                            buy.Add(deal.Lot);
                        }
                        // Иначе это следующее колено сетки buy
                        else
                        {
                            #region Записываем сделку в БД
                            command = new SQLiteCommand(
                                CreateMainTables.CreateNewDeal(
                                    deal.Number,
                                    gridCountBuy,
                                    deal.Date,// 02.01.2020 3:22:00
                                    deal.Lot,
                                    deal.Symbol,
                                    deal.Buy_Sell),
                                connection);
                            rez = command.ExecuteNonQuery();
                            #endregion

                            buy.Add(deal.Lot);
                        }

                    }
                    // Иначе это закрытие ордера buy
                    else
                    {
                        #region Записываем закрытие сделки в БД
                        command = new SQLiteCommand(
                            CreateMainTables.UpdateDeal(
                                deal.Date,// 02.01.2020 3:22:00
                                deal.Profit,
                                deal.Balance,
                                deal.Number),
                            connection);
                        rez = command.ExecuteNonQuery();
                        #endregion

                        buy.Add(deal.Lot * -1);

                        // Если сетка buy закрыта
                        if ( Math.Round(buy.Sum(), 2) == 0)
                        {
                            buy.Clear();
                        }
                    }

                }
                // Иначе это сделка sell
                else
                {
                    // Если это открытие позиции
                    if(deal.Direct == "open")
                    {
                        if(sell.Count == 0)
                        {
                            gridCountSell = (Math.Max(gridCountBuy, gridCountSell)) + 1;

                            #region Записываем в БД новую сетку
                            command = new SQLiteCommand(
                                CreateMainTables.CreateNewGrid(
                                    gridCountSell,
                                    deal.Buy_Sell,
                                    report.Symbol,
                                    report.FilePath),
                                connection);
                            rez = command.ExecuteNonQuery();
                            #endregion

                            #region Записываем сделку в БД
                            command = new SQLiteCommand(
                                CreateMainTables.CreateNewDeal(
                                    deal.Number,
                                    gridCountSell,
                                    deal.Date,// 02.01.2020 3:22:00
                                    deal.Lot,
                                    deal.Symbol,
                                    deal.Buy_Sell),
                                connection);
                            rez = command.ExecuteNonQuery();
                            #endregion
                        
                            sell.Add(deal.Lot);
                        }
                        // Иначе это следующее колено сетки sell
                        else
                        {
                            #region Записываем сделку в БД
                            command = new SQLiteCommand(
                                CreateMainTables.CreateNewDeal(
                                    deal.Number,
                                    gridCountSell,
                                    deal.Date,// 02.01.2020 3:22:00
                                    deal.Lot,
                                    deal.Symbol,
                                    deal.Buy_Sell),
                                connection);
                            rez = command.ExecuteNonQuery();
                            #endregion

                            sell.Add(deal.Lot);
                        }
                    }
                    // Иначе это закрытие ордера sell
                    else
                    {
                        #region Записываем закрытие сделки в БД
                        command = new SQLiteCommand(
                            CreateMainTables.UpdateDeal(
                                deal.Date,// 02.01.2020 3:22:00
                                deal.Profit,
                                deal.Balance,
                                deal.Number),
                            connection);
                        rez = command.ExecuteNonQuery();
                        #endregion

                        sell.Add(deal.Lot * -1);

                        // Если сетка sell закрыта
                        if ( Math.Round(sell.Sum(), 2) == 0)
                        {
                            // gridCount++;
                            sell.Clear();
                        }
                    }
                }
            }

            #endregion

            #region Сохраняем БД
            if (!Directory.Exists("database"))
            { Directory.CreateDirectory("database"); }
            string pathToBD = $"database/{report.FileName.Split('.')[0] + ".db"}";

            //const string databaseName = @"F:\!Coding\C#\MartinAnalyzer\database\daptabase111.db";
            using (var destination = new SQLiteConnection(string.Format("Data Source={0};", pathToBD)))
            {
                destination.Open();
                connection.BackupDatabase(destination, "main", "main", -1, null, 0);
                destination.Close();
            }
            #endregion

            connection.Close();
        }
    }
}
