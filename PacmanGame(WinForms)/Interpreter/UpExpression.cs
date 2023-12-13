using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Interpreter
{
    public class UpExpression : Expression
    {
        public override void Interpret(Pacman pacman)
        {
            if (pacman.CheckPoint(Direction.UP))
            {
                pacman.Direction = Direction.UP;
            }
            else
            {
                pacman.nextDirection = Direction.UP;
            }
            pacman.ChangeImage(pacman.Direction);
        }
    }
}
