using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PingPong
{
    public class Camera2D
    {

        public Matrix _transform; // Matrix Transform
        public Vector2 _pos; // Camera Position
        protected float _rotation; // Camera Rotation
        protected Vector2 _size; // Camera View Size

        public Camera2D()
        {
            _rotation = 0.1f;
            _pos = Vector2.Zero;
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            _pos += amount;
        }
        // Get set position
        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public Vector2 Size
        {
            get { return _size; }
            set { _size = value; }
        }


        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            float scaleX = graphicsDevice.Viewport.Width / _size.X;
            float scaleY = graphicsDevice.Viewport.Height / _size.Y;
            float scale = Math.Min(scaleX, scaleY);

            float translateX = (graphicsDevice.Viewport.Width - (_size.X * scale)) / 2f;
            float translateY = (graphicsDevice.Viewport.Height - (_size.Y * scale)) / 2f;


            _transform =       // Thanks to o KB o for this solution
                        Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateScale(new Vector3(scale, scale, 1)) *
                        Matrix.CreateTranslation(new Vector3(translateX, translateY, 0));

            return _transform;
        }
    }
}
