using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwiftSands
{
    static class SaveManager
    {
		#region method
		/// <summary>
		/// Saves files.
		/// </summary>
		/// <param name="filename">A file name.</param>
		public static void Save(String filename)
		{
			try
			{
				using(Stream outStream = File.OpenWrite("Data//Savefiles//" + filename))
				{
					using(BinaryWriter output = new BinaryWriter(outStream))
					{
						output.Write(Party.Count);
						for(int i = 0; i < Party.Count; i++)
						{
							#region player
							Player player = Party.PartyList[i];
							
							//Name
							output.Write(player.Name);

							//Health, mana, death data
							output.Write(player.MaxHealth);
							output.Write(player.Health);
							output.Write(player.Mana);
							output.Write(player.DeathsAllowed);
							output.Write(player.NumDeaths);

							//Leveling
							output.Write(player.Level);
							output.Write(player.Exp);
							output.Write(player.ExpNeeded);

							//Stats
							output.Write(player.Accuracy);
							output.Write(player.Speed);
							output.Write(player.Strength);

							//Rectangle
							Rectangle pos = player.Position;
							output.Write(pos.X);
							output.Write(pos.Y);
							output.Write(pos.Width);
							output.Write(pos.Height);

							//Booleans
							output.Write(player.IsActive);
							output.Write(player.IsOnField);
							#endregion
						}

						output.Write(Inventory.Count);
						for(int i = 0; i < Inventory.Count; i++)
						{
							#region inventory
							Item item = Inventory.Items[i];

							//Name, item type,description
							output.Write(item.Name);
							output.Write(System.Enum.GetName(typeof(ItemType),(Object)(item.Type)));
							output.Write(item.Description);

							//Healing and damage
							output.Write(item.Healing);
							output.Write(item.Damage);

							//rectangle
							Rectangle position = item.Position;
							output.Write(position.X);
							output.Write(position.Y);
							output.Write(position.Width);
							output.Write(position.Height);

							//sprite control bools
							output.Write(item.Collected);
							output.Write(item.IsActive);
							output.Write(item.IsOnField);
							#endregion
						}
					}
				}
				
				//player textures
				for(int i = 0; i < Party.Count; i++)
				{
					using(Stream imgStream = File.OpenWrite("PlayerSprites\\" + filename + "\\Player" + i))
					{
						Texture2D texture = Party.PartyList[i].Texture;
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
