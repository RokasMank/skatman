using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Bonuses
{
    internal class FactoryProducer
    {
        public static BonusFactory getFactory(bool Minus)
        {
            if (Minus)
            {
                return new MinusBonusFactory();
            }
            else
            {
                return new PlusBonusFactory();
            }
        }
    }
}
