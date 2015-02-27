using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftSands
{
	class Character:Sprite
	{
		//stats
		private int speed;
		private int health;
		private int mana;
		private int strength;
		private int accuracy;
		private int level;
		private bool canJoin;
		private String name;
		
		public Character(int speed, int health, int mana, int strength, int accuracy, int level, bool a, String name)
		{
			this.speed = speed;
			this.health = health;
			this.mana = mana;
			this.strength = strength;
			this.accuracy = accuracy;
			this.level = level;
			canJoin = a;
			this.name = name;
		}
		
		//Properties
		public int Speed
		{
			get
			{
				return speed;
			}
			set
			{
				speed = value;
			}
		}
		public int Health
		{
			get
			{
				return health;
			}
			set
			{
				health = value;
			}
		}
		public int Mana
		{
			get
			{
				return mana;
			}
			set
			{
				mana = value;
			}
		}
		public int Strength
		{
			get
			{
				return strength;
			}
			set
			{
				strength = value;
			}
		}
		public int Accuracy
		{
			get
			{
				return accuracy;
			}
			set
			{
				accuracy = value;
			}
		}
		public int Level
		{
			get
			{
				return level;
			}
			set
			{
				level = value;
			}
		}
		public String Name
		{
			get
			{
				return name;
			}
		}
		
		//Methods
		public void Attack(Item weapon, Character enemy)
		{
			
		}
		public void Cast(Item spell, Character target)
		{
			
		}
	}
}
