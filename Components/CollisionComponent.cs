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
        public Action CollisionActions;

        public void CheckCollision(Rectangle rectangle1, Rectangle rectangle2)
        {

            if (rectangle1.Intersects(rectangle2))
            {
                CollisionActions.Invoke();
            }
        }
    }
}
