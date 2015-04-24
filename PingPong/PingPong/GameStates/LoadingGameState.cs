using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.GameObjects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong.GameStates
{
    public class LoadingGameState: GameState
    {

        public LoadingGameState() :base("Loading")
        {
            add(new Property<int>("time", 0));
            add(new Property<String>("dots", "."));
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
            String dots = get<String>("dots");

            if (time >= 10)
            {
                time = 0;
                dots += ".";

                if (dots == "....")
                {
                    dots = ".";
                }
            }
            time++;

            set("time", time);
            set("dots", dots);
        }

        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int h = get<SpriteFont>("font").LineSpacing;
            spriteBatch.DrawString(get<SpriteFont>("font"), "Loading" + get<String>("dots"), new Vector2(10, Window.ClientBounds.Height - h), Color.White);
        }

    }
}
