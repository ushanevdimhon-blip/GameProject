using GameProject.Components;
using Microsoft.Xna.Framework;
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
        Texture2D model;
        PositionComponent previousPosition;
        PositionComponent currentPosition;
        RenderComponent render;
        InputComponent input;
        public Rectangle collisionRectangle;

        float wigth;
        float height;
        public float Width { get { return wigth; } private set { wigth = value; } }
        public float Height { get { return height; } private set { height = value; } }
        public int Health {  get; private set; }

        public Player(Texture2D model, float x, float y, float scale)
        {
            this.model = model;
            this.wigth = model.Width * scale;
            this.height = model.Height * scale;
            currentPosition = new PositionComponent(x, y);
            render = new RenderComponent(model, scale);
            input = new InputComponent();
            Health = 100;
        }

        public void Update()
        {
            previousPosition = new PositionComponent(currentPosition.X, currentPosition.Y);
            input.Update(currentPosition);
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
