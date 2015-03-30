//John Palermo

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwiftSands
{
	class Character:Sprite
    {
        #region Fields
        private int maxHealth;
		private int health;
		private int mana;
		private int speed;
		private int strength;
		private int accuracy;
		private int level;
		private bool canJoin;
		private bool alive;
		private String name;
        private Item equipItem;
        #endregion

        public Character(int max, int health, int mana, int speed, int strength, int accuracy, int level, bool a,
            Texture2D texture, Rectangle pos, bool active, bool field, String name):base(texture, pos, active, field, name)
		{
			this.health = health;
			this.mana = mana;
			this.speed = speed;
			this.strength = strength;
			this.accuracy = accuracy;
			this.level = level;
            this.name = name;
			canJoin = a;
			alive = true;
            equipItem = null;
		}
		
        #region Properties
		public int MaxHealth
		{
			get
			{
				return maxHealth;
			}
			set
			{
				maxHealth = value;
			}
		}
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
				if(value > maxHealth)
					health = maxHealth;
				else
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
		public bool CanJoin
		{
			get
			{
				return canJoin;
			}
		}
		public bool Alive
		{
			get
			{
				return alive;
			}
			set
			{
				alive = value;
			}
		}
        public Item EquipItem
        {
            get
            {
                return equipItem;
            }
            set
            {
                equipItem = value;
            }
        }
        #endregion

        #region Methods
        public void Attack(Item weapon, Character enemy)
		{
			Random rand = new Random();
			if(rand.Next(101) <= this.accuracy)
			{
				int damage = this.Strength;
				if(weapon.Type == ItemType.Melee)
				{
					damage += weapon.Damage;
				}
				if(weapon.Type == ItemType.Gun)
				{
					damage = weapon.Damage;
				}
				enemy.TakeDamage(damage);
			}
		}
		public void Cast(Item spell, Character target)
		{
			if(spell.Type == ItemType.HealingSpell)
			{
				target.Heal(spell.Healing);
			}
			if(spell.Type == ItemType.AttackSpell)
			{
				target.TakeDamage(spell.Damage);
			}
		}
		public void TakeDamage(int damage)
		{
			this.Health -= damage;
			if(this.Health <= 0)
			{
				alive = false;
			}
		}
		public void Heal(int addHealth)
		{
			this.Health += addHealth;
		}
    }
        #endregion
}
