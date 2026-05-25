using GameProject.Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.TilemapItems
{
    public class Tilemap
    {
        public Tile[,] tiles;
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }

        public Tilemap(string[,] tileData, int tileWidth, int tileHeight, Texture2D wallTexture, 
            Texture2D floorTexture, Texture2D doorTexture)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            tiles = new Tile[tileData.GetLength(0), tileData.GetLength(1)];

            for (int i = 0; i < tileData.GetLength(0); i++)
            {
                for (int j = 0; j < tileData.GetLength(1); j++)
                {
                    float x = tileWidth / 2 + j * tileWidth;
                    float y = tileHeight / 2 + i * tileHeight;

                    if (tileData[i, j] == "01")
                    {
                        var tile = new Tile((i, j), tileWidth, tileHeight, new PositionComponent(x, y), wallTexture);
                        tile.IsWall = true;
                        tile.Price = 10;
                        tiles[i, j] = tile;
                    }                  
                    else if (tileData[i, j] == "09")
                    {
                        var tile = new Tile((i, j), tileWidth, tileHeight, new PositionComponent(x, y), floorTexture);
                        tile.IsFloor = true;
                        tile.Price = 1;
                        tiles[i, j] = tile;
                    }
                    else if (tileData[i, j] == "02")
                    {
                        var tile = new Tile((i, j), tileWidth, tileHeight, new PositionComponent(x, y), doorTexture);
                        tile.IsClosedDoor = true;
                        tile.Price = 10;
                        tiles[i, j] = tile;
                    }
                }
            }
        }

        public void SpaunItem(Texture2D itemTexture, int itemCount)//пока спаунит только ключи
        {
            var rand = new Random();
            while (itemCount > 0)
            {
                int row = rand.Next(tiles.GetLength(0));
                int col = rand.Next(tiles.GetLength(1));
                if (tiles[row, col].IsFloor)
                {
                    var tile = new Tile(
                        (row, col), 
                        TileWidth, 
                        TileHeight, 
                        new PositionComponent(tiles[row, col].position.X, tiles[row, col].position.Y), 
                        itemTexture);
                    tile.IsKey = true;
                    tile.Price = 5;
                    tiles[row, col] = tile;
                    itemCount--;
                }
            }
        }

        public List<(int X, int Y)> GetKeysIndexes()
        {
            var keysIndexes = new List<(int X, int Y)>();
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j].IsKey)
                    {
                        keysIndexes.Add((j, i));
                    }
                }
            }
            return keysIndexes;
        }

        public (int X, int Y) GetDoorIndex()
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j].IsClosedDoor)
                    {
                        return (j, i);
                    }
                }
            }
            return (-1, -1);
        }

        public (int X, int Y) GetRandomFloorTileIndex()//почему путает координаты и вылетает за пределы массива 
        {
            var rand = new Random();
            int row = rand.Next(2, tiles.GetLength(0)-2);
            int col = rand.Next(2, tiles.GetLength(1)-2);

            while (true)
            {
                if (tiles[row, col].IsFloor)
                {
                    return (col, row);
                }
                row = rand.Next(2, tiles.GetLength(0)-2);
                col = rand.Next(2, tiles.GetLength(1)-2);
            }
        }

        public void Update((int X, int Y) index, Texture2D newTexture)
        {
            tiles[index.Y, index.X] = new Tile(
                (index.Y, index.X),
                TileWidth,
                TileHeight,
                new PositionComponent(tiles[index.Y, index.X].position.X, tiles[index.Y, index.X].position.Y),
                newTexture);
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
