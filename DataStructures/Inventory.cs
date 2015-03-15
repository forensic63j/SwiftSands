//John Palermo

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftSands
{
    class Inventory
    {
        #region Fields
        static private List<Item> items;
        #endregion

        #region Parameters
        static public List<Item> Items
        {
            get
            {
                return items;
            }
        }
        static public int Count
        {
            get
            {
                return items.Count;
            }
        }
        public Item this[int index]
        {
            get
            {
                if (index > 0 && index < items.Count)
                {
                    return items[index];
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region Methods/Constructor
        public Inventory()
        {
            items = new List<Item>();
        }
        public void AddItem(Item item)
        {
            items.Add(item);
        }
        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }
        public void Clear()
        {
            items.Clear();
        }
        #endregion
    }
}
