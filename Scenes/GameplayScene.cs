using GameProject.Animation;
using GameProject.Collision;
using GameProject.Components;
using GameProject.DebugHelper;
using GameProject.Entities;
using GameProject.TilemapItems;
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
        GraphicsDevice _graphicsDevice;
        SpriteBatch _spriteBatch;
        SpriteFont arialFont;
        SpriteSheet playerSpriteSheet;
        SpriteSheet enemySpriteSheet;
        SpriteSheet enemyAttackSpriteSheet;
        Player player;
        Enemy enemy;
        Tilemap tilemap;
        Texture2D wallTexture;
        Texture2D floorTexture;
        Texture2D keyTexture;
        Texture2D key2Texture;
        Texture2D doorTexture;
        Texture2D medTexture;
        Texture2D boostTexture;
        Camera camera;
        UIPresenter uiPresenter;
        UIModel uiModel;
        UIView uiView;

        List<(int X, int Y)> patrolTargets;
        string[,] tileData;
        float worldWidth;
        float worldHeight;
        const int keysToCollect = 2;
        const int meds = 5;
        const int boosts = 5;
        float delay = 3.0f;
        bool isDetected = false;

        public Action OnGameOver;
        public Action OnGameWon;

        public GameplayScene(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _graphicsDevice = graphicsDevice;
            Content = contentManager;
        }

        public override void Initialize()
        {
            base.Initialize();

            tilemap = new Tilemap(110, 110, wallTexture, floorTexture, doorTexture);
            tilemap.Create(tileData);

            tilemap.SpawnItem(TileType.Key, keyTexture, keysToCollect);
            tilemap.SpawnItem(TileType.Medicine, medTexture, meds);
            tilemap.SpawnItem(TileType.Boost, boostTexture, boosts);

            worldWidth = tilemap.tiles.GetLength(1) * tilemap.TileWidth;
            worldHeight = tilemap.tiles.GetLength(0) * tilemap.TileHeight;
            patrolTargets = tilemap.GetItemIndexes(TileType.Key);

            uiModel = new UIModel(10, 10, 100, 100, keysToCollect, arialFont);
            uiView = new UIView(_graphicsDevice);
            uiPresenter = new UIPresenter(uiModel, uiView);

            player = new Player(playerSpriteSheet, playerSpriteSheet.GetFrameRect(0), 2.0f, keysToCollect, tilemap);
            SubscribeToPlayerEvents();

            enemy = new Enemy(enemySpriteSheet, enemySpriteSheet.GetFrameRect(0), 3.0f, tilemap);
            SubscribeToEnemyEvents();

            camera = new Camera(_graphicsDevice.PresentationParameters.BackBufferWidth,
                _graphicsDevice.PresentationParameters.BackBufferHeight);

            camera.Follow(player.positionComponent);
            camera.Clamp(worldWidth, worldHeight);
            camera.Update();
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_graphicsDevice);

            arialFont = Content.Load<SpriteFont>("Fonts/Arial");

            playerSpriteSheet = new SpriteSheet(Content.Load<Texture2D>("Images/npc01_spritesheet"), 30, 48);
            enemySpriteSheet = new SpriteSheet(Content.Load<Texture2D>("Images/orc1_walk_full"), 64, 64);
            enemyAttackSpriteSheet = new SpriteSheet(Content.Load<Texture2D>("Images/orc1_attack_full"), 64, 64);

            wallTexture = Content.Load<Texture2D>("Images/wall_old");
            floorTexture = Content.Load<Texture2D>("Images/floor_old_3");
            keyTexture = Content.Load<Texture2D>("Images/Key_old");
            key2Texture = Content.Load<Texture2D>("Images/Key2_old");
            doorTexture = Content.Load<Texture2D>("Images/door_old_2");
            medTexture = Content.Load<Texture2D>("Images/med_old");
            boostTexture = Content.Load<Texture2D>("Images/boost_old_2");

            tileData = Tilemap.FromFile("map.txt");
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            enemy.Update(gameTime);

            camera.Follow(player.positionComponent);
            camera.Clamp(worldWidth, worldHeight);
            camera.Update();

            uiPresenter.Update(gameTime, player.Health, player.Stamina, keysToCollect - player.KeysCollected);

            //вынести
            if (CollisionChecker.CheckRectangleCollision(player.collision, enemy.collision))
            {
                enemy.Attack();
            }

            if (CollisionChecker.CheckCircleCollision(enemy.collision, player.collision) &&
                enemy.HasLineOfSight(player.positionComponent, tilemap))
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
                    enemy.Patrol(patrolTargets, tilemap, gameTime);
                    isDetected = false;
                    delay = 3.0f;
                }
            }

            CollisionChecker.CheckTilesCollision(tilemap, player.positionComponent, player.collision, player.collision.TileCollisionDetected);

            Rectangle cameraBounds = camera.GetCameraBounds();
            CollisionChecker.GetCameraCollision(player.collision, cameraBounds);
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
            Debug.DrawRectangle(_spriteBatch, player.collision.collisionRectangle, Color.Green);

            enemy.Draw(_spriteBatch);
            Debug.DrawRectangle(_spriteBatch, enemy.collision.collisionRectangle, Color.Green);
            Debug.DrawCircle(_spriteBatch, enemy.collision.collisionCircle, Color.Green);

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

        private void SubscribeToPlayerEvents()
        {
            player.collision.TileCollisionDetected += (tile) =>
            {
                if (tile.Type == TileType.Wall || tile.Type == TileType.ClosedDoor)
                    player.Block();
                if (tile.Type == TileType.Key)
                {
                    tilemap.Update((tile.TileIndex.Y, tile.TileIndex.X), key2Texture, TileType.Floor);
                    player.KeysCollected++;
                    patrolTargets[patrolTargets.IndexOf((tile.TileIndex.Y, tile.TileIndex.X))] = tilemap.GetRandomFloorTileIndex();
                }
                if (tile.Type == TileType.Medicine)
                {
                    if (player.Health != Player.MaxHealth)
                    {
                        tilemap.Update((tile.TileIndex.Y, tile.TileIndex.X), floorTexture, TileType.Floor);
                        player.Heal(20);
                    }
                }
                if (tile.Type == TileType.Boost)
                {
                    if (player.Stamina != Player.MaxStamina)
                    {
                        tilemap.Update((tile.TileIndex.Y, tile.TileIndex.X), floorTexture, TileType.Floor);
                        player.RestoreStamina();
                    }
                }
                if (tile.Type == TileType.OpenDoor)
                {
                    OnGameWon.Invoke();
                    player.Block();
                }
            };
            player.OnAllKeysCollected += () =>
            {
                var indexes = tilemap.GetItemIndexes(TileType.ClosedDoor);
                patrolTargets.Clear();
                foreach (var index in indexes)
                {
                    tilemap.Update(index, floorTexture, TileType.OpenDoor);
                    patrolTargets.Add(index);
                }
                uiModel.allButtonsPressed = true;
            };
            player.OnDeath += () => OnGameOver.Invoke();
        }

        private void SubscribeToEnemyEvents()
        {
            enemy.OnAttack += () => player.TakeDamage(40);
        }
    }
}//!!!!ПРИ ОТРИСОВКЕ ТЕПЕРЬ МОДЕЛЬ РИСУЕТСЯ ОТ ЦЕНТРА(ТОЧНЕЕ ЦЕНТР МОДЕЛЬКИ НАТЯГИВАЕТСЯ НА КООРДИНАТЫ),
//А НЕ ОТ ВЕРХНЕГО ЛЕВОГО УГЛА!!!!!