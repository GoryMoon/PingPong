using PingPong.Events;
using PingPong.GameObjects;
using PingPong.GameScreens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PingPong.Ui
{
    public abstract class Menu: GameObject
    {

        private HashSet<Button> buttons;
        private ButtonEventHandler eventHandler;

        public MenuHandler handler;
        public String name;
        public float textHeight;

        public SpriteFont buttonFont;

        protected KeyboardState keyboardState;
        protected KeyboardState lastKeyboardState;

        public bool loaded;

        public Menu(int x, int y)
            : this(x, y, null)
        {

        }


        public Menu(int x, int y, String name) 
            : base(null, x, y)
        {
            this.name = name;
            buttons = new HashSet<Button>();
            eventHandler = new ButtonEventHandler(buttons);
            eventHandler.Click += onButtonClick;
        }

        public void addHandler(MenuHandler handler)
        {
            this.handler = handler;
        }

        public override void LoadContent(ContentManager Content)
        {
            buttonFont = Content.Load<SpriteFont>("ButtonFont");
        }

        public void PostLoadContent(ContentManager Content)
        {
            int i = 0;
            textHeight = (name != null && name != "") ? buttonFont.MeasureString(name).Y: 0;
            foreach (Button btn in buttons)
            {
                btn.font = buttonFont;
                btn.id = i;
                btn.Y = Y + textHeight + 5 + i++ * 60;
                btn.X = X;

                btn.LoadContent(Content);
            }
            loaded = true;
        }

        public override void Update(GameTime gameTime, GameWindow window)
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            foreach (Button btn in buttons)
            {
                btn.Y = Y + textHeight + 5 + btn.id * 60;
                btn.X = X;
                btn.Update(gameTime, window);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Button btn in buttons)
            {
                btn.Draw(gameTime, spriteBatch);
            }
            if (name != null && name != "") spriteBatch.DrawString(buttonFont, name, new Vector2(X + (buttons.ElementAt(0).Width - buttonFont.MeasureString(name).X) / 2, Y), Color.White);
        }

        public void Unload()
        {
            buttons.Clear();
            loaded = false;
        }

        public abstract void onButtonClick(Button btn);

        public void addButton(Button btn)
        {
            buttons.Add(btn);
            eventHandler.addButton(btn);
        }

    }
}
