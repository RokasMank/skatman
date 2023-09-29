using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_
{
    class PlusCoinBonus : Bonus
    {
        int timeToAppear;
        int levelToAppear;

        public PlusCoinBonus() : base()
        {
            Image = Properties.Resources.AddCoin;
        }

        public override int LevelToAppear
        {
            get => levelToAppear;
            set => levelToAppear = value;
        }

        public override int TimeToAppear
        {
            get => timeToAppear;
            set => timeToAppear = value;
        }

        public override void GetScore()
        {
            Controller.GetInstance().PlusCoin();
        }
    }
}