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
        public int speed = 1;
        private int speedX;
        private int speedY;

        public int SpeedX { get {return speedX; } set { speedX = value; } }
        public int SpeedY { get {return speedY; } set { speedY = value; } }
        /// <summary>
        /// Create a new player paddle at a specified position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Ball(Game game, float x, float y) : base(game, x, y, 38, 38)
        {
            speedX = speed;
            speedY = speed;
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

            if (this.position.Y < 0)
            {
                this.speedY *= -1;   
            }

            else if (this.position.Y > 430)
            {
                this.speedY *= -1;
            }

            //this.position.X += this.speed;
            this.position.X += this.speedX;
            this.position.Y += this.speedY;

            if (startOver == true)
            {
                KeyboardState state = Keyboard.GetState();

                if (state.IsKeyDown(Keys.Up))
                {
                    this.speed = 4;
                    startOver = false;
                }

                if (state.IsKeyDown(Keys.Down))
                {
                    this.speed = 4;
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
    }
}
