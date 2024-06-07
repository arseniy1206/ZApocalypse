
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyGame.models
{
    internal class Hero : Person
    {
        public int ammoCount = 2;
        public Hero()
        {
            position = new Vector2(520, 488);
            frameIndex = new Point(0, 0);
            spriteSize = new Point(3, 4);
            X = 8;
            Y = 8;
        }

        public bool CanMove(Direction direction, GameMap map)
        {
            int dx = 0;
            int dy = 0;

            if (direction == Direction.Left)
            {
                dx = -1;
            }
            else if (direction == Direction.Right)
            {
                dx = 1;
            }
            else if (direction == Direction.Up)
            {
                dy = -1;
            }
            else if (direction == Direction.Down)
            {
                dy = 1;
            }

            int newX = X + dx;
            int newY = Y + dy;

            if (newX < 0 || newX >= map.Size || newY < 0 || newY >= map.Size)
            {
                return false;
            }

            char tile = map.SchemeMap[newY][newX];

            if (tile == 'f' || tile == 'e')
            {
                return true;
            }
            return false;
        }

        public bool IsAlive(List<Zombie> zombies)
        {
            foreach (Zombie zombie in zombies)
            {
                if (zombie.IsAlive())
                { 
                    var dx = Math.Abs(X - zombie.X);
                    var dy = Math.Abs(Y - zombie.Y);
                    if (dx == 1 && dy == 0 || dx == 0 && dy == 1)
                        return false;
                }
            }
            return true;
        }

        public void Die()
        {
            frameIndex.X = 2;
            frameIndex.Y = 4;
        }

        public void Shoot(Zombie aim)
        {
            ammoCount--;
            var direction = CalculateAimDirection(aim.X, aim.Y);
            switch (direction)
            {
                case Direction.Left:
                    frameIndex = new Point(1, 4);
                    break;
                case Direction.Right:
                    frameIndex = new Point(0, 4);
                    break;
                case Direction.Up:
                    frameIndex = new Point(1, 3);
                    break;
                case Direction.Down:
                    frameIndex = new Point(1, 4);
                    break;
            }
        }

        public Direction CalculateAimDirection(int aimX, int aimY)
        {
            var deltaX = aimX - X;
            var deltaY = aimY - Y;
            if (Math.Abs(deltaX) > Math.Abs(deltaY))
            {
                if (deltaX > 0)
                {
                    return Direction.Right;
                }
                else
                {
                    return Direction.Left;
                }
            }
            else
            {
                if (deltaY > 0)
                {
                    return Direction.Down;
                }
                else
                {
                    return Direction.Up;
                }
            }
        }

        public bool CanExit()
        {
            if (X == 1 && Y == 1)
                return true;
            else
                return false;
        }
    }

}
