using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwiftSands.Managers
{

	class LoadManager
    {
        #region fields
        private DirectoryInfo saveInfo;
		private GraphicsDeviceManager graphics;
		private Game1 game;
        #endregion

        #region method
		/// <summary>
		/// Creates a load manager.
		/// </summary>
        public LoadManager(Game1 game)
        {
			this.game = game;
			graphics = new GraphicsDeviceManager(game);
			saveInfo = Directory.CreateDirectory("Data//SaveFiles"); 
        }

        /// <summary>
        /// Loads content.
        /// </summary>
		/// <param name="characterList">List of characters in the game.</param>
		/// <param name="itemList">List of the items in the game.</param>
        public void LoadContent(ref List<Character> characterList, ref List<Item> itemList)
        {
			try
			{
				#region characters
				using(StreamReader input = new StreamReader("Data//GameEntities//Characters"))
				{
					String characterData = input.ReadToEnd();
					String[] characters = characterData.Split(';');
					for(int i = 0; i < characters.Length; i++)
					{
						String[] characterStats = characters[i].Split(',');
						//name,recruitable
						String name = characterStats[0];
						bool recruitable = bool.Parse(characterStats[1]);

						//Health, mana, death data
						int health = int.Parse(characterStats[2]);
						int mana = int.Parse(characterStats[3]);
						int deathsAllowed = int.Parse(characterStats[4]);

						//Leveling
						int level = int.Parse(characterStats[5]);

						//stats
						int accuracy = int.Parse(characterStats[6]);
						int speed = int.Parse(characterStats[7]);
						int strength = int.Parse(characterStats[8]);

						if(characterStats.Length > 9)
						{
							int xpAwarded = int.Parse(characterStats[9]);
							//create enemy
							Enemy tempEnemy = new Enemy(health,mana,speed,strength,accuracy,level,recruitable,name,xpAwarded);
							characterList.Add(tempEnemy);
						} else
						{
							//Builds character
							Character tempCharacter = new Character(health,mana,speed,strength,accuracy,level,recruitable,name);
							characterList.Add(tempCharacter);
						}
					}
				}
				#endregion

				#region items
				using(StreamReader input = new StreamReader("Data//GameEntities//Item"))
				{
					String itemData = input.ReadToEnd();
					String[] items = itemData.Split(';');
					for(int i = 0; i < items.Length; i++)
					{
						String[] itemStats = items[i].Split(',');

						//Name and type
						String name = itemStats[0];
						ItemType type = Item.ParseType(itemStats[1]);
						String description = itemStats[2];

						//Healing and damage
						int healing = int.Parse(itemStats[3]);
						int damage = int.Parse(itemStats[4]);

						//Item creation
						Item tempItem = new Item(type,healing,damage,name,description);
						itemList.Add(tempItem);
					}
				}
				#endregion
			} catch(Exception e)
			{
				
			}
        }

        /// <summary>
        /// Loads a savefile.
        /// </summary>
        /// <param name="filename">The name of the file to load.</param>
        public void LoadSavefile(String filename, ref Party players)
		{
			#region texture
			Texture2D[] sprites = null;
			try
            {
				//Load textures
				String[] files = Directory.GetFiles("Content//PlayerSprites");
				//Counts how many textures are character sprites
				int textureCount = 0;
				for(int i = 0; i < files.Length; i++){
					String filepath = files[i];
					int index = -1;
						if((filepath.Substring(0,6) == "Player") && int.TryParse(filepath.Substring(filepath.Length-1),out index)){
							textureCount++;
						}
				}
				//Setting texures
				sprites = new Texture2D[textureCount];
				for(int i = 0; i < files.Length; i++)
				{
					String filepath = files[i];
					using(Stream imgStream = File.OpenRead(filepath))
					{
						int index = -1;
						if((filepath.Substring(0,6) == "Player") && int.TryParse(filepath.Substring(filepath.Length - 1),out index))
						{
							sprites[index] = Texture2D.FromStream(game.GraphicsDevice,imgStream);
						}
					}
				}
			#endregion

				using(Stream inStream = File.OpenRead("Savefiles//" + filename))
				{
					using(BinaryReader input = new BinaryReader(inStream))
					{
						#region player
						players.Clear();

						for(int i = 0; i < sprites.Length; i++)
						{
							//Name
							String name = input.ReadString();

							//Health, mana, death data
							//int maxHealth = input.ReadInt32();
							int health = input.ReadInt32();
							int mana = input.ReadInt32();
							//int numDeaths = input.ReadInt32();
							int deathsAllowed = 0/*input.ReadInt32()*/;

							//Leveling
							int level = input.ReadInt32();
							int exp = input.ReadInt32();
							int expNeeded = input.ReadInt32();

							//stats
							int accuracy = input.ReadInt32();
							int speed = input.ReadInt32();
							int strength = input.ReadInt32();

							//instantiate player
							players.Add(new Player(health,mana,speed,strength,accuracy,level,true,name,deathsAllowed));
						}
						#endregion	
					}
				}
			} catch(Exception e)
			{

			}
        }
        #endregion
    }
}
