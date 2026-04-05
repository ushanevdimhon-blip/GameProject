using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
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
        uint stamina = 10;

        public void Update(PositionComponent positionComponent)
        {
            var key = Keyboard.GetState();

            bool isSprinting = key.IsKeyDown(Keys.LeftShift);

            var velocity = isSprinting ? 4 : 2;

            if (isSprinting && stamina == 0)
                stamina = 0;
            else if (isSprinting)
                stamina--;
            else
                stamina++;

            if (key.IsKeyDown(Keys.W))
            {
                positionComponent.Y -= 1 * (stamina == 0 ? 2 : velocity);
                //OnUp();
            }
                
            if (key.IsKeyDown(Keys.S))
            {
                positionComponent.Y += 1 * (stamina == 0 ? 2 : velocity);
                //OnDown();
            }
                
            if (key.IsKeyDown(Keys.D))
            {
                positionComponent.X += 1 * (stamina == 0 ? 2 : velocity);
                //OnRight();
            }
                
            if (key.IsKeyDown(Keys.A))
            {
                positionComponent.X -= 1 * (stamina == 0 ? 2 : velocity);
                //OnLeft();
            }        
        }
    }
}
