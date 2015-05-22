using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong.Ui
{
    public class ToggleButton: Button
    {
        public bool state;

        private Texture2D boxUnChecked;
        private Texture2D boxChecked;

        public ToggleButton(String text, bool state)
            : base(text)
        {
            this.state = state;
        }

        public ToggleButton(String text)
            : this(text, false)
        {

        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            boxUnChecked = content.Load<Texture2D>("checkbox-unchecked");
            boxChecked = content.Load<Texture2D>("checkbox-checked");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(state ? boxChecked : boxUnChecked, new Vector2(Pos.X + spriteTexture.Width + 10, Pos.Y), Color.White);
            spriteBatch.DrawString(font, text, textPos, Color.White);
            //base.Draw(gameTime, spriteBatch);
        }

        public override void onClick(Button btn, EventArgs e)
        {
            state = !state;
        }

        public override Rectangle Bounding { get { return new Rectangle((int)X, (int)Y, (int)Width + boxChecked.Width + 10, (int)Height); } }

    }
}
