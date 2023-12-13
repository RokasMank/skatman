using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Mediator
{
    public interface IMenuMediator
    {
        void SendMessage(string message, IColleague sender);
        void RegisterUser(IColleague user);
    }


}
