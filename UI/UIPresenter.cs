using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.UI
{
    public class UIPresenter
    {
        private UIModel model;
        private UIView view;

        public UIPresenter(UIModel model, UIView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Update(int health, float stamina)
        {
            model.health = health;
            model.stamina = stamina;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            view.Draw(spriteBatch, model);
        }
    }
}
