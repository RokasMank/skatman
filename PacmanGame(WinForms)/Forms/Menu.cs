using Microsoft.AspNetCore.SignalR.Client;
using PacmanGame_WinForms_.Forms;
using PacmanGame_WinForms_.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PacmanGame_WinForms_
{
    public partial class Menu : Form
    {
        IMenuMediator mediator;
        string clientId;
        HubConnection hubConnection;
        public Menu()
        {
            InitializeComponent();
            hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5335/chathub").Build();
           
            hubConnection.On<string>("SetClientId", id =>
            {
                clientId = id;
                Console.WriteLine($"Client Id set to: {clientId}");
                mediator = new ChatMediator(hubConnection, clientId);
            });


            hubConnection.On<List<string>>("UserListUpdated", (userList) =>
            {
                mediator.UpdateUserList(userList);
            });

            hubConnection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await hubConnection.StartAsync();
            };
        }
         
        public static void OpenForm(Form form, bool setSettings = false)
        {
            form.ShowDialog();
            if (setSettings)
                Program.Set = (Settings)form;
        }

        public static void CloseForm(Form form)
        {
            form.Close();
        }

        public static void ExitApp()
        {
            Application.Exit();
        }

        private void startGame_MouseUp(object sender, MouseEventArgs e)
        {
            
            Game Game = new Game();
            Game.Show();
            
        }

        private void help_MouseUp(object sender, MouseEventArgs e)
        {
            new OpenHelp().Execute();
        }

        private void exit_MouseUp(object sender, MouseEventArgs e)
        {
            new ExitApp().Execute();
        }

        private void settings_MouseUp(object sender, MouseEventArgs e)
        {
            new OpenSettings().Execute();
        }

        private void resultBtn_Click(object sender, EventArgs e)
        {
            new OpenResults().Execute();
        }

        private void chat_Click(object sender, EventArgs e)
        {
            var form = new Chat( mediator);
            form.ShowDialog();
        }
        private async void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        private async void Menu_Load(object sender, EventArgs e)
        {
            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var newMessage = $"{user}: {message}";
                // Use Invoke to update the UI element on the main UI thread.
                listBox1.Invoke((MethodInvoker)delegate {
                    listBox1.Items.Add(newMessage);
                });
            });

            try
            {
                await hubConnection.StartAsync();
                // Use Invoke to update the UI element on the main UI thread.
                listBox1.Invoke((MethodInvoker)delegate {
                    listBox1.Items.Add(hubConnection.State);
                });
            }
            catch (Exception ex)
            {
                // Use Invoke to update the UI element on the main UI thread.
                listBox1.Invoke((MethodInvoker)delegate {
                    listBox1.Items.Add(ex.Message);
                });
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var a = new System.Windows.Forms.TextBox();
            switch (keyData)
            {
                case Keys.Enter:
                    // Start Game
                    PlaceholderMethod();
                    return true;
                case Keys.H:
                    // Help
                    new OpenHelp().Execute();
                    return true;
                case Keys.S:
                    // Settings
                    new OpenSettings().Execute();
                    return true;
                case Keys.R:
                    // Results
                    new OpenResults().Execute();
                    return true;
                case Keys.E:
                    // Exit
                    new ExitApp().Execute();
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        void PlaceholderMethod()
        {
            throw new NotImplementedException();
        }

        public void ReceiveMessage(string user, string message)
        {
            var newMessage = $"{user}: {message}";
  
                listBox1.Invoke((MethodInvoker)delegate {
                    listBox1.Items.Add(newMessage);
                });
            
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //await hubConnection.InvokeAsync("SendMessage", textBox2.Text, textBox1.Text);
            }
            catch (Exception ex)
            {
                // Use Invoke to update the UI element on the main UI thread.
                listBox1.Invoke((MethodInvoker)delegate {
                    listBox1.Items.Add(ex.Message);
                });
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

    }
}