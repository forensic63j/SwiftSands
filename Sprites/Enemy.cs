using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftSands
{
	class Enemy:Character
	{
		private int expAwarded;
		public Enemy(int h, int m, int sp, int st, int a, int l, bool c, String n, int e):base(sp, h, m, st, a, l, c, n)
		{
			expAwarded = e;
		}
	}
}
