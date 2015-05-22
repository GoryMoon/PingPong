using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace PingPong.Ui.Transitions
{
    class SlideUp: Transition
    {
        public override void preTransition(Menu oldMenu, Menu newMenu)
        {
            newMenu.Y += newMenu.handler.WindowHeight;
        }

        public override void doTransition(Menu oldMenu, Menu newMenu, GameTime gameTime)
        {
            if (newMenu.Y <= newGoal.Y)
            {
                currentStatus = Status.DONE;
            }
            else
            {
                float dY = -(float)(gameTime.ElapsedGameTime.Milliseconds * 1.5);

                if (newMenu.Y + dY <= newGoal.Y)
                {
                    newMenu.Y = newGoal.Y;
                }
                else
                {
                    newMenu.Y += dY;
                    oldMenu.Y += dY;
                }
            }
        }
    }
}
