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

namespace PingPong.GameObjects
{
    /// <summary>
    /// Base class for paddles
    /// </summary>
    abstract class Paddle : GameObject
    {
        protected Texture2D spriteTexture;

        /// <summary>
        /// Create a new player paddle at a specified position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Paddle(Game game, float x, float y) : base(game, x, y, 38, 150)
        {

        }

        /// <summary>
        /// Load the sprite texture
        /// </summary>
        public override void LoadContent()
        {
            this.spriteTexture = this.game.Content.Load<Texture2D>("pad");
        }

        /// <summary>
        /// Draw the paddle
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        override public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.spriteTexture, this.position, Color.White);
        }
    }
}
