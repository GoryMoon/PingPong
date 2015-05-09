using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.GameScreens;

using Microsoft.Xna.Framework;

namespace PingPong.GameObjects
{
    public abstract class AdvCollision: GameObject
    {
        public AdvCollision(GameScreen game, float x, float y)
            : base(game, x, y)
        {
            
        }

        private Plane left;
        private Plane right;
        private Plane top;
        private Plane bottom;
        private Rectangle rect;

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            left = new Plane(Vector3.Left, Width);
            right = new Plane(Vector3.Right, Height);
            top = new Plane(Vector3.Up, Width);
            bottom = new Plane(Vector3.Down, Width);
        }

        public Vector2 checkCollision(AdvCollision col)
        {
            BoundingBox b = new BoundingBox(new Vector3(col.X, col.Y, 0), new Vector3(col.Width, col.Height, 0));

            if (this.right.Intersects(b) == PlaneIntersectionType.Front)
            {
                Console.WriteLine("Inter");
            }
            return new Vector2();
        }
    }
}
