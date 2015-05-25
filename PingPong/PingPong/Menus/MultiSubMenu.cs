using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.Ui;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace PingPong.Menus
{
    class MultiSubMenu: Menu
    {

        public MultiSubMenu(Game1 game, int x, int y) : base(game, x, y, "Local or Online")
        {

        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);

            addButton(new Button("Local"));
            addButton(new Button("Online"));
            addButton(new Button("Back (Esc)"));
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.GameWindow window)
        {
            base.Update(gameTime, window);

            if (InputManager.isKeyDown(Keys.Escape))
            {
                handler.returnToLast(TransitionType.SLIDEDOWN);
            }
        }

        public override void onButtonClick(Button btn)
        {
            switch (btn.id)
            {
                case 0:
                    handler.changeTo("None");
                    handler.game.screenHandler.changeTo("Main");
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
