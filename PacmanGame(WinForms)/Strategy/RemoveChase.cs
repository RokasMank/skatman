using System;

namespace PacmanGame_WinForms_
{
	public class RemoveChasing : Chasing
	{
		public RemoveChasing()
		{
		}
		public override void Chase(GhostTeam ghostTeam)
		{
			GhostTeam.SetChaseMode(false); // add stuff
		}
	}
}