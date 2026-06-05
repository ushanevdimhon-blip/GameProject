using GameProject.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Camera
{
    internal class Camera
    {
        public PositionComponent position;
        public PositionComponent targetPosition;
        public float width;
        public float height;
        public Matrix Matrix { get; private set; }

        public Camera(float width, float height)
        {
            this.width = width;
            this.height = height;
            position = new PositionComponent(0, 0);
            targetPosition = new PositionComponent(0, 0);
        }

        public void Update()
        {
            Matrix = Matrix.CreateTranslation(-position.X + width / 2, -position.Y + height / 2, 0);
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)(position.X - width / 2), (int)(position.Y - height / 2), (int)width, (int)height);
        }
    }
}
