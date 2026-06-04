using GameProject.Collision;
using GameProject.TilemapItems;
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
        public Circle collisionCircle;
        public int radius;
        public float width;
        public float height;
        public PositionComponent currentPosition;
        public Action<Tile> TileCollisionDetected;

        public CollisionComponent(PositionComponent currentPosition, float width, float height, int radius=0)
        {
            this.width = width;
            this.height = height;
            this.currentPosition = currentPosition;
            collisionRectangle = new Rectangle((int)(currentPosition.X - width / 2),
                (int)(currentPosition.Y - height / 2), (int)width, (int)height);
            //(int)(currentPosition.X - width / 2),(int)(currentPosition.Y - height / 2) - это для того,
            //чтобы нарисовать прямоугольник, т.к. он рисуется от левого верхнего угла
            if (radius > 0)
            {
                this.radius = radius;
                collisionCircle = new Circle(currentPosition, radius);
            }    
        }

        public void Update()
        {
            UpdateRectangleCollision();
            if (!collisionCircle.IsNull)
                UpdateCircleCollision();
        }

        private void UpdateRectangleCollision()
        {
            collisionRectangle = new Rectangle((int)(currentPosition.X - width / 2),
                (int)(currentPosition.Y - height / 2), (int)width, (int)height);
        }

        private void UpdateCircleCollision()
        {
            collisionCircle = new Circle(currentPosition, radius);
        }
    }
}
