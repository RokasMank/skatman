using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Mediator
{
    public class ChatMediator : IMenuMediator
    {
        private HubConnection hubConnection;
        private List<string> users;
        private string SenderID;

        public ChatMediator(HubConnection hubConnection, string sender)
        {
            this.hubConnection = hubConnection;
            SenderID = sender;
        }

        public void SendMessage(string message, string sender)
        {
            foreach (var user in users)
            {
               if (user != SenderID)
               {
                    try
                    {
                        hubConnection.InvokeAsync("SendMessage", user, message, sender);
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions
                    }
                  
               }
            }
        }

        void IMenuMediator.UpdateUserList(List<string> userList)
        {
            users = userList;
        }
    }

}
