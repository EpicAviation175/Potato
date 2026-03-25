using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Potato
{
    class Spike : Sprite
    {
        private float velocity = 10f;

        public Spike(Texture2D texture, Vector2 position): base(texture, position)
        {
            this.texture = texture;
            this.position = position;
        }

        public override Rectangle Rect
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, 100, 100);
            }
        }

        public void Update()
        {
            position.X -= velocity;
        }
    }
}
