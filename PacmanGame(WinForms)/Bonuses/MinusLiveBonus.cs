using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_
{
    class MinusLiveBonus : Bonus
    {
        int timeToAppear;
        int levelToAppear;

        public MinusLiveBonus() : base()
        {
            Image = Properties.Resources.breakHeart;
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
            Controller.GetInstance().MinusLife();
        }

        public override void Speed()
        {
            throw new NotImplementedException();
        }
    }
}