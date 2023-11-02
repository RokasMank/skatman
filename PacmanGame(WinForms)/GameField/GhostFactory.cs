using PacmanGame_WinForms_.Bridge;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_
{
    public class GhostFactory
    {
        public Ghost getGhost(string name)
        {
            if(name == null)
            {
                return null;
            }

            if (name.Equals("BLINKY"))
            {
                return new Blinky(new SpeedUp());

            }
            else if (name.Equals("CLYDE"))
            {
                return new Clyde(new SpeedDown());

            }
            else if (name.Equals("INKY"))
            {
                return new Inky(new SpeedUp());
            }
            else if (name.Equals("PINKY"))
            {
                return new Pinky(new SpeedDown());
            }

            return null;
        }


    }
}
