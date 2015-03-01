using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwiftSands
{
	class Button:Sprite
	{
		#region fields
		private String name;
		private SpriteFont font;
		public delegate void Clicked();
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
		public Button(String name,SpriteFont font,Texture2D sprite,Rectangle position,bool active,bool onScreen,Clicked clickDelegate)
			: base(sprite,position,active,onScreen)
		{

		}
		#endregion
	}
}
