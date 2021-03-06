﻿//John Palermo

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwiftSands
{
	class Sprite
	{
		#region fields
		private Texture2D texture;
		private Rectangle position;
		private bool isActive;
        private String name;
		#endregion

		#region properties
		/// <summary>
		/// Gets texture.
		/// </summary>
		public Texture2D Texture
		{
			get { return texture; }
		}

		/// <summary>
		/// Gets position.
		/// </summary>
		public Rectangle Position
		{
			get { return position; }
            set { position = value; }
		}
		
		public bool IsActive
		{
			get { return isActive; }
			set { isActive = value; }
		}

        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public Vector2 TilePosition
        {
            get
            {
                Rectangle convertedPos = StateManager.ConvertPosition(Position, StateManager.CurrentState.StateCamera);
                return new Vector2(convertedPos.X, convertedPos.Y);
            }
            set
            {
                Vector2 unconvertedPosition = StateManager.UnConvertPosition(value, StateManager.CurrentState.StateCamera);
                Position = new Rectangle((int)unconvertedPosition.X, (int)unconvertedPosition.Y, Position.Width, Position.Height);
            }
        }
		#endregion

        public Sprite(Texture2D tex, Rectangle pos, bool act, String name)
        {
            this.texture = tex;
            this.position = pos;
            isActive = act;
            this.name = name;
        }

        #region methods
        /// <summary>
        /// Draws the sprite.
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }

        /// <summary>
        /// Draws the sprite.
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (IsActive)
            {
                spriteBatch.Draw(texture, position, color);
            }
        }
        #endregion
    }
}
                                                                                                                                            