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
       // private static ChatMediator instance;
        private HubConnection hubConnection;
        private List<string> users;
        private string SenderID;
       // private readonly object usersLock = new object();
        public ChatMediator(HubConnection hubConnection, string sender)
        {
            this.hubConnection = hubConnection;
            SenderID = sender;
        }

       
        public void SendMessage(string message, string sender)
        {
            //hubConnection.On<List<string>>("UserListUpdated", (userList) =>
            //{
            //    // Update your UI with the new user list
            //    UpdateUserList(userList);
            //});
           // List<string> users = UserManager.GetUsers();
            foreach (var user in users)
            {

               
               if (user != SenderID)
                {
                    try
                    {
                        hubConnection.InvokeAsync("SendMessage", sender, message);
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
