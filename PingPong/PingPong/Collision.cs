using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong
{
    class Collsion
    {
        Texture2D texture;
        public Vector2 Position;
        public Vector2 Velocity;

        public Rectangle Collision
        {
            get 
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    texture.Width,
                    texture.Height);
            }
        }

        public Collsion(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.Position = position;
        }

        public Collsion(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            this.texture = texture;
            this.Position = position;
            this.Velocity = velocity;
        }
    }
}
