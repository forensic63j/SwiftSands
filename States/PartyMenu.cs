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
	class PartyMenu : State
	{
		#region fields
		private SpriteFont font;

		private Button[] partyButtons;

		Texture2D buttonSprite;

		private MouseState mState;
		private MouseState mPrevious;

		private int buttonWidth;
		private int selectedPlayer;
		#endregion

		#region properties
		/// <summary>
		/// Gets the quit button.
		/// </summary>
		public Button[] PartyButtons
		{
			get { return partyButtons; }
		}
		#endregion

		/// <summary>
		/// Instatiates the Party Menu
		/// </summary>
		public PartyMenu(SpriteFont font,Texture2D sprite,Game1 game,Viewport port)
			: base(game,port)
		{
			this.font = font;
			base.StateCamera.InputEnabled = false;
			buttonWidth = 120;
			int centering = (port.Width - buttonWidth) / 2;
			buttonSprite = sprite;
			partyButtons = new Button[6];
			selectedPlayer = 0;
			String characterName;
			for(int i = 0; i < 6; i++)
			{
				if(Party.PartyList.Count > i)
				{
					characterName = Party.PartyList[i].Name;
				} else
				{
					characterName = "<empty>";
				}
				partyButtons[i] = new Button(characterName,font,sprite,new Rectangle(5,40 * i + 20,buttonWidth,30),true);
			}
			partyButtons[0].OnClick = Player0;
			partyButtons[1].OnClick = Player1;
			partyButtons[2].OnClick = Player2;
			partyButtons[3].OnClick = Player3;
			partyButtons[4].OnClick = Player4;
			partyButtons[5].OnClick = Player5;
		}

		#region Methods
		/// <summary>
		/// Runs when menu is started/
		/// </summary>
		public override void OnEnter()
		{
			base.OnEnter();
		}

		/// <summary>
		/// Runs when menu closes.
		/// </summary>
		public override void OnExit()
		{
			base.OnExit();
		}

		/// <summary>
		/// Runs when menu is closed.
		/// </summary>
		public override void OnDestroy()
		{
			base.OnExit();
		}

		/// <summary>
		/// Updates once per frame.
		/// </summary>
		/// <param name="time">The time elapsed</param>
		public override void Update(GameTime time)
		{
			mState = Mouse.GetState();
			StateManager.KState = Keyboard.GetState();

			if(StateManager.KState.IsKeyDown(Keys.Escape) && StateManager.KPrevious.IsKeyUp(Keys.Escape))
			{
				StateManager.CloseState();
			}

            if (StateManager.KState.IsKeyDown(Keys.I) && StateManager.KPrevious.IsKeyUp(Keys.I))
            {
                StateManager.CloseState();
                StateManager.OpenState(StateGame.InventoryMenu);
            }

			for(int i = 0; i < 6; i++)
			{
				if(Party.PartyList.Count > i)
				{
					partyButtons[i].Name = Party.PartyList[i].Name;
					partyButtons[i].Clickable = true;
				} else
				{
					partyButtons[i].Name = "<empty>";
					partyButtons[i].Clickable = false;
				}
				partyButtons[i].Update();
			}

			base.Update(time);
		}

		/// <summary>
		/// Draws frame.
		/// </summary>
		/// <param name="time">Time elapsed.</param>
		/// <param name="spriteBatch"></param>
		public override void DrawScreen(GameTime time,SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(font,"Party:",new Vector2(350,0),Color.Brown);
			for(int i = 0; i < 6; i++)
			{
				partyButtons[i].Draw(spriteBatch);
			}
			spriteBatch.DrawString(font,"Player stats: ",new Vector2(buttonWidth + 20,20),Color.Black);
			if(Party.PartyList[selectedPlayer] != null)
			{
				Player cPlayer = Party.PartyList[selectedPlayer];
				String nameString = "Name: " + cPlayer.Name + "   Level: " + cPlayer.Level + " (XP to next level: " + cPlayer.ExpNeeded + ")";
				spriteBatch.DrawString(font,nameString,new Vector2(buttonWidth+30,42),Color.Black);
				String healthString = "Health: " + cPlayer.Health + "\\" + cPlayer.MaxHealth + "   Deaths: " + cPlayer.NumDeaths + "   Mana: " + cPlayer.Mana;
				spriteBatch.DrawString(font,healthString,new Vector2(buttonWidth + 30,64),Color.Black);
                String stats = "Item: " + cPlayer.EquipItem.Name + "    Item type: " + cPlayer.EquipItem.Type;
                spriteBatch.DrawString(font, stats, new Vector2(buttonWidth + 30, 86), Color.Black);
				stats = "Spd: " + cPlayer.Speed + "   Str: " + cPlayer.Strength + "   Acc: " + cPlayer.Accuracy + "   Move: " + cPlayer.MovementRange;
				spriteBatch.DrawString(font,stats,new Vector2(buttonWidth + 30,108),Color.Black);
				
				base.Draw(time,spriteBatch);
			} else
			{
				spriteBatch.DrawString(font,"<No player selected>",new Vector2(5,255),Color.Black);
			}
		}
		#endregion

		#region buttonMethods
		/// <summary>
		/// Selects player 0.
		/// </summary>
		public void Player0()
		{
			selectedPlayer = 0;
		}

		/// <summary>
		/// Selects player 1.
		/// </summary>
		public void Player1()
		{
			selectedPlayer = 1;
		}

		/// <summary>
		/// Selects player 2.
		/// </summary>
		public void Player2()
		{
			selectedPlayer = 2;
		}

		/// <summary>
		/// Selects player 3.
		/// </summary>
		public void Player3()
		{
			selectedPlayer = 3;
		}

		/// <summary>
		/// Selects player 4.
		/// </summary>
		public void Player4()
		{
			selectedPlayer = 4;
		}

		/// <summary>
		/// Selects player 5.
		/// </summary>
		public void Player5()
		{
			selectedPlayer = 5;
		}
		#endregion
	}
}
