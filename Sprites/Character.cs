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
        private int maxHealth = 0;
		private int health = 0;
        private int mana = 0;
        private int speed = 0;
        private int strength = 0;
        private int accuracy = 0;
        private int level = 0;
        private bool active = true;
        private bool canJoin = false;
        private bool alive = true;
        private bool selected = false;
        private String name = "";
        private Item equipItem;
        #endregion

        public Character(bool canJoin, Texture2D texture, Rectangle pos, bool active, String name): base(texture, pos, active, name)
        {
            MaxHealth = 1;
            Health = 1;
            Mana = 0;
            Speed = 0;
            Strength = 0;
            Accuracy = 0;
            Level = 0;
            Name = name;
            CanJoin = canJoin;
            this.active = active;
            alive = true;
            equipItem = null;
        }

        public Character(int max, int health, int mana, int speed, int strength, int accuracy, int level, bool canJoin,
            Texture2D texture, Rectangle pos, bool active, String name):base(texture, pos, active, name)
		{
            MaxHealth = max;
			Health = health;
			Mana = mana;
			Speed = speed;
			Strength = strength;
			Accuracy = accuracy;
			Level = level;
            Name = name;
			CanJoin = canJoin;
            this.active = active;
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
        public Vector2 TilePosition
        {
            get
            {
                Rectangle convertedPos = StateManager.ConvertPosition(Position, StateManager.CurrentState.StateCamera);
                return new Vector2(convertedPos.X, convertedPos.Y);
            }
            set
            {
                Vector2 unconvertedPosition = StateManager.UnConvertPosition(value, StateManager.CurrentState.StateCamera);
                Position = new Rectangle((int)unconvertedPosition.X, (int)unconvertedPosition.Y, Position.Width, Position.Height);
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
                if (value > maxHealth)
                    health = maxHealth;
				else
                    health = value;
                if (health <= 0)
                {
                    Alive = false;
                }
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
            set
            {
                canJoin = value;
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
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
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
        public Player ToPlayer()
        {
            return new Player(MaxHealth, Health, Mana, Speed, Strength, Accuracy, Level, CanJoin, 0, Texture, Position, active, Name);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (selected)
            {
                base.Draw(spriteBatch, Color.Red);
            }
            else
            {
                base.Draw(spriteBatch);
            }
        }
    }
        #endregion
}
