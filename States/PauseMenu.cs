//Clayton Scavone and Brian Sandon

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
	class PauseMenu : State
	{
		#region fields
		private SpriteFont font;

		private Button resume;
		private Button save;
		private Button options;
		private Button main;
		private Button quit;

		private MouseState mState;
		private MouseState mPrevious;
		#endregion

		#region properties
		/// <summary>
		/// Gets the resume button.
		/// </summary>
		public Button Resume
		{
			get { return resume; }
		}

		/// <summary>
		/// Gets the save button.
		/// </summary>
		public Button Save
		{
			get { return save; }
		}

		/// <summary>
		/// Gets the options button.
		/// </summary>
		public Button Options
		{
			get { return options; }
		}

		/// <summary>
		/// Gets the main menu button.
		/// </summary>
		public Button Main
		{
			get { return main; }
		}

		/// <summary>
		/// Gets the quit button.
		/// </summary>
		public Button Quit
		{
			get { return quit; }
		}
		#endregion

		public PauseMenu(SpriteFont font,Texture2D sprite,Game1 game,Viewport port)
			: base(game,port)
		{
			this.font = font;

			int buttonWidth = 120;
			int centering = (port.Width - buttonWidth) / 2;
			resume = new Button("Resume",font,sprite,new Rectangle(centering,20,buttonWidth,30),true,true);
			save = new Button("Save",font,sprite,new Rectangle(centering,60,buttonWidth,30),true,true);
			options = new Button("Options",font,sprite,new Rectangle(centering,100,buttonWidth,30),true,true);
			main = new Button("Main Menu",font,sprite,new Rectangle(centering,140,buttonWidth,30),true,true);
			quit = new Button("Quit",font,sprite,new Rectangle(centering,180,buttonWidth,30),true,true);
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
			
			resume.Update();
			save.Update();
			options.Update();
			main.Update();
			quit.Update();

			base.Update(time);
		}

		/// <summary>
		/// Draws frame.
		/// </summary>
		/// <param name="time">Time elapsed.</param>
		/// <param name="spriteBatch"></param>
		public override void DrawScreen(GameTime time,SpriteBatch spriteBatch)
		{

			spriteBatch.DrawString(font,"The game is paused.",new Vector2(320,0),Color.Brown);

			resume.Draw(spriteBatch);
			save.Draw(spriteBatch);
			options.Draw(spriteBatch);
			main.Draw(spriteBatch);
			quit.Draw(spriteBatch);

			base.Draw(time,spriteBatch);
		}
		#endregion
	}
}
