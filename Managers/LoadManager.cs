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
        public void LoadContent()
        {

        }

        /// <summary>
        /// Loads a savefile.
        /// </summary>
        /// <param name="filename">The name of the file to load.</param>
        public void LoadSavefile(String filename, ref Party players)
        {
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

				//Setting players
				using(Stream inStream = File.OpenRead(filename))
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
