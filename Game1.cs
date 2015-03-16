//Brian Sandon

#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace SwiftSands
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
	{
		#region fields
		//Base fields
		GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		//Menu states
		MainMenu mainMenu;
		OptionsMenu options;
		PauseMenu pause;

		//Map states
		LocalMap localMap;
		WorldMap worldMap;
        Map testMap;

		//Data structures
		List<Character> characterList;
		List<Item> itemList;
		List<Task> taskList;
		Inventory inventory;

		//GUI
		Texture2D buttonSprite;
		SpriteFont font;
		Viewport viewport;

		//State variables
		MouseState mState;
		#endregion

        #region properties
        /// <summary>
        /// Gets main menu.
        /// </summary>
        internal MainMenu MainMenu
        {
            get { return mainMenu; }
        }

        /// <summary>
        /// Gets pause menu.
        /// </summary>
        internal PauseMenu Pause
        {
            get { return pause; }
        }

        /// <summary>
        /// Gets options menu.
        /// </summary>
        internal OptionsMenu Options
        {
            get { return options; }
        }

        /// <summary>
        /// Gets world map.
        /// </summary>
        internal WorldMap WorldMap
        {
            get { return worldMap; }
        }

        /// <summary>
        /// Gets local map.
        /// </summary>
        internal LocalMap LocalMap
        {
            get { return localMap; }
        }
        #endregion

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

		#region base methods
		/// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
			viewport = this.GraphicsDevice.Viewport;

			localMap = new LocalMap(this,viewport);
			worldMap = new WorldMap(this,viewport);

			characterList = new List<Character>();
			itemList = new List<Item>();
			taskList = new List<Task>();
			inventory = new Inventory();

			font = null;
			buttonSprite = null;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
			
            // TODO: use this.Content to load your game content here
			LoadManager.UpdateGame(this);

			LoadManager.LoadContent(ref characterList, ref itemList,ref buttonSprite, ref font);
            
			//Menus
            font = this.Content.Load<SpriteFont>("GUI/menuFont");
			mainMenu = new MainMenu(font,buttonSprite,this,viewport);
			options = new OptionsMenu(font,buttonSprite,this,viewport);
			pause = new PauseMenu(font,buttonSprite,this,viewport);

			//Buttons:
			//Main Menu Buttons
			mainMenu.Play.OnClick = ToMap;
			mainMenu.Load.OnClick = Load;
			mainMenu.Options.OnClick = ToOptions;
			mainMenu.Quit.OnClick = Exit;

			//Options buttons
			options.Resolution.OnClick = Resolution;
			options.Volume.OnClick = Volume;
			options.Back.OnClick = Back;
			
			//Pause buttons
			pause.Main.OnClick = ToMainMenu;
			pause.Options.OnClick = ToOptions;
			pause.Save.OnClick = Save;
			pause.Resume.OnClick = Back;
			pause.Quit.OnClick = Exit;

			StateManager.OpenState(mainMenu);
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			mState = Mouse.GetState();

            // TODO: Add your update logic here
			StateManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Cornsilk);

            // TODO: Add your drawing code here
			spriteBatch.Begin();
			StateManager.Draw(gameTime,spriteBatch);
            //testMap.Draw(gameTime, spriteBatch);
			spriteBatch.Draw(buttonSprite,new Rectangle(mState.X,mState.Y,5,5),Color.Black);
			spriteBatch.End();

            base.Draw(gameTime);
		}
		#endregion

		#region button methods
		
		/// <summary>
		/// Changes state to WorldMap
		/// </summary>
		private void ToMap()
		{
			StateManager.OpenState(localMap);
		}

		/// <summary>
		/// Calls loading system.
		/// </summary>
		private void Load()
		{
			LoadManager.LoadSavefile("Save1.data",inventory,itemList,taskList);
			StateManager.OpenState(worldMap);
		}

		/// <summary>
		/// Opens options menu.
		/// </summary>
		private void ToOptions()
		{
			StateManager.OpenState(options);
		}

		/// <summary>
		/// Prompts user on volume chnges.
		/// </summary>
		private void Volume()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Prompts user on resolution changes.
		/// </summary>
		private void Resolution()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns program to the last state.
		/// </summary>
		private void Back()
		{
			StateManager.CloseState();
		}

		/// <summary>
		/// Opens the main menu.
		/// </summary>
		private void ToMainMenu()
		{
			StateManager.OpenState(mainMenu);
		}

		/// <summary>
		/// Saves the file.
		/// </summary>
		private void Save()
		{
			SaveManager.Save("Save1.data");
		}
		#endregion
	}
}
