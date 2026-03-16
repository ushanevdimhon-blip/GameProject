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

        public RenderComponent(Texture2D model)
        {
            this.model = model;
        }

        public void Draw(SpriteBatch spriteBatch, PositionComponent positionComponent)
        {
            spriteBatch.Draw(model, new Vector2(positionComponent.x, positionComponent.y), Color.White);
        }
    }
}
