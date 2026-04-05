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
        public PositionComponent previousPosition;
        public PositionComponent currentPosition;
        public RenderComponent render;
        public InputComponent input;
        float wigth;
        float height;
        public float Wigth { get { return wigth; } private set { wigth = value; } }
        public float Height { get { return height; } private set { height = value; } }
        public int Health {  get; private set; }

        public Player(Texture2D model, float x, float y, float scale)
        {
            this.wigth = model.Width * scale;
            this.height = model.Height * scale;
            currentPosition = new PositionComponent(x, y);
            render = new RenderComponent(model, scale);
            input = new InputComponent();
            Health = 100;
        }

        public void Update()
        {
            previousPosition = new PositionComponent(currentPosition.x, currentPosition.y);
            input.Update(currentPosition);
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
