using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Components
{
    public class CollisionComponent
    {
        Rectangle object1Bounds;
        Rectangle object2Bounds;
        public Action Collision;

        public void CheckCollision(Player object1, Enemy object2)
        {
            object1Bounds = new Rectangle((int)object1.currentPosition.x, 
                (int)object1.currentPosition.y, (int)object1.Wigth, (int)object1.Height);
            object2Bounds = new Rectangle((int)object2.currentPosition.x, 
                (int)object2.currentPosition.y, (int)object2.Wigth, (int)object2.Height);

            if (object1Bounds.IntersectsWith(object2Bounds))
            {
                Collision.Invoke();
            }
        }
    }
}
