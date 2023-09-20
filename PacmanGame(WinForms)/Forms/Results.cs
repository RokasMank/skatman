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
using MySql.Data.MySqlClient;

namespace PacmanGame_WinForms_.Forms
{
    public partial class Results : Form
    {
        public Results()
        {
            InitializeComponent();
        }

        private void Results_Load(object sender, EventArgs e)
        {


            PrintResults();
        }

        DataBase db = new DataBase();
        DataTable table = new DataTable();
        SQLiteCommand command;

        string selectCommand = "select * from results order by score desc";

        void PrintResults()
        {
            command = db.CreateCommand();
            dataGridView1.DataSource = table;

            db.OpenConnection();

            command.CommandText = selectCommand;
            table.Load(command.ExecuteReader());

            db.CloseConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
