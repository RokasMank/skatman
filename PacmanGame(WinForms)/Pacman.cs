using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PacmanGame_WinForms_
{
    public class Pacman : BasePoint
    {
        public bool GhostHit = false;
        public bool PortalOpened = false;
        public Direction Direction = Direction.UP;
        public Direction nextDirection = 0;


        public Pacman(int x, int y) : base(x, y)
        {
            Image = Properties.Resources.Pacman_R;
        }

        public void Move()
        {
            GhostHit = false;

            if (CheckPoint(nextDirection))
            {
                GetNextPoint(nextDirection);
                ++Game.Steps;

                Direction = nextDirection;
                nextDirection = 0;
            }

            if (CheckPoint(Direction) && Direction != nextDirection)
            {
                GetNextPoint(Direction);
                ++Game.Steps;
            }

            Controller.GetInstance().PacmanHitGhost(X, Y);
        }

        public void ChangeImage(Direction direction)
        {
            switch (direction)
            {
                case Direction.RIGHT:
                    Image = Properties.Resources.Pacman_R;
                    break;
                case Direction.LEFT:
                    Image = Properties.Resources.Pacman_L;
                    break;
                case Direction.DOWN:
                    Image = Properties.Resources.Pacman_D;
                    break;
                case Direction.UP:
                    Image = Properties.Resources.Pacman_UP;
                    break;
            }
        }



        void GetNextPoint(Direction direction)
        {
            switch (direction)
            {
                case Direction.LEFT:
                    if (PortalOpened)
                    {
                        X = Controller.GetInstance().MapWidth - 1;
                        PortalOpened = false;
                    }
                    else
                        X -= 1;
                    break;
                case Direction.RIGHT:
                    if (PortalOpened)
                    {
                        X = 0;
                        PortalOpened = false;
                    }
                    else
                        X += 1;
                    break;
                case Direction.UP:
                    Y -= 1;
                    break;
                case Direction.DOWN:
                    Y += 1;
                    break;
            }
        }

        public void ChangeDirection(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    if (CheckPoint(Direction.RIGHT))
                    {
                        Direction = Direction.RIGHT;
                    }
                    else
                        nextDirection = Direction.RIGHT;
                    break;
                case Keys.Left:
                    if (CheckPoint(Direction.LEFT))
                    {
                        Direction = Direction.LEFT;
                    }
                    else
                        nextDirection = Direction.LEFT;
                    break;
                case Keys.Down:
                    if (CheckPoint(Direction.DOWN))
                    {
                        Direction = Direction.DOWN;
                    }
                    else
                        nextDirection = Direction.DOWN;
                    break;
                case Keys.Up:
                    if (CheckPoint(Direction.UP))
                    {
                        Direction = Direction.UP;
                    }
                    else
                        nextDirection = Direction.UP;
                    break;
            }
            ChangeImage(Direction);
        }

        public bool CheckPoint(Direction direction)
        {
            switch (direction)
            {
                case Direction.LEFT:
                    if (CheckPoint(Y, X - 1))
                    {
                        ChangeImage(direction);
                        return true;
                    }
                    break;
                case Direction.RIGHT:
                    if (CheckPoint(Y, X + 1))
                    {
                        ChangeImage(direction);
                        return true;
                    }
                    break;
                case Direction.UP:
                    if (CheckPoint(Y - 1, X))
                    {
                        ChangeImage(direction);
                        return true;
                    }
                    break;
                case Direction.DOWN:
                    if (CheckPoint(Y + 1, X))
                    {
                        ChangeImage(direction);
                        return true;
                    }
                    break;
            }
            return false;
        }

        private bool CheckPoint(int y, int x)
        {
            if (Controller.GetInstance().PacmanCanMove(y, x))
            {
                var matrix = Game.Field;
                matrix[y, x].Action();
                Interface.UpdatePanel(matrix[y, x]);
                return true;
            }

            return false;
        }
    }
}