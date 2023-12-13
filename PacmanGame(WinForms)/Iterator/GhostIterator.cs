using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Iterator
{
    public class GhostIterator : IIterator<Ghost>
    {
        private readonly GhostTeam _ghostTeam;
        private int _index;

        public GhostIterator(GhostTeam ghostTeam)
        {
            _ghostTeam = ghostTeam;
            _index = 0;
        }

        public bool HasNext()
        {
            return _index < _ghostTeam.List.Count;
        }

        public (int Index, Ghost Item) Next()
        {
            if (HasNext())
            {
                var item = (_index, _ghostTeam.List[_index++]);
                return item;
            }
            return (-1, null);
        }
    }
}
