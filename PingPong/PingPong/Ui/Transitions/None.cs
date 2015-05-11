using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace PingPong.Ui.Transitions
{
    public class None: Transition
    {
        private Vector2 none = Vector2.Zero;

        public override void doTransition(Menu oldMenu, Menu newMenu, GameTime gameTime)
        {
            currentStatus = Status.DONE;
        }
    }
}
