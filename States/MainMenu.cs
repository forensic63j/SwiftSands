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
        private Button play;
        private Button save;
        private Button load;
        private Button options;
        private Button quit;
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
        /// Gets the save button.
        /// </summary>
        public Button Save
        {
            get { return save; }
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
        public MainMenu(SpriteFont font,Texture2D sprite, Game game, Viewport port)
            : base(game, port)
        {
            play = new Button("Play", font, sprite, new Rectangle(70, 20, 10, 30), true, true);
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
            base.Update(time);
        }   

        /// <summary>
        /// Draws frame.
        /// </summary>
        /// <param name="time">Time elapsed.</param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime time, SpriteBatch spriteBatch)
        {
            base.Draw(time, spriteBatch);
        }
        #endregion
    }
}
