using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacmanGame_WinForms_.Forms
{
    public partial class LogInForm : Form
    {
        SQLiteCommand command;
        DataBase db = new DataBase();

        string selectCommand = "select * from users where login like @login and password like @pass";
        string loginParam = "@login";
        string passParam = "@pass";

        public static string User;

        public LogInForm()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string loginUser = loginField.Text;
            string passUser = passwordField.Text;

            command = db.CreateCommand();
            command.CommandText = selectCommand;

            addParams(loginParam, loginUser);
            addParams(passParam, passUser);            

            db.OpenConnection();

            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                User = loginUser;
                Hide();
                Program.Menu.Show();
            }
            else
            {
                MessageBox.Show("Your account hasn't found.\nCheck your login/password or register");
            }
            reader.Close();
            db.CloseConnection();
        }

        private void registerBtn_Click(object sender, EventArgs e)           
        {
            Hide();
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
        }

        void addParams(string param, string value)
        {
            command.Parameters.Add(param, DbType.String).Value = value;
        }

        //void Find()
        //{
        //    DataBase db = new DataBase();
        //    SQLiteCommand command = db.connection.CreateCommand();

        //    command.CommandText = "select * from users where login like @login and password like @pass";
        //    command.Parameters.Add("@login", DbType.String).Value = loginField.Text;
        //    command.Parameters.Add("@pass", DbType.String).Value = passwordField.Text;

        //    db.OpenConnection();

        //    SQLiteDataReader reader = command.ExecuteReader();
        //    if (reader.HasRows)
        //    {
        //        MessageBox.Show("OK");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Your account hasn't found.\nCheck your login/password or register");
        //    }

        //    db.CloseConnection();
        //}
    }
}
