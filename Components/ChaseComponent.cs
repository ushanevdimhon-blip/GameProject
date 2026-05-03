using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Components
{
    public class DijkstraData
    {
        public int Price { get; set; }
        public (int X, int Y) Previous { get; set; }
    }

    public class ChaseComponent
    {
        private readonly Tilemap tilemap;
        private List<PositionComponent> currentPath;
        private const float MovementSpeed = 150.0f;
        private const float Interval = 0.1f;
        private float time = 0.0f;

        public ChaseComponent(Tilemap tilemap)
        {
            this.tilemap = tilemap;
            this.currentPath = new List<PositionComponent>();
        }

        public void Chase(PositionComponent enemyPosition, PositionComponent playerPosition, GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            time += deltaTime;

            var startTileIndex = ((int)(enemyPosition.X / tilemap.TileWidth), (int)(enemyPosition.Y / tilemap.TileHeight));
            var endTileIndex = ((int)(playerPosition.X / tilemap.TileWidth), (int)(playerPosition.Y / tilemap.TileHeight));

            if (time >= Interval)
            {
                time = 0.0f;
                currentPath = GetPathByDijkstra(startTileIndex, endTileIndex);
            }

            Move(enemyPosition, deltaTime);
        }

        private void Move(PositionComponent enemyPosition, float deltaTime)
        {
            float distanceToMove = MovementSpeed * deltaTime;
            var currentPos = new Vector2(enemyPosition.X, enemyPosition.Y);

            for (int i = 1; i < currentPath.Count; i++)
            {
                var next = new Vector2(currentPath[i].X, currentPath[i].Y);
                Vector2 direction = next - currentPos;
                float distance = direction.Length();

                if (distance > distanceToMove)
                {
                    direction.Normalize();//Превращает вектор в единичный вектор (длиной 1), сохраняя его направление.
                    currentPos += direction * distanceToMove;
                    break;
                }
                else
                {
                    currentPos = next;
                    distanceToMove -= distance;
                }
            }

            enemyPosition.X = currentPos.X;
            enemyPosition.Y = currentPos.Y;
        }

        private List<PositionComponent> GetPathByDijkstra((int X, int Y) start, (int X, int Y) end)
        {
            var track = new Dictionary<(int, int), DijkstraData>();
            var queue = new PriorityQueue<(int, int), int>();

            track[start] = new DijkstraData { Price = 0, Previous = (-1, -1) };
            queue.Enqueue(start, 0);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                foreach (var tile in GetAbleTiles(current))
                {
                    int tilePrice = tilemap.tiles[tile.Y, tile.X].Price;
                    int newCost = track[current].Price + tilePrice;

                    if (!track.ContainsKey(tile) || track[tile].Price > newCost)
                    {
                        track[tile] = new DijkstraData { Price = newCost, Previous = current };
                        queue.Enqueue(tile, newCost);
                    }
                }

                if (current == end)
                {
                    return GetPath(track, end);
                }
            }

            return new List<PositionComponent>();
        }

        private IEnumerable<(int X, int Y)> GetAbleTiles((int X, int Y) position)
        {
            if (position.X + 1 >= 0 && position.X + 1 < tilemap.tiles.GetLength(1) &&
                   position.Y >= 0 && position.Y < tilemap.tiles.GetLength(0) &&
                   !tilemap.tiles[position.Y, position.X + 1].IsWall)
                yield return (position.X + 1, position.Y);

            if (position.X - 1 >= 0 && position.X - 1 < tilemap.tiles.GetLength(1) &&
                   position.Y >= 0 && position.Y < tilemap.tiles.GetLength(0) &&
                   !tilemap.tiles[position.Y, position.X - 1].IsWall)
                yield return (position.X - 1, position.Y);

            if (position.X >= 0 && position.X < tilemap.tiles.GetLength(1) &&
                   position.Y + 1 >= 0 && position.Y + 1 < tilemap.tiles.GetLength(0) &&
                   !tilemap.tiles[position.Y + 1, position.X].IsWall)
                yield return (position.X, position.Y + 1);

            if (position.X >= 0 && position.X < tilemap.tiles.GetLength(1) &&
                   position.Y - 1 >= 0 && position.Y - 1 < tilemap.tiles.GetLength(0) &&
                   !tilemap.tiles[position.Y - 1, position.X].IsWall)
                yield return (position.X, position.Y - 1);
        }

        private List<PositionComponent> GetPath(Dictionary<(int, int), DijkstraData> track, (int X, int Y) end)
        {
            var path = new List<PositionComponent>();
            var current = end;

            while (current != (-1, -1))
            {
                float posX = tilemap.tiles[current.Y, current.X].position.X;
                float posY = tilemap.tiles[current.Y, current.X].position.Y;
                path.Add(new PositionComponent(posX, posY));
                current = track[current].Previous;
            }

            path.Reverse();
            return path;
        }
    }
}
