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
        public int MaxHealth { get; private set; }
        public HealthComponent(int health)
        {
            Health = health;
            MaxHealth = health;
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

        public void Heal(int hp, Action OnHeal)
        {
            if ((Health + hp) >= MaxHealth)
            {
                Health = MaxHealth;
            }
            else
            {
                Health += hp;
            }
        }
    }
}
