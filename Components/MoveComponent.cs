using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Components
{
    public class MoveComponent
    {
        PositionComponent positionComponent;

        public MoveComponent(PositionComponent positionComponent)
        {
            this.positionComponent = positionComponent;
        }

        public void MoveForward()
        {
            positionComponent.Y -= 1.5f;
        }

        public void MoveBackward()
        {
            positionComponent.Y += 1.5f;
        }

        public void MoveRight()
        {
            positionComponent.X += 1.5f;
        }

        public void MoveLeft()
        {
            positionComponent.X -= 1.5f;
        }
    }
}
