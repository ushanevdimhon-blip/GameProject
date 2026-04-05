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
        public Rectangle collisionRectangle;

        float width;
        float height;
        public float Width { get { return width; } private set { width = value; } }
        public float Height { get { return height; } private set { height = value; } }

        public Enemy(Texture2D model, float scale)
        {
            this.model = model;
            this.width = model.Width* scale;
            this.height = model.Height* scale;
            random = new Random();
            render = new RenderComponent(model, scale);
            currentPosition = new PositionComponent((float)random.NextDouble()*500, (float)random.NextDouble() * 500);
            moveComponent = new MoveComponent(currentPosition);
            patrol = new PatrolComponent(moveComponent, currentPosition);
        }

        public void Update()
        {
            previousPosition = new PositionComponent(currentPosition.X, currentPosition.Y);
            //patrol.Patrol();
            collisionRectangle = new Rectangle((int)currentPosition.X,
                (int)currentPosition.Y, (int)Width, (int)Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            render.Draw(spriteBatch, currentPosition);
        }
        /// <summary>
        /// останавливает объект, нужен для коллизий
        /// </summary>
        public void Block()
        {
            currentPosition.X = previousPosition.X;
            currentPosition.Y = previousPosition.Y;
        }
    }
}
