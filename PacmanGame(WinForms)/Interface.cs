using PacmanGame_WinForms_.Decorator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacmanGame_WinForms_
{
    class Interface
    {
        public static Game game;

        public static void UpdateBonus(Bonus bonus, Panel panel)
        {
            if (bonus._isEmpty)
            {
                panel.Dispose();
            }
            else
            {
                panel.Size = new Size(game.ElementSize, game.ElementSize);
                panel.Location = new Point(bonus.X * game.ElementSize,
                            bonus.Y * game.ElementSize);
                panel.BringToFront();
                panel.BackgroundImage = bonus.Image;
            }
        }

        public static Label SetInfoLabel()
        {            
            int Level = game.Level;
            int Score = game.Score;
            int Steps = game.Steps;
            int Lives = game.Lives;

            Label label1 = new Label();
            label1.BackColor = Color.Lime;
            label1.BorderStyle = BorderStyle.Fixed3D;
            label1.Font = new Font("Berlin Sans FB", 20.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label1.ForeColor = SystemColors.MenuText;
            label1.Location = new Point(1120, 0);
            label1.Name = "label1";
            label1.Size = new Size(169, 194);
            label1.TabIndex = 0;

            IPacInfo pacInfo = new PacInfo();
            var pacLevelDecorator = new PacLevelDecorator(pacInfo, $"Level: {Level}");
            var pacLivesDecorator = new PacLivesDecorator(pacLevelDecorator, $"Lives: {Lives}");
            var pacScoreDecorator = new PacScoreDecorator(pacLivesDecorator, $"Score: {Score}");
            var pacStepsDecorator = new PacStepsDecorator(pacScoreDecorator, $"Steps: {Steps}").GetInfo();

            label1.Text = pacStepsDecorator;

            return label1;
        }

        public static void UpdatePanel(BasePoint element)
        {
            Panel[,] GameMap = game.GameMap;
            Field Field = game.Field;

            if (element != null)
            {
                if (element is Coin)
                {
                    GameMap[element.Y, element.X].Size = new Size(game.ElementSize, game.ElementSize);
                    GameMap[element.Y, element.X].Location = new Point(element.X * game.ElementSize,
                        element.Y * game.ElementSize);
                }

                else
                {
                    GameMap[element.Y, element.X].Size = new Size(game.ElementSize, game.ElementSize);
                    GameMap[element.Y, element.X].Location = new Point(element.X * game.ElementSize,
                        element.Y * game.ElementSize);
                }

                GameMap[element.Y, element.X].BackgroundImage = Field[element.Y, element.X].Image;
            }
        }

        public static void UpdateEnemy(Ghost element, int i)
        {
            GhostTeam GhostTeam = game.GhostTeam;
            Panel[] GhostTeamPanel = game.GhostTeamPanel;

            if (GhostTeam[i]._isEmpty)
            {
                GhostTeamPanel[i].Dispose();
            }
            else
            {
                GhostTeamPanel[i].Size = new Size(game.ElementSize, game.ElementSize);
                GhostTeamPanel[i].Location = new Point(element.X * game.ElementSize,
                            element.Y * game.ElementSize);
                GhostTeamPanel[i].BringToFront();
                GhostTeamPanel[i].BackgroundImage = element.Image;
            }
        }

        public static void UpdateHero()
        {
            Panel Hero = game.Hero;
            Pacman Pacman = game.Pacman;

            Hero.Size = new Size(game.ElementSize, game.ElementSize);
            Hero.Location = new Point(Pacman.X * game.ElementSize,
                Pacman.Y * game.ElementSize);

            Hero.BackgroundImage = Pacman.Image;
        }

        public static void ChangeColorBtn()
        {
            Program.Menu.startGame.BackColor = Color.Lime;
            Program.Menu.startGame.ForeColor = SystemColors.ControlText;
        }
    }
}