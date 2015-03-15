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

		public PauseMenu(SpriteFont font,Texture2D sprite,Game game,Viewport port)
			: base(game,port)
		{
			this.font = font;

			int centering = (port.Width - 40) / 2;
			resume = new Button("Resume",font,sprite,new Rectangle(centering,20,15,40),true,true);
			save = new Button("Save",font,sprite,new Rectangle(centering,40,15,40),true,true);
			options = new Button("Options",font,sprite,new Rectangle(centering,60,15,40),true,true);
			main = new Button("Main Menu",font,sprite,new Rectangle(centering,80,15,40),true,true);
			quit = new Button("Quit",font,sprite,new Rectangle(centering,100,15,40),true,true);
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
			MouseState mState = Mouse.GetState();

			resume.Update(mState);
			save.Update(mState);
			options.Update(mState);
			main.Update(mState);
			quit.Update(mState);

			base.Update(time);
		}

		/// <summary>
		/// Draws frame.
		/// </summary>
		/// <param name="time">Time elapsed.</param>
		/// <param name="spriteBatch"></param>
		public override void Draw(GameTime time,SpriteBatch spriteBatch)
		{

			spriteBatch.DrawString(font,"The game is paused.",new Vector2(255,0),Color.Brown);

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
