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
            positionComponent.y -= 1.5f;
        }

        public void MoveBackward()
        {
            positionComponent.y += 1.5f;
        }

        public void MoveRight()
        {
            positionComponent.x += 1.5f;
        }

        public void MoveLeft()
        {
            positionComponent.x -= 1.5f;
        }
    }
}
