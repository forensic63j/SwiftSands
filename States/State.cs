//Clayton Scavone

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
        Game1 game;
        Viewport viewPort;
        Camera camera;

        public Camera StateCamera
        {
            get { return camera; }
        }

        public Viewport ViewPort
        {
            get { return viewPort; }
        }

        public Game1 StateGame
        {
            get { return game; }
        }

        /// <summary>
        /// State creation
        /// </summary>
        public State(Game1 game,Viewport port)
        {
            this.camera = new Camera(port);
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
            StateManager.KPrevious = StateManager.KState;  
        }
        /// <summary>
        /// On Drawing State
        /// </summary>
        /// <param name="time"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime time, SpriteBatch spriteBatch)
        {
           
        }

        public virtual void DrawWorld(GameTime time, SpriteBatch spriteBatch)
        {
           
        }

        public virtual void DrawScreen(GameTime time, SpriteBatch spriteBatch)
        {

        }
    }
}
