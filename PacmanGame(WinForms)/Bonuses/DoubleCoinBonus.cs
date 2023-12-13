using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_
{
    class DoubleCoinBonus : Bonus
    {
        int timeToAppear;
        int levelToAppear;      

        public DoubleCoinBonus() : base()
        {
            Image = Properties.Resources.MultiplCoin;
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
            game.Score *= 2;
        }

        public static void DoubleScore()
        {
            game.Score *= 2;
        }

        public override void Speed()
        {
            throw new NotImplementedException();
        }
    }
}
