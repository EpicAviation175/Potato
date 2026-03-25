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
        private float groundY;

        public Player(Texture2D texture, Vector2 position, float groundY): base(texture, position)
        {
            this.texture = texture;
            this.position = position;
            this.groundY = groundY;
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

            if (position.Y >= groundY)
            {
                position.Y = groundY;
                jumpVelocity = 0;
                isJumping = false;
            }
        }
    }
}