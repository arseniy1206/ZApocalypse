using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace MyGame.models
{
    internal class GameMap
    {
        public readonly int Size;
        public string LevelName;
        public char[][] SchemeMap;

        public GameMap(int Size, string LevelName, char[][] SchemeMap)
        {
            this.LevelName = LevelName;
            this.Size = Size;
            this.SchemeMap = SchemeMap;
        }

        public List<Zombie> FindZombies()
        {
            List<Zombie> zombies = new List<Zombie>();
            for (int i = 0; i < SchemeMap.Length; i++)
            {
                for (int j = 0; j < SchemeMap[0].Length; j++)
                {
                    if (SchemeMap[i][j] == 'z')
                    {
                        zombies.Add(new Zombie(j, i));
                    }
                }
            }
            return zombies;
        }
    }
}
