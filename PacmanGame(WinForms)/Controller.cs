using PacmanGame_WinForms_.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacmanGame_WinForms_
{
    class Controller
    {
        private static Controller instance;
        // //private class SingletonHolder
        // {
        //    public static Controller instance = new Controller();
        //}
        // public static Controller GetInstance()
        // {
        //     return SingletonHolder.instance;
        // }
        private static object threadLock = new object();

        public Controller()
        {

        }
        public static Controller GetInstance()
        {
            lock (threadLock)
            {
                if (instance == null)
                {
                    instance = new Controller();
                }
            }
            
            return instance;
        }

        private List<IEnergiserObserver> EnergiserObservers = new List<IEnergiserObserver>();

        public int MapHeight = Game.Field.Rows;
        public int MapWidth = Game.Field.Columns;

        public int LevelMax = Game.MaxLevel;

        public void AttachEnergiserObserver(IEnergiserObserver observer)
        {
            EnergiserObservers.Add(observer);
        }

        public void DetachEnergiserObserver(IEnergiserObserver observer)
        {
            EnergiserObservers.Remove(observer);
        }

        public void NotifyEnergiserObservers()
        {
            foreach (var observer in EnergiserObservers)
            {
                observer.Update();
            }
        }

        public Vector2 GetPacmanPos()
        {
            return new Vector2(Game.Pacman.X, Game.Pacman.Y);
        }

        public void MakeEmpty(int y, int x)
        {
            Game.Field[y, x] = new EmptyPoint(x, y);
        }

        public void PacmanEatPoint()
        {
            Field.CoinsCount -= 1;
        }

        public bool CheckRndPos(int x, int y)
        {
            return Game.Field[y, x] is Wall || Game.Field[y, x - 1] is Wall && Game.Field[y, x + 1] is Wall;
        }

        public bool PacmanHit(int x, int y)//
        {
            return x == Game.Pacman.X && y == Game.Pacman.Y && !Game.Pacman.GhostHit;
        }

        public bool GhostHit(int x, int y)
        {
            return x == Game.Pacman.X && y == Game.Pacman.Y;
        }

        public bool PacmanEatBonus(int x, int y)
        {
            return x == Game.Pacman.X && y == Game.Pacman.Y;
        }

        public bool CheckFieldLimit(int y, int x)
        {
            return x > 0 && x < MapWidth && y > 0 && y < MapHeight;
        }

        bool IndexOutOfRange(int y, int x)
        {
            return x < 0 || x >= MapWidth || y < 0 || y >= MapHeight;
        }

        public bool GhostCheckWall(int y, int x)
        {
            if (IndexOutOfRange(y, x))
            {
                return false;
            }
            return Game.Field[y, x] is Wall;
        }

        public Vector2 PacmanFuturePos(int length)
        {
            int x = Game.Pacman.X;
            int y = Game.Pacman.Y;

            switch (Game.Pacman.Direction)
            {
                case Direction.LEFT:
                    x -= length;
                    break;
                case Direction.RIGHT:
                    x += length;
                    break;
                case Direction.UP:
                    y -= length;
                    break;
                case Direction.DOWN:
                    y += length;
                    break;
            }

            return new Vector2(x, y);
        }

        public void MinusLife()
        {
            Game.Lives -= 1;
        }

        public void PacmanHitGhost(int x, int y)
        {
            var list = Game.GhostTeam;

            for (int i = 0; i < list.List.Count; ++i)
            {
                if (list[i].passive)
                {
                    if (x == list[i].X && y == list[i].Y && !list[i].pacmanHit)
                    {
                        list[i].pacmanHit = false;
                        Game.Pacman.GhostHit = true;
                        list[i].Action();
                        Interface.UpdateEnemy(list[i], i);
                        Game.UpdateInfo();
                    }
                }
            }
        }

        public bool PacmanCanMove(int y, int x)
        {
            if (IndexOutOfRange(y, x))
            {
                return false;
            }

            var matrix = Game.Field;

            if (matrix[y, x] is Wall && matrix[y, x].Portal)
            {
                Game.Pacman.PortalOpened = true;
                return true;
            }

            if (matrix[y, x] is Energiser)
            {               
                Game.Energisers.Add((Energiser)((Energiser)(matrix[y, x])).Clone());

                // No active energisers before, ghosts stop chasing
                if (Game.Energisers.Count == 1)
                    Controller.GetInstance().NotifyEnergiserObservers();

                return true;
            }

            if (matrix[y, x] is Coin)
            {
                return true;
            }

            if (matrix[y, x] is EmptyPoint)
            {
                return true;
            }

            return false;
        }

        public void PlusCoin()
        {
            Game.Score += Game.Score / 2;
        }

        public void ExtraLife()
        {
            Game.Lives += 1;
        }

        public void DoubleScore()
        {
            Game.Score *= 2;
        }

        public int[,] FillLogicMap()
        {
            int[,] grid = new int[MapHeight, MapWidth];
            const int wall = -10;
            const int blank = -1;

            var matrix = Game.Field;
            for (int i = 0; i < MapHeight; ++i)
            {
                for (int j = 0; j < MapWidth; ++j)
                {
                    if (matrix[i, j] is Wall)
                    {
                        grid[i, j] = wall;
                    }
                    else
                    {
                        grid[i, j] = blank;
                    }
                }

            }
            return grid;
        }

        public void SaveResult(string st)
        {
            var user = LogInForm.User;
            var score = Game.Score;
            var state = st;
            var level = Game.Level;
            var steps = Game.Steps;

            string totalTime;
            if (Game.spentSecond > 9)
                totalTime = $"0{Game.spentMinute}:{Game.spentSecond}";
            else
                totalTime = $"0{Game.spentMinute}:0{Game.spentSecond}";

            

            DataBase db = new DataBase();
            db.OpenConnection();

            SQLiteCommand command = db.CreateCommand();

            string insertCommand = "insert into results(user, score, state, level, steps, [total time]) " +
                "values(@user, @score, @state, @level, @steps, @totalTime)";

            command.CommandText = insertCommand;

            AddStringParams(command, "@user", user);
            AddIntParams(command, "@score", score);
            AddStringParams(command, "@state", state);
            AddIntParams(command, "@level", level);
            AddIntParams(command, "@steps", steps);
            AddStringParams(command, "@totalTime", totalTime);           
            /*
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Results saved successfully");
            }
            else
            {
                MessageBox.Show("Fucking bitch!");
            }
            */

            db.CloseConnection();
        }

        public void AddStringParams(SQLiteCommand command, string param, string value)
        {
            command.Parameters.Add(param, DbType.String).Value = value;
        }

        public void AddIntParams(SQLiteCommand command, string param, int value)
        {
            command.Parameters.Add(param, DbType.Int32).Value = value;
        }
    }
}