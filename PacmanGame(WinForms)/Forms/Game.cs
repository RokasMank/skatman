using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PacmanGame_WinForms_
{
    public partial class Game : Form
    {
        public static Timer BlinkyTimer;
        public static Timer ClydeTimer;
        public static Timer InkyTimer;
        public static Timer PinkyTimer;


        public static int countdownMinute { get; set; }
        public static int countdownSecond { get; set; }

        static List<Timer> TimerList = new List<Timer>();

        public static int spentMinute { get; set; }
        public static int spentSecond { get; set; }


        private static PlusLiveBonus PlusLiveBonus { get; set; }
        private static MinusLiveBonus MinusLiveBonus { get; set; }
        private static DoubleCoinBonus DoubleCoinBonus { get; set; }
        private static PlusCoinBonus PlusCoinBonus { get; set; }
        private static Surprise Surprise { get; set; }

        private static List<Panel> BonusListPanel = new List<Panel>();
        private static List<Bonus> BonusList = new List<Bonus>();

        public static Panel[,] GameMap { get; set; }
        public static Panel Hero { get; set; }
        private static Panel YouWin { get; set; }
        private static Panel GameOver { get; set; }
        private bool gameOver = false;
        public static Panel[] GhostTeamPanel { get; set; }

        public static int ElementSize { get; set; }

        public static Field Field;
        public static Pacman Pacman;
        public static GhostTeam GhostTeam;

        public static int Score { get; set; }
        public static int Lives { get; set; }
        public static int Steps { get; set; }
        public static int Level { get; set; }

        public static List<Energiser> Energisers = new List<Energiser>();
        public static int TimeEnergiserActive { get; set; }
        
        public static int Interval { get; set; }
        public static string PlayerName;

        public const int MaxLevel = 5;
        const int MaxLivesValue = 7;

        const int MinuteConstVal = 1;
        const int SecondConstVal = 30;
        const int TimeEnergActConstVal = 3000;
        const int OneSecond = 1000;
        const int IntervalConstVal = 100;

        static int timeForChasing = IntervalConstVal / 2;
        static int timeForRunning = IntervalConstVal * 2 / 5;

        public Game()
        {
            InitializeGameElem();
            InitializeComponent();
        }

        void InitializeGameElem()
        {
            Field = new Field();
            GhostTeam = new GhostTeam();

            PlusLiveBonus = new PlusLiveBonus();
            MinusLiveBonus = new MinusLiveBonus();
            DoubleCoinBonus = new DoubleCoinBonus();
            PlusCoinBonus = new PlusCoinBonus();
            Surprise = new Surprise();
        }

        private void GameLoad(object sender, EventArgs e)
        {
            Sounds.PlayMusic();
            PlayerName = Program.Name;
            Interval = Settings.Interval;
            SetPacmanParams();

            ElementSize = Math.Min(Size.Height / Field.Rows, Size.Width / Field.Columns);
            SetGameField();
            FillBonusList();

            InfoBlock = Interface.SetInfoLabel();
            Controls.Add(InfoBlock);

            SetTimer();           
        }

        private void GameKeyDown(object sender, KeyEventArgs e)
        {
            label5.Text = $"Last key pressed: {e.KeyData}";

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                Pacman.ChangeDirection(e);
            }

            else if (e.KeyCode == Keys.F5)
            {
                if (Field.Finish() && Level == MaxLevel)
                {
                    YouWin.Dispose();
                }

                else if (gameOver)
                {
                    GameOver.Dispose();
                    BackColor = SystemColors.Window;
                    gameOver = false;
                }

                else
                {
                    ClearForm();
                }

                RestartGame();
            }

            else if (e.KeyCode == Keys.Escape)
            {
                YouFailed();
            }

            else if (e.KeyCode == Keys.Home)
            {
                Interface.ChangeColorBtn();
                Close();
            }

            Interface.UpdateHero();
        }

        private void RestartGame()
        {
            InitializeGameElem();
            SetPacmanParams();
            SetGameField();

            SetTimerSetting();
            EnableEachTimer();
        }

        private void NextLevel()
        {
            InitializeGameElem();
            Level += 1;
            SetGameField();

            SetTimerSetting();
        }

        private void PacmanMoving(object sender, EventArgs e)
        {
            Pacman.Move();
            //PacmanEatBonus();

            Interface.UpdateHero();
            UpdateInfo();

            if (Field.Finish())
            {
                if (Level != MaxLevel)
                {
                    ClearForm();
                    NextLevel();
                }

                else
                {
                    YouWon();
                }
            }

            else if (Lives <= 0)
            {
                YouFailed();
            }
        }

        private void BlinkyMoving(object sender, EventArgs e)
        {
            GhostMoving(0);
        }

        private void ClydeMoving(object sender, EventArgs e)
        {
            GhostMoving(3);
        }

        private void InkyMoving(object sender, EventArgs e)
        {
            GhostMoving(2);
        }

        private void PinkyMoving(object sender, EventArgs e)
        {
            GhostMoving(1);
        }

        void GhostMoving(int index)
        {
            GhostTeam[index].Move();
            GhostTeam[index].CheckEnergizerActive();
            RemoveEnergiser();

            Interface.UpdateEnemy(GhostTeam[index], index);
        }

        private void BonusMoving(object sender, EventArgs e)
        {
            for (int i = 0; i < BonusList.Count; ++i)
            {
                var b = BonusList[i];

                if (b.Active)
                {
                    b.Move();
                    PacmanEatBonus(b);
                    Interface.UpdateBonus(b, b.Panel);
                }
            }
        }

        private void YouFailed()
        {
            RemoveEachTimer();
            ClearForm();

            gameOver = true;

            BackColor = Color.Red;
            GameOver = new Panel()
            {
                Parent = this,
                Size = new Size(Width, Height),
                BackgroundImage = Properties.Resources.GameOver
            };

            if (PlayerName != null)
                MessageBox.Show($"{PlayerName},\r\nYOU FAILED!");
            else
                MessageBox.Show("YOU FAILED!");

            Controller.SaveResult("failed");
        }

        private void ClearForm()
        {
            Hero.Dispose();

            foreach (Panel p in GameMap)
            {
                p.Dispose();
            }

            foreach (Panel p in GhostTeamPanel)
            {
                p.Dispose();
            }

            foreach (Panel p in BonusListPanel)
            {
                p.Dispose();
            }
        }

        private void YouWon()
        {
            RemoveEachTimer();
            ClearForm();

            YouWin = new Panel()
            {
                Parent = this,
                BackgroundImageLayout = ImageLayout.Stretch,
                Size = new Size(1200, 1000),
                BackgroundImage = Properties.Resources.YouWin
            };

            if (PlayerName != null)
                MessageBox.Show($"{PlayerName},\r\nYOU WON!");
            else
                MessageBox.Show("YOU WON!");

            Controller.SaveResult("won");
        }

        void SetPacmanParams()
        {
            Level = 1;
            Lives = MaxLivesValue;
            Score = 0;
            Steps = 0;
        }

        void FillBonusList()
        {
            BonusList.Add(MinusLiveBonus);
            BonusList.Add(PlusCoinBonus);
            BonusList.Add(DoubleCoinBonus);
            BonusList.Add(PlusLiveBonus);
            BonusList.Add(Surprise);
        }

        private void SetTimer()
        {
            BlinkyTimer = new Timer(components);
            ClydeTimer = new Timer(components);
            InkyTimer = new Timer(components);
            PinkyTimer = new Timer(components);
            LevelTimer = new Timer(components);

            TimerList.Add(BlinkyTimer);
            TimerList.Add(ClydeTimer);
            TimerList.Add(InkyTimer);
            TimerList.Add(PinkyTimer);
            TimerList.Add(LevelTimer);
            TimerList.Add(PacmanTimer);
            TimerList.Add(StopWatchTimer);


            BlinkyTimer.Tick += new EventHandler(BlinkyMoving);
            ClydeTimer.Tick += new EventHandler(ClydeMoving);
            InkyTimer.Tick += new EventHandler(InkyMoving);
            PinkyTimer.Tick += new EventHandler(PinkyMoving);
            LevelTimer.Tick += new EventHandler(TimerForEachLevel);

            SetTimerSetting();

            PacmanTimer.Interval = Interval;
            BlinkyTimer.Interval = GhostTeam[0].Interval;
            ClydeTimer.Interval = GhostTeam[1].Interval;
            InkyTimer.Interval = GhostTeam[2].Interval;
            PinkyTimer.Interval = GhostTeam[3].Interval;
            LevelTimer.Interval = OneSecond;

            EnableEachTimer();
        }

        void EnableEachTimer()
        {
            for (int i = 0; i < TimerList.Count; ++i)
            {
                TimerList[i].Enabled = true;
            }
        }

        void SetTimerSetting()
        {
            if (Level == 1)
            {
                spentMinute = 0;
                spentSecond = 0;
            }

            if (Interval == IntervalConstVal)
            {
                countdownMinute = MinuteConstVal; 
                countdownSecond = SecondConstVal; 
                TimeEnergiserActive = TimeEnergActConstVal; 
            }

            else if (Interval < IntervalConstVal)
            {
                countdownMinute = MinuteConstVal; 
                countdownSecond = SecondConstVal - MinuteConstVal * 30; 
                TimeEnergiserActive = TimeEnergActConstVal - TimeEnergActConstVal / 3; 
            }

            else if (Interval > IntervalConstVal)
            {
                countdownMinute = MinuteConstVal * 2; 
                countdownSecond = SecondConstVal - MinuteConstVal * 30; 
                TimeEnergiserActive = TimeEnergActConstVal + TimeEnergActConstVal / 3; 
            }
        }             

        //void PacmanEatBonus()
        //{
        //    foreach (Bonus b in BonusList)
        //    {
        //        if (b.Active)
        //        {
        //            if (b.X == Pacman.X && b.Y == Pacman.Y)
        //            {
        //                b.Action();
        //                Interface.UpdateBonus(b, b.Panel);

        //                BonusList.Remove(b);
        //                BonusListPanel.Remove(b.Panel);
        //                return;
        //            }
        //        }
        //    }
        //}

        void PacmanEatBonus(Bonus b)
        {
            if (b.X == Pacman.X && b.Y == Pacman.Y)
            {
                b.Action();
                Interface.UpdateBonus(b, b.Panel);

                BonusList.Remove(b);
                BonusListPanel.Remove(b.Panel);
            }
        }

        void RemoveEachTimer()
        {
            for (int i = 0; i < TimerList.Count; ++i)
            {
                TimerList[i].Enabled = false;
            }
            BonusTimer.Stop();
        }

        void RemoveEnergiser()
        {
            for (int i = 0; i < Energisers.Count; ++i)
            {
                if (Energisers[i].ReadyToStop(Energisers[i].Time))
                {
                    Energisers.RemoveAt(i);
                }
            }
        }

        private void SetGameField()
        {
            GameMap = new Panel[Field.Rows, Field.Columns];

            for (int i = 0; i < GameMap.GetLength(0); i++)
            {
                for (int j = 0; j < GameMap.GetLength(1); j++)
                {
                    GameMap[i, j] = CreatePanel();

                    Interface.UpdatePanel(Field[i, j]);
                    Controls.Add(GameMap[i, j]);
                }
            }


            GhostTeamPanel = new Panel[4];

            for (int i = 0; i < GhostTeamPanel.Length; ++i)
            {
                GhostTeamPanel[i] = CreatePanel();
                Controls.Add(GhostTeamPanel[i]);
                Interface.UpdateEnemy(GhostTeam[i], i);
            }

            UpdateInfo();

            Hero = CreatePanel();           
            Hero.BringToFront();
            Interface.UpdateHero();
        }

        public Panel CreatePanel()
        {
            Panel panel = new Panel()
            {
                Parent = this,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            return panel;
        }

        public static void UpdateInfo()
        {
            InfoBlock.Text = $"Level: {Level}\r\nScore: {Score}\r\nSteps: {Steps}\r\nLives: {Lives}\r\nSpent time:\r\n0{spentMinute}:{spentSecond}";
        }

        private void TimerForEachLevel(object sender, EventArgs e)
        {
            label6.Text = $"Time left:\r\n0{countdownMinute}:{countdownSecond}\r\n\r\n";

            countdownSecond -= 1;
            if (countdownSecond == -1)
            {
                countdownMinute -= 1;
                countdownSecond = 59;
            }

            if (countdownMinute == 0 && countdownSecond == 0)
            {               
                label6.Text = $"Time left:\r\n0{countdownMinute}:{countdownSecond}\r\n\r\n";
                YouFailed();
            }

            GhostChasingWave();
            CreateBonus();
            RespaunGhost();
        }

        void GhostChasingWave()
        {
            if (countdownSecond == timeForChasing)
            {
                GhostTeam.SetChaseMode(true);
                timeForChasing = timeForChasing * 2 / 3;

                if (timeForChasing == 1)
                {
                    timeForChasing = IntervalConstVal / 2;
                }
            }
            else if (countdownSecond == timeForRunning)
            {
                GhostTeam.SetChaseMode(false);
                timeForRunning = timeForRunning * 5 / 8;

                if (timeForRunning == 1)
                {
                    timeForRunning = IntervalConstVal * 2 / 5;
                }
            }
        }

        void CreateBonus()
        {
            foreach (Bonus b in BonusList)
            {
                if (Level == b.LevelToAppear && countdownSecond == b.TimeToAppear)
                {
                    b.MakeActive();
                    b.Panel.Parent = this;
                    Controls.Add(b.Panel);
                    BonusListPanel.Add(b.Panel);
                    Interface.UpdateBonus(b, b.Panel);
                    BonusTimer.Enabled = true;
                }
            }
        }

        void RespaunGhost()
        {
            if (Level == MaxLevel || Level == MaxLevel - 1)
            {
                int index = GhostTeam.Respaun();

                if (index == -1)
                {
                    return;
                }

                Controls.Add(GhostTeamPanel[index]);
                Interface.UpdateEnemy(GhostTeam[index], index);
            }
        }

        private void StopWatch(object sender, EventArgs e)
        {
            spentSecond += 1;
            if (spentSecond == 60)
            {
                spentMinute += 1;
                spentSecond = 0;
            }

            UpdateInfo();
        }        
    }
}