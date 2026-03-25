using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Potato
{
    public class Button
    {
        private Texture2D texture;
        private Vector2 position;
        private int Width;
        private int Height;

        public Button(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public Rectangle rect
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (3*texture.Width), (3*texture.Height));
            }
        }

        public Rectangle rect2
        {
            get
            {
                return new Rectangle((int)position.X-5, (int)position.Y-5, (3*texture.Width)+10, (3*texture.Height)+10);
            }
        }

        public bool mouseOverlap()
        {
            MouseState mouse = Mouse.GetState();
            if (rect.Contains(mouse.Position))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isPressed()
        {
            MouseState mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed && mouseOverlap())
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