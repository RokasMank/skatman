using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Bridge
{
    public interface ISpeedBehaviour
    {
        void Speed(Ghost ghost, int speed);
    }
}
