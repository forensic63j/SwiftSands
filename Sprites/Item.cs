using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
		private String name;
		private String description;
		private ItemType itemType;

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

		/// <summary>
		/// Converts a String into a ItemType. Throws an exception if the String does not match a type.
		/// </summary>
		/// <param name="s">The string to be parsed.</param>
		/// <returns>The type or an exception.</returns>
		public static ItemType ParseType(String s){
			switch(s)
			{
				case "Melee": return ItemType.Melee;
				case "AttackSpell": return ItemType.AttackSpell;
				case "Evidence": return ItemType.Evidence;
				case "Gun": return ItemType.Gun;
				case "HealingSpell": return ItemType.HealingSpell;
				default: throw new ArgumentException();
			}
		}
	}
}
