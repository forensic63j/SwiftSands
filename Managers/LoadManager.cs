using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwiftSands
{

	static class LoadManager
    {
        #region fields
        static private DirectoryInfo saveInfo;
		static private GraphicsDeviceManager graphics;
		static private Game1 game;
        #endregion

        #region method
		/// <summary>
		/// Creates a load manager.
		/// </summary>
        static public void UpdateGame(Game1 game2)
        {
			game = game2;
			graphics = new GraphicsDeviceManager(game);
			saveInfo = Directory.CreateDirectory("Data//SaveFiles"); 
        }

        /// <summary>
        /// Loads content.
        /// </summary>
		/// <param name="characterList">List of characters in the game.</param>
		/// <param name="itemList">List of the items in the game.</param>
		/// <param name="buttonSprite">The sprite used for all buttons.</param>
		/// <param name="font">The font for the GUI.</param>
        static public void LoadContent(ref List<Character> characterList, ref List<Item> itemList, ref Texture2D buttonSprite, ref SpriteFont font)
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
						int maxHealth = int.Parse(characterStats[2]);
						int health = int.Parse(characterStats[3]);
						int mana = int.Parse(characterStats[4]);
						int deathsAllowed = int.Parse(characterStats[5]);

						//Leveling
						int level = int.Parse(characterStats[6]);

						//stats
						int accuracy = int.Parse(characterStats[7]);
						int speed = int.Parse(characterStats[8]);
						int strength = int.Parse(characterStats[9]);

						//sprite fields
						String textureFile = characterStats[10];
						//Texture
						Texture2D sprite = null;
						using(Stream imgStream = File.OpenRead("Content//CharacterSprites//" + textureFile)){
							sprite = Texture2D.FromStream(game.GraphicsDevice,imgStream);
						}
						//rectangle
						int x = int.Parse(characterStats[11]);
						int y = int.Parse(characterStats[12]);
						int width = int.Parse(characterStats[13]);
						int height = int.Parse(characterStats[14]);
						Rectangle position = new Rectangle(x,y,width,height);

						//active/on screen
						bool active = bool.Parse(characterStats[15]);
						bool onScreen = bool.Parse(characterStats[16]);

						if(characterStats.Length > 17)
						{
							int xpAwarded = int.Parse(characterStats[17]);
							//create enemy
							Enemy tempEnemy = new Enemy(maxHealth,health,mana,speed,strength,accuracy,level,recruitable,xpAwarded,sprite,position,active,onScreen,name);
							characterList.Add(tempEnemy);
						} else
						{
							//Builds character
							Character tempCharacter = new Character(maxHealth,health,mana,speed,strength,accuracy,level,recruitable,sprite,position,active,onScreen,name);
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
					
						ItemType type = (ItemType)(System.Enum.Parse(typeof(ItemType),itemStats[1],true));
						String description = itemStats[2];

						//Healing and damage
						int healing = int.Parse(itemStats[3]);
						int damage = int.Parse(itemStats[4]);

						//sprite fields
						String textureFile = itemStats[5];
						//Texture
						Texture2D sprite = null;
						using(Stream imgStream = File.OpenRead("Content//ItemSprites//" + textureFile))
						{
							sprite = Texture2D.FromStream(game.GraphicsDevice,imgStream);
						}
						//rectangle
						int x = int.Parse(itemStats[6]);
						int y = int.Parse(itemStats[7]);
						int width = int.Parse(itemStats[8]);
						int height = int.Parse(itemStats[9]);
						Rectangle position = new Rectangle(x,y,width,height);

						//active/on screen
						bool collected = bool.Parse(itemStats[10]);
						bool active = bool.Parse(itemStats[11]);
						bool onScreen = bool.Parse(itemStats[12]);


						//Item creation
						Item tempItem = new Item(type,healing,damage,description,collected,sprite,position,active,onScreen,name);
						itemList.Add(tempItem);
					}
				}
				#endregion

				using(StreamReader input = new StreamReader("Data//Tasks"))
				{
					String taskData = input.ReadToEnd();
					String[] tasks = taskData.Split(';');

					for(int i = 0; i < tasks.Length; i++)
					{
						String[] taskStats = tasks[i].Split(',');

						TaskType type = (TaskType)(System.Enum.Parse(typeof(TaskType),taskStats[0],true));
						String description = taskStats[i];
						String target = taskStats[2];
						int reward = int.Parse(taskStats[3]);

						Task tempTask = new Task(type,description,target,reward);
						TaskManager.AddTask(tempTask);
					}
				}

				using(Stream imgStream = File.OpenRead("Content//GUI//button.png"))//Update once filetype is decided.
				{
					buttonSprite = Texture2D.FromStream(game.GraphicsDevice,imgStream);
				}

				font = game.Content.Load<SpriteFont>("GUI//menuFont");//Update later.
			} catch(Exception e)
			{
				
			}
        }

        /// <summary>
        /// Loads a savefile.
        /// </summary>
        /// <param name="filename">The name of the file to load.</param>
        static public void LoadSavefile(String filename, Inventory inventory, List<Item> itemList, List<Task> taskList)
		{
			#region texture
			Texture2D[] sprites = null;
			try
            {
				//Load textures
				String[] files = Directory.GetFiles("Content\\PlayerSprites\\" + filename);
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
						Party.Clear();

						for(int i = 0; i < sprites.Length; i++)
						{
							//Name
							String name = input.ReadString();

							//Health, mana, death data
							int maxHealth = input.ReadInt32();
							int health = input.ReadInt32();
							int mana = input.ReadInt32();
							int numDeaths = input.ReadInt32();
							int deathsAllowed = input.ReadInt32();

							//Leveling
							int level = input.ReadInt32();
							int exp = input.ReadInt32();
							int expNeeded = input.ReadInt32();

							//Stats
							int accuracy = input.ReadInt32();
							int speed = input.ReadInt32();
							int strength = input.ReadInt32();

							//Rectangle
							int x = input.ReadInt32();
							int y = input.ReadInt32();
							int width = input.ReadInt32();
							int height = input.ReadInt32();
							Rectangle position = new Rectangle(x,y,width,height);

							//Booleans
							bool active = input.ReadBoolean();
							bool onScreen = input.ReadBoolean();

							//instantiate player
							Player tempPlayer = new Player(maxHealth,health,mana,speed,strength,accuracy,level,true,deathsAllowed,sprites[i],position,active,onScreen,name);
							tempPlayer.NumDeaths = numDeaths;
							Party.Add(tempPlayer);
						}
						#endregion	

						#region inventory
						inventory.Clear();

						int numItems = input.ReadInt32();
						for(int i = 0; i < numItems; i++)
						{
							//Name, item type, description
							String name = input.ReadString();
							String typeString = input.ReadString();
							ItemType type = (ItemType)(System.Enum.Parse(typeof(ItemType),typeString,true));
							String description = input.ReadString();

							//Healing and damage
							int healing = input.ReadInt32();
							int damage = input.ReadInt32();

							//Sprite
							Texture2D sprite = GetItemSprite(name,itemList);

							//Rectangle
							int x = input.ReadInt32();
							int y = input.ReadInt32();
							int width = input.ReadInt32();
							int height = input.ReadInt32();
							Rectangle position = new Rectangle(x,y,width,height);

							//Sprite control bools
							bool collected = input.ReadBoolean();
							bool active = input.ReadBoolean();
							bool onScreen = input.ReadBoolean();

							//Add to inventory
							Item tempItem = new Item(type,healing,damage,description,collected,sprite,position,active,onScreen,name);
							inventory.AddItem(tempItem);
						}
						#endregion
					}
				}
			} catch(Exception e)
			{

			}
        }

		/// <summary>
		/// Loads a map.
		/// </summary>
		/// <param name="filename">The file name for the map.</param>
		/// <returns></returns>
        static public Map LoadMap(string filename)
        {
            Map loadingMap = new Map();
            using (StreamReader input = new StreamReader("Content//Maps//" + filename))
            {
                
            }
            return loadingMap;
        }

		/// <summary>
		/// Searches items for matching names and returns the associated texture.
		/// </summary>
		/// <param name="name">The name of the </param>
		/// <param name="itemList">List of item in the game.</param>
		/// <returns>The appropriate sprite.</returns>
		static public Texture2D GetItemSprite(String name,List<Item> itemList)
		{
			for(int i = 0; i < itemList.Count; i++)
			{
				if(name == itemList[i].Name)
				{
					return itemList[i].Texture;
				}
			}
			return null;
		}
        #endregion
    }
}
