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
		Texture2D buttonSprite;

		#region Properties
		/// <summary>
		/// Gets the sprite for buttons.
		/// </summary>
		public Texture2D ButtonSprite
		{
			get { return buttonSprite; }
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
            StateCamera.RightCameraBound = map.Width*map.TileWidth;
            StateCamera.BottomCameraBound = map.Height * map.TileHeight;
            StateCamera.LeftCameraBound = 0;
            StateCamera.TopCameraBound = 0;
            buttonSprite = LoadManager.LoadSprite("GUI\\button-sprite.png");
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
            if (StateManager.KState.IsKeyDown(Keys.Escape))
            {
                if (!StateManager.KPrevious.IsKeyDown(Keys.Escape))
                {
                    StateManager.OpenState(StateGame.Pause);
                }
            }



			//For testing:
			if(StateManager.MState.LeftButton == ButtonState.Pressed && StateManager.MPrevious.LeftButton == ButtonState.Released && !(this is Combat))
			{
                if (Party.SelectedPlayer == null)
                {
                    Party.CheckForPlayers(map, StateManager.WorldMousePosition);
                }
                else
                {
                    (Party.SelectedPlayer as Character).Move(StateManager.WorldMousePosition);
                }
			}

            if (StateManager.MState.RightButton == ButtonState.Pressed && StateManager.MPrevious.RightButton == ButtonState.Released)
            {
                Party.UnselectPlayer();
            } 


            base.Update(time);
        }

        public override void DrawWorld(GameTime time, SpriteBatch spriteBatch)
        {
            map.Draw(time, spriteBatch);
            Party.DrawPartyOnMap(spriteBatch);
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
