using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Components
{
    public class DirectionComponent
    {
        public Action OnUp;
        public Action OnDown;
        public Action OnRight;
        public Action OnLeft;

        public void Update(Vector2 direction)
        {
            if (direction == Vector2.Zero)
                OnRight();

            float absX = Math.Abs(direction.X);
            float absY = Math.Abs(direction.Y);

            if (absX > absY)
            {
                if (direction.X > 0)
                    OnRight();
                else
                    OnLeft();
            }
            else
            {
                if (direction.Y > 0)
                    OnDown();
                else
                    OnUp();
            }
        }
    }
}
