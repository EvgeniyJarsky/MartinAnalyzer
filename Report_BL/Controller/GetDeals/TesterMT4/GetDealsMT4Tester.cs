using Report_BL.ReportModel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Report_BL.Controller.GetDeals.TesterMT4
{
    public static class GetDealsMT4Tester
    {
        /// <summary>
        /// выбираем сделки из отчета и заносим их в писок DealsCollection
        /// </summary>
        /// <param name="report">Объект класса Report</param>
        public static void Get(NewReport report)
        {
            int digits = 0; //количество знаков после запятой
            List<int> buy = new List<int>();
            List<int> sell = new List<int>();
            string[] keywordsMT4 = { ">buy<", ">sell<", "t/p", "s/l", "close at stop", "close" };

            // SQlite connection

            //const string databaseName = @"F:\!Coding\C#\MartinAnalyzer\database\database.db";
            //SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            
            // Создаем виртуальную БД
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source = :memory:;"));

            connection.Open();
            // Создаем таблицу
            string createTable = "CREATE TABLE deal (id INTEGER  PRIMARY KEY AUTOINCREMENT, order_number INTEGER, open_date    DATETIME, close_date   DATETIME, lot          REAL, profit       REAL, balance      REAL, magic INTEGER  DEFAULT (0), comment TEXT, grid_id INTEGER  REFERENCES grid (id) ON DELETE NO ACTION ON UPDATE NO ACTION, symbol_id INTEGER  REFERENCES symbol (id) ON DELETE NO ACTION ON UPDATE NO ACTION, buy_sell_id  INTEGER  REFERENCES buy_sell (id) ON DELETE NO ACTION  ON UPDATE NO ACTION, file_id      INTEGER  REFERENCES file (id));";
            SQLiteCommand command1 = new SQLiteCommand(createTable, connection);
            var g1 = command1.ExecuteNonQuery();

            foreach (string line in File.ReadLines(report.FilePath))//перебираем все строки в файле отчета
            {
                foreach (string key in keywordsMT4)
                {
                    if (line.Contains(key))
                    {
                        string[] parseResult =
                            Report_BL.Controller.GetDeals.TesterMT4.ParseMT4Tester.ParseDealsMT4Tester(line, report.Symbol);
                        // Number|Symbol|Date|Buy_Sell|Direct|Lot|Price|Profit|Balance
                        
                        // определяем максимальное количество знаков после запятой
                        int currentDigit = Report_BL.Controller.GetDeals.CountDigits.Count(parseResult[6]);
                        if (digits < currentDigit)
                            digits = currentDigit;
                        
                        // заменим "t/p" на тип сделки
                        if (parseResult[3] == "buy")
                            buy.Add(Int16.Parse(parseResult[0]));
                        if (parseResult[3] == "sell")
                            sell.Add(Int16.Parse(parseResult[0]));
                        if (parseResult[3] == "t/p" || parseResult[3] == "close at stop")
                        {
                            if (buy.Contains(Int16.Parse(parseResult[0])))
                                parseResult[3] = "buy";
                            else
                                parseResult[3] = "sell";
                        }
                        
                        Report_BL.DataCollection.DealsCollection.AddNewItem(parseResult);

                        //SQL_Work.ConnectToDB.ConnectToDB_AndExecuteFunc();
                        //////////////////////////////////
                        

                        string ccom = "INSERT INTO 'deal' (order_number, open_date, lot)" +
                            $" VALUES (1, \'{parseResult[2]}\', {parseResult[5]})"; 
                        SQLiteCommand command = new SQLiteCommand(ccom, connection);
                        var g = command.ExecuteNonQuery();

                        ///////////////////////////////////////
                        report.Digits = digits;
                        break;
                    }
                }
            }
                connection.Close();
        }
    }
}
