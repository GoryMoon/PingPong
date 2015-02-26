using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingPong.GameObjects
{
    class PlayerPaddle : GameObject
    {
        /// <summary>
        /// Create a new player paddle at a specified position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public PlayerPaddle(float x, float y) : base(x, y)
        {

        }
    }
}
