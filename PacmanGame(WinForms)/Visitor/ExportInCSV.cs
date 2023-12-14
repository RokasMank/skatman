using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.NetworkInformation;

namespace PacmanGame_WinForms_.Visitor
{
    internal class ExportInCSV : IExportResultsVisitor
    {
        public void ExportGame(Game game)
        {
            string filePath = "game" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            using (StreamWriter outputFile = new StreamWriter(filePath, false))
            {
                string str = "";
                str += "Score,";
                str += "Level Reached,";
                str += "Spent Time,";
                str += "Steps";
                outputFile.WriteLine(str);
                str = "";
                str += game.Score.ToString() + ',';
                str += game.Level.ToString() + ',';
                str += game.spentMinute.ToString() + ':' + game.spentSecond.ToString() + ',';
                str += game.Steps.ToString();
                outputFile.WriteLine(str);
            }
        }

        public void ExportPoint(BasePoint point)
        {
            string filePath = "pacman" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            using (StreamWriter outputFile = new StreamWriter(filePath, false))
            {
                string str = "";
                str += "X Coordinate,";
                str += "Y Coordinate";
                outputFile.WriteLine(str);
                str = "";
                str += point.X.ToString() + ',';
                str += point.Y.ToString() + ',';
                outputFile.WriteLine(str);
            }
        }
    }
}
