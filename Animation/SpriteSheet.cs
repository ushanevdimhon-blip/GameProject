using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Animation
{
    public class SpriteSheet
    {
        public Texture2D texture;
        private int frameWidth;
        private int frameHeight;
        private int columns;

        public SpriteSheet(Texture2D texture, int frameWidth, int frameHeight)
        {
            this.texture = texture;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            columns = texture.Width / frameWidth;
        }

        public Rectangle GetFrameRect(int index)
        {
            int col = index % columns;
            int row = index / columns;
            return new Rectangle(
                col * frameWidth,
                row * frameHeight,
                frameWidth,
                frameHeight
            );
        }
    }
}
