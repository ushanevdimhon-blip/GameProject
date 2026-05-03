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
        SpeedComponent speedComponent;
        public Action OnUp;
        public Action OnDown;
        public Action OnRight;
        public Action OnLeft;

        public InputComponent()
        {
            speedComponent = new SpeedComponent();
        }

        public void Update(PositionComponent positionComponent)
        {
            var key = Keyboard.GetState();

            speedComponent.isSprinting = key.IsKeyDown(Keys.LeftShift);

            speedComponent.Update();

            if (key.IsKeyDown(Keys.W))
            {
                positionComponent.Y -= 1 * (speedComponent.stamina == 0 ? 2 : speedComponent.velocity);
                //OnUp();
            }
                
            if (key.IsKeyDown(Keys.S))
            {
                positionComponent.Y += 1 * (speedComponent.stamina == 0 ? 2 : speedComponent.velocity);
                //OnDown();
            }
                
            if (key.IsKeyDown(Keys.D))
            {
                positionComponent.X += 1 * (speedComponent.stamina == 0 ? 2 : speedComponent.velocity);
                //OnRight();
            }
                
            if (key.IsKeyDown(Keys.A))
            {
                positionComponent.X -= 1 * (speedComponent.stamina == 0 ? 2 : speedComponent.velocity);
                //OnLeft();
            }        
        }
    }
}
