using GameProject.Collision;
using GameProject.Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.TilemapManager
{
    public class Tile
    {
        Texture2D model;
        public PositionComponent position;
        public CollisionComponent collision;
        RenderComponent render;
        public TileType Type { get; set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        public (int X, int Y) TileIndex { get; set; }
        public int Price { get; set; }

        public Tile((int X, int Y) TileIndex, int tileWidth, int tileHeight, PositionComponent position, Texture2D model)
        {
            this.TileIndex = TileIndex;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            this.position = position;
            this.model = model;
            collision = new CollisionComponent(position, tileWidth, tileHeight);
            render = new RenderComponent(model, 1.0f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            render.DrawTile(spriteBatch, position, TileWidth, TileHeight);
        }
    }
}
