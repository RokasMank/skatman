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
    public class PacmanPauseState : IPacManState
    {
        private readonly Pacman _pacman;
        private readonly Game _game;
        private static Panel GameOver { get; set; }
        private static Panel YouWin { get; set; }

        private bool isPaused = false;
        public PacmanPauseState(Pacman pacman, Game game)
        {
            _pacman = pacman;
            _game = game;
        }


        public void Move()
        {
            
            _game.BackColor = Color.Blue;

                isPaused = true;
   
            
        }
        private void DisplayPauseMessage()
        {
            MessageBox.Show("YOU WON!");
            // You can use a MessageBox or any other UI element to display the message
            //MessageBox.Show("Game Paused", "Pause", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
