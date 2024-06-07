using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MyGame.models
{
    internal class Person
    {
        public Vector2 position;
        public Point frameIndex;
        public Point spriteSize;
        public Vector2 velocity;
        public int X;
        public int Y;

        public void Move(int dx, int dy)
        {
            if (dx == -1)
            {
                frameIndex.Y = 1;
            }
            else if (dx == 1)
            {
                frameIndex.Y = 2;
            }
            else if (dy == -1)
            {
                frameIndex.Y = 3;
            }
            else if (dy == 1)
            {
                frameIndex.Y = 0;
            }
            velocity = new Vector2(dx, dy);
            for (int i = 0; i < 64; i++)
            {
                position += velocity;
                frameIndex.X += 1;
                if (frameIndex.X >= spriteSize.X)
                    frameIndex.X = 0;
            }
        }
    }
}
