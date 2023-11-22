using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Proxy
{
    public class FieldBackupProxy : IField
    {
        private Field field;
        private bool fieldIsValid = false;

        public FieldBackupProxy() { /*FieldValidation();*/ }

        public int Rows { 
            get { 
                FieldValidation();
                return field.Rows; 
            }  
            set { 
                FieldValidation();
                field.Rows = value; 
            }
        }

        public int Columns
        {
            get
            {
                FieldValidation();
                return field.Columns;
            }
            set
            {
                FieldValidation();
                field.Columns = value;
            }
        }

        public BasePoint this[int i, int j] { 
            get {
                FieldValidation();
                return field[i, j]; 
            } 
            set { 
                FieldValidation();
                field[i, j] = value; 
            } 
        }

        public bool Finish()
        {
            FieldValidation();
            return field.Finish();
        }

        private void FieldValidation()
        {
            if (fieldIsValid) return;
            if (field == null)
            {
                field = new Field();
                if (field.matrixIsCreated == false)
                    field.CreateMatrix(backupMatrix);
            }
            fieldIsValid = true;
        }

        private char[,] backupMatrix
        {
            get
            {
                string str = "############################\r\n#............##............#\r\n#.####.#####.##.#####.####.#\r\n#.####.#####.##.#####.####.#\r\n#.####.#####.##.#####.####.#\r\n#....................@.....#\r\n#.####.#.##########.#.####.#\r\n#.####.#.##########.#.####.#\r\n#......#...@####....#......#\r\n######.####.####.####.######\r\n######.####.####.####.######\r\n######.#............#.######\r\n######.#.##########.#.######\r\nO........#...*....#........O\r\n######.#.#### #####.#.######\r\n######.#............#.######\r\n######.#.##########.#.######\r\n######.#.##########.#.######\r\n#............##...........@#\r\n#.####.#####.##.#####.####.#\r\n#.####.#####.##.#####.####.#\r\n#...##@...............##...#\r\n###.##.##.########.##.##.###\r\n#..........................#\r\n############################";
                Columns = 28;
                Rows = 25;
                int sIndex = 0;
                char[,] a = new char[Rows, Columns];
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        while (sIndex + 1 < str.Length && (str[sIndex] == '\r' || str[sIndex] == '\n'))
                            sIndex++;
                        a[i, j] = str[sIndex++];
                    }
                }
                return a;
            }
        }
    }
}
