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

        public CollisionComponent(PositionComponent currentPosition, float width, float height)
        {
            this.width = width;
            this.height = height;
            this.currentPosition = currentPosition;
            collisionRectangle = new Rectangle((int)(currentPosition.X - width / 2),
                (int)(currentPosition.Y - height / 2), (int)width, (int)height);
        }

        public CollisionComponent(PositionComponent currentPosition, float width, float height, int radius)
        {
            this.width = width;
            this.height = height;
            this.radius = radius;
            this.currentPosition = currentPosition;
            collisionRectangle = new Rectangle((int)(currentPosition.X - width / 2),
                (int)(currentPosition.Y - height / 2), (int)width, (int)height);
            collisionCircle = new Circle(currentPosition, radius);
        }

        public CollisionComponent(PositionComponent currentPosition, int radius)
        {
            this.currentPosition = currentPosition;
            this.radius = radius;
            collisionCircle = new Circle(currentPosition, radius);
        }

        public CollisionComponent(Rectangle collisionRectangle)
        {
            this.collisionRectangle = collisionRectangle;
        }

        public void UpdateRectangleCollision()
        {
            collisionRectangle = new Rectangle((int)(currentPosition.X - width / 2),
                (int)(currentPosition.Y - height / 2), (int)width, (int)height);
            //(int)(currentPosition.X - width / 2),(int)(currentPosition.Y - height / 2) - это для того, чтобы нарисовать прямоугольник,
            //т.к. он рисуется от левого верхнего угла
        }

        public void UpdateCircleCollision()
        {
            collisionCircle = new Circle(currentPosition, radius);
        }

        public void DrawRectangle(SpriteBatch spriteBatch, Color color)
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

        public void DrawCircle(SpriteBatch spriteBatch, Color color)
        {
            // Количество отрезков для сглаживания круга
            int segments = (int)(collisionCircle.Radius * 2);
            float angle = 0f;
            float angleStep = MathHelper.TwoPi / segments;

            Texture2D pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            // Отрисовываем линии, соединяющие точки окружности
            for (int i = 0; i < segments; i++)
            {
                // Текущая точка на окружности
                Vector2 point1 = new Vector2(collisionCircle.X, collisionCircle.Y) +
                    collisionCircle.Radius * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

                // Следующая точка на окружности
                Vector2 point2 = new Vector2(collisionCircle.X, collisionCircle.Y) +
                    collisionCircle.Radius * new Vector2((float)Math.Cos(angle + angleStep), (float)Math.Sin(angle + angleStep));

                // Вычисляем длину линии между двумя точками
                Vector2 line = point2 - point1;
                float lineLength = line.Length();

                if (lineLength > 0)
                {
                    // Угол поворота для отрисовки линии в нужном направлении
                    float lineAngle = (float)Math.Atan2(line.Y, line.X);

                    // Отрисовываем линию
                    spriteBatch.Draw(pixel,
                        new Rectangle((int)point1.X, (int)point1.Y, (int)lineLength, 1),
                        null,
                        color,
                        lineAngle,
                        Vector2.Zero,
                        SpriteEffects.None,
                        0f);
                }

                angle += angleStep;
            }
        }
    }
}
