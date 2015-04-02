//Brian Sandon

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwiftSands
{
	static class Party
	{
		#region fields
		static private List<Player> partyList;
        static private Player selectedPlayer;
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

        static public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set { selectedPlayer = value; }
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

        static public bool CheckForPlayers(Map map, Vector2 pos) {
            foreach (Player p in PartyList)
            {
                if (map.ConvertPosition(p.Position, StateManager.CurrentState.StateCamera).Contains(pos))
                {
                    SelectedPlayer = p;
                    return true;
                }
            }
            return false;
        }

        static public void DrawPartyOnMap(SpriteBatch spriteBatch)
        {
            foreach (Player p in PartyList)
            {
                p.Draw(spriteBatch);
            }
        }
		#endregion
	}

}
