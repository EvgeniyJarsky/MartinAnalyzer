//using Microsoft.Data.Sqlite;
using System.Data.SQLite;

namespace SQL_Work
{
    public class Connect
    {
        public static void Con()
        {
            const string databaseName = @"F:\!Coding\C#\code\database.db";
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            connection.Open();
            string com1 = "INSERT INTO 'DEALS'";
            string com2 = "('SYMBOL')";
            string com3 = $"VALUES (\"SSSSSSS\");";
            string com = com1 + " " + com2 + " " + com3;

            SQLiteCommand command = new SQLiteCommand(com, connection);
            var g = command.ExecuteNonQuery();
            connection.Close();
        }
    }
}