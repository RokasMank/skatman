using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Interpreter
{
    public class DownExpression : Expression
    {
        public override void Interpret(Pacman pacman)
        {
            if (pacman.CheckPoint(Direction.DOWN))
            {
                pacman.Direction = Direction.DOWN;
            }
            else
            {
                pacman.nextDirection = Direction.DOWN;
            }
            pacman.ChangeImage(pacman.Direction);
        }
    }
}
