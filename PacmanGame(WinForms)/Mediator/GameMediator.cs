using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Mediator
{
    public class GameMediator : IMenuMediator
    {
        private List<IColleague> users = new List<IColleague>();

        public void SendMessage(string message, IColleague sender)
        {
            foreach (var user in users)
            {
                // Don't send the message back to the sender
                if (user != sender)
                {
                    user.ReceiveMessage($"{sender}: {message}");
                }
            }
        }

        public void RegisterUser(IColleague user)
        {
            users.Add(user);
        }


    }

}
