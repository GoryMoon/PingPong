using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace PingPong.Ui.Transitions
{
    public class SlideRight: Transition
    {

        public override void preTransition(Menu oldMenu, Menu newMenu)
        {
            newMenu.X -= newMenu.handler.WindowWidth;
        }

        public override void doTransition(Menu oldMenu, Menu newMenu, GameTime gameTime)
        {
            if (newMenu.X >= newGoal.X)
            {
                currentStatus = Status.DONE;
            }
            else
            {
                float dX = (float)(gameTime.ElapsedGameTime.Milliseconds * 1.5);

                if (newMenu.X + dX >= newGoal.X)
                {
                    newMenu.X = newGoal.X;
                }
                else
                {
                    newMenu.X += dX;
                    oldMenu.X += dX;
                }
            }
            
        }
    }
}
