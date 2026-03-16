using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Components
{
    public class RenderComponent
    {
        Texture2D model;
        float scale;

        public RenderComponent(Texture2D model, float scale)
        {
            this.model = model;
            this.scale = scale;
        }

        public void Draw(SpriteBatch spriteBatch, PositionComponent positionComponent)
        {
            spriteBatch.Draw(model, new Vector2(positionComponent.x, positionComponent.y), null, Color.White, 
                0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);
        }
    }
}
