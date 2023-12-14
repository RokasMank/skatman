using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        #region snippet_SendMessage
        public async Task SendMessage(string user, string message, string sender)
        {
            string connectionId = UserManager.GetConnectionId(user);

            if (connectionId != null)
            {
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", sender, message);
            }
            else
            {
                // Handle the case where the connection ID is not found for the given client ID
            }
           
        }
        #endregion
        public override async Task OnConnectedAsync()
        {
            var clientId = Guid.NewGuid().ToString();
            await Clients.Client(Context.ConnectionId).SendAsync("SetClientId", clientId);
        
            UserManager.AddUser(Context.ConnectionId, clientId);

            await Clients.All.SendAsync("UserListUpdated", UserManager.GetUsers());
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
    
            UserManager.RemoveUser(Context.ConnectionId);

            await Clients.All.SendAsync("UserListUpdated", UserManager.GetUsers());

            await base.OnDisconnectedAsync(exception);
        }
    }
}
