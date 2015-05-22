using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.Ui;

namespace PingPong.Menus
{
    class EmptyMenu: Menu
    {
        public EmptyMenu(Game1 game): base(game, 0, 0, "EmptyMenu")
        {

        }

        public override void onButtonClick(Button btn)
        {

        }
    }
}
