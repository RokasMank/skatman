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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        DataBase db = new DataBase();
        SQLiteCommand command;
        SQLiteDataReader reader;

        string insertCommand = "insert into users(login, password) values(@login, @pass)";
        string selectCommand = "select * from users where login like @login";

        string loginParam = "@login";
        string passParam = "@pass";

        private void registerBtn_Click(object sender, EventArgs e)
        {
            if (loginField.Text == "")
            {
                MessageBox.Show("Enter the login");
                return;
            }
            if (passwordField.Text == "")
            {
                MessageBox.Show("Enter the password");
                return;
            }

            db.OpenConnection();
            command = db.CreateCommand();

            if (checkUserExists())
            {               
                db.CloseConnection();
                return;
            }

            string loginUser = loginField.Text;
            string passUser = passwordField.Text;

            command.CommandText = insertCommand;
            addParams(loginParam, loginUser);
            addParams(passParam, passUser);           

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Registration completed successfully");
            }
            else
            {
                MessageBox.Show("Registration completed unsuccessfully");
            }

            db.CloseConnection();

            Close();
            Program.Authorization.Show();
        }

        public bool checkUserExists()
        {
            command.CommandText = selectCommand;
            addParams(loginParam, loginField.Text);

            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                MessageBox.Show("Such login already exists");
                reader.Close();
                return true;
            }
            else
            {
                reader.Close();
                return false;
            }
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            Close();
            Program.Authorization.Show();
        }

        void addParams(string param, string value)
        {
            command.Parameters.Add(param, DbType.String).Value = value;
        }

        //void Register()
        //{
        //    DataBase db = new DataBase();
        //    SQLiteCommand command = db.connection.CreateCommand();

        //    command.CommandText = "insert into users(login, password) values(@login, @pass)";
        //    command.Parameters.Add("@login", DbType.String).Value = loginField.Text;
        //    command.Parameters.Add("@pass", DbType.String).Value = passwordField.Text;

        //    db.OpenConnection();

        //    if (command.ExecuteNonQuery() == 1)
        //    {
        //        MessageBox.Show("Registration completed successfully");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Registration completed unsuccessfully");
        //    }

        //    db.CloseConnection();
        //}
    }
}
