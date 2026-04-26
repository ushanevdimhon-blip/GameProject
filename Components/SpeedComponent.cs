using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Components
{
    public class SpeedComponent
    {
        public uint stamina;
        public bool isSprinting;
        public float velocity;

        public SpeedComponent()
        {
            velocity = 2;
            stamina = 100;
        }

        public void Update()
        {
            velocity = isSprinting ? 4 : 2;

            if (isSprinting && stamina == 0)
                stamina = 0;
            else if (isSprinting)
                stamina--;
            else
                stamina++;
        }
    }
}
