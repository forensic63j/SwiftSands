﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftSands
{
	class Party
	{
		#region feilds
		private List<Player> partyList;
		#endregion

		#region properties
		/// <summary>
		/// Gets the party list.
		/// </summary>
		public List<Player> PartyList
		{
            get { return partyList; }
		}

		/// <summary>
		/// Gets partymember at index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public Player this[int index]
		{
			get
			{
				if(index > 0 && index < partyList.Count)
				{
					return partyList[index];
				} else
				{
					return null;
				}
			}
		}

		/// <summary>
		/// Gets count
		/// </summary>
		public int Count
		{
			get { return partyList.Count; }  //Would be more effective using a list dirrectly.
		}
		#endregion

		#region methods
		public Party()
		{
			partyList = new List<Player>();
		}

        public void Add(Player partymember)
        {
			if(Count < 6)
			{
				partyList.Add(partymember);
			}
        }

        public bool Remove(Player partymember)
        {
            return partyList.Remove(partymember);
        }
		#endregion
	}

	//Note: you cannot use foreach with this class.
}
