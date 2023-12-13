using Microsoft.AspNetCore.SignalR.Client;
using PacmanGame_WinForms_.Mediator;
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
    public partial class Chat : Form, IUser
    {
        private IMenuMediator mediator;
        
     
        //HubConnection hubConnection;
        public Chat( IMenuMediator mediator)
        {
            InitializeComponent();
           

            // hubConnection = hb;
            this.mediator = mediator;
            
        }

        private async void send_Click(object sender, EventArgs e)
        {
            try
            {
                SendMessage(textBox1.Text);
               // await hubConnection.InvokeAsync("SendMessage", textBox2.Text, textBox1.Text);
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

        public void ReceiveMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string message)
        {
            mediator.SendMessage(message, textBox2.Text);
        }
    }
}
