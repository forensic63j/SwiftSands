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
    class MainMenu : State
    {
        #region fields
		private SpriteFont font;
		
		private Button play;
        private Button load;
        private Button options;
        private Button quit;

		private MouseState mState;
		private MouseState mPrevious;
        #endregion

        #region properties
        /// <summary>
        /// Gets the play button.
        /// </summary>
        public Button Play
        {
            get { return play; }
        }

        /// <summary>
        /// Gets the load button.
        /// </summary>
        public Button Load
        {
            get { return load; }
        }

        /// <summary>
        /// Gets the options button.
        /// </summary>
        public Button Options
        {
            get { return options; }
        }

        /// <summary>
        /// Gets the quit button.
        /// </summary>
        public Button Quit
        {
            get { return quit; }
        }
        #endregion

        /// <summary>
        /// Instatiates the Main Menu
        /// </summary>
        public MainMenu(SpriteFont font,Texture2D sprite, Game1 game, Viewport port)
            : base(game, port)
        {
			this.font = font;

			int buttonWidth = 120;
			int centering = (port.Width - buttonWidth) / 2;
			play = new Button("Play",font,sprite,new Rectangle(centering,20,buttonWidth,30),true,true);
			load = new Button("Load",font,sprite,new Rectangle(centering,60,buttonWidth,30),true,true);
			options = new Button("Options",font,sprite,new Rectangle(centering,100,buttonWidth,30),true,true);
			quit = new Button("Quit",font,sprite,new Rectangle(centering,140,buttonWidth,30),true,true);
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
			play.Update();
			load.Update();
			options.Update();
			quit.Update();

			base.Update(time);
        }   

        /// <summary>
        /// Draws frame.
        /// </summary>
        /// <param name="time">Time elapsed.</param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime time, SpriteBatch spriteBatch)
        {

			spriteBatch.DrawString(font,"Main menu:",new Vector2(350,0),Color.Brown);

			play.Draw(spriteBatch);
			load.Draw(spriteBatch);
			options.Draw(spriteBatch);
			quit.Draw(spriteBatch);
			
			base.Draw(time, spriteBatch);
        }
        #endregion
    }
}
