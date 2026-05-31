using GameProject.Animation;
using GameProject.Components;
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
        Rectangle rectangle;
        AnimationManager animationManager;
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

        float width;
        float height;
        int keysToCollect;
        int keysCollected;

        public int KeysCollected { get { return keysCollected; } set { keysCollected = value; } }
        /// <summary>
        /// ширина модели умноженная на масштаб
        /// </summary>
        public float Width { get { return width; } private set { width = value; } }
        /// <summary>
        /// высота модели умноженная на масштаб
        /// </summary>
        public float Height { get { return height; } private set { height = value; } }

        public int Health { get { return healthComponent.Health; } }
        public int MaxHealth { get { return healthComponent.MaxHealth; } }
        public float Stamina { get { return input.speedComponent.stamina; } }
        public float MaxStamina { get { return input.speedComponent.MaxStamina; } }

        public Player(SpriteSheet sheet, Rectangle rectangle, float x, float y, float scale, int keysToCollect)
        {
            this.model = sheet.texture;
            this.rectangle = rectangle;
            width = (rectangle.Width-10) * scale;
            height = rectangle.Height * scale;
            this.keysToCollect = keysToCollect;

            animationManager = new AnimationManager();
            //вынести в animationManager? типа init
            animationManager.Add(AnimState.WalkDown, 
                new AnimationComponent(sheet, new int[] { 16, 17, 18, 19, 20, 21, 22, 23}, 0.1f));
            animationManager.Add(AnimState.WalkUp,
                new AnimationComponent(sheet, new int[] { 24, 25, 26, 27, 28, 29, 30, 31 }, 0.1f));
            animationManager.Add(AnimState.WalkLeft,
                new AnimationComponent(sheet, new int[] { 8, 9, 10, 11, 12, 13, 14, 15 }, 0.1f));
            animationManager.Add(AnimState.WalkRight,
                new AnimationComponent(sheet, new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 0.1f));
            animationManager.Add(AnimState.Idle,
                new AnimationComponent(sheet, new int[] { 32, 33, 34, 35 }, 0.1f));
            animationManager.currentAnim = animationManager.animations[AnimState.Idle];

            positionComponent = new PositionComponent(x, y);
            speedComponent = new SpeedComponent(140.0f, 100.0f);//сделать константой
            render = new RenderComponent(model, scale);
            input = new InputComponent(speedComponent);
            collision = new CollisionComponent(positionComponent, width, height);
            healthComponent = new HealthComponent(100);

            OnDeath += () => Debug.WriteLine("Player died!");
            input.OnUp += () => animationManager.Play(AnimState.WalkUp);
            input.OnDown += () => animationManager.Play(AnimState.WalkDown);
            input.OnRight += () => animationManager.Play(AnimState.WalkRight);
            input.OnLeft += () => animationManager.Play(AnimState.WalkLeft);
            input.OnIdle += () => animationManager.Play(AnimState.Idle);
        }

        public void Update(GameTime gameTime)
        {
            if (keysCollected >= keysToCollect)
            {
                OnAllKeysCollected?.Invoke();
                keysCollected = 0;
            }
            input.Update(positionComponent, gameTime);
            collision.UpdateRectangleCollision();
            animationManager.currentAnim.FrameDuration = speedComponent.isSprinting ? 0.04f : 0.1f;
            animationManager.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            render.Draw(spriteBatch, positionComponent, animationManager.GetCurrentFrameRect());
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
    }
}
