using PacmanGame_WinForms_.Bonuses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PacmanGame_WinForms_
{
    public partial class GameEasyMode : Game
    {


        private static BonusFactory minusbonusFactory = FactoryProducer.getFactory(true);
        private static BonusFactory pliusbonusFactory = FactoryProducer.getFactory(false);
        private static Bonus PlusLiveBonus { get; set; }
        private static Bonus MinusLiveBonus { get; set; }
        private static Bonus DoubleCoinBonus { get; set; }
        private static Bonus PlusCoinBonus { get; set; }
        private static Bonus Surprise { get; set; }

        private static List<Panel> BonusListPanel = new List<Panel>();
        private static List<Bonus> BonusList = new List<Bonus>();
        private static Panel YouWin { get; set; }
        private static Panel GameOver { get; set; }
        private bool gameOver = false;



        const int MaxLivesValue = 7;

        protected override int MinuteConstVal => 1;
        protected override int SecondConstVal => 5;
        const int TimeEnergActConstVal = 3000;
        const int OneSecond = 1000;
        const int IntervalConstVal = 100;

        static int timeToChange = IntervalConstVal / 2;
        static int timeForRunning = IntervalConstVal * 2 / 5;

        protected override void SetParameters()
        {
            Field = new Field(this);
            Interface.game = this;
            BasePoint.game = this;
            Controller.game = this;
            BasePoint.game = this;
        }

        public override void InitializeGameElem()
        {
            SetParameters();
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

        //private void YouFailed()
        //{
        //    RemoveEachTimer();
        //    ClearForm();

        //    gameOver = true;

        //    BackColor = Color.Red;
        //    GameOver = new Panel()
        //    {
        //        Parent = this,
        //        Size = new Size(Width, Height),
        //        BackgroundImage = Properties.Resources.GameOver
        //    };

        //    if (PlayerName != null)
        //        MessageBox.Show($"{PlayerName},\r\nYOU FAILED!");
        //    else
        //        MessageBox.Show("YOU FAILED!");

        //    Controller.GetInstance().SaveResult("failed");
        //}

        protected override void ClearForm()
        {
            Hero.Dispose();

            foreach (Panel p in GameMap)
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


        void EnableEachTimer()
        {
            for (int i = 0; i < TimerList.Count; ++i)
            {
                TimerList[i].Enabled = true;
            }
        }


        protected override void SetTimer()
        {
            LevelTimer = new Timer(components);

            TimerList.Add(LevelTimer);
            TimerList.Add(PacmanTimer);
            TimerList.Add(StopWatchTimer);


            LevelTimer.Tick += new EventHandler(TimerForEachLevel);

            SetTimerSetting();

            PacmanTimer.Interval = Interval;
            LevelTimer.Interval = OneSecond;

            EnableEachTimer();
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

            UpdateInfo();

            Hero = CreatePanel();
            Hero.BringToFront();
            Interface.UpdateHero();
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

            CreateBonus();
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