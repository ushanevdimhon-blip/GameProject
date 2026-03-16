using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Components
{
    public class InputComponent
    {
        public Action OnUp;
        public Action OnDown;
        public Action OnRight;
        public Action OnLeft;

        public void Update(PositionComponent positionComponent)
        {
            var key = Keyboard.GetState();

            if (key.IsKeyDown(Keys.W))
            {
                positionComponent.y -= 1;
                //OnUp();
            }
                
            if (key.IsKeyDown(Keys.S))
            {
                positionComponent.y += 1;
                //OnDown();
            }
                
            if (key.IsKeyDown(Keys.D))
            {
                positionComponent.x += 1;
                //OnRight();
            }
                
            if (key.IsKeyDown(Keys.A))
            {
                positionComponent.x -= 1;
                //OnLeft();
            }        
        }
    }
}
