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
        static protected Item _defaultItem = new Item(ItemType.Melee, 0, 5, 2, "Fists", true, null, new Rectangle(), false, false, "Fist");

        private int maxHealth = 0;
		private int health = 0;
        private int mana = 0;
        private int speed = 0;
        private int strength = 0;
        private int accuracy = 0;
        private int level = 0;
        private int movementRange = 1;
        private bool active = true;
        private bool canJoin = false;
        private bool alive = true;
        private bool selected = false;
        private String name = "";
        private String conversation;
        private Item equipItem;
        #endregion

        #region Constructors
        public Character(bool canJoin, Texture2D texture, Rectangle pos, bool active, String name)
            : base(texture, pos, active, name)
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
            equipItem = _defaultItem;
        }

        public Character(int max, int health, int mana, int speed, int strength, int accuracy, int moverange, int level, bool canJoin,
            Texture2D texture, Rectangle pos, bool active, String name)
            : base(texture, pos, active, name)
        {
            MaxHealth = max;
            Health = health;
            Mana = mana;
            Speed = speed;
            Strength = strength;
            Accuracy = accuracy;
            Level = level;
            MovementRange = moverange;
            Name = name;
            CanJoin = canJoin;
            this.active = active;
            alive = true;
            equipItem = null;
        }


        public Character(bool canJoin, Texture2D texture, Rectangle pos, bool active, String name, String conversation)
            : base(texture, pos, active, name)
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
            this.conversation = conversation;
            equipItem = null;
        }

        public Character(int max, int health, int mana, int speed, int strength, int accuracy, int moverange, int level, bool canJoin,
            Texture2D texture, Rectangle pos, bool active, String name, String conversation):base(texture, pos, active, name)
		{
            MaxHealth = max;
			Health = health;
			Mana = mana;
			Speed = speed;
			Strength = strength;
			Accuracy = accuracy;
			Level = level;
            MovementRange = moverange;
            Name = name;
			CanJoin = canJoin;
            this.active = active;
			alive = true;
            this.conversation = conversation;
            equipItem = null;
		}
        #endregion

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
        public int MovementRange
        {
            get { return movementRange; }
            set { movementRange = value; }
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
                if (equipItem != null)
                {
                    return equipItem;
                }
                else
                {
                    return _defaultItem;
                }
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
        public String Conversation
        {
            get
            {
                return conversation;
            }
            set
            {
                conversation = value;
            }
        }
        static public Item DefaultItem
        {
            get { return _defaultItem; }
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

        public void DrawHealthbar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(StateManager.CurrentState.StateGame.PixTex, new Rectangle(this.Position.X, this.Position.Y - 3, 30 * (health / MaxHealth), 2), Color.Green);
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
		public virtual void TakeDamage(int damage)
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
            Player player = new Player(MaxHealth, Health, Mana, Speed, Strength, Accuracy, 4, Level, CanJoin, 0, Texture, Position, active, Name, conversation);
			player.EquipItem = equipItem;
			return player;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (selected)
            {
                base.Draw(spriteBatch, new Color(255, 165, 165));
            }
            else
            {
                base.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Finds the valid movements for the current character
        /// </summary>
        /// <param name="validTiles">A map of the tiles on the screen, you can move to tiles with a value of "false".</param>
        /// <param name="x">Current x.</param>
        /// <param name="y">Current y.</param>
        /// <param name="move">Moves left.</param>
        public void ValidMovements(ref bool[,] validTiles, List<Character> characters, int x, int y, int move)
        {
            Map currentmap = new Map(0, 0, 0, 0, "error");
            if (StateManager.CurrentState is LocalMap)
            {
                LocalMap localmap = StateManager.CurrentState as LocalMap;
                currentmap = localmap.Map;
            }
            if (StateManager.CurrentState is WorldMap)
            {
                WorldMap localmap = StateManager.CurrentState as WorldMap;
                currentmap = localmap.Map;
            }
			if(currentmap.InBounds(x,y) && currentmap.ColliderLayer[x,y] <= 0 && (TileOccupent(characters,x,y) == null || TileOccupent(characters,x,y) == this))
            {
                validTiles[x, y] = true;
                //checks adjacent
                if (move >= 1)
                {
                    //top
                    if (currentmap.InBounds(x - 1, y - 1)/* && !validTiles[x - 1, y - 1]*/)
                    {
                        //ValidMovements(ref validTiles, x - 1, y - 1, move - 1);    //Can't move diagonally
                    }
                    if (currentmap.InBounds(x, y - 1)/* && !validTiles[x, y - 1]*/)
                    {
                        ValidMovements(ref validTiles, characters, x, y - 1, move - 1);
                    }
                    if (currentmap.InBounds(x + 1, y - 1)/* && !validTiles[x + 1, y - 1]*/)
                    {
                        //ValidMovements(ref validTiles, characters, x + 1, y - 1, move - 1);
                    }

                    //middle
                    if (currentmap.InBounds(x - 1, y)/* && !validTiles[x - 1, y]*/)
                    {
                        ValidMovements(ref validTiles, characters, x - 1, y, move - 1);
                    }
                    if (currentmap.InBounds(x + 1, y)/* && !validTiles[x + 1, y]*/)
                    {
                        ValidMovements(ref validTiles, characters, x + 1, y, move - 1);
                    }

                    //bottom
                    if (currentmap.InBounds(x - 1, y + 1)/* && !validTiles[x - 1, y + 1]*/)
                    {
                        //ValidMovements(ref validTiles, x - 1, y + 1, move - 1);
                    }
                    if (currentmap.InBounds(x, y + 1)/* && !validTiles[x, y + 1]*/)
                    {
                        ValidMovements(ref validTiles, characters, x, y + 1, move - 1);
                    }
                    if (currentmap.InBounds(x + 1, y + 1)/* && !validTiles[x + 1, y + 1]*/)
                    {
                        //ValidMovements(ref validTiles,characters, x + 1, y + 1, move - 1);
                    }
                }
            }
        }

        /// <summary>
        /// Finds the character at a certian tile.
        /// </summary>
        /// <param name="x">The tiles x.</param>
        /// <param name="y">The tiles y.</param>
        /// <returns>The character on that tile.</returns>
        static public Character TileOccupent(List<Character> characters, int x, int y)
        {
            foreach (Character c in characters)
            {
                Vector2 cPosition = c.TilePosition;
                if (cPosition.X == x && cPosition.Y == y)
                {
                    return c;
                }
            }
            return null;
        }

        public bool OnTile(Vector2 tile)
        {
            if (this.TilePosition == tile)
                return true;
            return false;
        }

		public virtual int Move(Vector2 newTile)
		{
            Map currentmap = new Map(0,0,0,0,"error");
            int intDistance = 0;
            if(StateManager.CurrentState is LocalMap){
                LocalMap localmap = StateManager.CurrentState as LocalMap;
                           currentmap = localmap.Map;
            }   
            if(StateManager.CurrentState is WorldMap){
                WorldMap localmap = StateManager.CurrentState as WorldMap;
                           currentmap = localmap.Map;
            }
			if(Selected)
			{
				Vector2 startTile = TilePosition;
				//double distance = Math.Ceiling(Math.Sqrt(Math.Pow((startTile.X - newTile.X),2) + Math.Pow((startTile.Y - newTile.Y),2)));
                float distance = Math.Abs(startTile.X - newTile.X) + Math.Abs(startTile.Y - newTile.Y);
                intDistance = (int)distance;
                Console.WriteLine("Distance: " + intDistance + " MovementRange: " + movementRange);
                if (intDistance <= movementRange)
				{
                    if (!currentmap.TileCollide((int)newTile.X, (int)newTile.Y)) 
                    {
					    TilePosition = newTile;
                        return intDistance;
                    }
				}
			}
            intDistance = 0;
            return intDistance;
		}
    }
        #endregion
}
