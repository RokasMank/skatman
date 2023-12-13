using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_
{
    class Surprise : Bonus
    {
        int timeToAppear;
        int levelToAppear;

        public Surprise() : base()
        {
            Image = Properties.Resources.Surprise;
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
            int num = rnd.Next(1, 6);

            switch(num % 2)
            {
                case 1:
                    game.countdownSecond /= 2;
                    break;
                case 0:
                    if (num == 2)
                    {
                        Controller.GetInstance().MinusLife();
                    }
                    else if (num == 4)
                    {
                        Controller.GetInstance().DoubleScore();
                    }
                    break;
            }
        }

        public override void Speed()
        {
            throw new NotImplementedException();
        }
    }
}