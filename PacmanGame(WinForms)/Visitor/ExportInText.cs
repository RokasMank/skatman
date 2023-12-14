using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.NetworkInformation;

namespace PacmanGame_WinForms_.Visitor
{
    internal class ExportInText: IExportResultsVisitor
    {
        public void ExportGame(Game game)
        {
            string filePath = "game" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            using (StreamWriter outputFile = new StreamWriter(filePath, false))
            {
                string str = "";
                str = "Score: " + game.Score.ToString();
                outputFile.WriteLine(str);
                str = "Level reached: " + game.Level.ToString();
                outputFile.WriteLine(str);
                str = "Spent Time: " + game.spentMinute.ToString() + ':' + game.spentSecond.ToString();
                outputFile.WriteLine(str);
                str = "Steps: " + game.Steps.ToString();
                outputFile.WriteLine(str);
            }
        }

        public void ExportPoint(BasePoint point)
        {
            string filePath = "pacman" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            using (StreamWriter outputFile = new StreamWriter(filePath, false))
            {
                string str = "";
                str = "X Coordinate: " + point.X.ToString();
                outputFile.WriteLine(str);
                str = "Y Coordinate: " + point.Y.ToString();
                outputFile.WriteLine(str);
            }
        }
    }
}
