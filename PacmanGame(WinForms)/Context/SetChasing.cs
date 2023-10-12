using System;
using System.Collections.Generic;

namespace PacmanGame_WinForms_
{
	public class SetChasing : Chasing
	{
		public SetChasing()
		{
		}
        public override void Chase(GhostTeam ghostTeam)
		{
            for (int i = 0; i < ghostTeam.GetCount(); ++i)
            {
                ghostTeam[i].ChaseMode = true;
            }
        }

    }
}