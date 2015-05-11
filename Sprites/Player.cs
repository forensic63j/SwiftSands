//John Palermo

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace SwiftSands
{
	class Player:Character
    {
        #region Fields
        private int numDeaths;
		private int exp;
		private int deathsAllowed;
		private int expNeeded;
		Dictionary<String, int> stats;
        #endregion

        public Player(int maxHealth, int health, int maxMana,int mana, int speed, int strength, int accuracy, int moverange, int level, bool canJoin, int deaths, Texture2D texture,
            Rectangle pos, bool active, String name)
            : base(maxHealth, health, maxMana,mana, speed, strength, accuracy, moverange, level, canJoin, texture, pos, active, name)
        {
            numDeaths = 0;
            exp = 0;
            deathsAllowed = deaths;
            expNeeded = (int)(10 * Math.Pow(2, level - 1));
            stats = new Dictionary<String, int>();
            stats.Add("Health", MaxHealth);
            stats.Add("Mana", MaxMana);
            stats.Add("Speed", Speed);
            stats.Add("Strength", Strength);
            stats.Add("Accuracy", Accuracy);
            stats.Add("Range", MovementRange);
        }

        public Player(int maxHealth, int health, int mana, int speed, int strength, int accuracy, int moverange, int level, bool canJoin, int deaths, Texture2D texture,
            Rectangle pos, bool active, String name, String conversation)
            : base(maxHealth, health, mana, speed, strength, accuracy, moverange, level, canJoin, texture, pos, active, name, conversation)
		{
			numDeaths = 0;
			exp = 0;
			deathsAllowed = deaths;
			expNeeded = (int)(10 * Math.Pow(2, level - 1));
            stats = new Dictionary<String, int>();
            stats.Add("Health", MaxHealth);
            stats.Add("Mana", MaxMana);
            stats.Add("Speed", Speed);
            stats.Add("Strength", Strength);
            stats.Add("Accuracy", Accuracy);
            stats.Add("Range", MovementRange);
		}

        #region Properties

        public int Exp
		{
			get
			{
				return exp;
			}
			set
			{
				exp = value;
                if (exp >= this.ExpNeeded)
                    while (exp >= this.ExpNeeded)
                        LevelUp();
			}
		}
		
		public int ExpNeeded
		{
			get
			{
				return expNeeded;
			}
			set
			{
				expNeeded = value;
			}
		}

        public int NumDeaths
        {
            get
            {
                return numDeaths;
            }
            set
            {
                numDeaths = value;
            }
        }

        public int DeathsAllowed
        {
            get
            {
                return deathsAllowed;
            }
        }
        #endregion

        #region Methods
        public void ReturnToDoctor()
		{
			if(this.Health <= 0)
			{
                Party.Remove(this);
				numDeaths++;
			}
		}
		
		public void LeaveTeam()
		{
			if(numDeaths == deathsAllowed)
			{
                this.CanJoin = false;
			}
		}
		
		public void LevelUp()
		{
			this.Level++;
			exp -= expNeeded;
            expNeeded = (int)(.5 * Math.Pow(2, Level - 1));
            TextBox.Instance.Text = "Congratulations! You leveled up!";
            TextBox.Instance.IsActive = true;
            String s = "No stats";
            String s1 = "";
            Dictionary<String, int> temp = new Dictionary<string, int>();
			foreach(KeyValuePair<String, int> stat in stats)
			{
                String key = stat.Key;
                int v = stat.Value;
                if (StateManager.CurrentState.StateGame.Rand.Next(0, 101) >= 25) //75% chance for each stat to be boosted
                {
                    s1 += key + ", ";
                    if (key == "Health")
                    {
                        MaxHealth += 5;
                        v += 5;
                    }
                    else if (key == "Mana")
                    {
                        MaxMana += 5;
                        v += 5;
                    }
                    else if (key == "Speed")
                    {
                        Speed += 5;
                        v += 5;
                    }
                    else if (key == "Strength")
                    {
                        Strength += 5;
                        v += 5;
                    }
                    else if (key == "Accuracy")
                    {
                        Accuracy += 5;
                        v += 5;
                    }
                    else if (key == "Range")
                    {
                        MovementRange++;
                        v++;
                    }
                }
                temp.Add(key, v);
			}
            this.Health = this.MaxHealth;
            this.Mana = this.MaxMana;
            stats = temp;
            if (s1 != "")
                s = s1;
            TextBox.Instance.Text = s;
        }
        /// <summary>
        /// Checks for the completion of tasks
        /// </summary>
        public void UpdateTasks(Sprite sprite)
        {
            for (int i = 0; i < TaskManager.Count; i++)
            {
                Task task = TaskManager.Tasks[i];
                TaskType type = task.Type;
                if (sprite is Enemy && type == TaskType.Hunt)
                {
                    Enemy e = (Enemy)sprite;
                    if (e.Name == task.Target && !e.Alive)
                    {
                        task.EndTask();
                    }
                }
                else if (sprite is Item && type == TaskType.CollectItem)
                {
                    Item item = (Item)sprite;
                    if (item.Name == task.Target && item.Collected)
                    {
                        task.EndTask();
                    }
                }
                else if (sprite is Character && type == TaskType.Converse)
                {
                    Character c = (Character)sprite;
                    if (c.Name == task.Target)
                    {
                        task.EndTask();
                    }
                }
            }
        }
        public override int Move(Vector2 newTile)
        {
            Interact();
            return base.Move(newTile);
        }
        public void Interact()
        {
            Vector2 currentPos = TilePosition;
            Dictionary<String, Character> characters = StateManager.CurrentState.StateGame.CharacterList;
            Dictionary<String, Item> items = StateManager.CurrentState.StateGame.ItemList;
            foreach (KeyValuePair<String, Item> its in items)
            {
                Item it = its.Value;
                if (it.IsActive && it.TilePosition == currentPos)
                {
                    PickUpItem(it);
                    UpdateTasks(it);
                }
            }
            foreach(KeyValuePair<String, Character> character in characters)
            {
                Character ch = character.Value;
                if (ch != this && ch.IsActive && ch.TilePosition == currentPos)
                {
                    Converse(ch);
                    UpdateTasks(ch);
                }
            }
        }
        public void PickUpItem(Item item)
        {
            Inventory.AddItem(item);
            item.IsActive = false;
            TextBox.Instance.Text = "You picked up " + item.Name;
            TextBox.Instance.IsActive = true;
        }
        public void Converse(Character character)
        {
            TextBox.Instance.Text = character.Conversation;
            TextBox.Instance.IsActive = true;
        }
        #endregion
    }
}
