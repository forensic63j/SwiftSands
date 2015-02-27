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
    interface IState
    {
        /// <summary>
        /// On becoming current state
        /// </summary>
        public void OnEnter()
        {
        }

        /// <summary>
        /// On not becoming current state
        /// </summary>
        public void OnExit()
        {

        }

        /// <summary>
        /// On Destroying State
        /// </summary>
        public void OnDestroy()
        {

        }

        /// <summary>
        /// On Updating State
        /// </summary>
        /// <param name="time"></param>
        public void Update(GameTime time)
        {

        }
        /// <summary>
        /// On Drawing State
        /// </summary>
        /// <param name="time"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime time, SpriteBatch spriteBatch)
        {

        }
    }
}
