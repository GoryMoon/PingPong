using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.GameScreens;

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
    public class Ball : AdvCollision
    {
        private int speedX = 5;
        private int speedY = 5;
        private bool startOver = false;

        /// <summary>
        /// Create a new player paddle at a specified position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Ball(GameScreen game, float x, float y) : base(game, x, y)
        {
        }

        /// <summary>
        /// Load the sprite texture
        /// </summary>
        public override void LoadContent(ContentManager content)
        {
            this.spriteTexture = content.Load<Texture2D>("ball");
            base.LoadContent(content);
        }

        /// <summary>
        /// Update game logic. Move the ball
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime, GameWindow window)
        {

            if (this.X > gameScreen.WindowWidth)
            {
                this.X = (gameScreen.WindowWidth / 2) - (spriteTexture.Width / 2);
                this.Y = (gameScreen.WindowHeight / 2) - (spriteTexture.Height / 2);
                this.speedX = this.speedY *= 0;
                startOver = true;
                ((MainGameScreen)gameScreen).playerScore += 1;
            }
            else if (this.X < 0)
            {
                this.X = (gameScreen.WindowWidth / 2) - (spriteTexture.Width / 2);
                this.Y = (gameScreen.WindowHeight / 2) - (spriteTexture.Height / 2);
                this.speedX = this.speedY *= 0;
                startOver = true;
                ((MainGameScreen)gameScreen).computerScore += 1;
            }


            if (this.Y > gameScreen.WindowHeight - spriteTexture.Height)
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

                if (state.IsKeyDown(gameScreen.handler.game.settings.getKey("P1-U")))
                {
                    this.speedX = this.speedY = -5;
                    startOver = false;
                }

                if (state.IsKeyDown(gameScreen.handler.game.settings.getKey("P1-D")))
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
