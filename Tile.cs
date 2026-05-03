using GameProject.Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Tile
    {
        Texture2D model;
        PositionComponent position;
        public CollisionComponent collision;
        RenderComponent render;
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        public bool IsWall { get; set; }
        public bool IsFloor { get; set; }

        public Tile(int tileWidth, int tileHeight, PositionComponent position, Texture2D model)
        {
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
