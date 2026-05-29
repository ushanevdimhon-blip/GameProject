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
        public GraphicsDevice graphicsDevice;

        public UIView(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public void Draw(SpriteBatch spriteBatch, UIModel model)
        {
            if (pixel == null)
            {
                pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                pixel.SetData(new[] { Color.White });
            }

            switch(model.currentScene)
            {
                case Scenes.Menu:
                    DrawMenu(spriteBatch, model);
                    break;
                case Scenes.Gameplay:
                    DrawGameStats(spriteBatch, model);
                    break;
            }
        }

        private void DrawGameStats(SpriteBatch spriteBatch, UIModel model)
        {
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

        private void DrawMenu(SpriteBatch spriteBatch, UIModel model)
        {
            spriteBatch.DrawString(model.arialFont,
                model.menuTitle,
                new Vector2(graphicsDevice.PresentationParameters.BackBufferWidth/2 - model.arialFont.MeasureString(model.menuTitle).X / 2,
                            graphicsDevice.PresentationParameters.BackBufferHeight/8 - model.arialFont.MeasureString(model.menuTitle).Y / 2),
                Color.Black);

            spriteBatch.Draw(pixel, model.playButtonRect, model.playButtonColor);
            spriteBatch.DrawString(model.arialFont,
                "Play",
                new Vector2(model.playButtonRect.X + model.playButtonRect.Width / 2 - model.arialFont.MeasureString("Play").X / 2,
                            model.playButtonRect.Y),
                Color.Black);

            spriteBatch.Draw(pixel, model.quitButtonRect, model.quitButtonColor);
            spriteBatch.DrawString(model.arialFont,
                "Quit",
                new Vector2(model.quitButtonRect.X + model.quitButtonRect.Width / 2 - model.arialFont.MeasureString("Quit").X / 2,
                            model.quitButtonRect.Y),
                Color.Black);
        }
    }
}
