﻿using System;
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

        public Player(int max, int h, int m, int sp, int st, int acc, int level, bool canJoin, String name, int deaths, Texture2D texture,
            Rectangle pos, bool active, bool field):base(max, h, m, sp, st, acc, level, canJoin, name, texture, pos, active, field)
		{
			numDeaths = 0;
			exp = 0;
			deathsAllowed = deaths;
			expNeeded = (int)(10 * Math.Pow(2, level - 1));
			stats = new List<int>();
			stats.Add(h);
			stats.Add(m);
			stats.Add(sp);
			stats.Add(st);
			stats.Add(acc);
		}

        #region Parameters
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
        #endregion

        #region Methods
        public void ReturnToDoctor(Party party)
		{
			if(this.Health <= 0)
			{
                party.Remove(this);
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
        public void CheckTasks(TaskManager manager, Enemy enemy, Item item, Character character, Party party)
        {
            if (manager.Count > 0)
            {
                for (int i = 0; i < manager.Count; i++)
                {
                    if (manager[i].Type == TaskType.Hunt)
                    {
                        manager[i].UpdateHuntTask(enemy, party);
                    }
                    else if (manager[i].Type == TaskType.CollectItem)
                    {
                        manager[i].UpdateCollectionTask(item, party);
                    }
                    else if (manager[i].Type == TaskType.Converse)
                    {
                        manager[i].UpdateConverseTask(character, party);
                    }
                }
            }
        }
        public void CollectItem(Item item, Inventory inventory)
        {
            inventory.AddItem(item);
        }
        #endregion
    }
}
