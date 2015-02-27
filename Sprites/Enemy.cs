using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftSands
{
	class Enemy:Character
	{
		private int expAwarded; //How much exp this enemy gives when it's defeated
		public Enemy(int h, int m, int sp, int st, int a, int l, bool c, String n, int e):base(sp, h, m, st, a, l, c, n)
		{
			expAwarded = e;
		}
		
		public void Defeat(Party party)
		{
			for(int i = 0; i < party.Count; i++)
			{
				party[i].Exp += expAwarded;
			}
		}
	}
}
