using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Animation
{
    public class Animation
    {
        private SpriteSheet spriteSheet;
        private int[] frames;
        private float frameDuration = 0.1f;
        private int currentIndex;
        private float timer;
        public bool isLooping;
        public bool isFinished;

        public float FrameDuration { get { return frameDuration; } set { frameDuration = value; } }

        public Animation(SpriteSheet spriteSheet, int[] frames, bool isLooping)
        {
            this.spriteSheet = spriteSheet;
            this.frames = frames;
            this.frameDuration = frameDuration;
            this.isLooping = isLooping;
        }

        public void Update(float deltaTime)
        {
            if (isFinished)
                return;

            timer += deltaTime;
            if (timer >= frameDuration)
            {
                timer -= frameDuration;
                currentIndex++;

                if (currentIndex >= frames.Length)
                {
                    if (isLooping)
                    {
                        currentIndex = 0;
                    }
                    else
                    {
                        currentIndex = frames.Length - 1;
                        isFinished = true;
                    }
                }
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
            isFinished = false;
        }
    }
}
