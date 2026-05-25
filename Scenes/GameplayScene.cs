using GameProject.Components;
using GameProject.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameProject.Scenes
{
    public class GameplayScene : Scene
    {
        SpriteBatch _spriteBatch;
        Player player;
        Enemy enemy;
        Texture2D playerTexture;
        Texture2D enemyTexture;
        Tilemap tilemap;
        Texture2D wallTexture;
        Texture2D floorTexture;
        Texture2D keyTexture;
        Texture2D key2Texture;
        Texture2D doorTexture;
        Camera camera;
        UIPresenter uiPresenter;
        float worldWidth;
        float worldHeight;
        List<(int X, int Y)> patrolTargets;
        int keysToCollect = 1;
        float delay = 3.0f;
        bool isDetected = false;

        GraphicsDevice _graphicsDevice;
        public Action OnGameOver;
        public Action OnGameWon;

        public GameplayScene(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _graphicsDevice = graphicsDevice;
            Content = contentManager;
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_graphicsDevice);

            playerTexture = Content.Load<Texture2D>("Images/5053745_0");
            enemyTexture = Content.Load<Texture2D>("Images/vecteezy_angry-face-emoji-png-file_11997334");
            wallTexture = Content.Load<Texture2D>("Images/Wall");
            floorTexture = Content.Load<Texture2D>("Images/Floor");
            keyTexture = Content.Load<Texture2D>("Images/Key");
            key2Texture = Content.Load<Texture2D>("Images/Key2");
            doorTexture = Content.Load<Texture2D>("Images/Door");

            string[,] tileData = new string[,] {
                { "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01", "01" },
                { "01", "09", "09", "09", "09", "09", "09", "01", "09", "09", "09", "01", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "09", "09", "09", "01", "09", "01", "09", "01", "09", "01", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "01", "01", "09", "09", "09", "09", "01", "09", "01", "09", "01", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "01", "09", "09", "09", "09", "01", "09", "01", "09", "01", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "01", "09", "09", "09", "09", "01", "09", "01", "09", "01", "09", "09", "09", "09", "09", "09", "01" },
                { "02", "09", "01", "09", "09", "09", "09", "01", "09", "01", "09", "01", "09", "09", "09", "09", "09", "09", "01" },
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
                { "01", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "01" },
                { "01", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "09", "01" },
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

            tilemap = new Tilemap(tileData, 50, 50, wallTexture, floorTexture, doorTexture);
            tilemap.SpaunItem(keyTexture, keysToCollect);

            worldWidth = tilemap.tiles.GetLength(1) * tilemap.TileWidth;
            worldHeight = tilemap.tiles.GetLength(0) * tilemap.TileHeight;
            patrolTargets = tilemap.GetKeysIndexes();

            player = new Player(playerTexture, 300, 500, 0.08f, keysToCollect);
            player.collision.TileCollisionDetected += (tile) =>
            {
                if (tile.IsWall || tile.IsClosedDoor)
                    player.Block();
                if (tile.IsKey)
                {
                    tilemap.Update((tile.TileIndex.Y, tile.TileIndex.X), key2Texture);
                    player.KeysCollected++;
                    patrolTargets[patrolTargets.IndexOf((tile.TileIndex.Y, tile.TileIndex.X))] = tilemap.GetRandomFloorTileIndex();
                }
                if (tile.IsOpenDoor)
                {
                    OnGameWon.Invoke();
                    player.Block();
                }
            };
            player.OnAllKeysCollected += () =>
            {
                var index = tilemap.GetDoorIndex();
                tilemap.Update(index, floorTexture);
                tilemap.tiles[index.Y, index.X].IsOpenDoor = true;
            };
            player.OnDeath += () => OnGameOver.Invoke();

            enemy = new Enemy(enemyTexture, 0.01f, tilemap);
            enemy.OnAttack += () => player.TakeDamage(30);
            
            uiPresenter = new UIPresenter(new UIModel(10, 10, 100, 100), new UIView());

            camera = new Camera(_graphicsDevice.PresentationParameters.BackBufferWidth,
                _graphicsDevice.PresentationParameters.BackBufferHeight);
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            enemy.Update(gameTime);

            camera.Follow(player.positionComponent);
            camera.Clamp(worldWidth, worldHeight);
            camera.Update();

            uiPresenter.Update(player.Health, player.Stamina);

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

            CheckTilesCollision(tilemap, player.positionComponent, player.collision, player.collision.TileCollisionDetected);

            Rectangle cameraBounds = camera.GetCameraBounds();
            GetCameraCollision(player.collision, cameraBounds);
        }

        public override void Draw(GameTime gameTime)
        {
            _graphicsDevice.Clear(Color.Beige);

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

        private static bool HasLineOfSight(PositionComponent playerPosition, PositionComponent enemyPosition,
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

        private bool CheckRectangleCollision(CollisionComponent collision1, CollisionComponent collision2)
        {
            return collision1.collisionRectangle.Intersects(collision2.collisionRectangle);
        }

        private bool CheckCircleCollision(CollisionComponent collision1, CollisionComponent collision2)
        {
            return collision1.collisionCircle.Intersects(collision2.collisionRectangle);
        }

        private void GetCameraCollision(CollisionComponent collisionObject, Rectangle cameraBounds)
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
            CollisionComponent collision, Action<Tile> colAction)
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
                            colAction(tilemap.tiles[nTileY, nTileX]);
                        }
                    }
                }
            }
        }
    }
}//!!!!ПРИ ОТРИСОВКЕ ТЕПЕРЬ МОДЕЛЬ РИСУЕТСЯ ОТ ЦЕНТРА(ТОЧНЕЕ ЦЕНТР МОДЕЛЬКИ НАТЯГИВАЕТСЯ НА КООРДИНАТЫ),
//А НЕ ОТ ВЕРХНЕГО ЛЕВОГО УГЛА!!!!!