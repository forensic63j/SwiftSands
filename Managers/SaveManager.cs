//Brian Sandon

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
		public static void Save(String filename, State currentState)
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
                            output.Write(player.MovementRange);

							//Item
							output.Write(player.EquipItem.Name);

							//Rectangle
							Rectangle pos = player.Position;
							output.Write(pos.X);
							output.Write(pos.Y);
							output.Write(pos.Width);
							output.Write(pos.Height);

							//Booleans
							output.Write(player.IsActive);
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
                            output.Write(item.Range);

							//Rectangle
							Rectangle position = item.Position;
							output.Write(position.X);
							output.Write(position.Y);
							output.Write(position.Width);
							output.Write(position.Height);

							//Sprite control bools
							output.Write(item.Collected);
							output.Write(item.IsActive);
							#endregion
						}

						output.Write(TaskManager.Count);
						for(int i = 0; i < TaskManager.Count; i++)
						{
							Task task = TaskManager.Tasks[i];

							//Task type
							output.Write(System.Enum.GetName(typeof(TaskType),(Object)(task.Type)));
							
							//Description
							output.Write(task.Description);

							//Details
							output.Write(task.Target);
							output.Write(task.ExpReward);
							output.Write(task.Completed);
						}

                        output.Write(currentState.ToString());
						if(currentState is Combat) 
						{
							Combat combat = currentState as Combat;
                            output.Write(combat.Name);
                            output.Write(combat.CurrentTurn);
							output.Write(combat.EnemyList.Count);
							foreach(Enemy enemy in combat.EnemyList){
							output.Write(enemy.Name);

							//Health, mana, death data
							output.Write(enemy.MaxHealth);
							output.Write(enemy.Health);
							output.Write(enemy.Mana);

							//Leveling
							output.Write(enemy.Level);

							//Stats
							output.Write(enemy.Accuracy);
							output.Write(enemy.Speed);
							output.Write(enemy.Strength);
                            output.Write(enemy.MovementRange);

							//Item
							output.Write(enemy.EquipItem.Name);

							//Rectangle
							Rectangle pos = enemy.Position;
							output.Write(pos.X);
							output.Write(pos.Y);
							output.Write(pos.Width);
							output.Write(pos.Height);

							//Booleans
							output.Write(enemy.IsActive);
							output.Write(enemy.ExpAwarded);
							}
						}else if(currentState is LocalMap)
						{
							LocalMap lMap = currentState as LocalMap;
                            output.Write(lMap.Name);
						}
					}
				}
				
				//player textures
                String texturePath = "";
				/*for(int i = 0; i < Party.Count; i++)
				{
                    texturePath = "Content\\PlayerSprites\\Player" + i + ".png";
                    using(Stream imgStream = File.OpenWrite(texturePath))
					{
						Texture2D texture = Party.PartyList[i].Texture;
						texture.SaveAsPng(imgStream,texture.Width,texture.Height);
					}
				}*/

			} catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
		#endregion
	}
}
