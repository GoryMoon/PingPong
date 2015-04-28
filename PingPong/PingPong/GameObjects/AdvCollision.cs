using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.GameStates;

using Microsoft.Xna.Framework;

namespace PingPong.GameObjects
{
    public abstract class AdvCollision: GameObject
    {
        public AdvCollision(GameState game, float x, float y)
            : base(game, x, y)
        {

        }

        private Plane left;
        private Plane right;
        private Plane top;
        private Plane bottom;
        private Rectangle rect;

        public Vector2 checkCollision(AdvCollision col)
        {
           
                return new Vector2();
        }

        public Rectangle Bounding { get { return new Rectangle((int)X, (int)Y, (int)Width, (int)Height); } }


    }
}
