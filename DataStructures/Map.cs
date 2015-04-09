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
        Color[,] colorLayer;
        Texture2D tileset;

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public int TileWidth
        {
            get { return tilewidth; }
        }

        public int TileHeight
        {
            get { return tileheight; }
        }

		public int[,] ColliderLayer
		{
			get { return colliderLayer; }
		}

        public Color[,] ColorLayer
        {
            get { return colorLayer; }
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
            colorLayer = new Color[width,height];
            for (int i = 0; i < width; i++)
            {
                for (int c = 0; c < height; c++)
                {
                    colorLayer[i, c] = Color.White;
                }
            }
                this.tilesetName = tilesetname;
        }

        public Map(int pWidth, int pHeight, int pTileWidth, int pTileHeight, string tilesetname)
        {
            width = pWidth;
            height = pHeight;
            tilewidth = pTileWidth;
            tileheight = pTileHeight;
            colorLayer = new Color[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int c = 0; c < height; c++)
                {
                    colorLayer[i, c] = Color.White;
                }
            }
            this.tilesetName = tilesetname;
        }

        public Map(int pWidth, int pHeight, int pTileWidth, int pTileHeight, Texture2D tileset)
        {
            width = pWidth;
            height = pHeight;
            tilewidth = pTileWidth;
            tileheight = pTileHeight;
            colorLayer = new Color[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int c = 0; c < height; c++)
                {
                    colorLayer[i, c] = Color.White;
                }
            }
            this.tileset = tileset;
        }

        public void LoadTileset(Game1 game)
        {
            tileset = game.Content.Load<Texture2D>("Tilesets/"+tilesetName);
        }

        public void TintTile(Vector2 tilePos, Color col)
        {
            if (InBounds((int)tilePos.X, (int)tilePos.Y))
            {
                colorLayer[(int)tilePos.X, (int)tilePos.Y] = col;
            }
        }

        public void Draw(GameTime time, SpriteBatch spriteBatch)
        {
            for (int r = 0; r < width; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    if (groundLayer[r, c] - 1 > 0)
                    {
                        spriteBatch.Draw(tileset, new Rectangle(r * tilewidth, c * tileheight, tilewidth, tileheight), new Rectangle(((groundLayer[r, c] - 1) % (8) * tilewidth), (((groundLayer[r, c] - 1) / 8) * tileheight), tilewidth, tileheight), ColorLayer[r,c]);
                    }
                    if (colliderLayer[r, c] - 1 > 0)
                    {
                        spriteBatch.Draw(tileset, new Rectangle(r * tilewidth, c * tileheight, tilewidth, tileheight), new Rectangle(((colliderLayer[r, c] - 1) % (8) * tilewidth), (((colliderLayer[r, c] - 1) / 8) * tileheight), tilewidth, tileheight), ColorLayer[r, c]);
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
            Vector2 newPosition = position;
            newPosition = Vector2.Transform(newPosition, camera.InverseTransform);
            int x = (int)(Math.Floor((newPosition.X) / tilewidth));
            int y = (int)(Math.Floor((newPosition.Y) / tilewidth));
			return new Vector2(x,y);
		}

		/// <summary>
		/// Translates a rectangle's coodinates into tile coodinates.
		/// </summary>
		/// <param name="position">The rectangle coodinates in terms of the world.</param>
		/// <returns>The mouse coodunates in terms of the tile system.</returns>
		public Rectangle ConvertPosition(Rectangle position, Camera camera)
		{
            Vector2 newPosition = new Vector2(position.X, position.Y);
            newPosition = Vector2.Transform(new Vector2(position.X, position.Y), camera.InverseTransform);
            int x = (int)(Math.Floor((newPosition.X / tilewidth)));
            int y = (int)(Math.Floor((newPosition.Y / tilewidth)));
            int width = tilewidth;
            int height = tileheight;
			return new Rectangle(x,y,width,height);
		}

        public Boolean TileCollide(Vector2 pos)
        {
            Console.Out.WriteLine((int) pos.X + " " + (int) pos.Y);
            if (pos.X > -1 && pos.X < width && pos.Y > -1 && pos.Y < height)
            {
                if (colliderLayer[(int)pos.X, (int)pos.Y] > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
           
        }

        public Boolean TileCollide(int x, int y)
        {
            if (colliderLayer[x, y] > 0)
            {
                return true;
            }
            else return false;
        }

		/// <summary>
		/// Checks if coodinate is in bounds.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coodinate.</param>
		/// <returns></returns>
		public bool InBounds(int x, int y){
			return (x >= 0 && x < colliderLayer.GetLength(0)) && (y >= 0 && y < colliderLayer.GetLength(1));
		}
    }
}
