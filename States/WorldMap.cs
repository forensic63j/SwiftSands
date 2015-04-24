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
        Random rand = new Random();
       

         public Map Map
         {
             get { return map; }
         }

        public override void OnEnter()
        {
            map = LoadManager.LoadMap("overworld.txt");
            tintedTiles = new bool[map.Width, Map.Height];
            Party.PartyList[0].TilePosition = Party.WorldTilePosition;
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

            if (StateManager.KState.IsKeyDown(Keys.I) && StateManager.KPrevious.IsKeyUp(Keys.I))
            {
                StateManager.OpenState(StateGame.InventoryMenu);
            }

			if(StateManager.KState.IsKeyDown(Keys.P) && StateManager.KPrevious.IsKeyUp(Keys.P))
			{
				StateManager.OpenState(StateGame.PartyMenu);
			}

            if (StateManager.KState.IsKeyDown(Keys.T) && StateManager.KPrevious.IsKeyUp(Keys.T))
            {
                StateManager.OpenState(StateGame.TaskMenu);
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
                    if (Party.Move(StateManager.TileMousePosition))
                    {
                        if (Roll(0.25f))
                        {
                            Party.SelectedPlayer.Selected = false;
                            Party.SelectedPlayer = null;
                            List<Enemy> enList = new List<Enemy>();
                            enList.Add(base.StateGame.CharacterList["enemy"] as Enemy);
                            Combat newCombat = new Combat(base.StateGame, base.ViewPort, enList);
                            StateManager.OpenState(newCombat);
                        }
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

        public bool Roll(float chance)
        {

            if (chance > 1)
            {
                chance = 1;
            }
            if (chance < 0)
            {
                chance = 0;
            }
            float testVal = (float)rand.Next(0, 101) / 100f;
            Console.Out.WriteLine("Rolled a " + testVal + " with a chance of " + chance);
            if (testVal < chance)
            {
                return true;
            }
            return false;
        }
    }
}
