using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MyGame.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Views
{
    internal class HeroView : View
    {
        public HeroView(Texture2D texture) : base(texture)
        {
        }
        public void DrawHero(SpriteBatch spriteBatch, Hero hero)
        {
            spriteBatch.Draw(texture, hero.position,
            new Rectangle(hero.frameIndex.X * frameWidth,
                hero.frameIndex.Y * frameHeight,
                frameWidth, frameHeight),
            Color.White, 0, Vector2.Zero,
            1, SpriteEffects.None, 0);
        }
    }
}
