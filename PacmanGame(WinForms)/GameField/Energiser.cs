using System;
using System.Drawing;

namespace PacmanGame_WinForms_
{
    public class Energiser : BasePoint, ICloneable
    {
        public Energiser(int x, int y)
            : base(x, y)
        {
            Image = Properties.Resources.Energizer;
        }

        public DateTime Time { get; set; }

        public Energiser(int x, int y, int timeStop)
             : base(x, y)
        {
            Time = SetTimeToChange(timeStop);
        }

        public override void Action()
        {
            GetScore();
            Image = Properties.Resources.Empty;
            Controller.GetInstance().MakeEmpty(Y, X);

        }

        public override void GetScore()
        {
            Game.Score += 50;
        }

        public bool ReadyToStop(DateTime timeToChange)
        {
            int differenceTime = DateTime.Compare(timeToChange, DateTime.Now);
            if (differenceTime <= 0)
            {
                return true;
            }
            return false;
        }

        DateTime SetTimeToChange(double milliseconds)
        {
            DateTime time = DateTime.Now;
            return time.AddMilliseconds(milliseconds);
        }

        public object Clone()
        {
            return this.MemberwiseClone();  
        }
        public object DeepClone(int timeStop)
        {
            var newenergiser = (Energiser)MemberwiseClone();
            newenergiser.Image = new Bitmap(Properties.Resources.Energizer);
            newenergiser.Time = SetTimeToChange(timeStop);
            return newenergiser;
        }
    }
}