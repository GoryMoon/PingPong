using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.Ui;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PingPong.Menus
{
    public class PauseMenu: Menu
    {

        public PauseMenu(Game1 game, int x, int y)
            : base(game, x, y, "Paused", true)
        {

        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);

            addButton(new Button("Return to Menu"));
            addButton(new Button("Options"));
            addButton(new Button("Exit Game"));
        }

        public override void onButtonClick(Button btn)
        {
            switch (btn.id)
            {
                case 0:
                    handler.game.screenHandler.changeTo("Menu");
                    break;
                case 1:
                    handler.changeTo("Options", TransitionType.SLIDELEFT);
                    break;
                case 2:
                    handler.game.ExitApplication();
                    break;
                default:
                    break;
            }
        }

        public override bool canUnPause
        {
            get
            {
                return !handler.isTransitionRunning;
            }
        }
    }
}
