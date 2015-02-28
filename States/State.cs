using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace SwiftSands
{
    abstract class State
    {
        Game game;
        Viewport viewPort;
        /// <summary>
        /// State creation
        /// </summary>
        public State(Game game, Viewport port)
        {
            this.game = game;
            this.viewPort = port;
        }

        /// <summary>
        /// On becoming current state
        /// </summary>
        public virtual void OnEnter()
        {

        }

        /// <summary>
        /// On not becoming current state
        /// </summary>
        public virtual void OnExit()
        {

        }

        /// <summary>
        /// On Destroying State
        /// </summary>
        public virtual void OnDestroy()
        {

        }

        /// <summary>
        /// On Updating State
        /// </summary>
        /// <param name="time"></param>
        public virtual void Update(GameTime time)
        {
            
        }
        /// <summary>
        /// On Drawing State
        /// </summary>
        /// <param name="time"></param>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(GameTime time, SpriteBatch spriteBatch)
        {

        }
    }
}
