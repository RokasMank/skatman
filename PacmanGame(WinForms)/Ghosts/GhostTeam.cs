using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PacmanGame_WinForms_
{
    public class GhostTeam
    {
        internal List<Ghost> List = new List<Ghost>();          

        public GhostTeam()
        {
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

                        Game.GhostTeamPanel[i] = new Panel()
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
            Ghost Blinky = new Blinky();
            Ghost Pinky = new Pinky();
            Ghost Inky = new Inky();
            Ghost Clyde = new Clyde();

            List.Add(Blinky);
            List.Add(Pinky);
            List.Add(Inky);
            List.Add(Clyde);
        }
    }
}