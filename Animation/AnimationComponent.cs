using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Animation
{
    public class AnimationComponent
    {
        private SpriteSheet spriteSheet;
        private int[] frames;
        private float frameDuration;
        private int currentIndex;
        private float timer;

        public float FrameDuration
        {
            get { return frameDuration; }
            set { frameDuration = value; }
        }

        public AnimationComponent(SpriteSheet spriteSheet, int[] frames, float frameDuration)
        {
            this.spriteSheet = spriteSheet;
            this.frames = frames;
            this.frameDuration = frameDuration;
        }

        public void Update(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= frameDuration)
            {
                timer -= frameDuration;
                currentIndex = (currentIndex + 1) % frames.Length;
            }
        }

        public Rectangle GetCurrentFrameRect()
        {
            return spriteSheet.GetFrameRect(frames[currentIndex]);
        }

        public void Reset()
        {
            currentIndex = 0;
            timer = 0;
        }
    }
}
