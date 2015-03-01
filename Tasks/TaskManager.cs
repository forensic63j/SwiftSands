using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftSands
{
    class TaskManager
    {
        #region Fields
        private List<Task> tasks;
        #endregion

        #region Parameters
        public List<Task> Tasks
        {
            get
            {
                return tasks;
            }
        }
        public int Count
        {
            get
            {
                return tasks.Count;
            }
        }
        public Task this[int index]
        {
            get
            {
                if (index > 0 && index < tasks.Count)
                {
                    return tasks[index];
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region Methods/Constructor
        public TaskManager()
        {
            tasks = new List<Task>();
        }
        public void AddTask(Task task)
        {
            tasks.Add(task);
        }
        public void RemoveTask(Task task)
        {
            tasks.Remove(task);
        }
        public void Clear()
        {
            tasks.Clear();
        }
        #endregion
    }
}
