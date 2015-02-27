using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SwiftSands
{
	class Sprite
	{
		#region feilds
		private Texture2D texture;
		private Rectangle position;
		#endregion

		#region properties
		/// <summary>
		/// Gets texture.
		/// </summary>
		public Texture2D Texture
		{
			get { return texture; }
		}

		public Rectangle Position 
		{
			get { return position; }
		}
		#endregion
	}
}
