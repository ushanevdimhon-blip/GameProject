using Microsoft.Xna.Framework;
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
        PositionComponent positionComponent;
        public Action OnUp;
        public Action OnDown;
        public Action OnRight;
        public Action OnLeft;
        public Action OnIdle;
        

        public InputComponent(SpeedComponent speedComponent, PositionComponent positionComponent)
        {
            this.speedComponent = speedComponent;
            this.positionComponent = positionComponent;
        }

        public void Update(GameTime gameTime)
        {
            bool isMoving = false;
            var key = Keyboard.GetState();

            if (key.IsKeyDown(Keys.LeftShift))
                speedComponent.Sprinting();

            speedComponent.Update(gameTime);

            positionComponent.PreviousX = positionComponent.X;
            positionComponent.PreviousY = positionComponent.Y;

            if (key.IsKeyDown(Keys.W))
            {
                positionComponent.Y -= speedComponent.velocity;
                OnUp();
                isMoving = true;
            }

            if (key.IsKeyDown(Keys.S))
            {
                positionComponent.Y += speedComponent.velocity;
                OnDown();
                isMoving = true;
            }

            if (key.IsKeyDown(Keys.D))
            {
                positionComponent.X += speedComponent.velocity;
                OnRight();
                isMoving = true;
            }

            if (key.IsKeyDown(Keys.A))
            {
                positionComponent.X -= speedComponent.velocity;
                OnLeft();
                isMoving = true;
            }

            if (!isMoving)
            {
                OnIdle();
            }
        }
    }
}
