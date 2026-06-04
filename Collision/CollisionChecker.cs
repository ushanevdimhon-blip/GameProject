using GameProject.Components;
using GameProject.TilemapItems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Collision
{
    public static class CollisionChecker
    {
        public static bool CheckRectangleCollision(CollisionComponent collision1, CollisionComponent collision2)
        {
            return collision1.collisionRectangle.Intersects(collision2.collisionRectangle);
        }

        public static bool CheckCircleCollision(CollisionComponent collision1, CollisionComponent collision2)
        {
            return collision1.collisionCircle.Intersects(collision2.collisionRectangle);
        }

        public static void GetCameraCollision(CollisionComponent collisionObject, Rectangle cameraBounds)
        {
            if (collisionObject.collisionRectangle.Left < cameraBounds.Left)
            {
                collisionObject.currentPosition.X = cameraBounds.Left + collisionObject.width / 2;
            }
            else if (collisionObject.collisionRectangle.Right > cameraBounds.Right)
            {
                collisionObject.currentPosition.X = cameraBounds.Right - collisionObject.width / 2;
            }

            if (collisionObject.collisionRectangle.Top < cameraBounds.Top)
            {
                collisionObject.currentPosition.Y = cameraBounds.Top + collisionObject.height / 2;
            }
            else if (collisionObject.collisionRectangle.Bottom > cameraBounds.Bottom)
            {
                collisionObject.currentPosition.Y = cameraBounds.Bottom - collisionObject.height / 2;
            }
        }

        public static void CheckTilesCollision(Tilemap tilemap, PositionComponent currentPosition,
            CollisionComponent collision, Action<Tile> colAction)
        {
            int tileX = (int)(currentPosition.X / tilemap.TileWidth);
            int tileY = (int)(currentPosition.Y / tilemap.TileHeight);

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int nTileX = tileX + j;
                    int nTileY = tileY + i;

                    if (nTileX >= 0 && nTileX < tilemap.tiles.GetLength(1) &&
                        nTileY >= 0 && nTileY < tilemap.tiles.GetLength(0))
                    {
                        if (CheckRectangleCollision(collision,
                            tilemap.tiles[nTileY, nTileX].collision))
                        {
                            colAction(tilemap.tiles[nTileY, nTileX]);
                        }
                    }
                }
            }
        }
    }
}
