using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Proxy
{
    public interface IField
    {
        int Rows { get; set; }
        int Columns { get; set; }
        bool Finish();
        BasePoint this[int i, int j] { get; set; }
    }
}
