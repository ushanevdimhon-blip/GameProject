using GameProject.Animation;
using GameProject.Collision;
using GameProject.Components;
using GameProject.TilemapManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

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
        KeysComponent keysComponent;
        public PositionComponent positionComponent;
        public CollisionComponent collision;

        public Action OnDeath;
        public Action OnDamage;
        public Action OnHeal;
        public Action OnAllKeysCollected;

        public int KeysCollected { get { return keysComponent.KeysCollected; } private set { } }
        public int Health { get { return healthComponent.Health; } }
        public float Stamina { get { return speedComponent.stamina; } }

        public const int MaxHealth = 100;
        public const float MaxStamina = 100.0f;
        private const float BaseVelocity = 160.0f;

        public Player(SpriteSheet sheet, Rectangle rectangle, float x, float y, float scale, int keysToCollect, Tilemap tilemap)
        {
            model = sheet.texture;
            var width = (rectangle.Width-10) * scale;
            var height = rectangle.Height * scale;

            SetAnimations(sheet);
            
            positionComponent=  new PositionComponent(x, y);
            speedComponent = new SpeedComponent(BaseVelocity, MaxStamina);
            render = new RenderComponent(model, scale);
            input = new InputComponent(speedComponent, positionComponent);
            collision = new CollisionComponent(positionComponent, width, height);
            healthComponent = new HealthComponent(MaxHealth);
            keysComponent = new KeysComponent(keysToCollect);

            SubcribeToEvents();
        }

        public void Update(GameTime gameTime)
        {
            keysComponent.Update();
            input.Update(gameTime);
            collision.Update();
            animationComponent.SetFrameDuration(speedComponent.isSprinting ? 0.04f : 0.1f);
            animationComponent.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            render.Draw(spriteBatch, positionComponent, animationComponent.GetCurrentFrameRect());
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

        public void CollectKey()
        {
            keysComponent.CollectKey();
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

            keysComponent.OnAllKeysCollected += () => OnAllKeysCollected?.Invoke();
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
