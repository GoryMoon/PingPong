using PingPong.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong.Ui
{
    public class MenuHandler
    {

        public Game1 game;
        private Menu oldMenu;
        private Menu activeMenu;
        private Dictionary<String, Menu> menus;

        public MenuHandler(Game1 game)
        {
            this.game = game;
            menus = new Dictionary<String, Menu>();
            registerMenu("none", new EmptyMenu());
            changeTo("none");
        }

        public void registerMenu(String name, Menu menu)
        {
            menu.addHandler(this);
            menus.Add(name, menu);
        }

        public bool changeTo(String name) {
            Menu temp = menus[name];

            if (temp != null)
            {
                oldMenu = activeMenu;
                temp.LoadContent(game.Content);
                temp.PostLoadContent(game.Content);
                activeMenu = temp;
            }

            return false;
        }

        public void updateMenu(GameTime gameTime)
        {
            activeMenu.Update(gameTime, game.Window);
        }

        public void drawMenu(GameTime gameTime, SpriteBatch spriteBatch)
        {
            activeMenu.Draw(gameTime, spriteBatch);
        }

    }
}
