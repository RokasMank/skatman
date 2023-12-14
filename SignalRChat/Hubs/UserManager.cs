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
        public static string GetConnectionId(string clientID)
        {
            foreach (var entry in users)
            {
                if (entry.Value == clientID)
                {
                    return entry.Key; // Return the connection ID associated with the clientId
                }
            }

            return null;
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
