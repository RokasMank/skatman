using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Decorator
{
    public class PacStepsDecorator : PacInfoDecorator
    {
        private string _pacInfo;

        public PacStepsDecorator(IPacInfo pacInfo, string info) : base(pacInfo)
        {
            _pacInfo = pacInfo.GetInfo() + info;
        }

        public override string GetInfo()
        {
            return _pacInfo;
        }
    }
}