using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.GameObjects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong.GameScreens
{
    public class LoadingGameScreen: GameScreen
    {
        private int time;
        private String dots = ".";
        private SpriteFont font;

        public LoadingGameScreen() :base("Loading")
        {
            add(new Property<SpriteFont>("font"));
        }

        public override void init()
        {
            font = put("font", Content.Load<SpriteFont>("Font"));
        }

        public override void unload()
        {
            time = 0;
            dots = ".";
        }

        public override void update(GameTime gameTime)
        {
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
        }

        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int h = font.LineSpacing;
            spriteBatch.DrawString(font, "Loading" + dots, new Vector2(10, Window.ClientBounds.Height - h), Color.White);
        }

    }
}
