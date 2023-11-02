using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_
{
    public class OpenHelp : OpenTab
    {
        public override void Execute()
        {
            openForm = new Help(this);
            Menu.OpenForm(openForm);
        }
    }
}
