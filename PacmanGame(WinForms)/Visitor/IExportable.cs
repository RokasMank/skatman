using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Visitor
{
    public interface IExportable
    {
        void Accept(IExportResultsVisitor visitor);
    }
}
