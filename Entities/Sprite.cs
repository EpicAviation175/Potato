using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Potato
{
    public class Sprite
    {
        public Texture2D texture;
        public Vector2 position;

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, 100, 100);
            }
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }
    }
}