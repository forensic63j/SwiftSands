//Clayton Scavone

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace SwiftSands
{
  
    class LocalMap:State
	{
		#region map
		Map map;
		MouseState mState;
		Texture2D buttonSprite;

		Vector2 mouse;

		KeyboardState oldState;

		Character c;//For testing.

		#region Properties
		/// <summary>
		/// Gets the sprite for buttons.
		/// </summary>
		public Texture2D ButtonSprite
		{
			get { return buttonSprite; }
		}

		/// <summary>
		/// Gets the mouse coodinates relitive to the camara.
		/// </summary>
		public Vector2 MouseData
		{
			get { return mouse; }
		}

		/// <summary>
		/// Gets the map.
		/// </summary>
		public Map Map
		{
			get { return map; }
		}
		#endregion

		public LocalMap(Game1 game, Viewport port) : base(game, port) 
		{
			
		}

        public override void OnEnter()
        {
            map = LoadManager.LoadMap("desert.txt");
            buttonSprite = LoadManager.LoadSprite("GUI\\button-sprite.png");
			c = base.StateGame.CharacterList[0];
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnDestroy()
        {
            base.OnExit();
        }

        public override void Update(GameTime time)
        {
            StateManager.KState = Keyboard.GetState();
            if (StateManager.KState.IsKeyDown(Keys.Escape))
            {
                if (!StateManager.KPrevious.IsKeyDown(Keys.Escape))
                {
                    StateManager.OpenState(StateGame.Pause);
                }
            }

			mState = Mouse.GetState();
            mouse = new Vector2(mState.X, mState.Y);
            mouse = Vector2.Transform(mouse, Matrix.Transpose(StateCamera.Transform));

			//For testing:
			if(StateManager.MState.LeftButton == ButtonState.Pressed && StateManager.MPrevious.LeftButton == ButtonState.Released)
			{
				Console.WriteLine("Mouse: " + map.ConvertPosition(mouse,StateCamera));
				Console.WriteLine("Character: " + map.ConvertPosition(c.Position,StateCamera));
			} 


            base.Update(time);
        }

        public override void DrawWorld(GameTime time, SpriteBatch spriteBatch)
        {
            map.Draw(time, spriteBatch);
			c.Draw(spriteBatch);
            base.DrawWorld(time, spriteBatch);
        }

        public override void DrawScreen(GameTime time, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(buttonSprite, new Rectangle((int)mouse.X, (int)mouse.Y, 5, 5), Color.Black);
            base.DrawScreen(time, spriteBatch);
		}
		#endregion
	}
}
