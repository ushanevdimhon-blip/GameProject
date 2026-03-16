using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameProject.Components
{
    public class PatrolComponent
    {
        List<Action> list;
        int index = 60;
        MoveComponent moveComponent;
        PositionComponent positionComponent;
        Random random = new Random();
        int randomInt = 1;

        public PatrolComponent(MoveComponent moveComponent, PositionComponent positionComponent)
        {
            this.moveComponent = moveComponent;
            this.positionComponent = positionComponent;
            list = new List<Action>()
            {
                moveComponent.MoveForward, 
                moveComponent.MoveBackward, 
                moveComponent.MoveRight,
                moveComponent.MoveLeft
            };
            
        }
        public void Patrol()
        {  
            if (index == 0)
            {
                randomInt = random.Next(list.Count - 1);
                index = 60;
            }             
            list[randomInt]();
            index--;
        }
    }
}
