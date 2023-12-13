using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        #region snippet_SendMessage
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user,message);
        }
        #endregion
        public override async Task OnConnectedAsync()
        {
            var clientId = Guid.NewGuid().ToString();
            await Clients.Client(Context.ConnectionId).SendAsync("SetClientId", clientId);
            // Add the connected user to the user list
            UserManager.AddUser(Context.ConnectionId, clientId);

            // Send the updated user list to all clients
            await Clients.All.SendAsync("UserListUpdated", UserManager.GetUsers());

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Remove the disconnected user from the user list
            UserManager.RemoveUser(Context.ConnectionId);

            // Send the updated user list to all clients
            await Clients.All.SendAsync("UserListUpdated", UserManager.GetUsers());

            await base.OnDisconnectedAsync(exception);
        }
    }
}
