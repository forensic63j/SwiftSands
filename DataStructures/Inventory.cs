//John Palermo

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftSands
{
    static class Inventory
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
        #endregion

        #region Methods/Constructor
        static Inventory()
        {
            items = new List<Item>();
        }
        static public void AddItem(Item item)
        {
            items.Add(item);
        }
        static public void RemoveItem(Item item)
        {
            items.Remove(item);
        }
        static public Item FindItem(String s)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Name == s)
                    return items[i];
            }
            return null;
        }
        static public void Clear()
        {
            items.Clear();
        }
        #endregion
    }
}
