using GameProject.Components;
using GameProject.TilemapItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Entities
{
    public class Enemy
    {
        Texture2D model;
        RenderComponent render;       
        PatrolComponent patrol;        
        ChaseComponent chaseComponent;
        AttackComponent attackComponent;
        public PositionComponent positionComponent;
        public CollisionComponent collision;

        public Action OnAttack;
        public Action OnCooldown;

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

        public Enemy(Texture2D model, float scale, Tilemap tilemap)
        {
            this.model = model;
            width = model.Width * scale;
            height = model.Height * scale;
            render = new RenderComponent(model, scale);
            positionComponent = new PositionComponent(800, 400);
            patrol = new PatrolComponent(tilemap);
            collision = new CollisionComponent(positionComponent, width*1.5f, height*1.5f, 300);
            chaseComponent = new ChaseComponent(tilemap, 0.1f, 180.0f);//сделать константой
            attackComponent = new AttackComponent(4.0f);//сделать константой

            OnCooldown += () => { chaseComponent.ChangeMovementSpeed(50.0f); };//сделать константой
        }

        public void Update(GameTime gameTime)
        {
            attackComponent.Update(gameTime);
            if (attackComponent.cooldown >= 4.0f)//сделать константой
                chaseComponent.ChangeMovementSpeed(180.0f);//сделать константой
            collision.UpdateRectangleCollision();
            collision.UpdateCircleCollision();
        }

        public void Chase(PositionComponent playerPosition, GameTime gameTime)
        {
            chaseComponent.Chase(positionComponent, playerPosition, gameTime);
        }

        public void Patrol(List<(int X, int Y)> targetsPositions, GameTime gameTime)
        {
            patrol.Patrol(positionComponent, targetsPositions, gameTime);
        }

        public void Attack()
        {
            attackComponent.Attack(OnAttack, OnCooldown);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            render.Draw(spriteBatch, positionComponent);
        }
    }
}
