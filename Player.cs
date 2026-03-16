using GameProject.Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Player
    {
        PositionComponent position;
        RenderComponent render;
        InputComponent input;

        public Player(Texture2D model, float x, float y)
        {
            position = new PositionComponent(x, y);
            render = new RenderComponent(model);
            input = new InputComponent();
        }

        public void Update()
        {
            input.Update(position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            render.Draw(spriteBatch, position);
        }
    }
}
