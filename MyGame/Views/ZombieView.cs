using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MyGame.models;

namespace MyGame.Views
{
    namespace MyGame.Views
    {
        internal class ZombieView : View
        {
            public ZombieView(Texture2D texture) : base(texture)
            {
            }
            public void DrawZombie(SpriteBatch spriteBatch, Zombie zombie)
            {
                spriteBatch.Draw(texture, zombie.position,
                new Rectangle(zombie.frameIndex.X * frameWidth,
                    zombie.frameIndex.Y * frameHeight,
                    frameWidth, frameHeight),
                Color.White, 0, Vector2.Zero,
                1, SpriteEffects.None, 0);
            }
        }
    }
}
