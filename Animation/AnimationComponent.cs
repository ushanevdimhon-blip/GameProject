using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Animation
{
    public enum AnimState { Idle, WalkRight, WalkLeft, WalkUp, WalkDown, AttackRight, AttackLeft, AttackUp, AttackDown }
    public class AnimationComponent
    {
        public Dictionary<AnimState, Animation> animations = new();
        private AnimState? currentState;
        public Animation currentAnim;

        public void Add(AnimState state, Animation animation)
        {
            animations[state] = animation;
        }

        public void Play(AnimState state)
        {
            if (currentState == state)
                return;
            currentState = state;
            currentAnim = animations[state];
            currentAnim.Reset();
        }

        public void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            currentAnim?.Update(deltaTime);
        }

        public void SetFrameDuration(float duration)
        {
            currentAnim.FrameDuration = duration;
        }

        public Rectangle GetCurrentFrameRect()
        {
            return currentAnim.GetCurrentFrameRect();
        }
    }
}
