using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacmanGame_WinForms_
{
    public class OpenTab : IMenuCommand
    {
        public Form openForm { get; set; }

        public virtual void Execute() { }

        public void Undo()
        {
            if (openForm != null)
            {
                Menu.CloseForm(openForm);
                openForm = null;
            }
        }
    }
}
