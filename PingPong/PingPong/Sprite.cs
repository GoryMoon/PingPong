using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using PingPong.GameObjects;

public class Sprite
{
    public int Height { get; set; }
    public int Width { get; set; }
    protected Vector2 Position;

    public Rectangle BoundingBox
    {
        get { return new Rectangle((int)Position.X, (int)Position.Y, Width, Height); }
    }

    public Sprite(Vector2 position, int width, int height)
    {
        this.Width = width;
        this.Height = height;
        this.Position = position;
    }

    public Vector2 Pos { get { return Position; } }
}