using System;
using System.Collections.Generic;
using System.Text;

namespace PacmanGame_WinForms_
{
    class EmptyPoint : BasePoint
    {
        public EmptyPoint(int x, int y)
            : base(x, y)
        {
            Image = Properties.Resources.Empty;
        }
    }
}