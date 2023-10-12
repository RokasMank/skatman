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
            for (int i = 0; i < ghostTeam.GetCount(); ++i)
            {
                ghostTeam[i].ChaseMode = false;
            }
        }
    }
}