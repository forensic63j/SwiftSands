using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
						//saves players
						output.Write(players.Count);
						for(int i = 0; i < players.Count; i++)
						{
							Player p = players[i];
							
							output.Write(p.Name);
							output.Write(p.Level);
							output.Write(p.Health);
						}
					}
				}
			} catch(Exception e)
			{

			}
		}
		#endregion
	}
}
