using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.UI
{
    public enum Scenes
    {
        Menu,
        Gameplay
    }
    public class UIModel
    {
        public int X, Y;
        public int barSide = 10;
        public float stamina;
        public int health;
        public int maxHealth;
        public int buttonsToPress;
        public float maxStamina;
        public Rectangle playButtonRect;
        public Rectangle quitButtonRect;
        public Color playButtonColor;
        public Color quitButtonColor;
        public SpriteFont arialFont;
        public Scenes currentScene;
        public string menuTitle;
        public bool allButtonsPressed;
        public float elapsedTimeSinceLastButtonPress;

        public UIModel(int x, int y, int health, float stamina, int buttonsToPress, SpriteFont arialFont)
        {
            currentScene = Scenes.Gameplay;
            X = x;
            Y = y;
            this.health = health;
            this.stamina = stamina;
            this.buttonsToPress = buttonsToPress;
            this.arialFont = arialFont;
            maxHealth = health;
            maxStamina = stamina;          
        }

        public UIModel(Rectangle playButtonRect, Rectangle quitButtonRect, SpriteFont arialFont, string menuTitle)
        {
            currentScene = Scenes.Menu;
            this.playButtonRect = playButtonRect;
            this.quitButtonRect = quitButtonRect;
            this.arialFont = arialFont;
            this.menuTitle = menuTitle;
            this.playButtonColor = Color.LightGray;
            this.quitButtonColor = Color.Red;
        }
    }
}
