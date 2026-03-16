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
        PositionComponent position;
        MoveComponent moveComponent;
        PatrolComponent patrol;

        public Enemy(Texture2D model)
        {
            random = new Random();
            render = new RenderComponent(model, 0.03f);
            position = new PositionComponent((float)random.NextDouble()*500, (float)random.NextDouble() * 500);
            moveComponent = new MoveComponent(position);
            patrol = new PatrolComponent(moveComponent, position);
        }

        public void Update()
        {
            patrol.Patrol();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            render.Draw(spriteBatch, position);
        }
    }
}
