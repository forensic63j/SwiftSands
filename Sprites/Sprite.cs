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
		#endregion
	}
}
