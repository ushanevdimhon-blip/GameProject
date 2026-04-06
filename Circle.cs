using GameProject.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public readonly struct Circle
    {
        public readonly PositionComponent Location;

        public readonly int X;

        public readonly int Y;

        public readonly int Radius;

        public Circle(PositionComponent location, int radius)
        {
            Location = location;
            X = (int)location.X;
            Y = (int)location.Y;
            Radius = radius;
        }

        public bool Intersects(Circle other)
        {
            int radiiSquared = (this.Radius + other.Radius) * (this.Radius + other.Radius);
            float distanceSquared = Vector2.DistanceSquared(this.Location.vector, other.Location.vector);
            return distanceSquared < radiiSquared;
        }

        public bool Intersects(Rectangle rectangle)
        {
            // Находим ближайшую точку на прямоугольнике к центру круга
            float closestX = MathHelper.Clamp(Location.X, rectangle.Left, rectangle.Right);
            float closestY = MathHelper.Clamp(Location.Y, rectangle.Top, rectangle.Bottom);

            // Вычисляем расстояние между центром круга и ближайшей точкой
            float distanceX = Location.X - closestX;
            float distanceY = Location.Y - closestY;

            // Проверяем, пересекается ли круг с прямоугольником
            float distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);
            return distanceSquared < (Radius * Radius);
        }
    }
}
