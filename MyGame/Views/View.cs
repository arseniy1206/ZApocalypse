using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MyGame.Views
{

    internal class View
    {
        public int frameWidth = 52;
        public int frameHeight = 72;
        public Texture2D texture;
        public View(Texture2D texture)
        {
            this.texture = texture;
        }

        public void DrawGameOver(SpriteBatch spriteBatch, Texture2D gameOverTexture)
        {
            spriteBatch.Draw(gameOverTexture, new Rectangle(40, 90, 590, 380), Color.White);
        }

        public void DrawAmmoCount(SpriteBatch spriteBatch, Hero hero, SpriteFont font) 
        {
            spriteBatch.DrawString(font, "Ammo: " + hero.ammoCount, new Vector2(10, 660), Color.Red);
        }

        public void DrawMessage(SpriteBatch spriteBatch, string message, SpriteFont font)
        {
            spriteBatch.DrawString(font, message, new Vector2(200, 660), Color.Gold);
        }
    }
}
