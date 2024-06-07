using MyGame.models;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.WebSockets;

namespace MyGame.Views
{
    struct Tile
    {
        public Rectangle DrawRectangle;
    }
    internal class MapView
    {
        public readonly int Size;
        public int TileSize;
        public Texture2D Tileset;
        public string LevelName;
        public char[][] SchemeMap;
        public char[][] InitialPositions;
        public Tile[][] Grid;
        public Tile[] FloorTiles;
        public MapView(int Size, Texture2D Tileset, string LevelName, char[][] randomLevelMap = null)
        {
            this.LevelName = LevelName;
            this.Size = Size;
            this.Tileset = Tileset;
            TileSize = 64;
            if (randomLevelMap == null)
                SchemeMap = FillScemeMap(LevelName);
            else
                SchemeMap = randomLevelMap;

            InitialPositions = DeepCopy(SchemeMap);
            FillGameMap();
        }

        public Tile[] FillFloreTiles()
        {
            var floorTiles = new Tile[6];
            floorTiles[0] = new Tile();
            floorTiles[0].DrawRectangle = new Rectangle(0, 0, 64, 64);
            floorTiles[1] = new Tile();
            floorTiles[1].DrawRectangle = new Rectangle(65, 0, 64, 64);
            floorTiles[2] = new Tile();
            floorTiles[2].DrawRectangle = new Rectangle(0, 65, 64, 64);
            floorTiles[3] = new Tile();
            floorTiles[3].DrawRectangle = new Rectangle(65, 65, 64, 64);
            floorTiles[4] = new Tile();
            floorTiles[4].DrawRectangle = new Rectangle(0, 130, 64, 64);
            floorTiles[5] = new Tile();
            floorTiles[5].DrawRectangle = new Rectangle(65, 130, 64, 64);
            return floorTiles;
        }

        private static char[][] FillScemeMap(string levelName)
        {
            List<char[]> rows = new List<char[]>();
            var filePath = @"C:\Users\arska\source\repos\MyGame\MyGame\Content\" + $"{levelName}.csv";
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        char[] row = line.Split(',').Select(elem => char.Parse(elem)).ToArray();
                        rows.Add(row);
                    }
                }
                return rows.ToArray();
            }
            else
            {
                var mapGenerator = new MapGenerator();
                return mapGenerator.GenerateMap();
            }
        }

        private void FillGameMap()
        {
            FloorTiles = FillFloreTiles();
            Grid = new Tile[Size][];
            for (int i = 0; i < Size; i++)
            {
                Grid[i] = new Tile[Size];
                for (int j = 0; j < Size; j++)
                {
                    switch (SchemeMap[i][j])
                    {
                        case 'f':
                            Grid[i][j] = FloorTiles[0];
                            break;
                        case 'z':
                            Grid[i][j] = FloorTiles[0];
                            break;
                        case 'w':
                            Grid[i][j] = FloorTiles[1];
                            break;
                        case 'e':
                            Grid[i][j] = FloorTiles[2];
                            break;
                        case 'b':
                            Grid[i][j] = FloorTiles[3];
                            break;
                        case 't':
                            Grid[i][j] = FloorTiles[4];
                            break;
                        case 'p':
                            Grid[i][j] = FloorTiles[5];
                            break;

                    }
                }
            }
        }

        public void DrawMap(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    spriteBatch.Draw(Tileset, new Rectangle(i * TileSize, j * TileSize, TileSize, TileSize), Grid[j][i].DrawRectangle, Color.White);
                }
            }
        }

        static char[][] DeepCopy(char[][] original)
        {
            char[][] copy = new char[original.Length][];

            for (int i = 0; i < original.Length; i++)
            {
                copy[i] = new char[original[i].Length];
                Array.Copy(original[i], copy[i], original[i].Length);
            }
            return copy;
        }
    }
}
