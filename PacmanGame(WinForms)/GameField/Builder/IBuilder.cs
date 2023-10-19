using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.GameField.Builder
{
    public interface IBuilder
    {
        IBuilder BuildImage();
        IBuilder BuildAction();
        IBuilder SetPosition(int x, int y);
    }
}
