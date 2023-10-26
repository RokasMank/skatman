using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Decorator
{
    public class PacLivesDecorator : PacInfoDecorator
    {
        public PacLivesDecorator(IPacInfo pacInfo, string info) : base(pacInfo)
        {
        }

        public override string GetInfo(string info)
        {
            return _pacInfo + info;
        }
    }
}