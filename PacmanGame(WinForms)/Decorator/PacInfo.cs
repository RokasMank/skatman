using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Decorator
{
    public class PacInfo : IPacInfo
    {
        public string _info { get; set; }
        public PacInfo()
        {
        }

        public string GetInfo(string info)
        {
            return base.ToString();
        }
    }
}
