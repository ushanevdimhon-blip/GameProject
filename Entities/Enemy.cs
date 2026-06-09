using GameProject.Animation;
using GameProject.Collision;
using GameProject.Components;
using GameProject.TilemapManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameProject.Entities
{
    public class Enemy
    {
        Texture2D model;
        AnimationComponent animationComponent;
        RenderComponent render;       
        ViewComponent viewComponent;
        PatrolComponent patrol;        
        ChaseComponent chaseComponent;
        AttackComponent attackComponent;
        DirectionComponent directionComponent;
        public PositionComponent positionComponent;
        public CollisionComponent collision;

        public Action OnAttack;
        public Action OnCooldown;

        const float BaseVelocity = 200.0f;
        const float PatrolVelocity = 150.0f;
        const float CooldownTime = 4.0f;
        const int SightRadius = 800;

        public Enemy(SpriteSheet sheet, Rectangle rectangle, float scale, Tilemap tilemap)
        {
            model = sheet.texture;
            var width = rectangle.Width * scale;
            var height = rectangle.Height * scale;

            SetAnimations(sheet);
            Spawn(tilemap);

            directionComponent = new DirectionComponent();
            viewComponent = new ViewComponent(positionComponent);
            render = new RenderComponent(model, scale);         
            collision = new CollisionComponent(positionComponent, width, height, SightRadius);
            chaseComponent = new ChaseComponent(tilemap, BaseVelocity);
            patrol = new PatrolComponent(tilemap, chaseComponent, PatrolVelocity);
            attackComponent = new AttackComponent(CooldownTime);

            SubcribeToEvents();
        }

        public void Update(GameTime gameTime)
        {
            attackComponent.Update(gameTime);
            if (attackComponent.cooldown >= CooldownTime)
                chaseComponent.ChangeMovementSpeed(BaseVelocity);
            
            collision.Update(chaseComponent.CurrentDirection);
            
            directionComponent.Update(chaseComponent.CurrentDirection);
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
        }

        public void Chase(PositionComponent playerPosition, GameTime gameTime)
        {
            chaseComponent.Chase(positionComponent, playerPosition, gameTime);
        }

        public void Patrol(List<(int X, int Y)> targetsPositions, Tilemap tilemap, GameTime gameTime)
        {
            patrol.Patrol(positionComponent, targetsPositions, gameTime);
        }

        public void Attack()
        {
            attackComponent.Attack(OnAttack, OnCooldown);
        }

        public bool HasLineOfSight(PositionComponent playerPosition, Tilemap map)
        {
            return viewComponent.HasLineOfSight(playerPosition, map);
        }

        private void SubcribeToEvents()
        {
            OnCooldown += () => { chaseComponent.ChangeMovementSpeed(BaseVelocity / 4); };
            directionComponent.OnUp += () =>
            {
                if (animationComponent.currentAnim.isFinished || animationComponent.currentAnim.isLooping)
                    animationComponent.Play(AnimState.WalkUp);
            };
            directionComponent.OnDown += () =>
            {
                if (animationComponent.currentAnim.isFinished || animationComponent.currentAnim.isLooping)
                    animationComponent.Play(AnimState.WalkDown);
            };
            directionComponent.OnRight += () =>
            {
                if (animationComponent.currentAnim.isFinished || animationComponent.currentAnim.isLooping)
                    animationComponent.Play(AnimState.WalkRight);
            };
            directionComponent.OnLeft += () =>
            {
                if (animationComponent.currentAnim.isFinished || animationComponent.currentAnim.isLooping)
                    animationComponent.Play(AnimState.WalkLeft);
            };
        }

        private void SetAnimations(SpriteSheet sheet)
        {
            animationComponent = new AnimationComponent();

            animationComponent.Add(AnimState.WalkDown,
                new Animation.Animation(sheet, new int[] { 0, 1, 2, 3, 4, 5 }, true));
            animationComponent.Add(AnimState.WalkUp,
                new Animation.Animation(sheet, new int[] { 6, 7, 8, 9, 10, 11 }, true));
            animationComponent.Add(AnimState.WalkLeft,
                new Animation.Animation(sheet, new int[] { 12, 13, 14, 15, 16, 17 }, true));
            animationComponent.Add(AnimState.WalkRight,
                new Animation.Animation(sheet, new int[] { 18, 19, 20, 21, 22, 23 }, true));

            animationComponent.currentAnim = animationComponent.animations[AnimState.WalkDown];
        }
    }
}
