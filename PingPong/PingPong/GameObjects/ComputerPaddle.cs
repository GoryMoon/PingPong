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
    public class ComputerPaddle : Paddle
    {
        /// <summary>
        /// Create a new player paddle at a specified position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public ComputerPaddle(GameState game, float x, float y) : base(game, x, y)
        {

        }

        /// <summary>
        /// Update game logic; move player paddle on key press
        /// </summary>
        /// <param name="gameTime"></param>
        override public void Update(GameTime gameTime, GameWindow window)
        {
            if (game.get<Ball>("ball").SpeedX > 0)
            {

                if (Pos.Y < game.get<Ball>("ball").Pos.Y)
                {
                    this.Y += 2;
                }
                else if (Pos.Y > game.get<Ball>("ball").Pos.Y)
                {
                    this.Y -= 2;
                }

            }

        }
    }
}
