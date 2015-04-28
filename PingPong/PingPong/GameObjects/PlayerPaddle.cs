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
    public class PlayerPaddle : Paddle
    {
        /// <summary>
        /// Create a new player paddle at a specified position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public PlayerPaddle(GameState game, float x, float y) : base(game, x, y)
        {

        }

        /// <summary>
        /// Update game logic; move player paddle on key press
        /// </summary>
        /// <param name="gameTime"></param>
        override public void Update(GameTime gameTime, GameWindow window)
        {
            KeyboardState state = Keyboard.GetState();
            Ball ball = game.get<Ball>("ball");

            if (state.IsKeyDown(Keys.Up) && this.Pos.Y > 0)
            {
                if (!(this.Y - 4 < ball.Bottom && ((this.X <= ball.Right && this.X + this.Width >= ball.Right) || (this.X <= ball.X && this.X + this.Width >= ball.X))))
                {
                    this.Y -= 4;
                }
            }

            if (state.IsKeyDown(Keys.Down) && this.Pos.Y < window.ClientBounds.Height - spriteTexture.Height)
            {
                if (!(this.Bottom + 4 > ball.Bottom && ((this.X <= ball.Right && this.X + this.Width >= ball.Right) || (this.X <= ball.X && this.X + this.Width >= ball.X))))
                {
                    this.Y += 4;
                }
            }
        }

    }
}
