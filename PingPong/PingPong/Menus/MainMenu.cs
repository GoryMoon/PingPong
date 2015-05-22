using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.GameScreens;
using PingPong.Ui;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong.Menus
{
    public class MainMenu: Menu
    {
        public MainMenu(Game1 game, int x, int y)
            : base(game, x, y, "MainMenu")
        {

        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);

            addButton(new Button("Singleplayer"));
            addButton(new Button("Multiplayer"));
            addButton(new Button("Options"));
            addButton(new Button("Exit"));
        }

        public override void Update(GameTime gameTime, GameWindow window)
        {
            base.Update(gameTime, window);
        }

        public override void onButtonClick(Button btn)
        {
            switch (btn.id)
            {
                case 0:
                    handler.changeTo("None");
                    handler.game.screenHandler.changeTo("Main");
                    break;
                case 1:

                    break;
                case 2:
                    handler.changeTo("Options", TransitionType.SLIDELEFT);
                    break;
                case 3:
                    handler.game.ExitApplication();
                    break;
                default:
                    break;
            }
        }

    }
}
