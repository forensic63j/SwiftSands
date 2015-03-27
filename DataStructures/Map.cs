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
    class Map
    {
        int width;
        int height;
        int tilewidth;
        int tileheight;
        string tilesetName;
        int[,] groundLayer;
        int[,] colliderLayer;
        Texture2D tileset;

		public int[,] ColliderLayer
		{
			get { return colliderLayer; }
		}

        public Texture2D Tileset{
            get{
                return tileset;    
            }
            set{
                tileset = value;
            }
        }

        public Map(int pWidth, int pHeight, int pTileWidth, int pTileHeight, int[,] groundTiles, int[,] colliderTiles, string tilesetname)
        {
            width = pWidth;
            height = pHeight;
            tilewidth = pTileWidth;
            tileheight = pTileHeight;
            groundLayer = groundTiles;
            colliderLayer = colliderTiles;
            this.tilesetName = tilesetname;
        }

        public Map(int pWidth, int pHeight, int pTileWidth, int pTileHeight, string tilesetname)
        {
            width = pWidth;
            height = pHeight;
            tilewidth = pTileWidth;
            tileheight = pTileHeight;
            this.tilesetName = tilesetname;
        }

        public Map(int pWidth, int pHeight, int pTileWidth, int pTileHeight, Texture2D tileset)
        {
            width = pWidth;
            height = pHeight;
            tilewidth = pTileWidth;
            tileheight = pTileHeight;
            this.tileset = tileset;
        }

        public void LoadTileset(Game1 game)
        {
            tileset = game.Content.Load<Texture2D>("Tilesets/"+tilesetName);
        }

        public void Draw(GameTime time, SpriteBatch spriteBatch)
        {
            for (int r = 0; r < width; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    if (groundLayer[r, c] - 1 >= 0)
                    {
                        spriteBatch.Draw(tileset, new Rectangle(r * tilewidth, c * tileheight, tilewidth, tileheight), new Rectangle(((groundLayer[r, c] - 1) % (8) * tilewidth), (((groundLayer[r, c] - 1) / 8) * tileheight), tilewidth, tileheight), Color.White);
                    }
                    if (colliderLayer[r, c] - 1 >= 0)
                    {
                        spriteBatch.Draw(tileset, new Rectangle(r * tilewidth, c * tileheight, tilewidth, tileheight), new Rectangle(((colliderLayer[r, c] - 1) % (8) * tilewidth), (((colliderLayer[r, c] - 1) / 8) * tileheight), tilewidth, tileheight), Color.White);
                    }
                }
            }
        }

		/// <summary>
		/// Translates a vector's coodinates into tile coodinates.
		/// </summary>
		/// <param name="position">The vector coodinates in terms of the world.</param>
		/// <returns>The mouse coodunates in terms of the tile system.</returns>
		public Vector2 ConvertPosition(Vector2 position,Camera camera)
		{
			float x = (float)(Math.Round((position.X - camera.Position.X) / tilewidth));
			float y = (float)(Math.Round((position.Y - camera.Position.Y)/ tilewidth));
			return new Vector2(x,y);
		}

		/// <summary>
		/// Translates a rectangle's coodinates into tile coodinates.
		/// </summary>
		/// <param name="position">The rectangle coodinates in terms of the world.</param>
		/// <returns>The mouse coodunates in terms of the tile system.</returns>
		public Rectangle ConvertPosition(Rectangle position,Camera camera)
		{
			int x = (int)(Math.Round((decimal)(position.X / tilewidth)));
			int y = (int)(Math.Round((decimal)(position.Y / tilewidth)));
			int width = (int)(Math.Round((decimal)(position.Width / tilewidth)) + 1);
			int height =  (int)(Math.Round((decimal)(position.Height / tileheight)) + 1);
			return new Rectangle(x,y,width,height);
		}

		/// <summary>
		/// Checks if coodinate is in bounds.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coodinate.</param>
		/// <returns></returns>
		public bool InBounds(int x, int y){
			return (x >= 0 && x < colliderLayer.GetLength(0)) && (x >= 0 && x < colliderLayer.GetLength(1));
		}
    }
}
