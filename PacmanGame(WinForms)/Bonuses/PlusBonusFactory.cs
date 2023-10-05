using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Bonuses
{
    internal class PlusBonusFactory : BonusFactory
    {
        public PlusBonusFactory() { }
        public override Bonus GetBonus(string BonusType)
        {
            if (BonusType.Equals("Live"))
                return new PlusLiveBonus();
            if (BonusType.Equals("Coin"))
                return new PlusCoinBonus();
            if (BonusType.Equals("Double"))
                return new DoubleCoinBonus();
            else
                return null;
        }
    }
}
