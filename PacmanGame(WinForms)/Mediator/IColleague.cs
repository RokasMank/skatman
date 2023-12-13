using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Mediator
{
    public interface IColleague
    {
        void ReceiveMessage(string message);
        void SendMessage(string message);
    }
}
