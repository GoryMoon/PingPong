using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.Ui;

namespace PingPong.Menus
{
    class EmptyMenu: Menu
    {
        public EmptyMenu(): base(0, 0)
        {

        }

        public override void onButtonClick(Button btn)
        {

        }
    }
}
