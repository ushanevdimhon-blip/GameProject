using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Animation
{
    public enum AnimState { Idle, WalkRight, WalkLeft, WalkUp, WalkDown }
    public class AnimationManager
    {
        public Dictionary<AnimState, AnimationComponent> animations = new();
        private AnimState? currentState;
        public AnimationComponent currentAnim;

        public void Add(AnimState state, AnimationComponent animation)
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

        public void Update(float deltaTime)
        {
            currentAnim?.Update(deltaTime);
        }

        public Rectangle GetCurrentFrameRect()
        {
            return currentAnim.GetCurrentFrameRect();
        }
    }
}
