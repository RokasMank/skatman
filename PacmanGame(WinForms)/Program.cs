using PacmanGame_WinForms_.Forms;
using PacmanGame_WinForms_.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacmanGame_WinForms_
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 
        public static LogInForm Authorization;
        public static Menu Menu;
        public static Settings Set;
        public static string Name;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
          //  IMenuMediator mediator = new GameMediator(chatWindow);
          
            Menu = new Menu();
            //Application.Run(Menu);
            Application.Run(new Menu());
            //Application.Run(new Registration());
            //Application.Run(new Results());
        }
    }
}