using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PacmanGame_WinForms_.State
{
    public class NormalState : IPacManState
    { 
        private readonly Pacman _pacman;
       

        public NormalState(Pacman pacman)
        {
            _pacman = pacman;
        }
    
        public void Move()
        {
            
            _pacman.GhostHit = false;

            if (_pacman.CheckPoint(_pacman.nextDirection))
            {
                _pacman.GetNextPoint(_pacman.nextDirection);
                ++Game.Steps;

                _pacman.Direction = _pacman.nextDirection;
                _pacman.nextDirection = 0;
            }

            if (_pacman.CheckPoint(_pacman.Direction) && _pacman.Direction != _pacman.nextDirection)
            {
                _pacman.GetNextPoint(_pacman.Direction);
                ++Game.Steps;
            }

            Controller.GetInstance().PacmanHitGhost(_pacman.X, _pacman.Y);

        }

       

    }
}
