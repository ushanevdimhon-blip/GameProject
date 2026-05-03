using GameProject.Components;
using Microsoft.Xna.Framework;
using System;

namespace GameProject
{
    public class Camera
    {
        PositionComponent position;
        PositionComponent targetPosition;
        float width;
        float height;
        public Matrix Matrix { get; private set; }

        public Camera(float width, float height)
        {
            this.width = width;
            this.height = height;
            position = new PositionComponent(0, 0);
            targetPosition = new PositionComponent(0, 0);     
        }

        public void Follow(PositionComponent targetPosition)
        {
            this.targetPosition = targetPosition;

            var vector = Vector2.Lerp(new Vector2(position.X, position.Y), 
                new Vector2(this.targetPosition.X, this.targetPosition.Y), 0.1f);
            position.X = vector.X;
            position.Y = vector.Y;
        }

        public void Clamp(float worldWidth, float worldHeight)
        {
            if (position.X - width / 2 < 0)
                position.X = width / 2;

            if (position.X + width / 2 > worldWidth)
                position.X = worldWidth - width / 2;

            if (position.Y - height / 2 < 0)
                position.Y = height / 2;

            if (position.Y + height / 2 > worldHeight)
                position.Y = worldHeight - height / 2;
        }

        public void Update()
        {
            // Перемещает объекты, чтобы игрок был в центре экрана
            Matrix = Matrix.CreateTranslation(
                -position.X + width / 2,
                -position.Y + height / 2,
                0
            );
        }

        public Rectangle GetCameraBounds()
        {
            return new Rectangle(
                (int)(position.X - width / 2),
                (int)(position.Y - height / 2),
                (int)width,
                (int)height
            );
        }

        public (int startCol, int endCol, int startRow, int endRow) GetVisibleRange(int tileSize, int cols, int rows)
        {  
            float halfTile = tileSize / 2f;// т.к. тайлы поз от центра

            int startCol = Math.Max(0, (int)((position.X - width / 2 - halfTile) / tileSize));
            int startRow = Math.Max(0, (int)((position.Y - height / 2 - halfTile) / tileSize));
            int endCol = Math.Min(cols, (int)((position.X + width / 2 + halfTile) / tileSize) + 1);
            int endRow = Math.Min(rows, (int)((position.Y + height / 2 + halfTile) / tileSize) + 1);

            return (startCol, endCol, startRow, endRow);
        }
    }
}
