using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.Ui;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace PingPong.Menus
{
    class OptionsMenu: Menu
    {
        public OptionsMenu(int x, int y) : base(x, y, "Options")
        {

        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);

            addButton(new Button("Back (Esc)"));
        }

        public override void Update(GameTime gameTime, GameWindow window)
        {
            base.Update(gameTime, window);

            if (keyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape))
            {
                handler.changeTo("Main", TransitionType.SLIDERIGHT);
            }

        }

        public override void onButtonClick(Button btn)
        {
            switch (btn.id)
            {
                case 0:
                    handler.changeTo("Main", TransitionType.SLIDERIGHT);
                    break;
                default:
                    break;
            }
        }
    }
}
