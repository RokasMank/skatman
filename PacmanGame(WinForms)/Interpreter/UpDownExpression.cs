using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Interpreter
{
    public class UpDownExpression : Expression
    {
        private readonly Expression upExpression;
        private readonly Expression downExpression;

        public UpDownExpression(Expression upExpression, Expression downExpression)
        {
            this.upExpression = upExpression;
            this.downExpression = downExpression;
        }

        public override void Interpret(Pacman pacman)
        {
            upExpression.Interpret(pacman);
            downExpression.Interpret(pacman);
        }
    }
}