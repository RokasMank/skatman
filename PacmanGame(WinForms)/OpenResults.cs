using PacmanGame_WinForms_.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_
{
    public class OpenResults : OpenTab
    {
        public override void Execute()
        {
            openForm = new Results(this);
            Menu.OpenForm(openForm);
        }
    }
}
