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
        bool[,] tintedTiles;
        Random rand = new Random();
        internal Character selectedCharacter;
        string mapName = "desert";

		#region Properties
		/// <summary>
		/// Gets the sprite for buttons.
		/// </summary>
		public Texture2D ButtonSprite
		{
			get { return buttonSprite; }
		}

        public Character SelectedCharacter
        {
            get
            {
                return selectedCharacter;
            }
            set
            {
                if (selectedCharacter != null)
                {
                    selectedCharacter.Selected = false;
                }
                selectedCharacter = value;
                if (selectedCharacter != null)
                {
                    selectedCharacter.Selected = true;
                }
            }
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

        public LocalMap(string map, Game1 game, Viewport port)
            : base(game, port)
        {
            mapName = map;
        }

        public override void OnEnter()
        {
            map = LoadManager.LoadMap(mapName + ".txt");
            tintedTiles = new bool[map.Width,Map.Height];
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

            if (StateManager.KState.IsKeyDown(Keys.I) && StateManager.KPrevious.IsKeyUp(Keys.I))
            {
                StateManager.OpenState(StateGame.InventoryMenu);
            }

            if (StateManager.KState.IsKeyDown(Keys.P) && StateManager.KPrevious.IsKeyUp(Keys.P))
            {
                StateManager.OpenState(StateGame.PartyMenu);
            }

            for (int r = 0; r < Map.Width; r++)
            {
                for (int c = 0; c < Map.Height; c++)
                {
                    if (tintedTiles[r, c])
                    {
                        Map.TintTile(new Vector2(r, c), Color.White);
                        tintedTiles[r, c] = false;
                    }
                }
            }
            if (Map.InBounds((int)StateManager.TileMousePosition.X, (int)StateManager.TileMousePosition.Y))
            {
                if (Map.ColliderLayer[(int)StateManager.TileMousePosition.X, (int)StateManager.TileMousePosition.Y] > 0)
                {
                        Map.TintTile(StateManager.TileMousePosition, new Color(255, 210, 210));
                }
                else
                {
                    if (Map.ColorLayer[(int)StateManager.TileMousePosition.X, (int)StateManager.TileMousePosition.Y] == Color.LightGreen)
                    {
                        Map.TintTile(StateManager.TileMousePosition, new Color(38, 205, 255));
                    }
                    else if (Map.ColorLayer[(int)StateManager.TileMousePosition.X, (int)StateManager.TileMousePosition.Y] == new Color(38, 205, 255))
                    {
                        Map.TintTile(StateManager.TileMousePosition, new Color(38, 205, 255));
                    }
                    else
                    {
                        Map.TintTile(StateManager.TileMousePosition, new Color(210, 210, 255));
                    }
                }
                if (Map.ColorLayer[(int)StateManager.TileMousePosition.X, (int)StateManager.TileMousePosition.Y] != Color.LightGreen)
                {
                    if (Map.ColorLayer[(int)StateManager.TileMousePosition.X, (int)StateManager.TileMousePosition.Y] != new Color(38, 205, 255))
                    {
                        tintedTiles[(int)StateManager.TileMousePosition.X, (int)StateManager.TileMousePosition.Y] = true;
                    }
                }
            }

			//For testing:
			if(StateManager.MState.LeftButton == ButtonState.Pressed && StateManager.MPrevious.LeftButton == ButtonState.Released && !(this is Combat))
			{
                if (SelectedCharacter == null)
                {
                    SelectedCharacter = Party.CheckForPlayers();
                }
                else
                {
                    SelectedCharacter.Move(StateManager.TileMousePosition);
                }
			}

            if (StateManager.MState.RightButton == ButtonState.Pressed && StateManager.MPrevious.RightButton == ButtonState.Released)
            {
                SelectedCharacter = null;
                Map.RemoveTints();
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
