using Microsoft.AspNetCore.SignalR.Client;
using PacmanGame_WinForms_.Forms;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacmanGame_WinForms_
{
    public partial class Menu : Form
    {
        HubConnection hubConnection;
        public Menu()
        {
            InitializeComponent();
            hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:53353/chathub").Build();

            hubConnection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await hubConnection.StartAsync();
            };
        }

        private void startGame_MouseUp(object sender, MouseEventArgs e)
        {
            Game Game = new Game();
            Game.Show();
            
        }

        private void help_MouseUp(object sender, MouseEventArgs e)
        {
            Help Help = new Help();
            Help.ShowDialog();
        }

        private void exit_MouseUp(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void settings_MouseUp(object sender, MouseEventArgs e)
        {
            Program.Set = new Settings();
            Program.Set.ShowDialog();
        }

        private void resultBtn_Click(object sender, EventArgs e)
        {
            Results res = new Results();
            res.Show();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                await hubConnection.InvokeAsync("SendMessage", "dumbfuk", textBox1.Text);
            }
            catch (Exception ex)
            {
                //hubConnection.Items.Add(ex.Message);
            }
        }

        private async void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private async void Menu_Load(object sender, EventArgs e)
        {
            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var newMessage = $"{user}: {message}";
                listBox1.Items.Add(newMessage);
            });

            try
            {
                await hubConnection.StartAsync();
                listBox1.Items.Add(hubConnection.State);
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}