using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.GameStates;

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
        private int speedX = 5;
        private int speedY = 5;
        private bool startOver = false;

        /// <summary>
        /// Create a new player paddle at a specified position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Ball(GameState game, float x, float y) : base(game, x, y)
        {
        }

        /// <summary>
        /// Load the sprite texture
        /// </summary>
        public override void LoadContent(ContentManager content)
        {
            this.spriteTexture = content.Load<Texture2D>("ball");
        }

        /// <summary>
        /// Update game logic. Move the ball
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime, GameWindow window)
        {

            if (this.X > window.ClientBounds.Width)
            {
                this.X = (window.ClientBounds.Width / 2) - (spriteTexture.Width / 2);
                this.Y = (window.ClientBounds.Height / 2) - (spriteTexture.Height / 2);
                this.speedX = this.speedY *= 0;
                startOver = true;
                game.set("playerScore", game.get<int>("playerScore") + 1);
            }
            else if (this.X < 0)
            {
                this.X = (window.ClientBounds.Width / 2) - (spriteTexture.Width / 2);
                this.Y = (window.ClientBounds.Height / 2) - (spriteTexture.Height / 2);
                this.speedX = this.speedY *= 0;
                startOver = true;
                game.set("computerScore", game.get<int>("computerScore") + 1);
            }


            if (this.Y > window.ClientBounds.Height - spriteTexture.Height)
            {
                this.speedY *= -1;
            }
            else if (this.Y < 0)
            {
                this.speedY *= -1;
            }

            if (startOver == true)
            {
                KeyboardState state = Keyboard.GetState();

                if (state.IsKeyDown(Keys.Up))
                {
                    this.speedX = this.speedY = -5;
                    startOver = false;
                }

                if (state.IsKeyDown(Keys.Down))
                {
                    this.speedX = this.speedY = 5;
                    startOver = false;
                }
            }

            this.X += this.speedX;
            this.Y += this.speedY;
        }

        public int SpeedX { get { return speedX; } set { speedX = value; } }
        public int SpeedY { get { return speedY; } set { speedY = value; } }
    }

}
