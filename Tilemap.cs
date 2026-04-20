using GameProject.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Tilemap
    {
        Tile[,] tiles;
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }

        public Tilemap(string[,] tileData, int tileWidth, int tileHeight)
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

                    var tile = new Tile();
                    tile.position = new PositionComponent(x, y);
                    //TODO: проверять чему равно tileData[i, j] и в зависимости от этого создавать разные типы тайлов
                    //TODO: решить вопрос с размерами тайлов, чтобы они закрывали весь экран
                    //RenderComponent.Draw() не надо менять, работает корректно
                    tiles[i, j] = tile;
                }
            }
        }
    }
}
