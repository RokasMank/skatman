using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.PropertyGridInternal;

namespace PacmanGame_WinForms_
{
    public class Wall : BasePoint
    {
        public Wall(int x, int y)
            : base(x, y)
        {
            Image = Properties.Resources.Wall;
        }       
    }
}