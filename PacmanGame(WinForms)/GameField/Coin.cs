using PacmanGame_WinForms_;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace PacmanGame_WinForms_
{
    class Coin : BasePoint
    {
        public Coin(int x, int y)
            : base(x, y)
        {
            Image = Properties.Resources.Coin;
        }

        public override void Action()
        {
            GetScore();
            Image = Properties.Resources.Empty;
            Controller.GetInstance().MakeEmpty(Y, X);
        }

        public override void GetScore()
        {
            Controller.GetInstance().PacmanEatPoint();
            Game.Score += 10;
        }
    }
}