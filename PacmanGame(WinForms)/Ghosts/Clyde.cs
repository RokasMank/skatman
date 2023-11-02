using PacmanGame_WinForms_.Bridge;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PacmanGame_WinForms_
{
    class Clyde : Ghost
    {
        private Direction control = Direction.DOWN;
        private int interval = Settings.Interval;

        private readonly Vector2 targetPoint = new Vector2(1, Controller.GetInstance().MapHeight - 2);


        public Clyde(ISpeedBehaviour speedBehaviour) : base(speedBehaviour)
        {
            Image = Properties.Resources.Clyde_D;
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
                case Direction.UP:
                    Image = Properties.Resources.Clyde_UP;
                    break;
                case Direction.DOWN:
                    Image = Properties.Resources.Clyde_D;
                    break;
                case Direction.LEFT:
                    Image = Properties.Resources.Clyde_UP;
                    break;
                case Direction.RIGHT:
                    Image = Properties.Resources.Clyde_UP;
                    break;
            }
        }

        public override void Speed()
        {
            _speedBehaviour.Speed(this, 30);
        }
    }
}