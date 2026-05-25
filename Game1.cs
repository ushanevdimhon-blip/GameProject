using GameProject.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace GameProject
{
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        Scene currentScene;
        Scene nextScene;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            MenuScene menuScene = new MenuScene(GraphicsDevice, Content);
            currentScene = menuScene;
            currentScene.Initialize();

            SubscribeToMenuActions(menuScene);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (nextScene != null)
            {
                currentScene.Dispose();
                currentScene = nextScene;
                currentScene.Initialize();
                nextScene = null;
            }

            currentScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            currentScene.Draw(gameTime);
            base.Draw(gameTime);
        }

        private void SubscribeToMenuActions(MenuScene menuScene)
        {
            menuScene.OnPlayGame += () =>
            {
                GameplayScene gameplayScene = new GameplayScene(GraphicsDevice, Content);
                IsMouseVisible = false;
                ChangeScene(gameplayScene);
                SubscribeToGameplayActions(gameplayScene);
            };

            menuScene.OnQuitGame += Exit;
        }

        private void SubscribeToGameplayActions(GameplayScene gameplayScene)
        {
            gameplayScene.OnGameOver += () =>
            {
                MenuScene newMenuScene = new MenuScene(GraphicsDevice, Content);
                IsMouseVisible = true;
                ChangeScene(newMenuScene);
                SubscribeToMenuActions(newMenuScene);
            };

            gameplayScene.OnGameWon += () =>
            {
                MenuScene newMenuScene = new MenuScene(GraphicsDevice, Content);
                IsMouseVisible = true;
                ChangeScene(newMenuScene);
                SubscribeToMenuActions(newMenuScene);
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                currentScene.Dispose();
                nextScene?.Dispose();
                _graphics.Dispose();
            }
            base.Dispose(disposing);
        }

        public void ChangeScene(Scene newScene)
        {
            nextScene = newScene;
        }
    }
}