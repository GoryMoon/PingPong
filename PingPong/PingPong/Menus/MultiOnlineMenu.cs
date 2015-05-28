using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.Ui;
using PingPong.Server;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;

namespace PingPong.Menus
{
    class MultiOnlineMenu: Menu
    {

        public MultiOnlineMenu(Game1 game, int x, int y)
            : base(game, x, y, "Host or Join")
        {

        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);

            addButton(new Button("Join"));
            addButton(new Button("Host"));
            addButton(new Button("Back (Esc)"));
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.GameWindow window)
        {
            base.Update(gameTime, window);

            if (InputManager.isKeyDown(Keys.Escape))
            {
                handler.returnToLast(TransitionType.SLIDEDOWN);
            }

            if (!Guide.IsVisible && SignedInGamer.SignedInGamers.Count == 0)
            {
                Guide.ShowSignIn(1, false);
            }
        }

        public override void onButtonClick(Button btn)
        {
            switch (btn.id)
            {
                case 0:
                    game.onlineHost = false;
                    game.screenHandler.changeTo("Lobby");
                    handler.changeTo("Sessions");
                    break;
                case 1:
                    game.onlineHost = true;
                    game.screenHandler.changeTo("Lobby");
                    handler.changeTo("Lobby");
                    break;
                case 2:
                    handler.returnToLast(TransitionType.SLIDEDOWN);
                    break;
                default:
                    break;
            }
        }
    }
}
