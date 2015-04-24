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
		int movesLeft;
		bool actionLeft;
		bool targeting;
        bool[,] validTiles;
        bool[,] targetingRange;
        List<Enemy> enemyList;

		//GUI
		SpriteFont font;
        Texture2D buttonSprite;
		Button attack;
		Button endTurn;

		//RNG
		Random rng;

		int combatTime;
		#endregion

        public List<Enemy> EnemyList{
            get{return enemyList;}
            set{enemyList =value;}
        }

		#region main methods
		/// <summary>
		/// The main constructor for combat.
		/// </summary>
		public Combat(Game1 game, Viewport port,List<Enemy> enemies) : base(game, port) {
			currentTurn = 0;
			actionLeft = true;
			targeting = false;
            EnemyList = enemies;
			combatants = new List<Character>();
			ResetCombatents(enemies);

			this.font = base.StateGame.Font;
            this.buttonSprite = base.StateGame.ButtonSprite;
			attack = new Button("Attack",font,buttonSprite,new Rectangle(5,port.Height-75,100,30),true);
			endTurn = new Button("End Turn",font,buttonSprite,new Rectangle(5,port.Height - 35,100,30),true);

			attack.OnClick = Attack;
			endTurn.OnClick = EndTurn;

			rng = new Random();
            movesLeft = combatants[currentTurn].MovementRange;
            combatTime = 0;

		}

        public override void OnEnter()
        {
            if ((!CombatentsInclude<Player>() || !CombatentsInclude<Enemy>()))
            {
                StateManager.CloseState();
            }
            base.OnEnter();
            for (int i = 0; i < Party.PartyList.Count; i++)
            {
                Party.PartyList[i].TilePosition = new Vector2(rng.Next(0, base.Map.Width / 3), rng.Next(0, base.Map.Height / 3));
                while (base.Map.TileCollide(Party.PartyList[i].TilePosition))
                {
                    Party.PartyList[i].TilePosition = new Vector2(rng.Next(0, base.Map.Width / 3), rng.Next(0, base.Map.Height / 3));
                }
            }
            SelectedCharacter = combatants[currentTurn];
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnDestroy()
        {
            base.OnExit();
        }

        public Character CheckForEnemies()
        {
            foreach (Character c in EnemyList)
            {
                //Console.Out.WriteLine("Player is located at " + p.Position.X + " " + p.Position.Y + "; Mouse clicked at " + StateManager.WorldMousePosition);
                if (new Rectangle((int)c.Position.X, (int)c.Position.Y, 32, 32).Contains(StateManager.WorldMousePosition))
                {
                    return c;
                }
            }
            return null;
        }

		/// <summary>
		/// Updates state each turn.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime time)
        {
			Rectangle cWorldPosition = combatants[currentTurn].Position;
			Rectangle cLocalPosition = base.Map.ConvertPosition(combatants[currentTurn].Position,StateCamera);
			Vector2 pVector = base.Map.ConvertPosition(Vector2.Transform(new Vector2(cWorldPosition.X,cWorldPosition.Y),StateCamera.Transform),StateCamera);

            validTiles = new bool[Map.ColliderLayer.GetLength(0), Map.ColliderLayer.GetLength(1)];
            targetingRange = new bool[Map.ColliderLayer.GetLength(0), Map.ColliderLayer.GetLength(1)];
            
            validTiles.Initialize();
            targetingRange.Initialize();

			if(combatants[currentTurn] is Player)
			{
				#region player
				Player cPlayer = combatants[currentTurn] as Player;
				endTurn.IsActive = true;
				attack.IsActive = true;

				if(targeting)
				{
					//cPlayer.Selected = false;
                    Map.RemoveTints();
					attack.Clickable = true;
					ValidTargets(ref validTiles, ref targetingRange, (int)pVector.X,(int)pVector.Y, cPlayer.EquipItem.Range);
                    /* OBSOLETE (i think)
                    for (int j = 0; j < validTiles.GetLength(0); j++)
                    {
                        for (int k = 0; k < validTiles.GetLength(0); k++)
                        {

                            Vector2 tintVector = /*Vector2.Transform(new Vector2(j, k)/*,StateCamera.Transform);
                            if (validTiles[j, k])
                            {
                                base.Map.TintTile(tintVector, Color.LightGreen);
                            }
                            else
                            {
                                base.Map.TintTile(tintVector, Color.White);
                            }
                        }
                    }
                    */
					if(StateManager.MState.LeftButton == ButtonState.Pressed && StateManager.MPrevious.LeftButton == ButtonState.Released)
					{
                        Vector2 tileVector = StateManager.TileMousePosition;
                        if (validTiles[(int)tileVector.X, (int)tileVector.Y])
						{
							ItemType pItemType = cPlayer.EquipItem.Type;
							if(pItemType == ItemType.Gun ||pItemType == ItemType.Melee)
							{
                                cPlayer.Attack(cPlayer.EquipItem, Character.TileOccupent(combatants,(int)tileVector.X, (int)tileVector.Y));
                                Console.WriteLine(cPlayer.Name + " attacked " + Character.TileOccupent(combatants, (int)tileVector.X, (int)tileVector.Y).Name);
							} else if(pItemType == ItemType.HealingSpell || pItemType == ItemType.AttackSpell)
							{
                                cPlayer.Cast(cPlayer.EquipItem, Character.TileOccupent(combatants, (int)tileVector.X, (int)tileVector.Y));
                                Console.WriteLine(cPlayer.Name + " healed " + Character.TileOccupent(combatants, (int)tileVector.X, (int)tileVector.Y).Name);
							}
							actionLeft = false;
                            StopAttack();
                            Map.RemoveTints();            
						}
					}
                    if (StateManager.MState.RightButton == ButtonState.Pressed && StateManager.MPrevious.RightButton == ButtonState.Released)
                    {
                        StopAttack();
                    }
				} 
                else //If players turn and not targeting 
				{
					attack.Clickable = actionLeft;
					combatants[currentTurn].ValidMovements(ref validTiles, combatants,(int)pVector.X,(int)pVector.Y, movesLeft);
					if(StateManager.MState.LeftButton == ButtonState.Pressed && StateManager.MPrevious.LeftButton == ButtonState.Released)
					{
						Vector2 tileVector = StateManager.TileMousePosition;
                        if (SelectedCharacter == null || Party.CheckForPlayers() != null)
                        {
                            Map.RemoveTints();
                            SelectedCharacter = Party.CheckForPlayers();
                        }
                        if (CheckForEnemies() != null)
                        {
                            Map.RemoveTints();
                            SelectedCharacter = CheckForEnemies();
                        }
                        //Console.Out.WriteLine("Moves Left: " + movesLeft + " Tile Validity: " + validTiles[(int)tileVector.X, (int)tileVector.Y] + " Tile Occupied: " + TileOccupent((int)tileVector.X, (int)tileVector.Y));
                        if (base.Map.InBounds((int)tileVector.X, (int)tileVector.Y))
                        {
                            if (movesLeft > 1)
                            {
                                if (validTiles[(int)tileVector.X, (int)tileVector.Y])
                                {
                                    if (Character.TileOccupent(combatants, (int)tileVector.X, (int)tileVector.Y) == null)
                                    {
                                        int distanceMoved = combatants[currentTurn].Move(StateManager.TileMousePosition);
                                        if (distanceMoved > 0)
                                        {
                                            movesLeft = movesLeft - distanceMoved;
                                        }
                                    }
                                }
                            }
                        }
					}
				}
				#endregion
                if (combatants[currentTurn] == SelectedCharacter)
                {
                    if (targeting)
                    {
                        for (int j = 0; j < validTiles.GetLength(0); j++)
                        {
                            for (int k = 0; k < validTiles.GetLength(0); k++)
                            {

                                Vector2 tintVector = /*Vector2.Transform(*/new Vector2(j, k)/*,StateCamera.Transform)*/;
                                if (targetingRange[j, k])
                                {
                                    base.Map.TintTile(tintVector, Color.LightBlue);
                                }
                                else
                                {
                                    base.Map.TintTile(tintVector, Color.White);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < validTiles.GetLength(0); j++)
                        {
                            for (int k = 0; k < validTiles.GetLength(0); k++)
                            {

                                Vector2 tintVector = /*Vector2.Transform(*/new Vector2(j, k)/*,StateCamera.Transform)*/;

                                if (validTiles[j, k])
                                {
                                    base.Map.TintTile(tintVector, Color.LightGreen);
                                }
                                else
                                {
                                    base.Map.TintTile(tintVector, Color.White);
                                }
                            }
                        }
                    }
                }
			} 
            else //Enemies turn
            {
                #region enemy
                combatTime += time.TotalGameTime.Milliseconds;
				if(combatTime >= 1){
					combatTime = 0;
					attack.IsActive = false;
					endTurn.IsActive = false;
					Enemy cEnemy = combatants[currentTurn] as Enemy;
					if(actionLeft && (movesLeft > 1))
					{
						targeting = (rng.Next(0,3) == 0);
					}
					if(targeting)
					{
						if(actionLeft)
						{
                            ValidTargets(ref validTiles, ref targetingRange, (int)pVector.X,(int)pVector.Y, 2);

                            Item enemyItem = cEnemy.EquipItem;

                            bool targetsAllies = enemyItem.Type == ItemType.HealingSpell;
                            if(NoValidTargets(validTiles,targetsAllies)){
                                actionLeft = false;
                                StopAttack();
                            }else{
                               
							int x = 0;
							int y = 0;
							do
							{
								x = rng.Next(0,validTiles.GetLength(0));
								y = rng.Next(0,validTiles.GetLength(1));
							} while(!validTiles[x,y]);

                            Character target = Character.TileOccupent(combatants, x, y);
							
							if(targetsAllies)
							{
								if(target is Enemy)
								{
									cEnemy.Cast(enemyItem,target);
								}
                                actionLeft = false;
                                StopAttack();
							} 
                            else
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
                                    actionLeft = false;
                                    StopAttack();
								}
							}
                            }
						} 
                        else
						{
                            StopAttack();
						}
						
					} 
                    else  // If not targeting
					{
						if(movesLeft > 0)
						{
                            combatants[currentTurn].ValidMovements(ref validTiles, combatants, (int)pVector.X,(int)pVector.Y, movesLeft);
                                                        //Screw it implement A* later
                            
							int x = 0;
							int y = 0;
							do
							{
								x = rng.Next(0,validTiles.GetLength(0));
								y = rng.Next(0,validTiles.GetLength(1));
							} while(!validTiles[x,y] && Map.InBounds(x,y) && !Map.TileCollide(x,y));
							Vector2 moveVector = new Vector2(x,y);
							int distanceMoved = combatants[currentTurn].Move(moveVector);
                            movesLeft -= distanceMoved;
                           
                           // movesLeft = 0;
							//targeting = true;
						}
                        else
                        {
                            targeting = true;
                        }
					}
                    if (!((movesLeft > 0) || actionLeft))
                    {
                        EndTurn();
                    }
				}
                /*
                    movesLeft = false;
                    actionLeft = false; */
                for (int j = 0; j < validTiles.GetLength(0); j++)
                {
                    for (int k = 0; k < validTiles.GetLength(0); k++)
                    {

                        Vector2 tintVector = /*Vector2.Transform(*/new Vector2(j, k)/*,StateCamera.Transform)*/;

                        if (validTiles[j, k])
                        {
                            base.Map.TintTile(tintVector, Color.LightGreen);
                        }
                        else if (targetingRange[j, k])
                        {
                            base.Map.TintTile(tintVector, Color.LightBlue);
                        }
                    }
                }
                #endregion
            }
			
			attack.Update();
			endTurn.Update();
			if(combatants[currentTurn] is Player)
			{
				if(StateManager.KState.IsKeyDown(Keys.Space) && StateManager.KPrevious.IsKeyUp(Keys.Space))
				{
					Attack();
				}
				if(StateManager.KState.IsKeyDown(Keys.Enter) && StateManager.KPrevious.IsKeyUp(Keys.Enter))
				{
					EndTurn();
				}
			}

			int i = 0;
			bool casualty = false;
			while(i < combatants.Count)
			{
				if(!combatants[i].Alive) 
				{
					casualty = true;
					if(combatants[i] is Player)
					{
						Player cPlayer = combatants[i] as Player;
						cPlayer.ReturnToDoctor();
					} else
					{
						Enemy cEnemy = combatants[i] as Enemy;
						(combatants[currentTurn] as Player).Exp += cEnemy.ExpAwarded;
					}

					combatants.RemoveAt(i);
                    if (currentTurn > i)
                    {
                        currentTurn--;
                    }
				} else
				{
					i++;
				}
			}

			if(casualty && (!CombatentsInclude<Player>() || !CombatentsInclude<Enemy>())){
                Party.MainCharacter.TilePosition = Party.WorldTilePosition;
				StateManager.CloseState();
                return;
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
					cName = c.Name;
					spriteBatch.DrawString(font,cName,new Vector2(c.Position.X,c.Position.Y+c.Position.Height + 2),Color.Black);
                    if (c.Selected == true)
                    {
                        cHealth = c.Health + "/" + c.MaxHealth;
                        spriteBatch.DrawString(font, cHealth, new Vector2(c.Position.X, c.Position.Y + c.Position.Height + 16), Color.Black);
                        if (combatants[currentTurn] == SelectedCharacter)
                        {
                            spriteBatch.DrawString(font, movesLeft + "", new Vector2(c.Position.X, c.Position.Y + c.Position.Height + 30), Color.Black);
                        }
                        else
                        {
                            spriteBatch.DrawString(font, 0 + "", new Vector2(c.Position.X, c.Position.Y + c.Position.Height + 30), Color.Black);
                        }
                    }
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
			Viewport port = this.StateGame.GraphicsDevice.Viewport;

            String turnDetails = "Current turn: " + combatants[currentTurn].Name;
            spriteBatch.DrawString(font, turnDetails, new Vector2(5, 5), Color.Black);
			if(this.SelectedCharacter != null)
			{
				Character cSelected = this.SelectedCharacter;
				String info = "Name: " + cSelected.Name + "   Health: " + cSelected.Health + "\\" + cSelected.MaxHealth + "   Mana: " + cSelected.Mana;
				TextBox textbox = new TextBox(StateGame.ButtonSprite,new Rectangle(128,port.Height - 74,400,70),true,info,font);
				textbox.Draw(spriteBatch);
				/*spriteBatch.Draw(StateGame.ButtonSprite,new Rectangle(128,port.Height - 74,400,70),Color.White);
				spriteBatch.DrawString(font,info,new Vector2(140,port.Height-70),Color.Black);
				info = "Item: (none)    Item type: N/A";
				if(cSelected.EquipItem != null)
				{
					info = "Item: " + cSelected.EquipItem.Name + "    Item type: " + cSelected.EquipItem.Type;
				}
				spriteBatch.DrawString(font,info,new Vector2(140,port.Height - 52),Color.Black);
				info = "Spd: " + cSelected.Speed + "   Str: " + cSelected.Strength + "   Acc: " + cSelected.Accuracy + "   Move: " + cSelected.MovementRange;
				spriteBatch.DrawString(font,info,new Vector2(140,port.Height - 32),Color.Black);*/
			}
 

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
            attack.Name = "Attacking";
			if(targeting)
			{
				targeting = false;
                Map.RemoveTints();
			} else
			{
				targeting = true;
			}
		}

        public void StopAttack()
        {
            attack.Name = "Attack";
            targeting = false;
            Map.RemoveTints();
        }

		/// <summary>
		/// Ends the current player's turn.
		/// </summary>
		public void EndTurn()
		{
            SelectedCharacter = null;
            Map.RemoveTints();
            validTiles = new bool[Map.ColliderLayer.GetLength(0), Map.ColliderLayer.GetLength(1)];
            movesLeft = 0;
			actionLeft = false;
            if (currentTurn < combatants.Count - 1)
            {
                currentTurn++;
            }
            else
            {
                currentTurn = 0;
            }
            actionLeft = true;
            SelectedCharacter = combatants[currentTurn];
            movesLeft = combatants[currentTurn].MovementRange;
            //this.StateCamera.Position = new Vector2(-combatants[currentTurn].Position.X, -combatants[currentTurn].Position.Y);
		}
		#endregion

		#region movement/targeting

		/// <summary>
		/// Finds the valid movements for the current character
		/// </summary>
		/// <param name="validTiles">A map of the tiles on the screen, you can attack enemies on tiles with a value of "true".</param>
		/// <param name="x">Current x.</param>
		/// <param name="y">Current y.</param>
		/// <param name="range">The range from this point the player can shoot.</param>
		public void ValidTargets(ref bool[,] validTiles, ref bool[,] targetingRange,int x,int y,int range)
		{
			//Console.WriteLine("Coordinate: (" + x + "," + y + ")");
			if(base.Map.InBounds(x,y))
			{
                targetingRange[x, y] = true;
                if (Character.TileOccupent(combatants, x, y) != null)
				{
					validTiles[x,y] = true;
				}

				////checks adjacent
				if(range > 1)
				{
					//top
					if(base.Map.InBounds(x - 1,y - 1)/* && !validTiles[x - 1,y - 1]*/)
					{
                        //ValidTargets(ref validTiles, ref targetingRange,x - 1,y - 1,range - 1);
					}
					if(base.Map.InBounds(x,y - 1)/* && !validTiles[x,y - 1]*/)
					{
                        ValidTargets(ref validTiles, ref targetingRange, x, y - 1, range - 1);
					}
					if(base.Map.InBounds(x + 1,y - 1)/* && !validTiles[x + 1,y - 1]*/)
					{
                        //ValidTargets(ref validTiles, ref targetingRange,x + 1,y - 1,range - 1);
					}

					//middle
					if(base.Map.InBounds(x - 1,y)/* && !validTiles[x - 1,y]*/)
					{
                        ValidTargets(ref validTiles, ref targetingRange, x - 1, y, range - 1);
					}
					if(base.Map.InBounds(x + 1,y)/* && !validTiles[x + 1,y]*/)
					{
                        ValidTargets(ref validTiles, ref targetingRange, x + 1, y, range - 1);
					}

					//bottom
					if(base.Map.InBounds(x - 1,y + 1)/* && !validTiles[x - 1,y + 1]*/)
					{
                        //ValidTargets(ref validTiles, ref targetingRange, x - 1, y + 1, range - 1);
					}
					if(base.Map.InBounds(x,y + 1)/* && !validTiles[x,y + 1]*/)
					{
                        ValidTargets(ref validTiles, ref targetingRange, x, y + 1, range - 1);
					}
					if(base.Map.InBounds(x + 1,y + 1)/* && !validTiles[x + 1,y + 1]*/)
					{
                        //ValidTargets(ref validTiles, ref targetingRange,x + 1,y + 1,range - 1);
					}

				}
			}
		}

		


        public bool NoValidTargets(bool[,] validTiles, bool targetsAllies)
        {
            for (int i = 0; i < validTiles.GetLength(0); i++)
            {
                for (int j = 0; j < validTiles.GetLength(1); j++)
                {
                    if (validTiles[i, j] && ((Character.TileOccupent(combatants, i, j) is Player && !targetsAllies) || (Character.TileOccupent(combatants, i, j) is Enemy && targetsAllies)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
		#endregion

		/// <summary>
		/// Resets combat.
		/// </summary>
		/// <param name="enemies">Enemies that the player fights.</param>
		public void ResetCombatents(List<Enemy> enemies)
		{
			combatants.Clear();

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
				enemy.Alive = true;
				enemy.Health = enemy.MaxHealth;
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

		}
	}
}
