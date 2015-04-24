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
        public Enemy(int maxHealth, int health, int mana, int speed, int strength, int accuracy, int movementrange, int level, bool canJoin,
            int exp, Texture2D texture, Rectangle pos, bool active, String n)
            : base(maxHealth, health, mana, speed, strength, accuracy, movementrange, level, canJoin, texture, pos, active, n)
        {

            expAwarded = exp;
        }
		public Enemy(int maxHealth, int health, int mana, int speed, int strength, int accuracy, int movementrange, int level, bool canJoin, 
            int exp, Texture2D texture, Rectangle pos, bool active, String n, String conversation)
            : base(maxHealth, health, mana, speed, strength, accuracy, movementrange, level, canJoin, texture, pos, active, n, conversation)
		{

			expAwarded = exp;
		}
        public int ExpAwarded
        {
            get { return expAwarded; }
            set
            {
                expAwarded = value;
            }
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            if (!Alive)
                Defeat();
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
