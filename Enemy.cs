using GameProject.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Enemy
    {
        Texture2D model;
        RenderComponent render;
        public PositionComponent currentPosition;
        PatrolComponent patrol;
        public CollisionComponent collision;
        ChaseComponent chaseComponent;

        float width;
        float height;
        /// <summary>
        /// ширина модели умноженная на масштаб
        /// </summary>
        public float Width { get { return width; } private set { width = value; } }
        /// <summary>
        /// высота модели умноженная на масштаб
        /// </summary>
        public float Height { get { return height; } private set { height = value; } }

        public Enemy(Texture2D model, float scale, Tilemap tilemap)
        {
            this.model = model;
            this.width = model.Width * scale;
            this.height = model.Height * scale;
            render = new RenderComponent(model, scale);
            currentPosition = new PositionComponent(200, 200);
            patrol = new PatrolComponent(tilemap);
            collision = new CollisionComponent(currentPosition, this.width*1.5f, this.height*1.5f, 150);
            chaseComponent = new ChaseComponent(tilemap, 0.1f, 150.0f);
        }

        public void Update()
        {
            collision.UpdateRectangleCollision();
            collision.UpdateCircleCollision();
        }

        public void Chase(PositionComponent playerPosition, GameTime gameTime)
        {
            chaseComponent.Chase(currentPosition, playerPosition, gameTime);
        }

        public void Patrol(List<(int X, int Y)> targetsPositions, GameTime gameTime)
        {
            patrol.Patrol(currentPosition, targetsPositions, gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            render.Draw(spriteBatch, currentPosition);
        }
    }
}
