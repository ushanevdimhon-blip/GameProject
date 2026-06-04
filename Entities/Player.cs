using GameProject.Animation;
using GameProject.Components;
using GameProject.TilemapItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Entities
{
    public class Player
    {
        Texture2D model;
        AnimationComponent animationComponent;
        RenderComponent render;
        InputComponent input;
        SpeedComponent speedComponent;
        HealthComponent healthComponent;
        public PositionComponent positionComponent;
        public CollisionComponent collision;

        public Action OnDeath;
        public Action OnDamage;
        public Action OnHeal;
        public Action OnAllKeysCollected;

        int keysToCollect;
        int keysCollected;

        public int KeysCollected { get { return keysCollected; } set { keysCollected = value; } }
        public int Health { get { return healthComponent.Health; } }
        public float Stamina { get { return speedComponent.stamina; } }

        public const int MaxHealth = 100;
        public const float MaxStamina = 100.0f;
        private const float BaseVelocity = 140.0f;

        public Player(SpriteSheet sheet, Rectangle rectangle, float scale, int keysToCollect, Tilemap tilemap)
        {
            model = sheet.texture;
            var width = (rectangle.Width-10) * scale;
            var height = rectangle.Height * scale;
            this.keysToCollect = keysToCollect;

            SetAnimations(sheet);
            Spawn(tilemap);

            speedComponent = new SpeedComponent(BaseVelocity, MaxStamina);
            render = new RenderComponent(model, scale);
            input = new InputComponent(speedComponent, positionComponent);
            collision = new CollisionComponent(positionComponent, width, height);
            healthComponent = new HealthComponent(MaxHealth);

            SubcribeToEvents();
        }

        public void Update(GameTime gameTime)
        {
            if (keysCollected >= keysToCollect)
            {
                OnAllKeysCollected?.Invoke();
                keysCollected = 0;
            }
            input.Update(gameTime);
            collision.Update();
            animationComponent.SetFrameDuration(speedComponent.isSprinting ? 0.04f : 0.1f);
            animationComponent.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            render.Draw(spriteBatch, positionComponent, animationComponent.GetCurrentFrameRect());
        }

        public void Spawn(Tilemap tilemap)
        {
            var tileInd = tilemap.GetRandomFloorTileIndex();
            var tile = tilemap.tiles[tileInd.Y, tileInd.X];
            positionComponent = new PositionComponent(tile.position.X, tile.position.Y);
            Debug.WriteLine($"Player spawned at: Position({tile.position.X},{tile.position.Y}), TileIndex({tileInd.Y},{tileInd.X})");
        }

        public void TakeDamage(int damage)
        {
            healthComponent.TakeDamage(damage, OnDamage, OnDeath);
        }

        public void Heal(int hp)
        {
            healthComponent.Heal(hp, OnHeal);
        }

        public void RestoreStamina()
        {
            speedComponent.RestoreStamina();
        }

        public void Block()
        {
            positionComponent.Block();
        }

        private void SubcribeToEvents()
        {
            input.OnUp += () => animationComponent.Play(AnimState.WalkUp);
            input.OnDown += () => animationComponent.Play(AnimState.WalkDown);
            input.OnRight += () => animationComponent.Play(AnimState.WalkRight);
            input.OnLeft += () => animationComponent.Play(AnimState.WalkLeft);
            input.OnIdle += () => animationComponent.Play(AnimState.Idle);
        }

        private void SetAnimations(SpriteSheet sheet)
        {
            animationComponent = new AnimationComponent();

            animationComponent.Add(AnimState.WalkDown,
                new Animation.Animation(sheet, new int[] { 16, 17, 18, 19, 20, 21, 22, 23 }, true));
            animationComponent.Add(AnimState.WalkUp,
                new Animation.Animation(sheet, new int[] { 24, 25, 26, 27, 28, 29, 30, 31 }, true));
            animationComponent.Add(AnimState.WalkLeft,
                new Animation.Animation(sheet, new int[] { 8, 9, 10, 11, 12, 13, 14, 15 }, true));
            animationComponent.Add(AnimState.WalkRight,
                new Animation.Animation(sheet, new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, true));
            animationComponent.Add(AnimState.Idle,
                new Animation.Animation(sheet, new int[] { 32, 33, 34, 35 }, true));

            animationComponent.currentAnim = animationComponent.animations[AnimState.Idle];
        }
    }
}
