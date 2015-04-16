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
    public class Ball : GameObject
    {
        private Texture2D spriteTexture;
        private int speedX = 5;
        private int speedY = 5;

        /// <summary>
        /// Create a new player paddle at a specified position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Ball(Game game, float x, float y) : base(game, x, y, 38, 38)
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

        public override void Update(GameTime gameTime, GameWindow window)
        {

            if (this.position.X > window.ClientBounds.Width)
            {
                this.position.X = (window.ClientBounds.Width / 2) - (spriteTexture.Width / 2);
                this.speedX = this.speedY *= 0;
                startOver = true;
                Game1.instance.playerScore += 1;
            }
            else if (this.position.X < 0)
            {
                this.position.X = (window.ClientBounds.Width / 2) - (spriteTexture.Width / 2);
                this.speedX = this.speedY *= 0;
                startOver = true;
                Game1.instance.computerScore += 1;
            }


            if (this.position.Y > window.ClientBounds.Height - spriteTexture.Height)
            {
                this.speedY *= -1;
            }
            else if (this.position.Y < 0)
            {
                this.speedY *= -1;
            }

            this.position.X += this.speedX;
            this.position.Y += this.speedY;

            if (startOver == true)
            {
                KeyboardState state = Keyboard.GetState();

                if (state.IsKeyDown(Keys.Up))
                {
                    this.speedX = this.speedY = 5;
                    startOver = false;
                }

                if (state.IsKeyDown(Keys.Down))
                {
                    this.speedX = this.speedY = 5;
                    startOver = false;
                }
            }
            this.Position.X = this.position.X;
            this.Position.Y = this.position.Y;

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


        public int SpeedX { get { return speedX; } set { speedX = value; } }
        public int SpeedY { get { return speedY; } set { speedY = value; } }
    }

}
