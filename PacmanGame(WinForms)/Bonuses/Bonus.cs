using System;
using System.Windows.Forms;

namespace PacmanGame_WinForms_
{
    abstract class Bonus : Ghost
    {
        public bool Active = false;      

        Direction control = Direction.LEFT;
        int interval = 500;

        public Bonus() : base()
        {
            SetRndTimeToAppear();
            _isEmpty = false;
        }
       
        public override Direction Direction
        {
            get { return control; }
            set { control = value; }
        }

        public override int Interval
        {
            get { return interval; }       
            set { interval = value; }
        }

        public Panel Panel { get; set; }

        public abstract int LevelToAppear { get; set; }
        public abstract int TimeToAppear { get; set; }

        public new virtual void Action()
        {
            GetScore();
            Image = Properties.Resources.Empty;
            _isEmpty = true;
        }

        public new void Move()
        {
            if (!_isEmpty)
            {
                PacmanHit();
                ChangeDirection();
                GetNextPoint();
            }
        }

        new void PacmanHit()
        {
            if (Controller.GetInstance().PacmanEatBonus(X, Y))
            {
                Action();
            }
        }

        public void MakeActive()
        {
            SetRandomPos();

            Active = true;
            Panel = new Panel()
            {
                BackgroundImageLayout = ImageLayout.Stretch
            };
        } 
        
        protected void SetRndTimeToAppear()
        {
            LevelToAppear = rnd.Next(1, Game.MaxLevel);
            TimeToAppear = rnd.Next(10, 60);
        }
    }
}