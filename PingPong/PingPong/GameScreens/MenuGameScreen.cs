using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.GameObjects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong.GameStates
{
    class MenuGameScreen: GameState
    {
        public SpriteFont font;
        int time;

        public MenuGameScreen() :base("Menu")
        {
            add(new Property<int>("time", 0));
            add(new Property<SpriteFont>("font"));
        }

        public override void init()
        {
            font = put("font", Content.Load<SpriteFont>("Font"));
        }

        public override void unload()
        {
            set("font", font);
            time = 0;
        }

        public override void update(GameTime gameTime)
        {
            if (time >= 100)
            {
                handler.changeTo("Main");
                return;
            }
            time++;
        }

        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Hello World: " + time, new Vector2(300, 300), Color.White);
        }

    }
}
