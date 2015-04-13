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
    class WorldMap : State
    {
        Map map;
        public WorldMap(Game1 game, Viewport port) : base(game, port) { }
        bool[,] tintedTiles;
       

         public Map Map
         {
             get { return map; }
         }

        public override void OnEnter()
        {
            map = LoadManager.LoadMap("desert.txt");
            tintedTiles = new bool[map.Width, Map.Height];
            StateCamera.RightCameraBound = map.Width * map.TileWidth;
            StateCamera.BottomCameraBound = map.Height * map.TileHeight;
            StateCamera.LeftCameraBound = 0;
            StateCamera.TopCameraBound = 0;
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
                        Console.Out.WriteLine("LIGHT GREEN! SET TO TEAL");
                        Map.TintTile(StateManager.TileMousePosition, new Color(38, 205, 255));
                    }
                    else if (Map.ColorLayer[(int)StateManager.TileMousePosition.X, (int)StateManager.TileMousePosition.Y] == new Color(38, 205, 255))
                    {
                        Console.Out.WriteLine("TEAL! KEEP TEAL!");
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
            if (StateManager.MState.LeftButton == ButtonState.Pressed && StateManager.MPrevious.LeftButton == ButtonState.Released && !(this is Combat))
            {
                if (Party.SelectedPlayer == null)
                {
                    Party.CheckForMainCharacter(map, StateManager.WorldMousePosition);
                }
                else
                {
                    if ((Party.SelectedPlayer as Character).Move(StateManager.TileMousePosition))
                    {
                        
                    }
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
            // TODO: Add your drawing code here  
            map.Draw(time, spriteBatch);
            Party.DrawMainCharacterOnMap(spriteBatch);
            base.Draw(time, spriteBatch);
        }

        public override void DrawScreen(GameTime time, SpriteBatch spriteBatch)
        {
            // TODO: Add your drawing code here          
            base.Draw(time, spriteBatch);
        }

        public void RollForCombat()
        {

        }
    }
}
