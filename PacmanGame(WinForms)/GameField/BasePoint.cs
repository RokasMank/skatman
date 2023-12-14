using PacmanGame_WinForms_.Visitor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PacmanGame_WinForms_
{
    public abstract class BasePoint : IExportable
    {
        public static Game game;
        public bool Portal = false;

        public BasePoint(int x, int y)
        {

            X = x;
            Y = y;
        }  

        public BasePoint() {}

        public int X { get; set; }
        public int Y { get; set; }

        public Image Image { get; set; }

        public virtual void GetScore()
        {
            return;
        }

        public virtual void Action()
        {
            GetScore();        
        }

        void IExportable.Accept(IExportResultsVisitor visitor)
        {
            visitor.ExportPoint(this);
        }
    }
}