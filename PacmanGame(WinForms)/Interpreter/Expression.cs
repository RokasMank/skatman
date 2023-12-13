using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Interpreter
{
    public abstract class Expression
    {
        public abstract void Interpret(Pacman pacman);
    }
}