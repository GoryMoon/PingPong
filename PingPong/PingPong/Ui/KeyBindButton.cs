using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong.Ui
{
    public class KeyBindButton: Button
    {

        public String prefix;
        public String bindName;
        public String keyValue;

        public KeyBindButton(String text, String keyValue, String bindName)
            : base(text + keyValue)
        {
            this.prefix = text;
            this.bindName = bindName;
            this.keyValue = keyValue;
        }

        public override void drawString(GameTime gameTime, SpriteBatch spriteBatch)
        {
            textSize = font.MeasureString(prefix);

            textPos.X = X + 10;
            spriteBatch.DrawString(font, prefix, textPos, isHovering ? Color.Gray : Color.Black);

            textSize = font.MeasureString(keyValue);
            textPos.X = X + spriteTexture.Width - textSize.X - 10;
            spriteBatch.DrawString(font, keyValue, textPos, isHovering ? Color.Gray : Color.Black);
        }


    }
}
