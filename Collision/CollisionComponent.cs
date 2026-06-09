using GameProject.Components;
using GameProject.TilemapManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Collision
{
    public class CollisionComponent
    {
        public Rectangle collisionRectangle;
        public Circle collisionCircle;
        public DirectionCircle directionCircle;
        public int radius;
        public float width;
        public float height;
        public PositionComponent currentPosition;
        public Action<Tile> TileCollisionDetected;
        private Vector2 direction = Vector2.Zero;

        public CollisionComponent(PositionComponent currentPosition, float width, float height, int radius=0)
        {
            this.width = width;
            this.height = height;
            this.currentPosition = currentPosition;
            collisionRectangle = new Rectangle((int)(currentPosition.X - width / 2),
                (int)(currentPosition.Y - height / 2), (int)width, (int)height);
            if (radius > 0)
            {
                this.radius = radius;
                collisionCircle = new Circle(currentPosition, radius);
            }    
        }

        public void Update(Vector2 newDirection = default)
        {            
            UpdateRectangleCollision();
            if (!collisionCircle.IsNull)
            {
                direction = newDirection;
                UpdateCircleCollision();
            }           
        }

        private void UpdateRectangleCollision()
        {
            collisionRectangle.X = (int)(currentPosition.X - width / 2);
            collisionRectangle.Y = (int)(currentPosition.Y - height / 2);
        }

        private void UpdateCircleCollision()
        {
            collisionCircle = new Circle(currentPosition, radius);
            directionCircle = new DirectionCircle(collisionCircle, direction);
        }
    }
}
