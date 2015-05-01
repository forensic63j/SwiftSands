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
        InventoryMenu inventoryMenu;
        PartyMenu partyMenu;
        TaskMenu taskMenu;

		/**/
		Combat combat;/**/

		//Map states
		LocalMap localMap;
		WorldMap worldMap;
        Map testMap;

		//Data structures
		Dictionary<String,Character> characterList;
		Dictionary<String,Item> itemList;
		List<Task> taskList;

		//GUI
        Texture2D buttonSprite;
		SpriteFont font;
		Viewport viewport;

        Texture2D pixTex;

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

        public Texture2D PixTex
        {
            get { return pixTex; }
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

		/// <summary>
		/// Gets inventory menu.
		/// </summary>
        /// <summary>
        /// Gets the inventory menu
        /// </summary>
        internal InventoryMenu InventoryMenu
        {
            get { return inventoryMenu; }
        }

		/// <summary>
		/// Gets party menu.
		/// </summary>
		internal PartyMenu PartyMenu
		{
			get { return partyMenu; }
		}

        /// <summary>
        /// Gets task menu
        /// </summary>
        internal TaskMenu TaskMenu
        {
            get { return taskMenu; }
        }
		
		/// <summary>
		/// Gets the character list.
		/// </summary>
		internal Dictionary<String,Character> CharacterList
		{
			get { return characterList; }
		}

        /// <summary>
        /// Gets the item list.
        /// </summary>
        internal Dictionary<String, Item> ItemList
        {
            get { return itemList; }
        }

        public SpriteFont Font
        {
            get { return font; }
        }

        public Texture2D ButtonSprite
        {
            get { return buttonSprite; }
        }

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
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

			characterList = new Dictionary<String,Character>();
			itemList = new Dictionary<String,Item>();
			taskList = new List<Task>();

			font = null;
			buttonSprite = null;

			base.IsMouseVisible = true;

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

            pixTex = new Texture2D(GraphicsDevice, 1, 1);
            pixTex.SetData<Color>(
            new Color[] { Color.White });

            //Testing
			Party.Add(this.CharacterList["Clayton"].ToPlayer());
            Party.Add(this.CharacterList["John"].ToPlayer());
            Party.Add(this.CharacterList["Brian"].ToPlayer());
           // Inventory.AddItem(new Item(ItemType.AttackSpell, 0, 15, 2, "A basic fire spell", true, null, new Rectangle(), false, false, "Fire Spell"));
            itemList.Add("Arrowhead", new Item(ItemType.AttackSpell, 0, 17, 2, "Although no longer attached to an arrow, it can still kill.", false,
                LoadManager.LoadSprite("ItemSprites//", "item-sprite.png"), new Rectangle(64, 0, 32, 32), true, true, "Arrowhead"));
            TaskManager.AddTask(new Task(TaskType.CollectItem, "Find and retrieve the Ice Spell", "Arrowhead", 50));
            TaskManager.AddTask(new Task(TaskType.Converse, "Talk to the old man", "Old Man", 5));
            characterList.Add("Old Man", new Character(false, null, new Rectangle(64, 0, 32, 32), true, "Old Man"));
            characterList["Old Man"].Conversation = "Well hello there, young man";

			//Menus
            font = this.Content.Load<SpriteFont>("GUI/menuFont");
			mainMenu = new MainMenu(font,buttonSprite,this,viewport);
			options = new OptionsMenu(font,buttonSprite,this,viewport);
			pause = new PauseMenu(font,buttonSprite,this,viewport);
			
			inventoryMenu = new InventoryMenu(font, buttonSprite, this, viewport);
			partyMenu = new PartyMenu(font,buttonSprite,this,viewport);
            taskMenu = new TaskMenu(font, buttonSprite, this, viewport);

			/**/List<Enemy> enemies = new List<Enemy>();
			enemies.Add(characterList["enemy"] as Enemy);
            enemies[0].EquipItem = Character.DefaultItem;
            enemies[0].ExpAwarded = 15;
			combat = new Combat(this,viewport,enemies);/**/

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

            TextBox.Instance.Font = this.font;
            TextBox.Instance.IsActive = false;
            TextBox.Instance.Texture = buttonSprite;

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
            StateManager.Draw(gameTime, spriteBatch);
            // TODO: Add your drawing code here
            base.Draw(gameTime);
		}
		#endregion

		#region button methods
		
		/// <summary>
		/// Changes state to WorldMap
		/// </summary>
		private void ToMap()
		{
			StateManager.OpenState(worldMap);
		}

		/// <summary>
		/// Calls loading system.
		/// </summary>
		private void Load()
		{
			LoadManager.LoadSavefile("Save1.data",itemList,taskList);
			//StateManager.OpenState(combat);//Changed temporalily for testing purposeses.
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
