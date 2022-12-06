using Report_BL.ReportModel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.SQL_Work
{
    public static class ReadDB
    {
        public static void ReadDBExampl(NewReport report)
        {
            // проверим существует ли файл БД
            string pathToBD = $"database/{report.FileName.Split('.')[0] + ".db"}";
            if (!File.Exists(pathToBD))
                return;
            using (var connection = new SQLiteConnection(string.Format("Data Source={0};", pathToBD)))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;

                // Определим сколько всего сеток было построено
                command.CommandText = "SELECT COUNT(id) FROM grid";
                var countGrid = command.ExecuteScalar();


                command.CommandText = "SELECT * FROM deal WHERE grid_id = 2";
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            var f = reader.GetValue(0);
                            var ff = reader.GetValue(1);
                            var fff= reader.GetValue(2);

                            object openDate = reader["open_date"];
                            object closeDate = reader["close_date"];

                            object id = reader[0];
                            object name = reader[1];
                            object age = reader[2];

                        }
                    }
                }
                connection.Close();
            }


        }
    }
}
