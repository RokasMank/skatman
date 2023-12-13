using PacmanGame_WinForms_.Ghosts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PacmanGame_WinForms_
{
    public class GhostTeam
    {
        public static Game game;
        internal List<Ghost> List = new List<Ghost>();          

        private GhostFactory GhostFactory = new GhostFactory();
        public GhostTeam(Game _game)
        {
            game = _game;
            CreateList();
        }

        internal Ghost this[int index]
        {
            get
            {
                return List[index];
            }
            set
            {
                List[index] = value;
            }
        }

        public int Respaun()
        {
            for (int i = 0; i < List.Count; ++i)
            {
                if (List[i]._isEmpty)
                {
                    ++List[i].Wait;
                    if (List[i].Wait == Ghost.ReadyRespaun)
                    {
                        List[i].Respaun();

                        game.GhostTeamPanel[i] = new Panel()
                        {
                            BackgroundImageLayout = ImageLayout.Stretch
                        };
                        return i;                        
                    }
                }
            }
            return -1;
        }

        public void SetChaseMode(bool chasing)
        {
            for (int i = 0; i < List.Count; ++i)
            {
                List[i].ChaseMode = chasing;
            }
        }

        private void CreateList()
        {             
            Ghost Blinky = GhostFactory.getGhost("BLINKY");
            Ghost Pinky = GhostFactory.getGhost("PINKY");
            Ghost Inky = GhostFactory.getGhost("INKY");
            Ghost Clyde = GhostFactory.getGhost("CLYDE");

            List.Add(Blinky);
            List.Add(Pinky);
            List.Add(Inky);
            List.Add(Clyde);
        }
    }
}