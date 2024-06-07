using System;
using System.Collections.Generic;

namespace MyGame.models
{
    internal class MapGenerator
    {
        static Random rand = new Random();

        private int maxZombieCount = 6;
        private int minZombieCount = 4;
        public char[][] GenerateMap()
        {
            int width = 10;
            int height = 10;
            char[][] map = new char[height][];

            for (int i = 0; i < height; i++)
            {
                map[i] = new char[width];
            }

            InitializeMap(map, width, height);
            PlaceExit(map);
            PlacePlayer(map, width, height);
            PlaceZombies(map, width, height);
            PlaceObstacles(map, width, height);
            return map;
        }

        static void InitializeMap(char[][] map, int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    {
                        map[y][x] = 'w';
                    }
                    else
                    {
                        map[y][x] = 'f';
                    }
                }
            }
        }

        static void PlaceExit(char[][] map)
        {
            map[1][1] = 'e';
        }

        static void PlacePlayer(char[][] map, int width, int height)
        {
            map[height - 2][width - 2] = 'p';
        }

        private void PlaceZombies(char[][] map, int width, int height)
        {
            int numZombies = rand.Next(minZombieCount, maxZombieCount);
            int placedZombies = 0;

            while (placedZombies < numZombies)
            {
                int x = rand.Next(1, width - 1);
                int y = rand.Next(1, height - 1);

                if (map[y][x] == 'f' && IsSafeDistance(x, y, width - 2, height - 2) && !IsObstacle(map, x, y))
                {
                    map[y][x] = 'z';
                    placedZombies++;
                }
            }
        }

        static void PlaceObstacles(char[][] map, int width, int height)
        {
            int numObstacles = rand.Next(5, 9);
            int placedObstacles = 0;
            char[][] tempMap = (char[][])map.Clone();

            while (placedObstacles < numObstacles)
            {
                int x = rand.Next(1, width - 1);
                int y = rand.Next(1, height - 1);

                if (map[y][x] == 'f' && IsSafeDistance(x, y, width - 2, height - 2))
                {
                    tempMap[y][x] = (placedObstacles % 2 == 0) ? 'b' : 't';
                    if (IsExitAccessible(tempMap, width, height))
                    {
                        map[y][x] = tempMap[y][x];
                        placedObstacles++;
                    }
                    else
                    {
                        tempMap[y][x] = 'f';
                    }
                }
            }
        }

        static bool IsSafeDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) > 2 || Math.Abs(y1 - y2) > 2;
        }

        static bool IsObstacle(char[][] map, int x, int y)
        {
            return map[y][x] == 'b' || map[y][x] == 't';
        }

        static bool IsExitAccessible(char[][] map, int width, int height)
        {
            bool[][] visited = new bool[height][];
            for (int i = 0; i < height; i++)
            {
                visited[i] = new bool[width];
            }

            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((1, 1));
            visited[1][1] = true;

            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            while (queue.Count > 0)
            {
                var (x, y) = queue.Dequeue();

                for (int i = 0; i < 4; i++)
                {
                    int nx = x + dx[i];
                    int ny = y + dy[i];

                    if (nx >= 1 && nx < width - 1 && ny >= 1 && ny < height - 1 && !visited[ny][nx] && map[ny][nx] != 'w' && map[ny][nx] != 'b' && map[ny][nx] != 't')
                    {
                        if (map[ny][nx] == 'f' || map[ny][nx] == 'p')
                        {
                            queue.Enqueue((nx, ny));
                            visited[ny][nx] = true;
                        }
                    }
                }
            }

            return visited[height - 2][width - 2];
        }
    }
}