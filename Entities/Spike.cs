using System;
using System.Runtime.CompilerServices;
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

        public void Update()
        {
            position.X -= velocity;
        }
    }
}