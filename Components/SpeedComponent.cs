using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Components
{
    public class SpeedComponent
    {
        public float stamina;
        public bool isSprinting;
        public float velocity;
        float maxStamina;
        float baseVelocity;
        float delay;

        public SpeedComponent(float velocity, float stamina)
        {
            this.baseVelocity = velocity;
            this.velocity = velocity;
            maxStamina = stamina;
            this.stamina = stamina;
        }

        public void Sprinting()
        {
            isSprinting = false;
            if (delay >= 2.0f || stamina != 0)
            {
                isSprinting = true;
                delay = 0;
            }
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            velocity = isSprinting ? (baseVelocity * 2) * deltaTime : baseVelocity * deltaTime;

            if (stamina!=0 || delay >= 2.0f)
            {
                if (isSprinting && stamina > 0)
                {
                    stamina -= 30.0f * deltaTime;
                    stamina = Math.Max(0, stamina);
                }
                else if (!isSprinting && stamina < 100)
                {
                    stamina += 10.0f * deltaTime;
                    stamina = Math.Min(100, stamina);
                }
            }
            else
            {
                isSprinting = false;
                delay += deltaTime;
            }
        }

        public void RestoreStamina()
        {
            stamina = maxStamina;
            delay = 0;
        }
    }
}
