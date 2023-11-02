using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacmanGame_WinForms_
{
    public partial class Settings : Form
    {
        public static int Interval = 100;
        internal IMenuCommand menuCommand;

        public Settings(IMenuCommand command)
        {
            InitializeComponent();
            menuCommand = command;
        }

        private void Settings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                menuCommand.Undo();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Program.Name = textBox1.Text;
            textBox1.Enabled = false;
            Enabled = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Interval = 150;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Interval = 100;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Interval = 70;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Sounds.SoundOn();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menuCommand.Undo();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Sounds.SoundOff();
        }
    }
}