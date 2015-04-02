//John Palermo

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
		private String description;
		private ItemType itemType;
        private bool collected; //Whether or not the item has been collected

		public Item(ItemType i, int h, int d, String de, bool col, Texture2D texture, Rectangle pos, bool active, 
			bool field, String name):base(texture, pos, active, name)
		{
			itemType = i;
			healing = h;
			damage = d;
			description = de;
            collected = col;
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
