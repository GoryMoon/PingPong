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
    class ComputerPaddle : Paddle
    {
        /// <summary>
        /// Create a new player paddle at a specified position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public ComputerPaddle(Game game, float x, float y) : base(game, x, y)
        {

        }

        /// <summary>
        /// Update game logic; move player paddle on key press
        /// </summary>
        /// <param name="gameTime"></param>
        override public void Update(GameTime gameTime)
        {
            Game1 game1 = (Game1)this.game;

            if (game1.ball.speed > 0)
            {

                if (Pos.Y < game1.ball.Pos.Y)
                {
                    this.position.Y += 2;
                } else if (Pos.Y > game1.ball.Pos.Y){
                    this.position.Y -= 2;
                }

            }


            // todo: AI
            this.Position.X = this.position.X;
            this.Position.Y = this.position.Y;
        }
    }
}
