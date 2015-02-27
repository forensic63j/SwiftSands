using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftSands
{
	enum ItemType
	{
		Weapon,
		HealingSpell,
		AttackSpell,
		Evidence
	}
	class Item:Sprite
	{
		private ItemType type;
		private int healing;
		private int damage;
		private String name;
		private String description;
		public Item(ItemType i, int h, int d, String n, String de)
		{
			itemType = i;
			healing = h;
			damage = d;
			name = n;
			description = de;
		}
		
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
		public String Name
		{
			get
			{
				return name;
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
	}
}
