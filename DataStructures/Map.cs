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
                    spriteBatch.Draw(tileset, new Rectangle(r * tilewidth, c * tileheight, tilewidth, tileheight), new Rectangle(((groundLayer[r, c]-1) % (width / tilewidth) * tilewidth), ((groundLayer[r, c]-1) % (height / tileheight) * tilewidth), tilewidth, tileheight), Color.White);
                }
            }
        }
    }
}
