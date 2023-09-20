using Microsoft.SqlServer.Server;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;


namespace PacmanGame_WinForms_
{ 
    public class Field
    {
        public int Columns { get; set; }
        public int Rows { get; set; }

        public static int CoinsCount { get; set; }
        public static BasePoint[,] Matrix { get; set; }

        private SourceFile[] Source { get; set; }

        public Field()
        {
            try
            {
                LoadJson();
                CreateMatrix();
            }
            catch
            {
                return;
            }
        }

        public BasePoint this[int i, int j]
        {
            get { return Matrix[i, j]; }
            set { Matrix[i, j] = value; }
        }

        public bool Finish()
        {
            return CoinsCount <= 0;
        }

        void LoadJson()
        {
            string path = @"sourceFile.json";
            using (StreamReader sr = new StreamReader(path))
            {
                Source = JsonConvert.DeserializeObject<SourceFile[]>(sr.ReadToEnd());
            }
        }

        char[,] FileReading()
        {
            SourceFile fieldParams = Source[0];

            string path = fieldParams.Path;
            Columns = fieldParams.Width;
            Rows = fieldParams.Height;

            StreamReader txtFile = new StreamReader(path);            

            char[,] gameMatrix = new char[Rows, Columns];

            string line;
            int i = 0;

            while ((line = txtFile.ReadLine()) != null)
            {
                for (int j = 0; j < line.Length; ++j)
                {
                    gameMatrix[i, j] = line[j];
                }
                ++i;
            }

            return gameMatrix;
        }


        void CreateMatrix()
        {
            CoinsCount = 0;           

            char[,] matrix = FileReading();
            Matrix = new BasePoint[Rows, Columns];

            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    switch (matrix[i, j])
                    {
                        case '#':
                            Matrix[i, j] = new Wall(j, i);
                            break;
                        case 'O':
                            Matrix[i, j] = new Wall(j, i) { Image = Properties.Resources.Portal, Portal = true };
                            break;
                        case '.':
                            Matrix[i, j] = new Coin(j, i);
                            ++CoinsCount;
                            break;
                        case '@':
                            Matrix[i, j] = new Energiser(j, i);
                            break;
                        case ' ':
                            Matrix[i, j] = new EmptyPoint(j, i);
                            break;
                        case '*':
                            Matrix[i, j] = new EmptyPoint(j, i);
                            Game.Pacman = new Pacman(j, i);
                            break;
                    }
                }
            }           
        }                
    }
}