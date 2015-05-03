//Brian Sandon and Clayton Scavone (LoadMap, LoadSprite, Various other and fixes)

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
			//graphics = new GraphicsDeviceManager(game);
			saveInfo = Directory.CreateDirectory("Data//SaveFiles"); 
        }

        static public Texture2D LoadSprite(string spritename)
        {
            Texture2D tempsprite;
            using (Stream imgStream = File.OpenRead("Content\\" + spritename))//Update once filetype is decided.
            {
                tempsprite = Texture2D.FromStream(game.GraphicsDevice, imgStream);
                return tempsprite;
            }
            return tempsprite;
        }

        //Testing
        static public Texture2D LoadSprite(String contentFilePath, String spritename)
        {
            Texture2D tempsprite;
            using (Stream imgStream = File.OpenRead("Content\\" + contentFilePath + spritename))
            {
                tempsprite = Texture2D.FromStream(game.GraphicsDevice, imgStream);
                return tempsprite;
            }
            return tempsprite;
        }

        /// <summary>
        /// Loads content.
        /// </summary>
		/// <param name="characterList">List of characters in the game.</param>
		/// <param name="itemList">List of the items in the game.</param>
		/// <param name="buttonSprite">The sprite used for all buttons.</param>
		/// <param name="font">The font for the GUI.</param>
		static public void LoadContent(ref Dictionary<String,Character> characterList,ref Dictionary<String,Item> itemList,ref Texture2D buttonSprite,ref SpriteFont font)
        {
			try
			{
                Map newMap = LoadMap("desert.txt");
				//GUI
				font = game.Content.Load<SpriteFont>("GUI\\menuFont");

				using(Stream imgStream = File.OpenRead("Content\\GUI\\button-sprite.png"))//Update once filetype is decided.
				{
					buttonSprite = Texture2D.FromStream(game.GraphicsDevice,imgStream);
				}

				#region items
				using(StreamReader input = new StreamReader("Content\\Data\\GameEntities\\Item.txt"))
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
                        int range = int.Parse(itemStats[5]);

						//sprite fields
						String textureFile = itemStats[6];
						//Texture
						Texture2D sprite = null;
						using(Stream imgStream = File.OpenRead("Content\\ItemSprites\\" + textureFile))
						{
							sprite = Texture2D.FromStream(game.GraphicsDevice,imgStream);
						}
						//rectangle
						int x = int.Parse(itemStats[7]);
						int y = int.Parse(itemStats[8]);
						int width = int.Parse(itemStats[9]);
						int height = int.Parse(itemStats[10]);
						Rectangle position = new Rectangle(x,y,width,height);

						//active/on screen
						bool collected = bool.Parse(itemStats[11]);
						bool active = bool.Parse(itemStats[12]);
						bool onScreen = bool.Parse(itemStats[13]);


						//Item creation
						Item tempItem = new Item(type,healing,damage, range, description,collected,sprite,position,active,onScreen,name);
						itemList.Add(name,tempItem);
                        Inventory.AddItem(tempItem);
					}
				}
				#endregion

				#region characters
				using(StreamReader input = new StreamReader("Content\\Data\\GameEntities\\Characters.txt"))
				{
					String characterData = input.ReadToEnd();
					String[] characters = characterData.Split(';');
					for(int i = 0; i < characters.Length; i++)
					{
						String[] characterStats = characters[i].Split(',');
						//name,recruitable
						String name = characterStats[0];
						//Console.WriteLine("Name: 0" + name + "!!!");
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
                        int movementRange = int.Parse(characterStats[10]);

						//sprite fields
						String textureFile = characterStats[11];

						//Texture
						Texture2D sprite = null;
						using(Stream imgStream = File.OpenRead("Content\\CharacterSprites\\" + textureFile)){
							sprite = Texture2D.FromStream(game.GraphicsDevice,imgStream);
						}

						//item
						String itemName = characterStats[12];
						Item charItem = null;
                        if (itemList.ContainsKey(itemName) || itemList.ContainsKey(itemName.ToLower()))
						{
                            if (itemList[itemName] != null)
                            {
                                charItem = itemList[itemName];
                            }
                            else if (itemList[itemName.ToLower()] != null)
                            {
                                charItem = itemList[itemName.ToLower()];
                            }
						}

						//rectangle
						int x = int.Parse(characterStats[13]);
						int y = int.Parse(characterStats[14]);
						int width = int.Parse(characterStats[15]);
						int height = int.Parse(characterStats[16]);
						Rectangle position = new Rectangle(x,y,width,height);

						//active/on screen
						bool active = bool.Parse(characterStats[17]);

						if(characterStats.Length > 18)
						{
							int xpAwarded = int.Parse(characterStats[18]);
							//create enemy
							Enemy tempEnemy = new Enemy(maxHealth,health,mana,speed,strength,accuracy,movementRange,level,recruitable,xpAwarded,sprite,position,active,name);
							tempEnemy.EquipItem = charItem;
							characterList.Add(name,tempEnemy);
						} else
						{
							//Builds character
                            Character tempCharacter = new Character(maxHealth, health, mana, speed, strength, accuracy, movementRange, level, recruitable, sprite, position, active, name);
							tempCharacter.EquipItem = charItem;
							characterList.Add(name,tempCharacter);
						}
					}
				}
				#endregion

				using(StreamReader input = new StreamReader("Content\\Data\\Tasks.txt"))
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

			} catch(Exception e)
			{
				Console.WriteLine("\n\n\t" + e.Message + "\n\n");
			}
        }

        /// <summary>
        /// Loads a savefile.
        /// </summary>
        /// <param name="filename">The name of the file to load.</param>
		static public void LoadSavefile(String filename,Dictionary<String,Item> itemList,List<Task> taskList)
		{
			#region texture
			Texture2D[] sprites = null;
			try
            {
				//Load textures
				String[] files = Directory.GetFiles("Content\\PlayerSprites");
				//Counts how many textures are character sprites
				int textureCount = 0;
				for(int i = 0; i < files.Length; i++){
					String filepath = files[i].Substring(22);
					int index = -1;
                    if ((filepath.Substring(0, 6) == "Player") && int.TryParse(filepath.Substring(6, 1), out index))
                    {
							textureCount++;
						}
				}
				//Setting texures
				sprites = new Texture2D[textureCount];
				for(int i = 0; i < files.Length; i++)
				{
					String filepath = files[i];
                    String shortpath = filepath.Substring(22);
					using(Stream imgStream = File.OpenRead(filepath))
					{
						int index = -1;
						if((shortpath.Substring(0,6) == "Player") && int.TryParse(shortpath.Substring(6,1),out index))
						{
							sprites[index] = Texture2D.FromStream(game.GraphicsDevice,imgStream);
						}
					}
				}
			#endregion

				using(Stream inStream = File.OpenRead("Data\\Savefiles\\" + filename))
				{
					using(BinaryReader input = new BinaryReader(inStream))
					{
						#region player
						Party.Clear();
						int partyCount = input.ReadInt32();

						for(int i = 0; i < partyCount; i++)
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
                            int moverange = input.ReadInt32();

							//Item
							String itemName = input.ReadString();
							Item charItem = null;
							if(itemList.ContainsKey(itemName))
							{
								charItem = itemList[itemName];
							}

							//Rectangle
							int x = input.ReadInt32();
							int y = input.ReadInt32();
							int width = input.ReadInt32();
							int height = input.ReadInt32();
							Rectangle position = new Rectangle(x,y,width,height);

							//Booleans
							bool active = input.ReadBoolean();

							//instantiate player
							Player tempPlayer = new Player(maxHealth,health,mana,speed,strength,accuracy, moverange,level,true,deathsAllowed,sprites[i],position,active,name);
							tempPlayer.NumDeaths = numDeaths;
							tempPlayer.EquipItem = charItem;
							Party.Add(tempPlayer);
						}
						#endregion	

						#region inventory
						Inventory.Clear();

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
                            int range = input.ReadInt32();

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

							//Add to inventory
							Item tempItem = new Item(type,healing,damage,range,description,collected,sprite,position,active,true,name);
							Inventory.AddItem(tempItem);
						}
						#endregion

						#region tasks
						TaskManager.Clear();
						int numTasks = input.ReadInt32();
						for(int i = 0; i < numTasks; i++)
						{
							String typeString = input.ReadString();
							TaskType type = (TaskType)(System.Enum.Parse(typeof(TaskType),typeString,true));
							String description = input.ReadString();
							String target = input.ReadString();
							int reward = input.ReadInt32();
							bool completed = input.ReadBoolean();

							Task tempTask = new Task(type,description,target,reward);
							tempTask.Completed = completed;
							TaskManager.AddTask(tempTask);
						}
						#endregion

						String stateString = input.ReadString();
                        switch (stateString)
                        {
                            case "Main": StateManager.OpenState(game.MainMenu);
                                break;
                            case "World": StateManager.OpenState(game.WorldMap);
                                break;
                            case "Local": StateManager.OpenState(game.LocalMap);
                                break;
                            case "Combat": StateManager.OpenState(game.Combat);
                                break;
                            default: StateManager.OpenState(game.MainMenu);
                                break;
                        }
					}
				}
			} catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
        }

		/// <summary>
		/// Loads a map.
		/// </summary>
		/// <param name="filename">The file name for the map.</param>
		/// <returns></returns>
        static public Map LoadMap(string filename)
        {
            Map loadingMap;
            int width = 0;
            int height = 0;
            int tilewidth = 0;
            int tileheight = 0;
            string tilesetname = "";
            int[,] groundLayer = new int[width, height];
            int[,] ground2Layer = new int[width, height];
            int[,] colliderLayer = new int[width, height];
            using (StreamReader input = new StreamReader("Content\\Maps\\" + filename))
            {
                string currentLine;
                int lineIndex = 0;
                int dataIndex = 0;
                string layerType = "";
                bool readingGroundData = false;
                bool readingGround2Data = false;
                bool readingColliderData = false;
                while ((currentLine = input.ReadLine()) != null)
                {

                    if (currentLine.Contains("width") && !currentLine.Contains("tile"))
                    {
                        width = Convert.ToInt32(currentLine.Substring(currentLine.IndexOf("=") + 1));
                    }
                    if (currentLine.Contains("height") && !currentLine.Contains("tile"))
                    {
                        height = Convert.ToInt32(currentLine.Substring(currentLine.IndexOf("=") + 1));
                        groundLayer = new int[width, height];
                        ground2Layer = new int[width, height];
                        colliderLayer = new int[width, height];
                    }
                    if (currentLine.Contains("tilewidth"))
                    {
                        tilewidth = Convert.ToInt32(currentLine.Substring(currentLine.IndexOf("=") + 1));
                    }
                    if (currentLine.Contains("tileheight"))
                    {
                        tileheight = Convert.ToInt32(currentLine.Substring(currentLine.IndexOf("=") + 1));
                    }
                    if (currentLine.Contains("tileset") && !currentLine.Contains("["))
                    {
                        int i = 0;
                        while ((i = currentLine.IndexOf('/', i)) != -1)
                        {
                            tilesetname = currentLine.Substring((i+1), currentLine.IndexOf(",")-(i+1));
                            Console.WriteLine(tilesetname);
                            i++;
                        }
                    }
                    if (currentLine.Contains("type=Ground"))
                    {
                        layerType = "Ground";
                    }
                    if (currentLine.Contains("type=TwoGround"))
                    {
                        layerType = "Ground2";
                    }
                    if (currentLine.Contains("type=Collider"))
                    {
                        layerType = "Collider";
                    }
                    if (currentLine.Contains("data="))
                    {
                        dataIndex = 0;
                        if (layerType.Equals("Ground"))
                        {
                            readingGroundData = true;
                        }
                        if (layerType.Equals("Ground2"))
                        {
                            readingGround2Data = true;
                        }
                        if (layerType.Equals("Collider"))
                        {
                            readingColliderData = true;
                        }
                    }
                    //In ground data block
                    if (readingGroundData)
                    {
                        if (dataIndex < height)
                        {
                            if (!currentLine.Contains("="))
                            {
                                string[] tempArray = currentLine.Split(',');
                                for (int i = 0; i < width; i++)
                                {
                                    groundLayer[i, dataIndex] = Convert.ToInt32(tempArray[i]);
                                }
                                dataIndex++;
                            }
                        }
                        else
                        {
                            dataIndex = 0;
                            readingGroundData = false;
                        }
                    }
                    if (readingGround2Data)
                    {
                        if (dataIndex < height)
                        {
                            if (!currentLine.Contains("="))
                            {
                                string[] tempArray = currentLine.Split(',');
                                for (int i = 0; i < width; i++)
                                {
                                    ground2Layer[i, dataIndex] = Convert.ToInt32(tempArray[i]);
                                }
                                dataIndex++;
                            }
                        }
                        else
                        {
                            dataIndex = 0;
                            readingGround2Data = false;
                        }
                    }
                    //In collider data block
                    if (readingColliderData)
                    {
                        if (dataIndex < height)
                        {
                            if (!currentLine.Contains("="))
                            {
                                string[] tempArray = currentLine.Split(',');
                                for (int i = 0; i < width; i++)
                                {
                                    colliderLayer[i, dataIndex] = Convert.ToInt32(tempArray[i]);
                                }
                                dataIndex++;
                            }
                        }
                        else
                        {
                            dataIndex = 0;
                            readingColliderData = false;
                        }
                    }
                }
            }     
            loadingMap = new Map(width, height, tilewidth, tileheight, groundLayer, ground2Layer, colliderLayer, tilesetname);
            loadingMap.LoadTileset(game);
            return loadingMap;
        }

		/// <summary>
		/// Searches items for matching names and returns the associated texture.
		/// </summary>
		/// <param name="name">The name of the </param>
		/// <param name="itemList">List of item in the game.</param>
		/// <returns>The appropriate sprite.</returns>
		static public Texture2D GetItemSprite(String name,Dictionary<String,Item> itemList)
		{
			if(itemList.ContainsKey(name))
			{
				return itemList[name].Texture;
			}
			return null;
		}
        #endregion
    }
}
