using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.GameObjects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PingPong.Ui;
using PingPong.Events;
using PingPong.Menus;

namespace PingPong.GameScreens
{
    class MenuGameScreen: GameScreen
    {
        public SpriteFont font;

        public MenuGameScreen() :base("Menu")
        {
            add(new Property<SpriteFont>("font"));
        }

        public override void init()
        {
            handler.game.IsMouseVisible = true;
            font = put("font", Content.Load<SpriteFont>("Font"));
            handler.game.menuHandler.changeTo("Main");
        }

        public override void unload()
        {
            handler.game.IsMouseVisible = false;
        }

        public override void update(GameTime gameTime)
        {

        }

        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

    }
}
