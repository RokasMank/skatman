using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacmanGame_WinForms_
{
    public partial class Help : Form
    {
        internal IMenuCommand menuCommand;
        public Help(IMenuCommand command)
        {
            InitializeComponent();
            menuCommand = command;
        }

        private void Help_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                menuCommand.Undo();
            }
        }
    }
}
