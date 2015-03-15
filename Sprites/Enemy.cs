//John Palermo

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
		public Enemy(int mH, int h, int m, int sp, int st, int a, int l, bool c, int e, Texture2D texture,
            Rectangle pos, bool active, bool field, String n):base(mH, h, m, sp, st, a, l, c, texture, pos, active, field, n)
		{
			expAwarded = e;
		}

        public int ExpAwarded
        {
            get { return expAwarded; }
            set
            {
                expAwarded = value;
            }
        }
		
		public void Defeat()
		{
			for(int i = 0; i < Party.PartyList.Count; i++)
			{
				Party.PartyList[i].Exp += expAwarded;
			}
            this.Alive = false;
		}
	}
}
