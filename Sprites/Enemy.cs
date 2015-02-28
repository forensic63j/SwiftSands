using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwiftSands
{
	class Enemy:Character
	{
		private int expAwarded; //How much exp this enemy gives when it's defeated
		public Enemy(int mH, int h, int m, int sp, int st, int a, int l, bool c, String n, int e, Texture2D texture,
            Rectangle pos, bool active, bool field):base(mH, h, m, sp, st, a, l, c, n, texture, pos, active, field)
		{
			expAwarded = e;
		}
		
		public void Defeat(Party party, out String name)
		{
			for(int i = 0; i < party.Count; i++)
			{
				party[i].Exp += expAwarded;
			}
            this.Alive = false;
            name = this.Name;
		}
	}
}
