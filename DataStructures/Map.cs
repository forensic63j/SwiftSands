using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftSands
{
    class Map
    {
        int width;
        int height;
        int tilewidth;
        int tileheight;

        string tileset;

        public Map(int pWidth, int pHeight, int pTileWidth, int pTileHeight)
        {
            width = pWidth;
            height = pHeight;
            tilewidth = pTileWidth;
            tileheight = pTileHeight;
        }

        public Map()
        {

        }
    }
}
