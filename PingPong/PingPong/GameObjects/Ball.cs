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
    /// Ball object
    /// </summary>
    class Ball : GameObject
    {
        private Texture2D spriteTexture;
        private int speed = 5;

        /// <summary>
        /// Create a new player paddle at a specified position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Ball(Game game, float x, float y) : base(game, x, y)
        {

        }

        /// <summary>
        /// Load the sprite texture
        /// </summary>
        public override void LoadContent()
        {
            this.spriteTexture = this.game.Content.Load<Texture2D>("ball");
        }

        /// <summary>
        /// Update game logic. Move the ball
        /// </summary>
        /// <param name="gameTime"></param>
        bool startOver=false;

        public override void Update(GameTime gameTime)
        {
            if (startOver == true)
            {
                KeyboardState state = Keyboard.GetState();

                if (state.IsKeyDown(Keys.Up))
                {
                    this.speed = 4;
                }

                if (state.IsKeyDown(Keys.Down))
                {
                    this.speed = 4;
                }
            }

            if (this.position.X > 800)
            {
                this.position.X = 400;
                this.speed *= 0;
                startOver = true;
            }
            else if (this.position.X < 0)
            {
                this.position.X = 400;
                this.speed *= 0;
                startOver = true;
            }

            this.position.X += this.speed;
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
