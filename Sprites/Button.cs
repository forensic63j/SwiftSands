//Brian Sandon

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
		public Button(String name,SpriteFont font,Texture2D sprite,Rectangle position,bool active)
			: base(sprite,position,active,name)
		{
			this.font = font;
		}

        /// <summary>
        /// Updates the button.
        /// </summary>
        /// <param name="mState">The state of the mouse currently.</param>
        public void Update()
        {
			Rectangle pos = this.Position;
            bool onButton = ((StateManager.MState.X >= pos.X && StateManager.MState.X < (pos.X + pos.Width)) && (StateManager.MState.Y >= pos.Y && StateManager.MState.Y < (pos.Y + pos.Height)));
            if (this.IsActive && (StateManager.MState.LeftButton == ButtonState.Pressed && StateManager.MPrevious.LeftButton != ButtonState.Pressed) && onButton)
            {
                onClick();
            }
        }

        /// <summary>
        /// Draws the button.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle pos = this.Position;
			base.Draw(spriteBatch);
			spriteBatch.DrawString(font,this.Name,new Vector2(pos.X + 5,pos.Y + 2),Color.Black);
        }
		#endregion
	}
}
