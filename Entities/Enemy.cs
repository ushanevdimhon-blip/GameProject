using GameProject.Animation;
using GameProject.Components;
using GameProject.TilemapItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Entities
{
    public class Enemy
    {
        Texture2D model;
        Rectangle rectangle;
        AnimationManager animationManager;
        RenderComponent render;       
        PatrolComponent patrol;        
        ChaseComponent chaseComponent;
        AttackComponent attackComponent;
        DirectionComponent directionComponent;
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

        public Enemy(SpriteSheet sheet, SpriteSheet attackSheet, Rectangle rectangle, float scale, Tilemap tilemap)
        {
            this.model = sheet.texture;
            this.rectangle = rectangle;
            width = rectangle.Width * scale;
            height = rectangle.Height * scale;

            animationManager = new AnimationManager();
            //вынести в animationManager? типа init
            animationManager.Add(AnimState.WalkDown,
                new AnimationComponent(sheet, new int[] { 0, 1, 2, 3, 4, 5 }, 0.1f, true));
            animationManager.Add(AnimState.WalkUp,
                new AnimationComponent(sheet, new int[] { 6, 7, 8, 9, 10, 11 }, 0.1f, true));
            animationManager.Add(AnimState.WalkLeft,
                new AnimationComponent(sheet, new int[] { 12, 13, 14, 15, 16, 17 }, 0.1f, true));
            animationManager.Add(AnimState.WalkRight,
                new AnimationComponent(sheet, new int[] { 18, 19, 20, 21, 22, 23 }, 0.1f, true));
            animationManager.Add(AnimState.AttackUp,
                new AnimationComponent(attackSheet, new int[] { 10, 11, 12, 13 }, 0.1f, true));
            animationManager.Add(AnimState.AttackDown,
                new AnimationComponent(attackSheet, new int[] { 2, 3, 4, 5 }, 0.3f, true));
            animationManager.currentAnim = animationManager.animations[AnimState.WalkDown];

            directionComponent = new DirectionComponent();
            render = new RenderComponent(model, scale);
            positionComponent = new PositionComponent(900, 400);
            collision = new CollisionComponent(positionComponent, width, height, 300);
            chaseComponent = new ChaseComponent(tilemap, 0.1f, 180.0f);//сделать константой
            patrol = new PatrolComponent(tilemap, chaseComponent);
            attackComponent = new AttackComponent(4.0f);//сделать константой

            OnCooldown += () => { chaseComponent.ChangeMovementSpeed(50.0f); };//сделать константой
            directionComponent.OnUp += () => 
            {
                if ((animationManager.currentAnim.isFinished || animationManager.currentAnim.isLooping) && !attackComponent.IsAttacking)
                    animationManager.Play(AnimState.WalkUp);
                else if (attackComponent.IsAttacking)
                    animationManager.Play(AnimState.AttackDown);
            };
            directionComponent.OnDown += () => 
            {
                if (animationManager.currentAnim.isFinished || animationManager.currentAnim.isLooping)
                    animationManager.Play(AnimState.WalkDown); 
            };
            directionComponent.OnRight += () => 
            {
                if (animationManager.currentAnim.isFinished || animationManager.currentAnim.isLooping)
                    animationManager.Play(AnimState.WalkRight); 
            };
            directionComponent.OnLeft += () => 
            {
                if (animationManager.currentAnim.isFinished || animationManager.currentAnim.isLooping)
                    animationManager.Play(AnimState.WalkLeft); 
            };
        }

        public void Update(GameTime gameTime)
        {
            attackComponent.Update(gameTime);
            if (attackComponent.cooldown >= 4.0f)//сделать константой
                chaseComponent.ChangeMovementSpeed(180.0f);//сделать константой
            collision.UpdateRectangleCollision();
            collision.UpdateCircleCollision();
            directionComponent.Update(chaseComponent.CurrentDirection);
            animationManager.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
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
            render.Draw(spriteBatch, positionComponent, animationManager.GetCurrentFrameRect());
        }
    }
}
