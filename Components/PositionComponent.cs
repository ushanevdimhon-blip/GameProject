using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Components
{
    public class PositionComponent
    {
        public float X { get { return vector.X; } set { vector.X = value; } }
        public float Y { get { return vector.Y; } set { vector.Y = value; } }

        public Vector2 vector;

        public float PreviousX { get; set; }
        public float PreviousY { get; set; }

        public PositionComponent(float x, float y)
        {
            vector = new Vector2(x, y);
            PreviousX = x;
            PreviousY = y;
        }

        public void Block()
        {
            X = PreviousX;
            Y = PreviousY;
        }
    }
}
