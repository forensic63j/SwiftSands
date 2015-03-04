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
		private bool isOnField;
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
		}
		
		public bool IsActive
		{
			get { return isActive; }
			set { isActive = value; }
		}
		
		public bool IsOnField
		{
			get { return isOnField; }
			set { isOnField = value; }
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
		#endregion

        public Sprite(Texture2D tex, Rectangle pos, bool act, bool field, String name)
        {
            this.texture = tex;
            this.position = pos;
            isActive = act;
            isOnField = field;
            this.name = name;
        }

        #region methods
        /// <summary>
        /// Draws the sprite.
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
        #endregion
    }
}
