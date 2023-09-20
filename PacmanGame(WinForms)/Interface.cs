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
        static int ElementSize = Game.ElementSize;

        public static void UpdateBonus(Bonus bonus, Panel panel)
        {
            if (bonus._isEmpty)
            {
                panel.Dispose();
            }
            else
            {
                panel.Size = new Size(ElementSize, ElementSize);
                panel.Location = new Point(bonus.X * ElementSize,
                            bonus.Y * ElementSize);
                panel.BringToFront();
                panel.BackgroundImage = bonus.Image;
            }
        }

        public static Label SetInfoLabel()
        {            
            int Level = Game.Level;
            int Score = Game.Score;
            int Steps = Game.Steps;
            int Lives = Game.Lives;

            Label label1 = new Label();
            label1.BackColor = Color.Lime;
            label1.BorderStyle = BorderStyle.Fixed3D;
            label1.Font = new Font("Berlin Sans FB", 20.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label1.ForeColor = SystemColors.MenuText;
            label1.Location = new Point(1120, 0);
            label1.Name = "label1";
            label1.Size = new Size(169, 194);
            label1.TabIndex = 0;
            label1.Text = $"Level: {Level}\r\nScore: {Score}\r\nSteps: {Steps}\r\nLives: {Lives}\r\n\r\n";

            return label1;
        }

        public static void UpdatePanel(BasePoint element)
        {
            Panel[,] GameMap = Game.GameMap;
            Field Field = Game.Field;

            if (element != null)
            {
                if (element is Coin)
                {
                    GameMap[element.Y, element.X].Size = new Size(ElementSize, ElementSize);
                    GameMap[element.Y, element.X].Location = new Point(element.X * ElementSize,
                        element.Y * ElementSize);
                }

                else
                {
                    GameMap[element.Y, element.X].Size = new Size(ElementSize, ElementSize);
                    GameMap[element.Y, element.X].Location = new Point(element.X * ElementSize,
                        element.Y * ElementSize);
                }

                GameMap[element.Y, element.X].BackgroundImage = Field[element.Y, element.X].Image;
            }
        }

        public static void UpdateEnemy(Ghost element, int i)
        {
            GhostTeam GhostTeam = Game.GhostTeam;
            Panel[] GhostTeamPanel = Game.GhostTeamPanel;

            if (GhostTeam[i]._isEmpty)
            {
                GhostTeamPanel[i].Dispose();
            }
            else
            {
                GhostTeamPanel[i].Size = new Size(ElementSize, ElementSize);
                GhostTeamPanel[i].Location = new Point(element.X * ElementSize,
                            element.Y * ElementSize);
                GhostTeamPanel[i].BringToFront();
                GhostTeamPanel[i].BackgroundImage = element.Image;
            }
        }

        public static void UpdateHero()
        {
            Panel Hero = Game.Hero;
            Pacman Pacman = Game.Pacman;

            Hero.Size = new Size(ElementSize, ElementSize);
            Hero.Location = new Point(Pacman.X * ElementSize,
                Pacman.Y * ElementSize);

            Hero.BackgroundImage = Pacman.Image;
        }

        public static void ChangeColorBtn()
        {
            Program.Menu.startGame.BackColor = Color.Lime;
            Program.Menu.startGame.ForeColor = SystemColors.ControlText;
        }
    }
}