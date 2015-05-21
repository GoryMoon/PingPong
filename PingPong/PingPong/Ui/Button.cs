using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.GameObjects;
using PingPong.GameScreens;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace PingPong.Ui
{  
    public class Button: GameObject
    {
        public int id;
        public Texture2D hoverTexture;
        public SpriteFont font;
        public String text;
        protected Vector2 textPos;
        protected Vector2 textSize;

        public event EventHandler Click;

        MouseState mouseState;

        protected bool isHovering;

        public Button(int id, GameScreen game, float x, float y, String text, SpriteFont font): base(game, x, y)
        {
            this.id = id;
            this.text = text;
            this.font = font;
        }

        /// <summary>
        /// NOTE! Only to be used with menus
        /// </summary>
        /// <param name="id"></param>
        /// <param name="game"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        public Button(int id, GameScreen game, String text, SpriteFont font)
            : this(id, game, -1, -1, text, font)
        {

        }

        /// <summary>
        /// NOTE! Only to be used with menus
        /// </summary>
        /// <param name="text"></param>
        public Button(String text)
            : this(-1, null, -1, -1, text, null)
        {

        }

        public override void LoadContent(ContentManager content)
        {
            this.spriteTexture = content.Load<Texture2D>("buttonBack");
            this.hoverTexture = content.Load<Texture2D>("buttonHover");

            textSize = new Vector2(font.MeasureString(text).X, font.MeasureString(text).Y);
            textPos.X = X + (Width - textSize.X) / 2;
            textPos.Y = Y + (Height - textSize.Y) / 2;
        }

        public override void Update(GameTime gameTime, GameWindow window)
        {
            MouseState lastState = mouseState;
            mouseState = Mouse.GetState();
            
            textPos.X = X + (Width - textSize.X) / 2;
            textPos.Y = Y + (Height - textSize.Y) / 2;

            Point mouse = new Point(mouseState.X, mouseState.Y);

            if (Bounding.Contains(mouse))
            {
                isHovering = true;

                if (mouseState.LeftButton == ButtonState.Pressed && lastState.LeftButton == ButtonState.Released)
                {
                    if (Click != null)
                    {
                        Click(this, EventArgs.Empty);
                        onClick(this, EventArgs.Empty);
                    }
                }
            }
            else if (isHovering)
            {
                isHovering = false;
            }
        }

        public virtual void onClick(Button btn, EventArgs e) {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.isHovering ? this.hoverTexture: this.spriteTexture, Pos, Color.White);
            drawString(gameTime, spriteBatch);
        }

        public virtual void drawString(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, textPos, isHovering ? Color.Gray : Color.Black);
        }

    }
}
