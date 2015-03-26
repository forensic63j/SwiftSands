//Clayton Scavone

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
		//For combat
		int currentTurn;
		List<Character> combatants;
		bool moveLeft;
		bool actionLeft;

		Button attack;
		Button endTurn;
		#endregion

		#region main methods
		public Combat(Game1 game, Viewport port,List<Enemy> enemies,SpriteFont font) : base(game, port) {
			currentTurn = 0;
			moveLeft = true;
			actionLeft = true;

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
			if(combatants[currentTurn] is Player)
			{
				attack.IsActive = actionLeft;
				endTurn.IsActive = true;
				Player cPlayer = combatants[currentTurn] as Player;
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
	}
}
