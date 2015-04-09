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
		SpriteFont font;
		Button attack;
		Button endTurn;

		//RNG
		Random rng;

		int combatTime;
		#endregion

		#region main methods
		/// <summary>
		/// The main constructor for combat.
		/// </summary>
		public Combat(Game1 game, Viewport port,List<Enemy> enemies,SpriteFont font,Texture2D buttonSprite) : base(game, port) {
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

			this.font = font;
			attack = new Button("Attack",font,buttonSprite,new Rectangle(5,port.Height-75,100,30),true);
			endTurn = new Button("End Turn",font,buttonSprite,new Rectangle(5,port.Height - 35,100,30),true);

			attack.OnClick = Attack;
			endTurn.OnClick = EndTurn;

			rng = new Random();

			combatTime = 0;
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

		/// <summary>
		/// Updates state each turn.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime time)
        {
			Rectangle cPosition = base.Map.ConvertPosition(combatants[currentTurn].Position,StateCamera);
			bool[,] invalidTiles = new bool[Map.ColliderLayer.GetLength(0), Map.ColliderLayer.GetLength(1)];
			invalidTiles.Initialize();
			if(combatants[currentTurn] is Player)
			{
				#region player
				Player cPlayer = combatants[currentTurn] as Player;
				endTurn.IsActive = true;
				attack.IsActive = true;

				if(targeting)
				{
					cPlayer.Selected = false;
					attack.Clickable = false;
					ValidTargets(ref invalidTiles,cPosition.X + cPosition.Center.X,cPosition.Y + cPosition.Height,2);
					if(StateManager.MState.LeftButton == ButtonState.Pressed && StateManager.MPrevious.LeftButton == ButtonState.Released)
					{
						Point mousePoint = StateManager.MState.Position;
						Rectangle tileVector = this.Map.ConvertPosition(new Rectangle(mousePoint.X,mousePoint.Y,0,0),this.StateCamera);
						if(!invalidTiles[tileVector.X,tileVector.Y])
						{
							ItemType pItemType = cPlayer.EquipItem.Type;
							if(pItemType == ItemType.Gun && pItemType == ItemType.Melee)
							{
								cPlayer.Attack(cPlayer.EquipItem,TileOccupent(tileVector.X,tileVector.Y));
							} else if(pItemType == ItemType.HealingSpell && pItemType == ItemType.AttackSpell)
							{
								cPlayer.Cast(cPlayer.EquipItem,TileOccupent(tileVector.X,tileVector.Y));
							}
							actionLeft = false;
						}
					}
				} else
				{
					attack.Clickable = actionLeft;
                    combatants[currentTurn].ValidMovements(ref invalidTiles, this.Map, cPosition.X + cPosition.Center.X, cPosition.Y + cPosition.Height, combatants[currentTurn].MovementRange);
					if(StateManager.MState.LeftButton == ButtonState.Pressed && StateManager.MPrevious.LeftButton == ButtonState.Released)
					{
						Vector2 mousePoint = StateManager.WorldMousePosition;
						Vector2 tileVector = this.Map.ConvertPosition(new Vector2(mousePoint.X,mousePoint.Y),this.StateCamera);
                        Party.CheckForPlayers(this.Map, StateManager.WorldMousePosition);
                        if (moveLeft)
                        {
                            if (!invalidTiles[(int)tileVector.X, (int)tileVector.Y])
                            {
                                if (TileOccupent((int)StateManager.WorldMousePosition.X, (int)StateManager.WorldMousePosition.Y) == null)
                                {
                                    if (combatants[currentTurn].Move(StateManager.WorldMousePosition))
                                    {
                                        moveLeft = false;
                                    }
                                }
                            }
                        }
					}
				}
				#endregion
			} else
			{
				combatTime += time.TotalGameTime.Milliseconds;
				if(combatTime >= 100){
					combatTime = 0;
					attack.IsActive = false;
					endTurn.IsActive = false;
					Enemy cEnemy = combatants[currentTurn] as Enemy;
					if(actionLeft && moveLeft)
					{
						targeting = (rng.Next(0,3) == 0);
					}
					if(targeting)
					{
						if(actionLeft)
						{
							ValidTargets(ref invalidTiles,cPosition.X + cPosition.Center.X,cPosition.Y + cPosition.Height,2);

							int x = 0;
							int y = 0;
							do
							{
								x = rng.Next(0,invalidTiles.GetLength(0));
								y = rng.Next(0,invalidTiles.GetLength(1));
							} while(invalidTiles[x,y]);

							Character target = TileOccupent(x,y);
							Item enemyItem = cEnemy.EquipItem;
							if(enemyItem.Type == ItemType.HealingSpell)
							{
								if(target is Enemy)
								{
									cEnemy.Cast(enemyItem,target);
								}
							} else
							{
								if(target is Player)
								{
									if(enemyItem.Type == ItemType.AttackSpell)
									{
										cEnemy.Cast(enemyItem,target);
									} else
									{
										cEnemy.Attack(enemyItem,target);
									}
								}
							}
							actionLeft = false;
							targeting = false;
						} else
						{
							targeting = false;
						}
						
					} else
					{
						if(moveLeft)
						{
                            combatants[currentTurn].ValidMovements(ref invalidTiles, Map, cPosition.X + cPosition.Center.X, cPosition.Y + cPosition.Height, 5);
							int x = 0;
							int y = 0;
							do
							{
								x = rng.Next(0,invalidTiles.GetLength(0));
								y = rng.Next(0,invalidTiles.GetLength(1));
							} while(invalidTiles[x,y]);
							Vector2 moveVector = new Vector2(x,y);
							combatants[currentTurn].Move(moveVector);
							moveLeft = false;
							targeting = true;
						}
					}
				}
			}

			attack.Update();
			endTurn.Update();

			int i = 0;
			bool casualty = false;
			while(i < combatants.Count)
			{
				if(!combatants[i].Alive)
				{
					casualty = true;
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
			if(casualty && (!CombatentsInclude<Player>() || !CombatentsInclude<Enemy>())){
				StateManager.CloseState();
			}

			if(!(moveLeft || actionLeft))
			{
				if(combatants[currentTurn] is Player)
				{
					combatants[currentTurn].Selected = false;
				}
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

		/// <summary>
		/// Draws the current state.
		/// </summary>
		/// <param name="time">Keeps track of timing details.</param>
		/// <param name="spriteBatch">Used to draw sprites to the screen.</param>
        public override void DrawWorld(GameTime time, SpriteBatch spriteBatch)
        {
			base.DrawWorld(time, spriteBatch);

			String cName;
			String cHealth;
			foreach(Character c in combatants)
			{
				if(c.Alive)
				{
					//Console.WriteLine(c.Name);
					c.Draw(spriteBatch);
					cName = "Name: " + c.Name;
					spriteBatch.DrawString(font,cName,new Vector2(c.Position.X,c.Position.Y+c.Position.Height + 2),Color.Black);
					cHealth = "Health: " + c.Health;
					spriteBatch.DrawString(font,cHealth,new Vector2(c.Position.X,c.Position.Y + c.Position.Height + 16),Color.Black);
				}
			}
        }

		/// <summary>
		/// Does something?
		/// </summary>
		/// <param name="time"></param>
		/// <param name="spriteBatch"></param>
        public override void DrawScreen(GameTime time, SpriteBatch spriteBatch)
        {
            if (combatants[currentTurn] is Player)
            {
                attack.Draw(spriteBatch);
                endTurn.Draw(spriteBatch);
            }
            //spriteBatch.Draw(buttonSprite, new Rectangle((int)mouse.X, (int)mouse.Y, 5, 5), Color.Black);
            base.DrawScreen(time, spriteBatch);
		}

		/// <summary>
		/// Determines if there are enemies or players in battle.
		/// </summary>
		/// <typeparam name="T">The type of character included.</typeparam>
		/// <returns></returns>
		public bool CombatentsInclude<T>()
		{
			foreach(Character c in combatants)
			{
				if(c is T)
				{
					return true;
				}
			}
			return false;
		}
		#endregion

		#region button methods
		/// <summary>
		/// Allows the player to attack another character.
		/// </summary>
		public void Attack()
		{
			if(targeting)
			{
				targeting = false;
			} else
			{
				targeting = true;
			}
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
		/// <param name="invalidTiles">A map of the tiles on the screen, you can move to tiles with a value of "false".</param>
		/// <param name="x">Current x.</param>
		/// <param name="y">Current y.</param>
		/// <param name="range">The range from this point the player can shoot.</param>
		public void ValidTargets(ref bool[,] invalidTiles,int x,int y,int range)
		{
			if(base.Map.InBounds(x,y))
			{
				if(TileOccupent(x,y) != null)
				{
					invalidTiles[x,y] = false;
				}

				////checks adjacent
				if(range > 1)
				{
					//top
					if(base.Map.InBounds(x - 1,y - 1) && invalidTiles[x - 1,y - 1])
					{
						ValidTargets(ref invalidTiles,x - 1,y - 1,range - 1);
					}
					if(base.Map.InBounds(x,y - 1) && !invalidTiles[x,y - 1])
					{
						ValidTargets(ref invalidTiles,x,y - 1,range - 1);
					}
					if(base.Map.InBounds(x + 1,y - 1) && !invalidTiles[x + 1,y - 1])
					{
						ValidTargets(ref invalidTiles,x + 1,y - 1,range - 1);
					}

					//middle
					if(base.Map.InBounds(x - 1,y) && !invalidTiles[x - 1,y])
					{
						ValidTargets(ref invalidTiles,x - 1,y,range - 1);
					}
					if(base.Map.InBounds(x + 1,y) && !invalidTiles[x + 1,y])
					{
						ValidTargets(ref invalidTiles,x + 1,y,range - 1);
					}

					//bottom
					if(base.Map.InBounds(x - 1,y + 1) && !invalidTiles[x - 1,y + 1])
					{
						ValidTargets(ref invalidTiles,x - 1,y + 1,range - 1);
					}
					if(base.Map.InBounds(x,y + 1) && !invalidTiles[x,y + 1])
					{
						ValidTargets(ref invalidTiles,x,y + 1,range - 1);
					}
					if(base.Map.InBounds(x + 1,y + 1) && !invalidTiles[x + 1,y + 1])
					{
						ValidTargets(ref invalidTiles,x + 1,y + 1,range - 1);
					}
				}
			}
		}

		/// <summary>
		/// Finds the character at a certian tile.
		/// </summary>
		/// <param name="x">The tiles x.</param>
		/// <param name="y">The tiles y.</param>
		/// <returns>The character on that tile.</returns>
		public Character TileOccupent(int x,int y)
		{
			foreach(Character c in combatants){
				Rectangle cPosition = this.Map.ConvertPosition(c.Position,this.StateCamera);
				if(cPosition.X == x && cPosition.Y == y)
				{
					return c;
				}
			}
			return null;
		}
		#endregion
	}
}
