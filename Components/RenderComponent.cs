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

        /// <summary>
        ///  отрисовывает спрайт так, чтобы его центр совпадал с позицией, заданной в PositionComponent
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="positionComponent"></param>
        public void Draw(SpriteBatch spriteBatch, PositionComponent positionComponent)
        {
            Vector2 origin = new Vector2(model.Width / 2f, model.Height / 2f);

            spriteBatch.Draw(model, new Vector2(positionComponent.X, positionComponent.Y), null, Color.White, 
                0.0f, origin, scale, SpriteEffects.None, 0.0f);
        }
    }
}
