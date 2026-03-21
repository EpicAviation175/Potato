using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Potato
{
    class Player : Sprite
    {
        private bool isJumping = false;
        private float jumpVelocity = -10f;
        private float gravity = 0f;

        public Player(Texture2D texture, Vector2 position): base(texture, position)
        {
            this.texture = texture;
            this.position = position;
        }

        public virtual void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !isJumping)
            {
                jumpVelocity = -10f;
                gravity = 0.5f;
                isJumping = true;
            }

            if (isJumping)
            {
                Jump();
            }
        }

        public void Jump()
        {
            position.Y += jumpVelocity;
            jumpVelocity += gravity;

            if (position.Y >= 330)
            {
                position.Y = 330;
                jumpVelocity = 0;
                isJumping = false;
            }
        }
    }
}