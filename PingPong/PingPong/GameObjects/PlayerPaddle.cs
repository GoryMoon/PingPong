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
    public class PlayerPaddle : Paddle
    {

        private string playerPrefix;

        /// <summary>
        /// Create a new player paddle at a specified position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public PlayerPaddle(GameScreen game, float x, float y, string p) : base(game, x, y)
        {
            playerPrefix = p;
        }

        private string getPlayerKey(string s)
        {
            return playerPrefix + "-" + s;
        }

        /// <summary>
        /// Update game logic; move player paddle on key press
        /// </summary>
        /// <param name="gameTime"></param>
        override public void Update(GameTime gameTime, GameWindow window)
        {
            KeyboardState state = Keyboard.GetState();
            Ball ball = gameScreen.get<Ball>("ball");

            if (state.IsKeyDown(gameScreen.handler.game.settings.getKey(getPlayerKey("U"))) && this.Pos.Y > 0)
            {
                if (!(this.Y - 4 < ball.Bottom && ((this.X <= ball.Right && this.X + this.Width >= ball.Right) || (this.X <= ball.X && this.X + this.Width >= ball.X))))
                {
                    this.Y -= 4;
                }
            }

            if (state.IsKeyDown(gameScreen.handler.game.settings.getKey(getPlayerKey("D"))) && this.Pos.Y < gameScreen.WindowHeight - spriteTexture.Height)
            {
                if (!(this.Bottom + 4 > ball.Top && ((this.X <= ball.Right && this.X + this.Width >= ball.Right) || (this.X <= ball.X && this.X + this.Width >= ball.X))))
                {
                    this.Y += 4;
                }
            }
        }

    }
}
