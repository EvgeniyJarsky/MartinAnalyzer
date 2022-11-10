using System;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;

namespace SQL_Work
{
    public class SQL_Connection
    {
        const string databaseName = @"F:\!Coding\C#\code\database1.db";
        SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
        
    }
}
