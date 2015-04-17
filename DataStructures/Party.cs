//Brian Sandon and Clayton Scavone

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
        static private Vector2 worldTilePosition;
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

        static public Player MainCharacter
        {
            get { return partyList[0]; }
            //set { selectedPlayer = value; }
        }

        static public Vector2 WorldPosition
        {
            get { return worldPosition; }
            set { worldPosition = value; }
        }

        static public Vector2 WorldTilePosition
        {
            get
            {
                return worldTilePosition;
            }
            set
            {       
                worldTilePosition = value;
                worldPosition = new Vector2(worldTilePosition.X * 32, worldTilePosition.Y * 32);
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

        static public Character CheckForPlayers()
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
            foreach (Player p in PartyList)
            {
                Console.Out.WriteLine("Player is located at " + p.Position.X + " " + p.Position.Y + "; Mouse clicked at " + StateManager.WorldMousePosition);
                if (new Rectangle((int)p.Position.X, (int)p.Position.Y, 32, 32).Contains(StateManager.WorldMousePosition))
                {
                    return p;
                }
            }
            return null;
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
            Console.Out.WriteLine(WorldPosition);
            if (new Rectangle((int)WorldPosition.X, (int)WorldPosition.Y, 32, 32).Contains(StateManager.WorldMousePosition))
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
            p.TilePosition = WorldTilePosition;
            if (p.Selected)
            {
                Vector2 startTile = WorldTilePosition;
                double distance = Math.Sqrt(Math.Pow((startTile.X - newTile.X), 2) + Math.Pow((startTile.Y - newTile.Y), 2));
                if (distance == 1)
                {
                    if (!currentmap.TileCollide((int)newTile.X, (int)newTile.Y))
                    {
                        Console.Out.WriteLine(newTile);
                        p.TilePosition = newTile;
                        WorldTilePosition = newTile;
                        Console.Out.WriteLine(WorldTilePosition);
                        Console.Out.WriteLine(WorldPosition);
                        return true;
                    }
                }
            }
            return false;
        }
		#endregion
	}

}
