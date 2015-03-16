﻿//Brian Sandon

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SwiftSands
{
	class Button:Sprite
	{
		#region fields
		private SpriteFont font;
		public delegate void Clicked();
		private Clicked onClick;
		#endregion

		#region properties
        /// <summary>
        /// Gets and sets the onClick delegate.
        /// </summary>
        public Clicked OnClick
        {
            get { return onClick; }
            set { onClick = value; }
        }
		#endregion

		#region methods
		/// <summary>
		/// Creates a new button
		/// </summary>
		/// <param name="name">Name of the button, this is displayed as text.</param>
		/// <param name="font">Font used for the button.</param>
		/// <param name="sprite">Uses a button sprite from content.</param>
		/// <param name="position">A rectangle describing the position and size of the button.</param>
		/// <param name="active">Tells whether a button is active and should appear on screen.</param>
		/// <param name="onScreen">Tells whether the button is within the window range.</param>
		/// <param name="clickDelegate"></param>
		public Button(String name,SpriteFont font,Texture2D sprite,Rectangle position,bool active,bool onScreen)
			: base(sprite,position,active,onScreen,name)
		{
			this.font = font;
		}

        /// <summary>
        /// Updates the button.
        /// </summary>
        /// <param name="mState">The state of the mouse currently.</param>
        public void Update(MouseState mState, MouseState mPrevious)
        {
			Rectangle pos = this.Position;
            bool onButton = ((mState.X >= pos.X && mState.X < (pos.X + pos.Width)) && (mState.Y >= pos.Y && mState.Y < (pos.Y + pos.Height)));
            if(this.IsActive && (mState.LeftButton == ButtonState.Pressed && mPrevious.LeftButton != ButtonState.Pressed) && onButton)
            {
                onClick();
            }
        }

        /// <summary>
        /// Draws the button.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Rectangle pos = this.Position;
            spriteBatch.DrawString(font, this.Name, new Vector2(pos.X + 5, pos.Y + 2), Color.Black);
        }
		#endregion
	}
}
