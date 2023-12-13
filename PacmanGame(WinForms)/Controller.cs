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
        public static Game game;

        private static Controller instance;
        
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
            return new Vector2(game.Pacman.X, game.Pacman.Y);
        }

        public void MakeEmpty(int y, int x)
        {
            game.Field[y, x] = new EmptyPoint(x, y);
        }

        public void PacmanEatPoint()
        {
            Field.CoinsCount -= 1;
        }

        public bool CheckRndPos(int x, int y)
        {
            return game.Field[y, x] is Wall || game.Field[y, x - 1] is Wall && game.Field[y, x + 1] is Wall;
        }

        public bool PacmanHit(int x, int y)//
        {
            return x == game.Pacman.X && y == game.Pacman.Y && !game.Pacman.GhostHit;
        }

        public bool GhostHit(int x, int y)
        {
            return x == game.Pacman.X && y == game.Pacman.Y;
        }

        public bool PacmanEatBonus(int x, int y)
        {
            return x == game.Pacman.X && y == game.Pacman.Y;
        }

        public bool CheckFieldLimit(int y, int x)
        {
            return x > 0 && x < game.Field.Columns && y > 0 && y < game.Field.Rows;
        }

        bool IndexOutOfRange(int y, int x)
        {
            return x < 0 || x >= game.Field.Columns || y < 0 || y >= game.Field.Rows;
        }

        public bool GhostCheckWall(int y, int x)
        {
            if (IndexOutOfRange(y, x))
            {
                return false;
            }
            return game.Field[y, x] is Wall;
        }

        public Vector2 PacmanFuturePos(int length)
        {
            int x = game.Pacman.X;
            int y = game.Pacman.Y;

            switch (game.Pacman.Direction)
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
            game.Lives -= 1;
        }

        public void PacmanHitGhost(int x, int y)
        {
            if (game is GameEasyMode)
                return;
            var list = game.GhostTeam;

            for (int i = 0; i < list.List.Count; ++i)
            {
                if (list[i].passive)
                {
                    if (x == list[i].X && y == list[i].Y && !list[i].pacmanHit)
                    {
                        list[i].pacmanHit = false;
                        game.Pacman.GhostHit = true;
                        list[i].Action();
                        Interface.UpdateEnemy(list[i], i);
                        game.UpdateInfo();
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

            var matrix = game.Field;

            if (matrix[y, x] is Wall && matrix[y, x].Portal)
            {
                game.Pacman.PortalOpened = true;
                return true;
            }

            if (matrix[y, x] is Energiser)

            {               
                game.Energisers.Add((Energiser)((Energiser)(matrix[y, x])).DeepClone(game.TimeEnergiserActive));
               // game.Energisers.Add(new Energiser(x, y, game.TimeEnergiserActive));
                Energiser prototype = new Energiser(x, y, game.TimeEnergiserActive);
                Energiser deepClone = (Energiser)prototype.DeepClone(game.TimeEnergiserActive);

                // Returns 'false' => the original instance was successfully copied to a new instance, ...
                bool isTheSameInstance = object.ReferenceEquals(prototype, deepClone);
                
                // ...and also the data is not the same => deep copy or deep clone
                isTheSameInstance = object.ReferenceEquals(prototype.Image, deepClone.Image); // false
                

                // No active energisers before, ghosts stop chasing
                if (game.Energisers.Count == 1)
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
            game.Score += game.Score / 2;
        }

        public void ExtraLife()
        {
            game.Lives += 1;
        }

        public void DoubleScore()
        {
            game.Score *= 2;
        }

        public int[,] FillLogicMap()
        {
            int[,] grid = new int[game.Field.Rows, game.Field.Columns];
            const int wall = -10;
            const int blank = -1;

            var matrix = game.Field;
            for (int i = 0; i < game.Field.Rows; ++i)
            {
                for (int j = 0; j < game.Field.Columns; ++j)
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
            var score = game.Score;
            var state = st;
            var level = game.Level;
            var steps = game.Steps;

            string totalTime;
            if (game.spentSecond > 9)
                totalTime = $"0{game.spentMinute}:{game.spentSecond}";
            else
                totalTime = $"0{game.spentMinute}:0{game.spentSecond}";



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