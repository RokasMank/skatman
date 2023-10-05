using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Bonuses
{
    internal class MinusBonusFactory : BonusFactory
    {
        //public MinusBonusFactory() { }
        
        public override Bonus GetBonus(string BonusType)
        {
            if (BonusType.Equals("Minus"))
                return new MinusLiveBonus();
            if (BonusType.Equals("Surprise"))
                return new Surprise();
            else
                return null;
            

        }
    }
}
