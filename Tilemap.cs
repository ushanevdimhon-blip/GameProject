using GameProject.Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Tilemap
    {
        public Tile[,] tiles;
        Texture2D wallTexture;
        Texture2D floorTexture;
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }

        public Tilemap(string[,] tileData, int tileWidth, int tileHeight, Texture2D wallTexture, Texture2D floorTexture)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            this.wallTexture = wallTexture;
            this.floorTexture = floorTexture;
            tiles = new Tile[tileData.GetLength(0), tileData.GetLength(1)];

            for (int i = 0; i < tileData.GetLength(0); i++)
            {
                for (int j = 0; j < tileData.GetLength(1); j++)
                {
                    float x = tileWidth / 2 + j * tileWidth;
                    float y = tileHeight / 2 + i * tileHeight;

                    if (tileData[i, j] == "01")
                    {
                        var tile = new Tile(tileWidth, tileHeight, new PositionComponent(x, y), wallTexture);
                        tile.IsWall = true;
                        tiles[i, j] = tile;
                    }                  
                    else if (tileData[i, j] == "09")
                    {
                        var tile = new Tile(tileWidth, tileHeight, new PositionComponent(x, y), floorTexture);
                        tile.IsFloor = true;
                        tiles[i, j] = tile;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, int startCol, int endCol, int startRow, int endRow)
        {
            for (int i = startRow; i < endRow; i++)
            {
                for (int j = startCol; j < endCol; j++)
                {
                    var tile = tiles[i, j];
                    tile.Draw(spriteBatch);
                }
            }
        }
    }
}
