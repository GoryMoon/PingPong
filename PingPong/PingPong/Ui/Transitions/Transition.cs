using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace PingPong.Ui.Transitions
{
    public enum Status {
        IDLE,
        PRESETUP,
        SETUP,
        RUNNING,
        DONE
    }

    public abstract class Transition
    {

        private Vector2 goal;
        private Vector2 start;
        private Status status = Status.IDLE;

        public virtual void preTransition(Menu oldMenu, Menu newMenu)
        {

        }
        public abstract void doTransition(Menu oldMenu, Menu newMenu, GameTime gameTime);

        public void transition(Menu oldMenu, Menu newMenu, GameTime gameTime)
        {
            if (status == Status.DONE)
            {
                oldMenu.X = start.X;
                oldMenu.Y = start.Y;
                newMenu.X = goal.X;
                newMenu.Y = goal.Y;

                goal = Vector2.Zero;
                start = Vector2.Zero;
                status = Status.IDLE;
            }

            if (status == Status.RUNNING)
            {
                doTransition(oldMenu, newMenu, gameTime);
            }

            if (status == Status.SETUP)
            {
                status = Status.RUNNING;
            }

            if (status == Status.PRESETUP)
            {
                status = Status.SETUP;

                goal = newMenu.Pos;
                if (oldMenu != null) start = oldMenu.Pos;

                preTransition(oldMenu, newMenu);
            }
            
        }

        public Vector2 newGoal
        {
            get
            {
                return goal;
            }
            set
            {
                goal = value;
            }
        }

        public Status currentStatus
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

    }
}
