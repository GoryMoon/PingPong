using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using PingPong.GameScreens;

namespace PingPong.GameObjects
{
    /// <summary>
    /// Base class for paddles
    /// </summary>
    public abstract class Paddle : AdvCollision
    {

        /// <summary>
        /// Create a new player paddle at a specified position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Paddle(GameScreen game, float x, float y) : base(game, x, y)
        {

        }

        /// <summary>
        /// Load the sprite texture
        /// </summary>
        public override void LoadContent(ContentManager content)
        {
            this.spriteTexture = content.Load<Texture2D>("pad");
            base.LoadContent(content);
        }

    }
}
