using GameProject.Scenes;
using Microsoft.Xna.Framework;


namespace GameProject
{
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        Scene currentScene;
        Scene nextScene;
        GameplayScene pausedGameplayScene;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            MenuScene menuScene = new MenuScene(GraphicsDevice, Content, Color.Beige, "Play Game");
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
                if (currentScene != pausedGameplayScene)
                {
                    currentScene.Dispose();
                }
                
                currentScene = nextScene;
                
                if (currentScene != pausedGameplayScene)
                {
                    currentScene.Initialize();
                }
                
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                currentScene.Dispose();
                nextScene?.Dispose();
                pausedGameplayScene?.Dispose();
                _graphics.Dispose();
            }
            base.Dispose(disposing);
        }

        public void ChangeScene(Scene newScene)
        {
            nextScene = newScene;
        }

        private void SubscribeToMenuActions(MenuScene menuScene)
        {
            menuScene.OnPlay += () =>
            {
                GameplayScene gameplayScene = new GameplayScene(GraphicsDevice, Content);
                IsMouseVisible = false;
                ChangeScene(gameplayScene);
                SubscribeToGameplayActions(gameplayScene);
            };

            menuScene.OnQuit += Exit;
        }

        private void SubscribeToGameplayActions(GameplayScene gameplayScene)
        {
            gameplayScene.OnLoss += () =>
            {
                MenuScene newMenuScene = new MenuScene(GraphicsDevice, Content, Color.IndianRed, "You've lost");
                IsMouseVisible = true;
                ChangeScene(newMenuScene);
                SubscribeToMenuActions(newMenuScene);
            };

            gameplayScene.OnWin += () =>
            {
                MenuScene newMenuScene = new MenuScene(GraphicsDevice, Content, Color.YellowGreen, "You won!");
                IsMouseVisible = true;
                ChangeScene(newMenuScene);
                SubscribeToMenuActions(newMenuScene);
            };

            gameplayScene.OnPause += () =>
            {
                MenuScene pauseMenuScene = new MenuScene(GraphicsDevice, Content, Color.Gray, "Paused", true);
                IsMouseVisible = true;
                if (pausedGameplayScene != gameplayScene)
                {
                    pausedGameplayScene = gameplayScene;
                }
                ChangeScene(pauseMenuScene);
                SubscribeToPauseMenuActions(pauseMenuScene);
            };
        }

        private void SubscribeToPauseMenuActions(MenuScene pauseMenuScene)
        {
            pauseMenuScene.OnContinue += () =>
            {
                IsMouseVisible = false;
                ChangeScene(pausedGameplayScene);
                pausedGameplayScene.IsPaused = false;             
            };

            pauseMenuScene.OnQuit += Exit;
        }
    }
}