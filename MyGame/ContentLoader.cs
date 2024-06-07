using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    internal class ContentLoader
    {
        public readonly Texture2D TileSet;
        public readonly Texture2D GameOverTexture;
        public readonly Texture2D Sprites;
        public readonly SpriteFont Font;


        public ContentLoader(ContentManager content)
        {
            TileSet = content.Load<Texture2D>("Tiles");
            GameOverTexture = content.Load<Texture2D>("gameOver");
            Sprites = content.Load<Texture2D>("sprites");
            Font = content.Load<SpriteFont>("File");
        }

    }
}
