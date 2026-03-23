using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Potato
{
    public class Button
    {
        private Texture2D texture;
        private Vector2 position;

        public Button(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public Rectangle rect
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, 100, 100);
            }
        }

        public bool isPressed()
        {
            MouseState mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed && rect.Contains(mouse.Position))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}