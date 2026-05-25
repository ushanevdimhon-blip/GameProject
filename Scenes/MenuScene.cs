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
        GraphicsDevice _graphicsDevice;
        UIView uiView;
        UIModel uiModel;
        UIPresenter uiPresenter;
        Rectangle playButtonRect;
        Rectangle quitButtonRect;

        public Action OnPlayGame;
        public Action OnQuitGame;

        public MenuScene(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _graphicsDevice = graphicsDevice;
            Content = contentManager;
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_graphicsDevice);

            int screenWidth = _graphicsDevice.PresentationParameters.BackBufferWidth;
            int screenHeight = _graphicsDevice.PresentationParameters.BackBufferHeight;

            playButtonRect = new Rectangle(screenWidth / 2 - 100, screenHeight / 2 - 100, 200, 60);
            quitButtonRect = new Rectangle(screenWidth / 2 - 100, screenHeight / 2 + 20, 200, 60);

            var pixel = new Texture2D(_graphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            var arialFont = Content.Load<SpriteFont>("Fonts/Arial");

            uiModel = new UIModel(playButtonRect, quitButtonRect, arialFont);
            uiView = new UIView();
            uiPresenter = new UIPresenter(uiModel, uiView);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            uiPresenter.Update(mouse: mouse);

            if (mouse.LeftButton == ButtonState.Pressed)
            {            
                if (playButtonRect.Contains(mouse.X, mouse.Y))
                {
                    OnPlayGame.Invoke();
                }
                if (quitButtonRect.Contains(mouse.X, mouse.Y))
                {
                    OnQuitGame.Invoke();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _graphicsDevice.Clear(Color.Beige);
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