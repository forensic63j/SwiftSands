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
		private String name;
		private SpriteFont font;
		public delegate void Clicked();
		public Clicked onClick;
		#endregion

		#region properties
		/// <summary>
		/// Gets the name of the button
		/// </summary>
		public String Name
		{
			get { return name; }
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
		public Button(String name,SpriteFont font,Texture2D sprite,Rectangle position,bool active,bool onScreen,Clicked clickDelegate)
			: base(sprite,position,active,onScreen)
		{
			this.name = name;
			this.font = font;
			onClick = clickDelegate;
		}

        /// <summary>
        /// Updates the button.
        /// </summary>
        /// <param name="mState">The state of the mouse currently.</param>
        public void Update(MouseState mState)
        {
            Rectangle pos = this.Position;
            bool onButton = ((mState.X >= pos.X && mState.X < (pos.X + pos.Width)) && (mState.Y >= pos.Y && mState.Y < (pos.Y + pos.Height)));
            if(this.IsActive && (mState.LeftButton == ButtonState.Pressed) && onButton)
            {
                onClick();
            }
        }
 
		#endregion
	}
}
