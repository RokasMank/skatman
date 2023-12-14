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
       
        public Chat( IMenuMediator mediator)
        {
            InitializeComponent();
            this.mediator = mediator;
        }

        private async void send_Click(object sender, EventArgs e)
        {
            try
            {
                SendMessage(textBox1.Text);
            }
            catch (Exception ex)
            {
               
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
