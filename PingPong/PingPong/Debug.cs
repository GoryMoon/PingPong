using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PingPong
{
    public class Debug: GameObjects.GameObject
    {
        private MouseState mouseState;
        private KeyboardState keyboardState;
        private SpriteFont font;
        private String pressedKeys;

        public Debug()
            : base(null, 0, 0)
        {

        }

        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fps");
        }

        public override void Update(GameTime gameTime, GameWindow window)
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            pressedKeys = "";
            int i = 1;
            Keys[] keys = keyboardState.GetPressedKeys();
            foreach (Keys key in keys)
            {
                pressedKeys += key.ToString() + ((i++ <= keys.Length - 1) ? "," : "");
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            drawString("mouse: [" + mouseState.X + "," + mouseState.Y + "]", new Vector2(22, 42), spriteBatch);
            drawString("mouse-left: " + mouseState.LeftButton.ToString(), new Vector2(22, 62), spriteBatch);
            drawString("mouse-right: " + mouseState.RightButton.ToString(), new Vector2(22, 82), spriteBatch);
            drawString("pressed: [" + pressedKeys + "]", new Vector2(22, 102), spriteBatch);
        }

        private void drawString(String s, Vector2 pos, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, s, pos, Color.Black);
            pos.X += 1;
            pos.Y += 1;
            spriteBatch.DrawString(font, s, pos, Color.White);
        }
    }
}
