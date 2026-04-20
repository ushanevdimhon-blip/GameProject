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
        public PositionComponent currentPosition;
        RenderComponent render;
        InputComponent input;
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
        public int Health {  get; private set; }

        public Player(Texture2D model, float x, float y, float scale)
        {
            this.model = model;
            this.width = model.Width * scale;
            this.height = model.Height * scale;
            currentPosition = new PositionComponent(x, y);
            render = new RenderComponent(model, scale);
            input = new InputComponent();
            collision = new CollisionComponent(currentPosition, this.width, this.height);
            
            Health = 100;
        }

        public void Update()
        {
            previousPosition = new PositionComponent(currentPosition.X, currentPosition.Y);
            input.Update(currentPosition);
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
