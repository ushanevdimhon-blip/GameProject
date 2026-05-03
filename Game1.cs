using GameProject.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.InteropServices.Marshalling;


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
        private Tilemap tilemap;
        string[,] tileData;
        Texture2D wallTexture;
        Texture2D floorTexture;
        Camera camera;
        float worldWidth;
        float worldHeight;

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
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playerTexture = Content.Load<Texture2D>("Images/5053745_0");
            player = new Player(playerTexture, 300, 500, 0.08f);

            enemyTexture = Content.Load<Texture2D>("Images/vecteezy_angry-face-emoji-png-file_11997334");
            

            tileData = new string[,] { 
                { "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01" },
                { "01", "09", "09", "09", "09", "09", "09", "01", "09", "09", "09", "01", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "09", "09", "09", "01", "09", "01", "09", "01", "09", "01", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "01", "01", "09", "09", "09", "09", "01", "09", "01", "09", "01", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "01", "09", "09", "09", "09", "01", "09", "01", "09", "01", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "01", "09", "09", "09", "09", "01", "09", "01", "09", "01", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "01", "09", "09", "09", "09", "01", "09", "01", "09", "01", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "01", "09", "09", "09", "09", "01", "09", "01", "09", "01", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "01", "09", "09", "09", "09", "01", "09", "01", "09", "01", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "01", "09", "09", "09", "09", "01", "09", "01", "09", "01", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "01", "09", "09", "09", "09", "01", "09", "01", "09", "01", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01" }
            };//сделать чтение из xml файла
            wallTexture = Content.Load<Texture2D>("Images/Wall");
            floorTexture = Content.Load<Texture2D>("Images/Floor"); 
            tilemap = new Tilemap(tileData, 50, 50, wallTexture, floorTexture);           
            worldWidth = tilemap.tiles.GetLength(1) * tilemap.TileWidth;
            worldHeight = tilemap.tiles.GetLength(0) * tilemap.TileHeight;
            enemy = new Enemy(enemyTexture, 0.01f, tilemap);

            camera = new Camera(GraphicsDevice.PresentationParameters.BackBufferWidth, 
                GraphicsDevice.PresentationParameters.BackBufferHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update();
            enemy.Update();

            

            camera.Follow(player.currentPosition);
            camera.Clamp(worldWidth, worldHeight);
            camera.Update();
            
            if (CheckRectangleCollision(player.collision, enemy.collision))
            {
                //TODO: обработать столкновение игрока и врага
            }

            if (CheckCircleCollision(enemy.collision, player.collision))
            {
                //TODO: обработать столкновение круга врага и прямоугольника игрока
                enemy.Chase(player.currentPosition, gameTime);
            }

            CheckTilesCollision(tilemap, player.currentPosition, player.collision, player.Block);

            Rectangle cameraBounds = camera.GetCameraBounds();
            GetCameraCollision(player.collision, cameraBounds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: camera.Matrix);

            var (c1, c2, r1, r2) = camera.GetVisibleRange(tilemap.TileWidth, 
                                                    tilemap.tiles.GetLength(1), 
                                                    tilemap.tiles.GetLength(0));
            tilemap.Draw(_spriteBatch, c1, c2, r1, r2);

            player.Draw(_spriteBatch);
            player.collision.DrawRectangle(_spriteBatch, Color.Green);

            enemy.Draw(_spriteBatch);
            enemy.collision.DrawRectangle(_spriteBatch, Color.Green);
            enemy.collision.DrawCircle(_spriteBatch, Color.Green);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        // вынести в отдельный класс, напр CollisionManager
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

        public void GetCameraCollision(CollisionComponent collisionObject, Rectangle cameraBounds)
        {
            if (collisionObject.collisionRectangle.Left < cameraBounds.Left)
            {
                collisionObject.currentPosition.X = cameraBounds.Left + collisionObject.width / 2;
            }
            else if (collisionObject.collisionRectangle.Right > cameraBounds.Right)
            {
                collisionObject.currentPosition.X = cameraBounds.Right - collisionObject.width / 2;
            }

            if (collisionObject.collisionRectangle.Top < cameraBounds.Top)
            {
                collisionObject.currentPosition.Y = cameraBounds.Top + collisionObject.height / 2;
            }
            else if (collisionObject.collisionRectangle.Bottom > cameraBounds.Bottom)
            {
                collisionObject.currentPosition.Y = cameraBounds.Bottom - collisionObject.height / 2;
            }
        }

        private void CheckTilesCollision(Tilemap tilemap, PositionComponent currentPosition, 
            CollisionComponent collision, Action colAction)
        {
            int tileX = (int)(currentPosition.X / tilemap.TileWidth);
            int tileY = (int)(currentPosition.Y / tilemap.TileHeight);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int nTileX = tileX + j;
                    int nTileY = tileY + i;

                    if (nTileX >= 0 && nTileX < tilemap.tiles.GetLength(1) &&
                        nTileY >= 0 && nTileY < tilemap.tiles.GetLength(0))
                    {
                        if (CheckRectangleCollision(collision,
                            tilemap.tiles[nTileY, nTileX].collision))
                        {
                            if (tilemap.tiles[nTileY, nTileX].IsWall)//добавить 1 action и подписывать на него действия
                                colAction();//Action<Tile>: colAction = (tile) => { if (tile.IsWall) player.Block(); else player.Unblock(); }
                        }
                    }
                }
            }
        }
    }
}
//!!!!ПРИ ОТРИСОВКЕ ТЕПЕРЬ МОДЕЛЬ РИСУЕТСЯ ОТ ЦЕНТРА(ТОЧНЕЕ ЦЕНТР МОДЕЛЬКИ НАТЯГИВАЕТСЯ НА КООРДИНАТЫ),
//А НЕ ОТ ВЕРХНЕГО ЛЕВОГО УГЛА!!!!!