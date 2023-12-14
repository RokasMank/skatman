using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Visitor
{
    internal interface IExportResultsVisitor
    {
        void ExportGame(Game game);
        void ExportPoint(BasePoint point);
    }
}
