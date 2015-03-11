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

		//Data lists
		List<Character> characterList;
		List<Item> itemsList;

		//GUI
		Texture2D buttonSprite;
		SpriteFont font;
		Viewport viewport;
		#endregion

		public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

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
			itemsList = new List<Item>();

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
			LoadManager.LoadContent(ref characterList, ref itemsList,ref buttonSprite, ref font);

			mainMenu = new MainMenu(font,buttonSprite,this,viewport);
			options = new OptionsMenu(font,buttonSprite,this,viewport);
			pause = new PauseMenu(font,buttonSprite,this,viewport);
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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
