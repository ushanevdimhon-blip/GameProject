using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Collision
{
    public readonly struct DirectionCircle
    {
        public readonly Circle Circle;
        public readonly Vector2 Direction;

        public DirectionCircle(Circle circle, Vector2 direction)
        {
            Circle = circle;
            Direction = Vector2.Normalize(direction);
        }

        public bool Intersects(Rectangle rectangle)
        {
            if (!Circle.Intersects(rectangle))
                return false;

            var rectCenterX = (rectangle.Left + rectangle.Right) / 2f;
            var rectCenterY = (rectangle.Top + rectangle.Bottom) / 2f;

            var toTargetX = rectCenterX - Circle.Location.X;
            var toTargetY = rectCenterY - Circle.Location.Y;

            var lengthSq = toTargetX * toTargetX + toTargetY * toTargetY;
            if (lengthSq < 0.001f)
                return false;

            var length = (float)Math.Sqrt(lengthSq);
            var normalizedX = toTargetX / length;
            var normalizedY = toTargetY / length;

            var dotPr = normalizedX * Direction.X + normalizedY * Direction.Y;

            return dotPr > 0;
        }
    }
}
