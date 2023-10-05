using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Bonuses
{
    internal abstract class BonusFactory
    {
        public abstract Bonus GetBonus(string BonusType);
    }
}
