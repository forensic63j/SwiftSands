﻿//John Palermo

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        #endregion

        public Task(TaskType type, String desc, String target, int reward)
        {
            this.type = type;
            this.description = desc;
            this.target = target;
            this.expReward = reward;
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
        #endregion

        #region Methods
        public void EndTask()
        {
            for (int i = 0; i < Party.PartyList.Count; i++)
            {
                Party.PartyList[i].Exp += this.ExpReward;
            }
        }
        #endregion
    }
}
