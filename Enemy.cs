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
        PatrolComponent patrol;

        public Enemy(Texture2D model)
        {
            random = new Random();
            render = new RenderComponent(model, 0.1f);
            position = new PositionComponent((float)random.NextDouble()*500, (float)random.NextDouble() * 500);
            patrol = new PatrolComponent();
        }

        public void Update()
        {
            patrol.Patrol(position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            render.Draw(spriteBatch, position);
        }
    }
}
