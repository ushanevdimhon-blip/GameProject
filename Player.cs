using GameProject.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
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

        float width;
        float height;
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

        public Player(Texture2D model, float x, float y, float scale)
        {
            this.model = model;
            this.width = model.Width * scale;
            this.height = model.Height * scale;
            positionComponent = new PositionComponent(x, y);
            speedComponent = new SpeedComponent(100.0f);
            render = new RenderComponent(model, scale);
            input = new InputComponent(speedComponent);
            collision = new CollisionComponent(positionComponent, this.width, this.height);
            healthComponent = new HealthComponent(100);

            OnDeath += () => Debug.WriteLine("Player died!");
            OnDeath += () => speedComponent.baseVelocity = 0.0f;
        }

        public void Update(GameTime gameTime)
        {
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
