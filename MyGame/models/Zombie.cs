using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.models
{
    internal class Zombie : Person
    {
        private bool isAlive;
        public Zombie(int x, int y)
        {
            X = x;
            Y = y;
            position = new Vector2(x * 65 + 4, y * 65 - 25);
            frameIndex = new Point(7, 0);
            spriteSize = new Point(3, 4);
            isAlive = true;
        }

        public void Move(Hero hero, GameMap map)
        {
            var path = AStar.FindPath(map.SchemeMap, X, Y, hero.X, hero.Y);
            if (path != null)
            {
                var dx = path[1].x - X;
                var dy = path[1].y - Y;
                map.SchemeMap[Y][X] = 'f';
                X += dx;
                Y += dy;
                map.SchemeMap[Y][X] = 'z';
                velocity = new Vector2(dx, dy);
                if (dx == 1)
                    frameIndex.Y = 2;
                else if (dx == -1)
                    frameIndex.Y = 1;
                else if (dy == -1)
                    frameIndex.Y = 3;
                else
                    frameIndex.Y = 0;
                for (int i = 0; i < 64; i++)
                {
                    position += velocity;
                    frameIndex.X += 1;
                    if (frameIndex.X >= spriteSize.X)
                        frameIndex.X = 6;
                }
            }
        }

        public bool IsAlive()
        {
            return isAlive;
        }

        public void Die()
        {
            isAlive = false;
            frameIndex = new Point(5, 0);
        }
    }
}
