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
        RenderComponent render;
        InputComponent input;
        SpeedComponent speedComponent;
        HealthComponent healthComponent;
        public PositionComponent positionComponent;
        public CollisionComponent collision;

        public Action OnDeath;
        public Action OnDamage;
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
        public float Stamina { get { return input.speedComponent.stamina; } }

        public Player(Texture2D model, float x, float y, float scale, int keysToCollect)
        {
            this.model = model;
            width = model.Width * scale;
            height = model.Height * scale;
            this.keysToCollect = keysToCollect;
            positionComponent = new PositionComponent(x, y);
            speedComponent = new SpeedComponent(140.0f);//сделать константой
            render = new RenderComponent(model, scale);
            input = new InputComponent(speedComponent);
            collision = new CollisionComponent(positionComponent, width, height);
            healthComponent = new HealthComponent(100);

            OnDeath += () => Debug.WriteLine("Player died!");
            OnDeath += () => speedComponent.baseVelocity = 0.0f;
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            render.Draw(spriteBatch, positionComponent);
        }

        public void TakeDamage(int damage)
        {
            healthComponent.TakeDamage(damage, OnDamage, OnDeath);
        }

        public void Block()
        {
            positionComponent.Block();
        }
    }
}
