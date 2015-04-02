//John Palermo

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwiftSands
{
	class Player:Character
    {
        #region Fields
        private int numDeaths;
		private int exp;
		private int deathsAllowed;
		private int expNeeded;
		List<int> stats;
        #endregion

        public Player(int maxHealth, int health, int mana, int speed, int strength, int accuracy, int level, bool canJoin, int deaths, Texture2D texture,
            Rectangle pos, bool active, String name):base(maxHealth, health, mana, speed, strength, accuracy, level, canJoin, texture, pos, active, name)
		{
			numDeaths = 0;
			exp = 0;
			deathsAllowed = deaths;
			expNeeded = (int)(10 * Math.Pow(2, level - 1));
			stats = new List<int>();
			stats.Add(Health);
			stats.Add(Mana);
			stats.Add(Speed);
			stats.Add(Strength);
			stats.Add(Accuracy);
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
				if(exp >= this.ExpNeeded)
					this.LevelUp();
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
				//Player does not return to team
			}
		}
		
		public void LevelUp()
		{
			Random rand = new Random();
			this.Level++;
			exp -= expNeeded;
			for(int i = 0; i < 5; i++)
			{
				if(rand.Next(0, 100) >= 75) //75% chance for each stat to boost
					stats[i]++;
			}
        }
        /// <summary>
        /// Checks for the completion of tasks
        /// </summary>
        /// <param name="manager">The list of tasks available</param>
        /// <param name="enemy">The enemy defeated (null if task is not a hunt task)</param>
        /// <param name="item">The item collected (null if task is not a collection task)</param>
        /// <param name="character">The character talked to (null if task is not a conversation task)</param>
        /// <param name="party">The current party</param>
        public void CheckTasks(TaskType type, Sprite sprite)
        {
            if (TaskManager.Count > 0)
            {
                for (int i = 0; i < TaskManager.Count; i++)
                {
                    TaskManager.Tasks[i].UpdateTasks(type, sprite);
                }
            }
        }
        public void CollectItem(Item item)
        {
            
        }
        #endregion
    }
}
