using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftSands
{
	static class Party
	{
		#region fields
		static private List<Player> partyList;
		#endregion

        static Party()
        {
            partyList = new List<Player>();
        }

		#region properties
		/// <summary>
		/// Gets the party list.
		/// </summary>
		static public List<Player> PartyList
		{
            get { return partyList; }
		}

		/*Unused indexer
		 * /// <summary>
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
		}*/

		/// <summary>
		/// Gets count
		/// </summary>
		static public int Count
		{
			get { return partyList.Count; }  //Would be more effective using a list dirrectly.
		}
		#endregion

		#region methods
		/// <summary>
		/// Adds a player to the party.
		/// </summary>
		/// <param name="partymember">The player added.</param>
		/// <returns>Whether the player was successfuly added.</returns>
        static public bool Add(Player partymember)
        {
			if(Count < 6)
			{
				partyList.Add(partymember);
				return true;
			} else
			{
				return false;
			}
        }

		/// <summary>
		/// Removes a player from the party
		/// </summary>
		/// <param name="partymember">The player to be removed.</param>
		/// <returns>The removed player.</returns>
        static public bool Remove(Player partymember)
        {
            return partyList.Remove(partymember);
        }

		/// <summary>
		/// Clears party.
		/// </summary>
		static public void Clear()
		{
			partyList.Clear();
		}
		#endregion
	}

	//Note: you cannot use foreach with this class.
}
