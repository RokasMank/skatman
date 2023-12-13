using PacmanGame_WinForms_.Bridge;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PacmanGame_WinForms_
{
    class Blinky : Ghost
    {
        private Direction control = Direction.LEFT;
        public int interval = Settings.Interval - 30;

        private readonly Vector2 targetPoint = new Vector2(10 - 2, 1);

        public Blinky(ISpeedBehaviour speedBehaviour) : base(speedBehaviour)
        {
            Image = Properties.Resources.Blinky_L;
        }

        public override Vector2 TargetPoint { get => targetPoint; }

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

        public override void ChangeImage()
        {
            switch (Direction)
            {
                case Direction.LEFT:
                    Image = Properties.Resources.Blinky_L;
                    break;
                case Direction.RIGHT:
                    Image = Properties.Resources.Blinky_R;
                    break;
                case Direction.UP:
                    Image = Properties.Resources.Blinky_UP;
                    break;
                case Direction.DOWN:
                    Image = Properties.Resources.Blinky_UP;
                    break;
            }
        }

        public override void Speed()
        {
            _speedBehaviour.Speed(this, 30);
        }
    }
}