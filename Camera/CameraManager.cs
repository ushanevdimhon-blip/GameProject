using GameProject.Components;
using Microsoft.Xna.Framework;
using System;

namespace GameProject.Camera
{
    public class CameraManager
    {
        Camera camera;

        public CameraManager(float width, float height)
        {
            camera = new Camera(width, height);  
        }

        public void Follow(PositionComponent targetPosition)
        {
            camera.targetPosition = targetPosition;

            var vector = Vector2.Lerp(new Vector2(camera.position.X, camera.position.Y), 
                new Vector2(camera.targetPosition.X, camera.targetPosition.Y), 0.1f);

            camera.position.X = vector.X;
            camera.position.Y = vector.Y;
        }

        public void Clamp(float worldWidth, float worldHeight)
        {
            if (camera.position.X - camera.width / 2 < 0)
                camera.position.X = camera.width / 2;

            if (camera.position.X + camera.width / 2 > worldWidth)
                camera.position.X = worldWidth - camera.width / 2;

            if (camera.position.Y - camera.height / 2 < 0)
                camera.position.Y = camera.height / 2;

            if (camera.position.Y + camera.height / 2 > worldHeight)
                camera.position.Y = worldHeight - camera.height / 2;
        }

        public void Update()
        {
            camera.Update();
        }

        public Rectangle GetCameraBounds()
        {
            return camera.GetBounds();
        }

        public Matrix GetMatrix()
        {
            return camera.Matrix;
        }

        public (int startCol, int endCol, int startRow, int endRow) GetVisibleRange(int tileSize, int cols, int rows)
        {  
            float halfTile = tileSize / 2f;

            int startCol = Math.Max(0, (int)((camera.position.X - camera.width / 2) / tileSize));
            int startRow = Math.Max(0, (int)((camera.position.Y - camera.height / 2) / tileSize));
            int endCol = Math.Min(cols, (int)((camera.position.X + camera.width / 2) / tileSize) + 2);
            int endRow = Math.Min(rows, (int)((camera.position.Y + camera.height / 2) / tileSize) + 2);

            return (startCol, endCol, startRow, endRow);
        }
    }
}
