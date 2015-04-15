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
        static private Vector2 worldPosition;
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

        static public Vector2 WorldPostion
        {
            get { return worldPosition; }
            set { worldPosition = value; }
        }

        static public Vector2 WorldTilePostion
        {
            get
            {
                Map currentmap = new Map(0, 0, 0, 0, "error");
                if (StateManager.CurrentState is LocalMap)
                {
                    LocalMap localmap = StateManager.CurrentState as LocalMap;
                    currentmap = localmap.Map;
                }
                if (StateManager.CurrentState is WorldMap)
                {
                    WorldMap localmap = StateManager.CurrentState as WorldMap;
                    currentmap = localmap.Map;
                }
                return new Vector2((float)Math.Floor(WorldPostion.X / currentmap.TileWidth), (float)Math.Floor(WorldPostion.Y / currentmap.TileHeight));
            }
            set
            {
                Map currentmap = new Map(0, 0, 0, 0, "error");
                if (StateManager.CurrentState is LocalMap)
                {
                    LocalMap localmap = StateManager.CurrentState as LocalMap;
                    currentmap = localmap.Map;
                }
                if (StateManager.CurrentState is WorldMap)
                {
                    WorldMap localmap = StateManager.CurrentState as WorldMap;
                    currentmap = localmap.Map;
                }
                WorldPostion = new Vector2((float)Math.Floor(value.X / currentmap.TileWidth), (float)Math.Floor(value.Y / currentmap.TileHeight));
            }
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

        static public bool CheckForPlayers(Map map) {
            foreach (Player p in PartyList)
            {
                Console.Out.WriteLine("Player is located at " + p.Position.X + " " + p.Position.Y + "; Mouse clicked at " + StateManager.WorldMousePosition);
                if (new Rectangle((int)p.Position.X, (int)p.Position.Y, 32, 32).Contains(StateManager.WorldMousePosition))
                {
                    if (SelectedPlayer != null)
                    {
                        SelectedPlayer.Selected = false;
                    }
                    p.Selected = true;
                    SelectedPlayer = p;
                    return true;
                }
            }
            return false;
        }

        static public bool CheckForMainCharacter(Map map, Vector2 pos)
        {
            Player p = partyList[0];
            if (new Rectangle((int)p.Position.X, (int)p.Position.Y, 32, 32).Contains(StateManager.WorldMousePosition))
            {
                if (SelectedPlayer != null)
                {
                    SelectedPlayer.Selected = false;
                }
                p.Selected = true;
                SelectedPlayer = p;
                return true;
            }
            return false;
        }

        static public void UnselectPlayer()
        {
            if (SelectedPlayer != null)
            {
                SelectedPlayer.Selected = false;
                SelectedPlayer = null;
            }
        }

        static public void DrawPartyOnMap(SpriteBatch spriteBatch)
        {
            foreach (Player p in PartyList)
            {
                p.Draw(spriteBatch);
            }
        }

        static public void DrawMainCharacterOnMap(SpriteBatch spriteBatch)
        {
             partyList[0].Draw(spriteBatch);
        }

        static public bool Move(Vector2 newTile)
        {
            Map currentmap = new Map(0, 0, 0, 0, "error");
            if (StateManager.CurrentState is LocalMap)
            {
                LocalMap localmap = StateManager.CurrentState as LocalMap;
                currentmap = localmap.Map;
            }
            if (StateManager.CurrentState is WorldMap)
            {
                WorldMap localmap = StateManager.CurrentState as WorldMap;
                currentmap = localmap.Map;
            }
            Player p = partyList[0];
            if (p.Selected)
            {
                Vector2 startTile = p.TilePosition;
                double distance = Math.Sqrt(Math.Pow((startTile.X - newTile.X), 2) + Math.Pow((startTile.Y - newTile.Y), 2));
                if (distance == 1)
                {
                    if (!currentmap.TileCollide((int)newTile.X, (int)newTile.Y))
                    {
                        p.TilePosition = newTile;
                        WorldTilePostion = newTile;
                        return true;
                    }
                }
            }
            return false;
        }
		#endregion
	}

}
