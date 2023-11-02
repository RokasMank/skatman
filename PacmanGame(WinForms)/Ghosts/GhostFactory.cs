using PacmanGame_WinForms_.Bridge;

namespace PacmanGame_WinForms_.Ghosts
{
    public class GhostFactory
    {
        public Ghost getGhost(string name)
        {
            if (name == null)
            {
                return null;
            }

            if (name.Equals("BLINKY"))
            {
                return new Blinky(new SpeedUp());

            }
            else if (name.Equals("CLYDE"))
            {
                return new Clyde(new SpeedDown());

            }
            else if (name.Equals("INKY"))
            {
                return new Inky(new SpeedUp());
            }
            else if (name.Equals("PINKY"))
            {
                return new Pinky(new SpeedDown());
            }

            return null;
        }


    }
}