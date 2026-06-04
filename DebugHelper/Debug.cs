using GameProject.Collision;
using GameProject.TilemapItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.DebugHelper
{
    static class Debug
    {
        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle collisionRectangle, Color color)
        {
            Texture2D pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            spriteBatch.Draw(pixel, new Rectangle(collisionRectangle.Left,
                collisionRectangle.Top, collisionRectangle.Width, 1), color);
            spriteBatch.Draw(pixel, new Rectangle(collisionRectangle.Left,
                collisionRectangle.Bottom - 1, collisionRectangle.Width, 1), color);
            spriteBatch.Draw(pixel, new Rectangle(collisionRectangle.Left,
                collisionRectangle.Top, 1, collisionRectangle.Height), color);
            spriteBatch.Draw(pixel, new Rectangle(collisionRectangle.Right - 1,
                collisionRectangle.Top, 1, collisionRectangle.Height), color);
        }

        public static void DrawCircle(SpriteBatch spriteBatch, Circle collisionCircle, Color color)
        {
            int segments = (int)(collisionCircle.Radius * 2);
            float angle = 0f;
            float angleStep = MathHelper.TwoPi / segments;

            Texture2D pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            for (int i = 0; i < segments; i++)
            {
                Vector2 point1 = new Vector2(collisionCircle.X, collisionCircle.Y) +
                    collisionCircle.Radius * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

                Vector2 point2 = new Vector2(collisionCircle.X, collisionCircle.Y) +
                    collisionCircle.Radius * new Vector2((float)Math.Cos(angle + angleStep), (float)Math.Sin(angle + angleStep));

                Vector2 line = point2 - point1;
                float lineLength = line.Length();

                if (lineLength > 0)
                {
                    float lineAngle = (float)Math.Atan2(line.Y, line.X);

                    spriteBatch.Draw(pixel,
                        new Rectangle((int)point1.X, (int)point1.Y, (int)lineLength, 1),
                        null,
                        color,
                        lineAngle,
                        Vector2.Zero,
                        SpriteEffects.None,
                        0f);
                }

                angle += angleStep;
            }
        }

        public static void DrawTilesCollision(Tilemap tilemap, SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < tilemap.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tilemap.tiles.GetLength(1); j++)
                {
                    DrawRectangle(_spriteBatch, tilemap.tiles[i, j].collision.collisionRectangle, Color.Red);
                }
            }
        }
    }
}
