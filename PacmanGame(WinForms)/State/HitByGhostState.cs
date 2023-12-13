using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.State
{
    public class HitByGhostState : IPacManState
    {
        private readonly Pacman _pacman;

        public HitByGhostState(Pacman pacman)
        {
            _pacman = pacman;
        }

        public void Move()
        {
            // Define behavior when hit by a ghost
            Console.WriteLine("Pac-Man is hit by a ghost!");
            _pacman.GhostHit = true;
            new NormalState(_pacman).Move();
            _pacman.Image = Properties.Resources.breakHeart;
            // Additional state-specific behavior...
        }

        
    }
}
