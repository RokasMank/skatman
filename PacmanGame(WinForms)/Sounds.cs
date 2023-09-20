using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_
{
    static class Sounds
    {
        public static SoundPlayer sound = new SoundPlayer(Properties.Resources.game1);
        static bool SoundEnabled;

        public static void PlayMusic()
        {
            if (SoundEnabled)
            {
                sound.PlayLooping();
            }
        }

        public static void SoundOff()
        {
            SoundEnabled = false;
        }

        public static void SoundOn()
        {
            SoundEnabled = true;
        }
    }
}