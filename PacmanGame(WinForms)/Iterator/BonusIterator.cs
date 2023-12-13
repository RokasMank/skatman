using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PacmanGame_WinForms_.Iterator
{
    public class BonusIterator : IIterator<Bonus>
    {
        private readonly List<Bonus> _bonusList;
        private int _index;

        public BonusIterator(List<Bonus> bonusList)
        {
            _bonusList = bonusList;
            _index = _bonusList.Count - 1;
        }

        public bool HasNext()
        {
            return _index >= 0;
        }

        (int Index, Bonus Item) IIterator<Bonus>.Next()
        {
            if (HasNext())
            {
                var item = (_index, _bonusList[_index--]);
                return item;
               
            }
            return (-1, null);
        }
    }
   

}
