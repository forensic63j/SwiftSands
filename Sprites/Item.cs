//John Palermo and Clayton Scavone

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwiftSands
{
	enum ItemType
	{
		Melee,
		Gun,
		HealingSpell,
		AttackSpell,
		Evidence
	}

	class Item:Sprite
	{
		private ItemType type;
		private int healing;
		private int damage;
        private int range;
		private String description;
		private ItemType itemType;
        private bool collected; //Whether or not the item has been collected

		public Item(ItemType i, int h, int d, int range, String de, bool col, Texture2D texture, Rectangle pos, bool active, 
			bool field, String name):base(texture, pos, active, name)
		{
			type = i;
			healing = h;
			damage = d;
			description = de;
            collected = col;
            this.range = range;
		}

        #region Properties
        public ItemType Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}
        public int Range
        {
            get
            {
                return range;
            }
            set
            {
                range = value;
            }
        }
		public int Healing
		{
			get
			{
				return healing;
			}
			set
			{
				healing = value;
			}
		}
		public int Damage
		{
			get
			{
				return damage;
			}
			set
			{
				damage = value;
			}
		}
		public String Description
		{
			get
			{
				return description;
			}
			set
			{
				description = value;
			}
		}
        public bool Collected
        {
            get
            {
                return collected;
            }
            set
            {
                collected = value;
            }
        }
        #endregion

        }
}
