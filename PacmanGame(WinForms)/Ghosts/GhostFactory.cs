﻿namespace PacmanGame_WinForms_.Ghosts
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
                return new Blinky();

            }
            else if (name.Equals("CLYDE"))
            {
                return new Clyde();

            }
            else if (name.Equals("INKY"))
            {
                return new Inky();
            }
            else if (name.Equals("PINKY"))
            {
                return new Pinky();
            }

            return null;
        }


    }
}