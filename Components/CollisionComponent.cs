using Microsoft.Xna.Framework;
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
    }
}
