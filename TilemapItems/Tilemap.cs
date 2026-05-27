using GameProject.Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.TilemapItems
{
    public enum TileType
    {
        Wall,
        Floor,
        ClosedDoor,
        OpenDoor,
        Medicine,
        Key
    }
    public class Tilemap
    {
        public Tile[,] tiles;
        Texture2D wallTexture;
        Texture2D floorTexture;
        Texture2D doorTexture;

        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }

        public Tilemap(int tileWidth, int tileHeight, Texture2D wallTexture, 
            Texture2D floorTexture, Texture2D doorTexture)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            this.wallTexture = wallTexture;
            this.floorTexture = floorTexture;
            this.doorTexture = doorTexture;
        }

        public void Create(string[,] tileData)
        {
            tiles = new Tile[tileData.GetLength(0), tileData.GetLength(1)];

            for (int i = 0; i < tileData.GetLength(0); i++)
            {
                for (int j = 0; j < tileData.GetLength(1); j++)
                {
                    float x = TileWidth / 2 + j * TileWidth;
                    float y = TileHeight / 2 + i * TileHeight;

                    if (tileData[i, j] == "1")
                    {
                        var tile = new Tile((i, j), TileWidth, TileHeight, new PositionComponent(x, y), wallTexture);
                        tile.Type = TileType.Wall;
                        tile.Price = 10;
                        tiles[i, j] = tile;
                    }
                    else if (tileData[i, j] == "2")
                    {
                        var tile = new Tile((i, j), TileWidth, TileHeight, new PositionComponent(x, y), floorTexture);
                        tile.Type = TileType.Floor;
                        tile.Price = 1;
                        tiles[i, j] = tile;
                    }
                    else if (tileData[i, j] == "3")
                    {
                        var tile = new Tile((i, j), TileWidth, TileHeight, new PositionComponent(x, y), doorTexture);
                        tile.Type = TileType.ClosedDoor;
                        tile.Price = 10;
                        tiles[i, j] = tile;
                    }
                }
            }
        }

        public string[,] FromFile(string fileName)
        {
            var lines = System.IO.File.ReadAllLines(fileName);
            var tileData = new string[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                var values = lines[i];
                for (int j = 0; j < values.Length; j++)
                {
                    tileData[i, j] = values[j].ToString();
                }
            }
            return tileData;
        }

        public void SpaunItem(TileType tileType, Texture2D itemTexture, int itemCount)//пока спаунит только ключи
        {
            var rand = new Random();
            while (itemCount > 0)
            {
                int row = rand.Next(tiles.GetLength(0));
                int col = rand.Next(tiles.GetLength(1));
                if (tiles[row, col].Type == TileType.Floor)
                {
                    var tile = new Tile(
                        (row, col), 
                        TileWidth, 
                        TileHeight, 
                        new PositionComponent(tiles[row, col].position.X, tiles[row, col].position.Y), 
                        itemTexture);
                    tile.Type = tileType;
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
                    if (tiles[i, j].Type == TileType.Key)
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
                    if (tiles[i, j].Type == TileType.ClosedDoor)
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
                if (tiles[row, col].Type == TileType.Floor)
                {
                    return (col, row);
                }
                row = rand.Next(2, tiles.GetLength(0)-2);
                col = rand.Next(2, tiles.GetLength(1)-2);
            }
        }

        public void Update((int X, int Y) index, Texture2D newTexture, TileType newType)
        {
            tiles[index.Y, index.X] = new Tile(
                (index.Y, index.X),
                TileWidth,
                TileHeight,
                new PositionComponent(tiles[index.Y, index.X].position.X, tiles[index.Y, index.X].position.Y),
                newTexture);
            tiles[index.Y, index.X].Type = newType;
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
