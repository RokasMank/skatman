using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.State
{
    public class ExtraboostState : IPacManState
    {
        private readonly Pacman _pacman;

        public ExtraboostState(Pacman pacman)
        {
            _pacman = pacman;
        }

        public void Move()
        {
            new NormalState(_pacman).Move();
            _pacman.Image = Properties.Resources.AddCoin;
          
            
        }
    }
}
