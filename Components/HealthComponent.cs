using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Components
{
    public class HealthComponent
    {
        public int Health { get; private set; }
        public HealthComponent(int health)
        {
            Health = health;
        }

        public void TakeDamage(int damage, Action OnDamage, Action OnDeath)
        {
            Health -= damage;
            OnDamage?.Invoke();
            if (Health < 0)
            {
                Health = 0;
                OnDeath?.Invoke();
            }
        }
    }
}
