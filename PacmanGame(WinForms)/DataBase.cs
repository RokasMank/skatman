using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_
{
    class DataBase
    {
        SQLiteConnection connection = new SQLiteConnection("Data Source=MyDataBase.db; Version=3");

        public void OpenConnection()
        {
            connection.Open();
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public SQLiteConnection GetConnection()
        {
            return connection;
        }

        public SQLiteCommand CreateCommand()
        {
            return connection.CreateCommand();
        }
    }
}