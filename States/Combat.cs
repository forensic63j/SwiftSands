//Brian Sandon and Clayton Scavone

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace SwiftSands
{
  
    class Combat : LocalMap
	{
		#region fields
		//Turns
		int currentTurn;
		List<Character> combatants;
		bool moveLeft;
		bool actionLeft;
		bool targeting;

		//GUI
		Button attack;
		Button endTurn;
		#endregion

		#region main methods
		public Combat(Game1 game, Viewport port,List<Enemy> enemies,SpriteFont font) : base(game, port) {
			currentTurn = 0;
			moveLeft = true;
			actionLeft = true;
			targeting = false;

			combatants = new List<Character>();

			List<Player> players = Party.PartyList;
			foreach(Player player in players)
			{ //Not sure if this is a good way to do this.
				if(combatants.Count == 0)
				{
					combatants.Add(player);
				} else
				{
					int i = 0;
					while(i < combatants.Count && combatants[i].Speed > player.Speed)
					{
						i++;
					}
					if(i == combatants.Count)
					{
						combatants.Add(player);
					} else
					{
						combatants.Insert(i,player);
					}
				}
			}

			foreach(Enemy enemy in enemies)
			{ //Not sure if this is a good way to do this.
				if(combatants.Count == 0)
				{
					combatants.Add(enemy);
				} else
				{
					int i = 0;
					while(i < combatants.Count && combatants[i].Speed > enemy.Speed)
					{
						i++;
					}
					if(i == combatants.Count)
					{
						combatants.Add(enemy);
					} else
					{
						combatants.Insert(i,enemy);
					}
				}
			}

			attack = new Button("Attack",font,base.ButtonSprite,new Rectangle(5,port.Height-75,100,30),false,true);
			endTurn = new Button("End Turn",font,base.ButtonSprite,new Rectangle(5,port.Height - 35,100,30),false,true);

			attack.OnClick = Attack;
			endTurn.OnClick = EndTurn;
		}

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnDestroy()
        {
            base.OnExit();
        }

        public override void Update(GameTime time)
        {
			Rectangle cPosition = base.Map.ConvertPosition(combatants[currentTurn].Position,StateCamera);
			if(combatants[currentTurn] is Player)
			{
				Player cPlayer = combatants[currentTurn] as Player;
				endTurn.IsActive = true;
				bool[,] validTiles = new bool[Map.ColliderLayer.GetLength(0),Map.ColliderLayer.GetLength(1)];
				validTiles.Initialize();

				if(targeting)
				{
					attack.IsActive = false;
				} else
				{
					attack.IsActive = actionLeft;
					ValidMovements(ref validTiles,cPosition.X + cPosition.Center.X,cPosition.Y + cPosition.Height,5);
				}
			} else
			{
				attack.IsActive = false;
				endTurn.IsActive = false;
				Enemy cEnemy = combatants[currentTurn] as Enemy;
			}

			attack.Update();
			endTurn.Update();

			int i = 0; 
			while(i < combatants.Count)
			{
				if(!combatants[i].Alive)
				{
					if(combatants[i] is Player)
					{
						Player cPlayer = combatants[currentTurn] as Player;
						cPlayer.ReturnToDoctor();
					} else
					{
						Enemy cEnemy = combatants[currentTurn] as Enemy;
						(combatants[currentTurn] as Player).Exp += cEnemy.ExpAwarded;
					}

					combatants.RemoveAt(i);
				} else
				{
					i++;
				}
			}

			if(!(moveLeft || actionLeft))
			{
				if(currentTurn < combatants.Count - 1)
				{
					currentTurn++;
				} else
				{
					currentTurn = 0;
				}
				moveLeft = true;
				actionLeft = true;
			}
            base.Update(time);
        }

        public override void DrawWorld(GameTime time, SpriteBatch spriteBatch)
        {
            base.DrawWorld(time, spriteBatch);
			if(combatants[currentTurn] is Player)
			{
				attack.Draw(spriteBatch);
				endTurn.Draw(spriteBatch);
			}
        }

        public override void DrawScreen(GameTime time, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(buttonSprite, new Rectangle((int)mouse.X, (int)mouse.Y, 5, 5), Color.Black);
            base.DrawScreen(time, spriteBatch);
		}
		#endregion

		#region button methods
		/// <summary>
		/// Allows the player to attack another character.
		/// </summary>
		public void Attack()
		{
			targeting = true;
		}

		/// <summary>
		/// Ends the current player's turn.
		/// </summary>
		public void EndTurn()
		{
			actionLeft = false;
			moveLeft = false;
		}
		#endregion

		#region movement/targeting
		/// <summary>
		/// Finds the valid movements for the current character
		/// </summary>
		/// <param name="validTiles">A map of the tiles on the screen, you can move to tiles with a value of "false".</param>
		/// <param name="x">Current x.</param>
		/// <param name="y">Current y.</param>
		/// <param name="move">Moves left.</param>
		public void ValidMovements(ref bool[,] validTiles, int x, int y, int move) 
		{
			if(base.Map.InBounds(x,y) && base.Map.ColliderLayer[x,y] < 0)
			{
				validTiles[x,y] = false;
				//checks adjacent
				if(move > 1)
				{
					//top
					if(base.Map.InBounds(x - 1,y - 1) && validTiles[x - 1,y - 1])
					{
						ValidMovements(ref validTiles,x - 1,y - 1,move - 1);
					}
					if(base.Map.InBounds(x,y - 1) && !validTiles[x,y - 1])
					{
						ValidMovements(ref validTiles,x,y - 1,move - 1);
					}
					if(base.Map.InBounds(x + 1,y - 1) && !validTiles[x + 1,y - 1])
					{
						ValidMovements(ref validTiles,x + 1,y - 1,move - 1);
					}

					//middle
					if(base.Map.InBounds(x - 1,y) && !validTiles[x - 1,y])
					{
						ValidMovements(ref validTiles,x - 1,y,move - 1);
					}
					if(base.Map.InBounds(x + 1,y) && !validTiles[x + 1,y])
					{
						ValidMovements(ref validTiles,x + 1,y,move - 1);
					}

					//bottom
					if(base.Map.InBounds(x - 1,y + 1) && !validTiles[x - 1,y + 1])
					{
						ValidMovements(ref validTiles,x - 1,y + 1,move - 1);
					}
					if(base.Map.InBounds(x,y + 1) && !validTiles[x,y + 1])
					{
						ValidMovements(ref validTiles,x,y + 1,move - 1);
					}
					if(base.Map.InBounds(x + 1,y + 1) && !validTiles[x + 1,y + 1])
					{
						ValidMovements(ref validTiles,x + 1,y + 1,move - 1);
					}
				}
			}
		}

		/// <summary>
		/// Finds the valid movements for the current character
		/// </summary>
		/// <param name="validTiles">A map of the tiles on the screen, you can move to tiles with a value of "false".</param>
		/// <param name="x">Current x.</param>
		/// <param name="y">Current y.</param>
		/// <param name="range">The range from this point the player can shoot.</param>
		public void ValidTargets(ref bool[,] validTiles,int x,int y,int range)
		{
			if(base.Map.InBounds(x,y))
			{
				if(false) //need some way to tell if a character is on a tile
				{
					validTiles[x,y] = false;
				}

				////checks adjacent
				if(range > 1)
				{
					//top
					if(base.Map.InBounds(x - 1,y - 1) && validTiles[x - 1,y - 1])
					{
						ValidTargets(ref validTiles,x - 1,y - 1,range - 1);
					}
					if(base.Map.InBounds(x,y - 1) && !validTiles[x,y - 1])
					{
						ValidTargets(ref validTiles,x,y - 1,range - 1);
					}
					if(base.Map.InBounds(x + 1,y - 1) && !validTiles[x + 1,y - 1])
					{
						ValidTargets(ref validTiles,x + 1,y - 1,range - 1);
					}

					//middle
					if(base.Map.InBounds(x - 1,y) && !validTiles[x - 1,y])
					{
						ValidTargets(ref validTiles,x - 1,y,range - 1);
					}
					if(base.Map.InBounds(x + 1,y) && !validTiles[x + 1,y])
					{
						ValidTargets(ref validTiles,x + 1,y,range - 1);
					}

					//bottom
					if(base.Map.InBounds(x - 1,y + 1) && !validTiles[x - 1,y + 1])
					{
						ValidTargets(ref validTiles,x - 1,y + 1,range - 1);
					}
					if(base.Map.InBounds(x,y + 1) && !validTiles[x,y + 1])
					{
						ValidTargets(ref validTiles,x,y + 1,range - 1);
					}
					if(base.Map.InBounds(x + 1,y + 1) && !validTiles[x + 1,y + 1])
					{
						ValidTargets(ref validTiles,x + 1,y + 1,range - 1);
					}
				}
			}
		}
		#endregion
	}
}
