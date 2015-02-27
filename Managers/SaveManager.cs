using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwiftSands.Managers
{
    class SaveManager
    {
		#region method
		/// <summary>
		/// Saves files.
		/// </summary>
		/// <param name="filename">A file name.</param>
		public void Save(String filename, Party players)
		{
			try
			{
				using(Stream outStream = File.OpenWrite(filename))
				{
					using(BinaryWriter output = new BinaryWriter(outStream))
					{
						output.Write(players.Count);
						for(int i = 0; i < players.Count; i++)
						{
							Player player = players[i];
							
							//Name
							output.Write(player.Name);
						}
					}
				}
				for(int i = 0; i < players.Count; i++)
				{
					using(Stream imgStream = File.OpenWrite("Player\\Sprites\\Player" + i))
					{
						Texture2D texture = players[i].Texture;
						texture.SaveAsPng(imgStream,texture.Width,texture.Height);
					}
				}
			} catch(Exception e)
			{

			}
		}
		#endregion
	}
}
