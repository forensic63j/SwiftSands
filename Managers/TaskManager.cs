using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftSands
{
    static class TaskManager
    {
        #region Fields
        static private List<Task> tasks;
        #endregion

        static TaskManager()
        {
            tasks = new List<Task>();
        }

        #region Parameters
        static public List<Task> Tasks
        {
            get
            {
                return tasks;
            }
        }
        static public int Count
        {
            get
            {
                return tasks.Count;
            }
        }
        #endregion

        #region Methods
        static public void AddTask(Task task)
        {
            tasks.Add(task);
        }
        static public void RemoveTask(Task task)
        {
            tasks.Remove(task);
        }
        static public void Clear()
        {
            tasks.Clear();
        }
        #endregion
    }
}
