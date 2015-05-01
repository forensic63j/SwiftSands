//John Palermo

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace SwiftSands
{
    enum TaskType
    {
        CollectItem,
        Converse,
        Hunt
    }
    class Task
    {
        #region Fields
        private TaskType type;
        private String description;
        private String target; //Name of item, person to talk to, or enemy to kill
        private int expReward;
        private bool completed;
        #endregion

        public Task(TaskType type, String desc, String target, int reward)
        {
            this.type = type;
            this.description = desc;
            this.target = target;
            this.expReward = reward;
            completed = false;
        }

        #region Properties
        public TaskType Type
        {
            get
            {
                return type;
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
        public String Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }
        public int ExpReward
        {
            get
            {
                return expReward;
            }
            set
            {
                expReward = value;
            }
        }
        public bool Completed
        {
            get
            {
                return completed;
            }
            set
            {
                completed = value;
            }
        }
        #endregion

        #region Methods
        public void EndTask()
        {
            for (int i = 0; i < Party.PartyList.Count; i++)
            {
                Party.PartyList[i].Exp += this.ExpReward;
            }
            this.completed = true;
            TextBox.Instance.Text = "You have completed a task!";
            TextBox.Instance.IsActive = true;
        }
        #endregion
    }
}
