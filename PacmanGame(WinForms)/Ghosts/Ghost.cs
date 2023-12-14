using PacmanGame_WinForms_.Bridge;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace PacmanGame_WinForms_
{
    public abstract class Ghost : BasePoint, IEnergiserObserver
    {
        protected ISpeedBehaviour _speedBehaviour;
        public bool passive = false;
        public bool _isEmpty = false;
        public bool pacmanHit = false;

        public const int ReadyRespaun = 10;
        public int Wait = 0;
        private readonly int respaunX;
        private readonly int respaunY;
        private bool energiserActive = false;

        public Ghost(ISpeedBehaviour speedBehaviour) : base()
        {
            SetRandomPos();

            respaunX = X;
            respaunY = Y;

            ChaseMode = false;
            FunkMode = false;
            _speedBehaviour = speedBehaviour;
        }

        public abstract void Speed();

        public abstract Direction Direction { get; set; }
        public abstract int Interval { get; set; }

        public bool ChaseMode { get; set; }
        public bool FunkMode { get; set; }
        public virtual Vector2 TargetPoint { get; }


        protected Random rnd = new Random();

        private Movement strategy;

        public void SetMovement(Movement strategy)
        {
            this.strategy = strategy;
        }

        public void executeStrategy()
        {
            strategy.Move(this);
        }
        protected void SetRandomPos()
        {
            X = rnd.Next(1, Controller.GetInstance().MapWidth - 2);
            Y = rnd.Next(1, Controller.GetInstance().MapHeight - 2);

            if (Controller.GetInstance().CheckRndPos(X, Y))
            {
                SetRandomPos();
            }
            else
            {
                return;
            }
        }

        public void Respaun()
        {
            Wait = 0;
            X = respaunX;
            Y = respaunY;
            _isEmpty = false;
        }

        public override void GetScore()
        {
            var lives = Game.LevelInfo.GetLives();
            if (!passive)
                Game.LevelInfo.Set(Game.LevelInfo.GetLevel(), --lives);
            else
                Game.Score += 100;
        }

        public override void Action()
        {
            GetScore();

            if (passive)
            {
                Image = Properties.Resources.Empty;
                _isEmpty = true;
            }          
        }

        public void PacmanHit()
        {
            if (Controller.GetInstance().PacmanHit(X, Y))
            {
                Action();
                pacmanHit = true;
            }
        }

        public void ChooseBehaviour()
        { 
            if (energiserActive)
            {
                Image = Properties.Resources.GrayGhost;               
                ChangeInterval();

                passive = true;
                FunkMode = true;
                ChaseMode = false;
            }

            else
            {
                if (!_isEmpty)
                {
                    if (passive)
                    {
                        passive = false;
                        FunkMode = false;

                        if (ChaseMode)
                        {
                            ChaseMode = false;
                        }
                        else
                        {
                            ChaseMode = true;
                        }

                        ChangeImage();
                        ChangeInterval();
                    }
                }
            }           
        }     

        public void ChangeInterval()
        {
            if (!passive)
            {
                Game.ClydeTimer.Interval = Interval;
            }
            else
                Game.ClydeTimer.Interval = Interval + 20;
        }

        public virtual void ChangeImage()
        {
            return;
        }

        public void Move()
        {
            if (!_isEmpty)
            {
                GetMextDir();
                GetNextPoint();

                if (!pacmanHit && !Game.Pacman.GhostHit)
                {
                    PacmanHit();
                    pacmanHit = false;
                }
            }
        }

        void GetMextDir()
        {
            if (FunkMode)
            {
                ChangeDirection();
                return;
            }

            ShortestPathSearch();
        }

        void ShortestPathSearch()
        {
            int W = Controller.GetInstance().MapWidth;
            int H = Controller.GetInstance().MapHeight;
            var grid = Controller.GetInstance().FillLogicMap();

            var finish = setFinishPoint();
            int finishX = (int)finish.X;
            int finishY = (int)finish.Y;

            int[] dx = new int[4] { 1, 0, -1, 0 };
            int[] dy = new int[4] { 0, 1, 0, -1 };

            const int blank = -1;
            bool stop;

            int d = 0;
            grid[Y, X] = 0;
            do
            {
                stop = true;
                for (int y = 0; y < H; ++y)
                {
                    for (int x = 0; x < W; ++x)
                    {
                        if (grid[y, x] == d)
                        {
                            for (int k = 0; k < 4; ++k)
                            {
                                int iy = y + dy[k];
                                int ix = x + dx[k];

                                if (iy >= 0 && iy < H && ix >= 0 && ix < W && grid[iy, ix] == blank)
                                {
                                    stop = false;
                                    grid[iy, ix] = d + 1;
                                }
                            }
                        }
                    }
                }
                ++d;
            }
            while (!stop && grid[finishY, finishX] == blank);

            ChooseNextStep(grid, finish, dy, dx);
        }

        void ChooseNextStep(int[,] grid, Vector2 finish, int[] dy, int[] dx)
        {
            int W = Controller.GetInstance().MapWidth;
            int H = Controller.GetInstance().MapHeight;

            int[] px = new int[W * H];
            int[] py = new int[W * H];

            int x = (int)finish.X;
            int y = (int)finish.Y;

            int len = grid[y, x];
            int d = len;

            while (d > 0)
            {
                px[d] = x;
                py[d] = y;
                --d;

                for (int k = 0; k < 4; ++k)
                {
                    int iy = y + dy[k];
                    int ix = x + dx[k];

                    if (iy >= 0 && iy < H && ix >= 0 && ix < W &&
                        grid[iy, ix] == d)
                    {
                        x += dx[k];
                        y += dy[k];
                        break;
                    }
                }
            }

            SetNextDir(px[1], py[1]);
        }

        void SetNextDir(int x, int y)
        {
            if (X - x == 0)
            {
                switch (Y - y)
                {
                    case 1:
                        Direction = Direction.UP;
                        break;
                    case -1:
                        Direction = Direction.DOWN;
                        break;
                }
                return;
            }
            if (Y - y == 0)
            {
                switch (X - x)
                {
                    case 1:
                        Direction = Direction.LEFT;
                        break;
                    case -1:
                        Direction = Direction.RIGHT;
                        break;
                }
            }
            ChangeImage();
        }

        bool Hit = true;

        Vector2 setFinishPoint()
        {
            if (Controller.GetInstance().GhostHit(X, Y))
            {
                Hit = true;
            }
            if (X == (int)TargetPoint.X && Y == (int)TargetPoint.Y)
            {
                Hit = false;
            }

            if (Hit)
            {
                return TargetPoint;
            }
            else
            {
                return Controller.GetInstance().GetPacmanPos();
            }
        }

        public void ChangeDirection()
        {
            switch (Direction)
            {
                case Direction.LEFT:
                    if (CheckWall(Y, X - 1))
                    {
                        Direction = Direction.RIGHT;
                    }
                    break;
                case Direction.RIGHT:
                    if (CheckWall(Y, X + 1))
                    {
                        Direction = Direction.LEFT;
                    }
                    break;
                case Direction.UP:
                    if (CheckWall(Y - 1, X))
                    {
                        Direction = Direction.DOWN;
                    }
                    break;
                case Direction.DOWN:
                    if (CheckWall(Y + 1, X))
                    {
                        Direction = Direction.UP;
                    }
                    break;
            }
            ChangeImage();
        }

        public void GetNextPoint()
        {
            switch (Direction)
            {
                case Direction.LEFT:
                    X -= 1;
                    break;
                case Direction.RIGHT:
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

        public bool CheckWall(int y, int x)
        {
            return Controller.GetInstance().GhostCheckWall(y, x);
        }

        public void Update()
        {
            energiserActive = !energiserActive;
        }
    }  
}