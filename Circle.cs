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
            float closestX = Math.Clamp(Location.X, rectangle.Left, rectangle.Right);
            float closestY = Math.Clamp(Location.Y, rectangle.Top, rectangle.Bottom);

            float catetX = Location.X - closestX;
            float catetY = Location.Y - closestY;

            float hypotenuseSquared = (catetX * catetX) + (catetY * catetY);
            return hypotenuseSquared < (Radius * Radius);
        }
    }
}
