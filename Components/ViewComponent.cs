using GameProject.TilemapManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Components
{
    public class ViewComponent
    {
        public PositionComponent positionComponent;
        public ViewComponent(PositionComponent positionComponent)
        {
            this.positionComponent = positionComponent;
        }

        public bool HasLineOfSight(PositionComponent playerPosition, TilemapManager.Tilemap map)
        {
            float dx = playerPosition.X - positionComponent.X;
            float dy = playerPosition.Y - positionComponent.Y;
            float dist = MathF.Sqrt(dx * dx + dy * dy);
            if (dist == 0) return true;

            int steps = (int)(dist / (map.TileWidth / 2f));
            for (int i = 1; i < steps; i++)
            {
                float t = (float)i / steps;
                int col = (int)((positionComponent.X + dx * t) / map.TileWidth);
                int row = (int)((positionComponent.Y + dy * t) / map.TileWidth);
                if (map.tiles[row, col].Type == TileType.Wall)
                    return false;
            }
            return true;
        }
    }
}
