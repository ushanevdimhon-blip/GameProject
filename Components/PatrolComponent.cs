using GameProject.TilemapItems;
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

        public PatrolComponent(Tilemap tilemap, ChaseComponent chaseComponent)
        {
            this.tilemap = tilemap;
            this.chaseComponent = chaseComponent;
        }

        public void Patrol(PositionComponent currentPosition, List<(int X, int Y)> targetsPositions, GameTime gameTime)
        {  
            if (targetsPositions.Count <= counter)
                counter = 0;
            
            var target = targetsPositions[counter];
            float posX = tilemap.tiles[target.Y, target.X].position.X;
            float posY = tilemap.tiles[target.Y, target.X].position.Y;
            chaseComponent.ChangeMovementSpeed(120.0f);
            chaseComponent.Chase(currentPosition, new PositionComponent(posX, posY), gameTime);
            var next = new Vector2(posX, posY);
            Vector2 direction = next - new Vector2(currentPosition.X, currentPosition.Y);
            float distance = direction.Length();
            if (distance < 70.0f)
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
