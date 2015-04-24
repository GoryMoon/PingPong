using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.GameObjects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong.GameStates
{
    class MenuGameState: GameState
    {

        public MenuGameState() :base("Menu")
        {
            add(new Property<int>("time", 0));
            add(new Property<SpriteFont>("font"));
        }

        public override void init()
        {
            set("font", Content.Load<SpriteFont>("Font"));
        }

        public override void unload()
        {
            
        }

        public override void update(GameTime gameTime)
        {
            int time = get<int>("time");

            if (time >= 200)
            {
                handler.changeTo("Main");
            }
            time++;

            set("time", time);
        }

        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(get<SpriteFont>("font"), "Hello World: " + get<int>("time"), new Vector2(300, 300), Color.White);
        }

    }
}
