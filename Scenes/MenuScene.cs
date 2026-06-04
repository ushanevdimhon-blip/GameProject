using GameProject.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameProject.Scenes
{
    public class MenuScene : Scene
    {
        SpriteBatch _spriteBatch;
        SpriteFont arialFont;
        GraphicsDevice _graphicsDevice;
        UIView uiView;
        UIModel uiModel;
        UIPresenter uiPresenter;
        Rectangle playButtonRect;
        Rectangle quitButtonRect;
        Color backgroundColor;
        string menuTitle;
        int screenWidth;
        int screenHeight;
        bool isPause;

        public Action OnPlay;
        public Action OnQuit;
        public Action OnContinue;

        public MenuScene(GraphicsDevice graphicsDevice, ContentManager contentManager, 
            Color backgroundColor, string menuTitle, bool isPause = false)
        {
            _graphicsDevice = graphicsDevice;
            Content = contentManager;
            this.backgroundColor = backgroundColor;
            this.menuTitle = menuTitle;
            this.isPause = isPause;
        }

        public override void Initialize()
        {
            base.Initialize();

            screenWidth = _graphicsDevice.PresentationParameters.BackBufferWidth;
            screenHeight = _graphicsDevice.PresentationParameters.BackBufferHeight;

            playButtonRect = new Rectangle(screenWidth / 2 - 100, screenHeight / 2 - 100, 200, 60);
            quitButtonRect = new Rectangle(screenWidth / 2 - 100, screenHeight / 2 + 20, 200, 60);

            uiModel = new UIModel(playButtonRect, quitButtonRect, arialFont, menuTitle);
            uiView = new UIView(_graphicsDevice);
            uiPresenter = new UIPresenter(uiModel, uiView);
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_graphicsDevice);

            arialFont = Content.Load<SpriteFont>("Fonts/Arial");
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            uiPresenter.Update(mouse: mouse);

            if (mouse.LeftButton == ButtonState.Pressed)
            {            
                if (playButtonRect.Contains(mouse.X, mouse.Y))
                {
                    if (isPause)
                    {
                        OnContinue?.Invoke();
                    }
                    else
                    {
                        OnPlay?.Invoke();
                    }
                }
                if (quitButtonRect.Contains(mouse.X, mouse.Y))
                {
                    OnQuit.Invoke();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _graphicsDevice.Clear(backgroundColor);
            _spriteBatch.Begin();

            uiPresenter.Draw(_spriteBatch);

            _spriteBatch.End();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _spriteBatch.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}