using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacmanGame_WinForms_.Forms
{
    public partial class Chat : Form
    {
        HubConnection hubConnection;
        public Chat(HubConnection hb)
        {
            InitializeComponent();
            hubConnection = hb;
        }

        private async void send_Click(object sender, EventArgs e)
        {
            try
            {
                await hubConnection.InvokeAsync("SendMessage", textBox2.Text, textBox1.Text);
            }
            catch (Exception ex)
            {
                // Use Invoke to update the UI element on the main UI thread.
                //listBox1.Invoke((MethodInvoker)delegate {
                //    listBox1.Items.Add(ex.Message);
                //});
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
