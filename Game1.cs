using GameProject.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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
        List<(int X, int Y)> patrolTargets;
        float delay = 3.0f;
        bool isDetected = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: почистить LoadContent, вынести логику иницилизации

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
            };//сделать чтение из xml файла например
            wallTexture = Content.Load<Texture2D>("Images/Wall");
            floorTexture = Content.Load<Texture2D>("Images/Floor"); 
            tilemap = new Tilemap(tileData, 50, 50, wallTexture, floorTexture);           
            worldWidth = tilemap.tiles.GetLength(1) * tilemap.TileWidth;
            worldHeight = tilemap.tiles.GetLength(0) * tilemap.TileHeight;
            patrolTargets = new List<(int X, int Y)> { (15, 11), (2, 2), (5, 7) };
            enemy = new Enemy(enemyTexture, 0.01f, tilemap);
            enemy.OnAttack += () => player.TakeDamage(30);

            camera = new Camera(GraphicsDevice.PresentationParameters.BackBufferWidth, 
                GraphicsDevice.PresentationParameters.BackBufferHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);
            enemy.Update(gameTime);

            camera.Follow(player.positionComponent);
            camera.Clamp(worldWidth, worldHeight);
            camera.Update();
            
            if (CheckRectangleCollision(player.collision, enemy.collision))
            {
                enemy.Attack();
            }

            if (CheckCircleCollision(enemy.collision, player.collision) && 
                HasLineOfSight(player.positionComponent, enemy.positionComponent, tilemap))
            {
                enemy.Chase(player.positionComponent, gameTime);
                isDetected = true;
            }
            else
            {
                if (delay > 0.1f && isDetected)
                {
                    enemy.Chase(new PositionComponent(player.positionComponent.X, player.positionComponent.Y), gameTime);
                    delay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    enemy.Patrol(patrolTargets, gameTime);
                    isDetected = false;
                    delay = 3.0f;
                }
            }

            CheckTilesCollision(tilemap, player.positionComponent, player.collision, player.Block);

            Rectangle cameraBounds = camera.GetCameraBounds();
            GetCameraCollision(player.collision, cameraBounds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

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

        public static bool HasLineOfSight(PositionComponent playerPosition, PositionComponent enemyPosition,
            Tilemap map)
        {
            float dx = playerPosition.X - enemyPosition.X;
            float dy = playerPosition.Y - enemyPosition.Y;
            float dist = MathF.Sqrt(dx * dx + dy * dy);
            if (dist == 0) return true;

            int steps = (int)(dist / (map.TileWidth / 2f));
            for (int i = 1; i < steps; i++)
            {
                float t = (float)i / steps;
                int col = (int)((enemyPosition.X + dx * t) / map.TileWidth);
                int row = (int)((enemyPosition.Y + dy * t) / map.TileWidth);
                if (map.tiles[row, col].IsWall)
                    return false;
            }
            return true;
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

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
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