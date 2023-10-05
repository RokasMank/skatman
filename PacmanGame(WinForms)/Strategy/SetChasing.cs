using System;

namespace PacmanGame_WinForms_
{
	public class SetChasing : Chasing
	{
		public SetChasing()
		{
		}
        public override void Chase(GhostTeam ghostTeam)
		{
            GhostTeam.SetChaseMode(true); // add stuff
        }
    }
}