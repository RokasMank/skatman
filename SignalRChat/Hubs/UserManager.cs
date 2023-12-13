using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public static class UserManager
    {
        private static readonly Dictionary<string, string> users = new Dictionary<string, string>();
        private static readonly object usersLock = new object();

        public static void AddUser(string connectionId, string clientId)
        {
            lock (usersLock)
            {
                if (!users.ContainsKey(connectionId))
                {
                    users.Add(connectionId, clientId);
                }
            }
        }

        public static List<string> GetUsers()
        {
            lock (usersLock)
            {
                return new List<string>(users.Values);
            }
        }
       

        public static void RemoveUser(string connectionId)
        {
            lock (usersLock)
            {
                users.Remove(connectionId);
            }
        }
    }

}
