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
        Random random;
        RenderComponent render;
        PositionComponent previousPosition;
        PositionComponent currentPosition;
        MoveComponent moveComponent;
        PatrolComponent patrol;
        public CollisionComponent collision;

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

        public Enemy(Texture2D model, float scale)
        {
            this.model = model;
            this.width = model.Width* scale;
            this.height = model.Height* scale;
            random = new Random();
            render = new RenderComponent(model, scale);
            currentPosition = new PositionComponent((float)random.NextDouble()*400, (float)random.NextDouble() * 400);
            moveComponent = new MoveComponent(currentPosition);
            patrol = new PatrolComponent(moveComponent, currentPosition);
            collision = new CollisionComponent(currentPosition, this.width, this.height, 70);   
        }

        public void Update()
        {
            previousPosition = new PositionComponent(currentPosition.X, currentPosition.Y);
            //patrol.Patrol();
            collision.UpdateRectangleCollision();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            render.Draw(spriteBatch, currentPosition);
        }
 
        public void Block()
        {
            currentPosition.X = previousPosition.X;
            currentPosition.Y = previousPosition.Y;
        }
    }
}
