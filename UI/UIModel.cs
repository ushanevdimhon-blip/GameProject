using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.UI
{
    public class UIModel
    {
        public int X, Y;
        public int barSide = 10;
        public float stamina;
        public int health;
        public int maxHealth;
        public float maxStamina;

        public UIModel(int x, int y, int health, float stamina)
        {
            X = x;
            Y = y;
            this.health = health;
            this.stamina = stamina;
            maxHealth = health;
            maxStamina = stamina;
        }
    }
}
