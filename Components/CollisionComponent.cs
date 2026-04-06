using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Components
{
    public class CollisionComponent
    {
        public Rectangle collisionRectangle;
        public float width;
        public float height;
        public PositionComponent currentPosition;

        public CollisionComponent(PositionComponent currentPosition, float width, float height)
        {
            this.width = width;
            this.height = height;
            this.currentPosition = currentPosition;
            collisionRectangle = new Rectangle((int)currentPosition.X,
                (int)currentPosition.Y, (int)width, (int)height);
        }

        public CollisionComponent(Rectangle collisionRectangle)
        {
            this.collisionRectangle = collisionRectangle;
        }

        public void Update()
        {
            collisionRectangle = new Rectangle((int)currentPosition.X,
                (int)currentPosition.Y, (int)width, (int)height);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            Texture2D pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            spriteBatch.Draw(pixel, new Rectangle(collisionRectangle.Left, 
                collisionRectangle.Top, collisionRectangle.Width, 1), color);
            spriteBatch.Draw(pixel, new Rectangle(collisionRectangle.Left, 
                collisionRectangle.Bottom - 1, collisionRectangle.Width, 1), color);
            spriteBatch.Draw(pixel, new Rectangle(collisionRectangle.Left, 
                collisionRectangle.Top, 1, collisionRectangle.Height), color);
            spriteBatch.Draw(pixel, new Rectangle(collisionRectangle.Right - 1, 
                collisionRectangle.Top, 1, collisionRectangle.Height), color);
        }
    }
}
