using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace PacmanGame_WinForms_.Visitor
{
    internal class ExportInJSON : IExportResultsVisitor
    {
        public void ExportGame(Game game)
        {
            string filePath = "game" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".json";
            using (StreamWriter outputFile = new StreamWriter(filePath, false))
            {
                var obj = new
                {
                    Score = game.Score.ToString(),
                    Level = game.Level.ToString(),
                    SpentTime = game.spentMinute.ToString() + ':' + game.spentSecond.ToString(),
                    Steps = game.Steps.ToString()
                };
                string jsonString = JsonSerializer.Serialize(obj);
                outputFile.WriteLine(jsonString);
            }
        }

        public void ExportPoint(BasePoint point)
        {
            string filePath = "pacman" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".json";
            using (StreamWriter outputFile = new StreamWriter(filePath, false))
            {
                var obj = new
                {
                    XCoord = point.X.ToString(),
                    YCoord = point.Y.ToString()
                };
                string jsonString = JsonSerializer.Serialize(obj);
                outputFile.WriteLine(jsonString);
            }
        }
    }
}
