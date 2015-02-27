using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftSands
{
	class Player:Character
	{
		private int numDeaths;
		private int exp;
		private int deathsAllowed;
		private int expNeeded;
		List<int> stats;
		public Player(int h, int m, int sp, int st, int a, int l, bool c, String n, int d):base(sp, h, m, st, a, l, c n)
		{
			numDeaths = 0;
			exp = 0;
			deathsAllowed = d;
			expNeeded = 10 * Math.Pow(2, l - 1);
			stats = new List<int>();
			stats.Add(h);
			stats.Add(m);
			stats.Add(sp);
			stats.Add(st);
			stats.Add(a);
		}
		
		public void ReturnToDoctor()
		
			if(this.Health <= 0)
			{
				//Remove from party roster
				numDeaths++;
			}
		}
		
		public void LeaveTeam()
		{
			if(numDeaths == deathsAllowed)
			{
				//Player does not return to team
			}
		}
		
		public void LevelUp()
		{
			Random rand = new Random();
			if(exp >= expNeeded)
			{
				this.Level++;
				exp -= expNeeded;
				for(int i = 0; i < 5; i++)
				{
					if(rand.Next(0, 100) >= 75) //75% chance for each stat to boost
						stats[i]++;
				}
			}
		}
	}
}
