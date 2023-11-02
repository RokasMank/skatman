using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacmanGame_WinForms_
{
    public class ExitApp : IMenuCommand
    {

        public void Execute() 
        {
            Menu.ExitApp();
        }

        public void Undo() { }
    }
}
