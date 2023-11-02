using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Decorator
{
    public abstract class PacInfoDecorator : IPacInfo
    {
        protected readonly IPacInfo _pacInfo;
        public PacInfoDecorator(IPacInfo pacInfo)
        {
            _pacInfo = pacInfo;
        }

        public abstract string GetInfo();
    }
}
