using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Components
{
    public class AttackComponent
    {
        public float cooldown { get; private set; }
        public bool IsAttacking { get; private set; }

        public AttackComponent(float cooldown) 
        { 
            this.cooldown = cooldown;
        }

        public void Attack(Action OnAttack, Action OnCooldown)
        {
            if (cooldown >= 4.0f)//сделать константой
            {
                OnAttack?.Invoke();
                IsAttacking = true;
                cooldown = 0.0f;
            }
            else
            {
                OnCooldown?.Invoke();
                IsAttacking = false;
            }
        }

        public void Update(GameTime gameTime)
        {
            cooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;
            cooldown = Math.Min(cooldown, 4.0f);//сделать константой
        }
    }
}
