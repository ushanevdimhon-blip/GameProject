using GameProject.Components;
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
        Random random;
        RenderComponent render;
        public PositionComponent previousPosition;
        public PositionComponent currentPosition;
        MoveComponent moveComponent;
        PatrolComponent patrol;
        float wigth;
        float height;
        public float Wigth { get { return wigth; } private set { wigth = value; } }
        public float Height { get { return height; } private set { height = value; } }

        public Enemy(Texture2D model, float scale)
        {
            this.wigth = model.Width* scale;
            this.height = model.Height* scale;
            random = new Random();
            render = new RenderComponent(model, scale);
            currentPosition = new PositionComponent((float)random.NextDouble()*500, (float)random.NextDouble() * 500);
            moveComponent = new MoveComponent(currentPosition);
            patrol = new PatrolComponent(moveComponent, currentPosition);
        }

        public void Update()
        {
            previousPosition = new PositionComponent(currentPosition.x, currentPosition.y);
            //patrol.Patrol();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            render.Draw(spriteBatch, currentPosition);
        }

        public void Block()
        {
            currentPosition.x = previousPosition.x;
            currentPosition.y = previousPosition.y;
        }
    }
}
