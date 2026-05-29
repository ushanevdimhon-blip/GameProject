using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public void Update(GameTime gameTime = null, int health = 0, float stamina = 0, int buttonsToPress = 0, MouseState mouse = default)
        {
            switch(model.currentScene)
            {
                case Scenes.Menu:
                    if (model.playButtonRect.Contains(mouse.X, mouse.Y))
                    {
                        model.playButtonColor = Color.DarkGray;
                    }
                    else if (model.quitButtonRect.Contains(mouse.X, mouse.Y))
                    {
                        model.quitButtonColor = Color.DarkRed;
                    }
                    else
                    {
                        model.playButtonColor = Color.LightGray;
                        model.quitButtonColor = Color.Red;
                    }
                    break;
                case Scenes.Gameplay:
                    model.health = health;
                    model.stamina = stamina;
                    if (!model.allButtonsPressed)
                        model.buttonsToPress = buttonsToPress;
                    else
                        model.elapsedTimeSinceLastButtonPress += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            view.Draw(spriteBatch, model);
        }
    }
}
