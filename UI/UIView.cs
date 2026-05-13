using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.UI
{
    public class UIView
    {
        private Texture2D pixel;

        public UIView()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, UIModel model)
        {
            pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1); 
            pixel.SetData(new[] { Color.White });

            spriteBatch.Draw(pixel, new Rectangle(model.X, model.Y,
                model.maxHealth, model.barSide), Color.DarkRed);

            int currentHealthWidth = (int)(model.maxHealth * ((float)model.health / model.maxHealth));
            spriteBatch.Draw(pixel, new Rectangle(model.X, model.Y,
                currentHealthWidth, model.barSide), Color.Red);

            spriteBatch.Draw(pixel, new Rectangle(model.X, model.Y + model.barSide * 2,
                (int)model.maxStamina, model.barSide), Color.DarkGreen);

            int currentStaminaHeight = (int)(model.maxStamina * (model.stamina / model.maxStamina));
            spriteBatch.Draw(pixel, new Rectangle(model.X,
                model.Y + model.barSide * 2,
                (int)model.stamina, model.barSide), Color.Green);
        }
    }
}
