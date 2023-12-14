using PacmanGame_WinForms_.Bonuses;
using PacmanGame_WinForms_.ChainOfResponsibility;
using PacmanGame_WinForms_.Visitor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PacmanGame_WinForms_
{
    public partial class Game : Form, IExportable
    {
        public Timer BlinkyTimer;
        public Timer ClydeTimer;
        public Timer InkyTimer;
        public Timer PinkyTimer;

        AbstractHandler handler = new BlinkyHandler();
        public int countdownMinute { get; set; }
        public int countdownSecond { get; set; }

        protected List<Timer> TimerList = new List<Timer>();

        public int spentMinute { get; set; }
        public int spentSecond { get; set; }

        private BonusFactory minusbonusFactory = FactoryProducer.getFactory(true);
        private BonusFactory pliusbonusFactory = FactoryProducer.getFactory(false);
        private Bonus PlusLiveBonus { get; set; }
        private Bonus MinusLiveBonus { get; set; }
        private Bonus DoubleCoinBonus { get; set; }
        private Bonus PlusCoinBonus { get; set; }
        private Bonus Surprise { get; set; }

        private List<Panel> BonusListPanel = new List<Panel>();
        private List<Bonus> BonusList = new List<Bonus>();

        public Panel[,] GameMap { get; set; }
        public Panel Hero { get; set; }
        private Panel YouWin { get; set; }
        private Panel GameOver { get; set; }
        private bool gameOver = false;
        public Panel[] GhostTeamPanel { get; set; }

        public int ElementSize { get; set; }

        public Field Field;
        public Pacman Pacman;
        public Pacman PacmanTwo = new Pacman(9,13);

        public GhostTeam GhostTeam;

        public int Score { get; set; }
        public int Lives { get; set; }
        public int Steps { get; set; }
        public int Level { get; set; }

        public List<Energiser> Energisers = new List<Energiser>();
        public int TimeEnergiserActive { get; set; }

        public int Interval { get; set; }
        public string PlayerName;

        public int MaxLevel = 5;
        const int MaxLivesValue = 7;

        protected virtual int MinuteConstVal => 1;
        protected virtual int SecondConstVal => 30;


        const int TimeEnergActConstVal = 3000;
        const int OneSecond = 1000;
        const int IntervalConstVal = 100;

        int timeToChange = IntervalConstVal / 2;
        int timeForRunning = IntervalConstVal * 2 / 5;
        protected List<IExportable> ExportableElements = new List<IExportable>();
        bool levelLoaded = false;

        public Game()
        {
            StartGame();
        }

        private void StartGame()
        {
            InitializeGameElem();
            InitializeComponent();
            levelLoaded = true;
        }

        protected virtual void SetParameters()
        {
            Field = new Field(this);
            Interface.game = this;
            BasePoint.game = this;
            Controller.game = this;
            BasePoint.game = this;
            GhostTeam = new GhostTeam(this);
        }

        public virtual void InitializeGameElem()
        {
            SetParameters();
            foreach (var ghost in GhostTeam.List)
            {
                Controller.GetInstance().AttachEnergiserObserver(ghost);
            }

            PlusLiveBonus = pliusbonusFactory.GetBonus("Live");
            DoubleCoinBonus = pliusbonusFactory.GetBonus("Double");
            PlusCoinBonus = pliusbonusFactory.GetBonus("Coin");
            MinusLiveBonus = minusbonusFactory.GetBonus("Minus");
            Surprise = minusbonusFactory.GetBonus("Surprise");
            ExportableElements.Add(Pacman);
            ExportableElements.Add(this);
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
                PacmanTwo.ChangeDirection(e);

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
            levelLoaded = true;
        }

        private void PacmanMoving(object sender, EventArgs e)
        {
            Pacman.Move();
            PacmanTwo.Move();
            //PacmanEatBonus();

            Interface.UpdateHero();
            UpdateInfo();

            if (Field.Finish())
            {
                levelLoaded = false;
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
            GhostTeam[index].ChooseBehaviour();
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

        protected void YouFailed()
        {
            IExportResultsVisitor visitor = new ExportInJSON();
            foreach (var e in ExportableElements)
            {
                e.Accept(visitor);
            }
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

            Controller.GetInstance().SaveResult("failed");
        }

        protected virtual void ClearForm()
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

            Controller.GetInstance().SaveResult("won");
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

        protected virtual void SetTimer()
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
            if (BonusTimer != null)
                BonusTimer.Stop();
        }

        void RemoveEnergiser()
        {
            bool activeEnergiserExisted = Energisers.Count > 0;
            for (int i = 0; i < Energisers.Count; ++i)
            {
                if (Energisers[i].ReadyToStop(Energisers[i].Time))
                {
                    Energisers.RemoveAt(i);
                }
            }
            // No active energisers left after removal, ghosts start chasing
            if (activeEnergiserExisted && Energisers.Count == 0)
                Controller.GetInstance().NotifyEnergiserObservers();
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

        public void UpdateInfo()
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
            handler.SetNext(new PinkyHandler()).SetNext(new InkyHandler()).SetNext(new ClydeHandler());
            
            for (int i = 0; i < 3; i++)
            {
                handler.Handle(GhostTeam[i]);
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

        void IExportable.Accept(IExportResultsVisitor visitor)
        {
            visitor.ExportGame(this);
        }
    }
}