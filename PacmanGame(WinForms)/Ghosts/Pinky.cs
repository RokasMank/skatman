using PacmanGame_WinForms_.Bridge;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PacmanGame_WinForms_
{
    class Pinky : Ghost
    {
        private Direction control = Direction.RIGHT;
        private int interval = Settings.Interval;

        private readonly Vector2 targetPoint = new Vector2(1, 1);

        public Pinky(ISpeedBehaviour speedBehaviour) : base(speedBehaviour)
        {
            Image = Properties.Resources.Pinky_R;
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
            switch(Direction)
            {
                case Direction.LEFT:
                    Image = Properties.Resources.Pinky_L;
                    break;
                case Direction.RIGHT:
                    Image = Properties.Resources.Pinky_R;
                    break;
                case Direction.UP:
                    Image = Properties.Resources.Pinky_R;
                    break;
                case Direction.DOWN:
                    Image = Properties.Resources.Pinky_D;
                    break;
            }
        }

        public override void Speed()
        {
            _speedBehaviour.Speed(this, 10);
        }
    }
}