using GameProject.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace GameProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private Enemy enemy;
        private Texture2D playerTexture;
        private Texture2D enemyTexture;
        CollisionComponent screenCollision;
        Rectangle screenBounds;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            screenBounds = new Rectangle(
                0,
                0,
                GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight
            );
            screenCollision = new CollisionComponent(screenBounds);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("Images/5053745_0");
            player = new Player(playerTexture, 50, 50, 0.1f);
            enemyTexture = Content.Load<Texture2D>("Images/vecteezy_angry-face-emoji-png-file_11997334");
            enemy = new Enemy(enemyTexture, 0.03f);          
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update();
            enemy.Update();
            if (CheckRectangleCollision(player.collision, enemy.collision))
                player.Block();
            if (CheckCircleCollision(enemy.collision, player.collision))
                player.Block();
            GetScreenCollision(player.collision);
            GetScreenCollision(enemy.collision);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Clear the back buffer.
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            player.Draw(_spriteBatch);
            enemy.Draw(_spriteBatch);
            enemy.collision.DrawRectangle(_spriteBatch, Color.Green);
            enemy.collision.DrawCircle(_spriteBatch, Color.Green);
            player.collision.DrawRectangle(_spriteBatch, Color.Green);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public bool CheckRectangleCollision(CollisionComponent collision1, CollisionComponent collision2)
        {
            if (collision1.collisionRectangle.Intersects(collision2.collisionRectangle))
            {
                return true;
            }
            return false;
        }

        public bool CheckCircleCollision(CollisionComponent collision1, CollisionComponent collision2)
        {
            if (collision1.collisionCircle.Intersects(collision2.collisionRectangle))
            {
                return true;
            }
            return false;
        }

        //по сути этот метод может подойти и для стен, чтобы при беге персонаж облизывал текстуры, а не липнул к ним
        public void GetScreenCollision(CollisionComponent collisionObject)
        {
            if (collisionObject.collisionRectangle.Left < screenBounds.Left)
            {
                collisionObject.currentPosition.X = screenBounds.Left + collisionObject.width/2;
            }
            else if (collisionObject.collisionRectangle.Right > screenBounds.Right)
            {
                collisionObject.currentPosition.X = screenBounds.Right - collisionObject.width/2;
            }

            if (collisionObject.collisionRectangle.Top < screenBounds.Top)
            {
                collisionObject.currentPosition.Y = screenBounds.Top + collisionObject.height / 2;
            }
            else if (collisionObject.collisionRectangle.Bottom > screenBounds.Bottom)
            {
                collisionObject.currentPosition.Y = screenBounds.Bottom - collisionObject.height / 2;
            }
        }
    }
}
//!!!!ПРИ ОТРИСОВКЕ ТЕПЕРЬ МОДЕЛЬ РИСУЕТСЯ ОТ ЦЕНТРА(ТОЧНЕЕ ЦЕНТР МОДЕЛЬКИ НАТЯГИВАЕТСЯ НА КООРДИНАТЫ),
//А НЕ ОТ ВЕРХНЕГО ЛЕВОГО УГЛА!!!!!