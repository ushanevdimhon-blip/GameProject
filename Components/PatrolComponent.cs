using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameProject.Components
{
    public class PatrolComponent
    {
        ChaseComponent chaseComponent;
        int counter = 0;
        Tilemap tilemap;

        public PatrolComponent(Tilemap tilemap)
        {
            this.tilemap = tilemap;
            this.chaseComponent = new ChaseComponent(tilemap, 0.2f, 100.0f);
        }

        public void Patrol(PositionComponent currentPosition, List<(int X, int Y)> targetsPositions, GameTime gameTime)
        {  
            var target = targetsPositions[counter];
            float posX = tilemap.tiles[target.Y, target.X].position.X;
            float posY = tilemap.tiles[target.Y, target.X].position.Y;
            chaseComponent.Chase(currentPosition, new PositionComponent(posX, posY), gameTime);
            var next = new Vector2(posX, posY);
            Vector2 direction = next - new Vector2(currentPosition.X, currentPosition.Y);
            float distance = direction.Length();
            if (distance < 30.0f)
            {
                counter++;
                if (counter >= targetsPositions.Count)
                {
                    counter = 0;
                }
            }
        }
    }
}
