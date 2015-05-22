using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.Ui;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong.Menus
{
    class OptionsMenu: Menu
    {
        private bool keySelecting;
        private string bindKey = "";
        private Keys pressedKey;
        private bool keyEntered;
        private KeyBindButton activeKeyBind;
        private int time;

        private Texture2D keyBindTexture;
        private SpriteFont font;

         KeyBindButton p1U;
         KeyBindButton p1D;
         KeyBindButton p2U;
         KeyBindButton p2D;

        public OptionsMenu(Game1 game, int x, int y) : base(game, x, y, "Options", true)
        {

        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            keyBindTexture = Content.Load<Texture2D>("buttonHover");
            font = Content.Load<SpriteFont>("ButtonFont");

            addButton(new ToggleButton("Fullscreen", Convert.ToBoolean(Settings.get("fullscreen"))));


            p1U = new KeyBindButton("P1 Up: ", Settings.getKey("P1-U").ToString(), "P1-U");
            p1D = new KeyBindButton("P1 Down: ", Settings.getKey("P1-D").ToString(), "P1-D");
            p2U = new KeyBindButton("P2 Up: ", Settings.getKey("P2-U").ToString(), "P2-U");
            p2D = new KeyBindButton("P2 Down: ", Settings.getKey("P2-D").ToString(), "P2-D");
            
            addButton(p1U);
            addButton(p1D);
            addButton(p2U);
            addButton(p2D);

            addButton(new Button("Back (Esc)"));
        }

        public override void Update(GameTime gameTime, GameWindow window)
        {
            base.Update(gameTime, window);

            if (!keySelecting)
            {
                if (keyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape))
                {
                    handler.changeTo("Main", TransitionType.SLIDERIGHT);
                }
            }
            else
            {
                if (!keyEntered) time += gameTime.ElapsedGameTime.Milliseconds;
                if (time >= 260)
                {
                    time = 0;
                    if (bindKey == "")
                    {
                        bindKey = "_";
                    }
                    else if (bindKey == "_")
                    {
                        bindKey = "";
                    }
                }

                Keys[] lastPressed = lastKeyboardState.GetPressedKeys();
                Keys[] pressed = keyboardState.GetPressedKeys();

                if (pressed.Length > 0)
                {
                    if (!lastPressed.Contains(pressed[0]) && pressed[0] != Keys.Escape)
                    {
                        keyEntered = true;
                        bindKey = pressed[0].ToString();
                        pressedKey = pressed[0];
                    }
                    else if (!lastPressed.Contains(pressed[0]) && pressed[0] == Keys.Escape)
                    {
                        if (keyEntered && pressedKey != Keys.None)
                        {
                            Settings.set(activeKeyBind.bindName, (int)pressedKey);
                            activeKeyBind.keyValue = pressedKey.ToString();
                        }

                        activeKeyBind = null;
                        bindKey = "";
                        keyEntered = false;
                        keySelecting = false;
                    }
                }
            }

        }

        public override void onButtonClick(Button btn)
        {
            if (!keySelecting)
            {
                switch (btn.id)
                {
                    case 0:
                        Point p = Settings.getResolution("res");
                        Settings.set("fullscreen", ((ToggleButton)btn).state);
                        Resolution.SetResolution((int)p.X, (int)p.Y, ((ToggleButton)btn).state);
                        break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        activeKeyBind = ((KeyBindButton)btn);
                        keySelecting = true;
                        break;
                    case 5:
                        handler.changeTo("Main", TransitionType.SLIDERIGHT);
                        break;
                    default:
                        break;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            if (keySelecting)
            {
                float centerX1 = (Game1.WindowWidth / 2) - ((keyBindTexture.Width * 2f) / 2);
                float centerY1 = (Game1.WindowHeight / 2) - ((keyBindTexture.Height * 2f) / 2);
                float centerX2 = (Game1.WindowWidth / 2) - ((keyBindTexture.Width * 1.8f) / 2);
                float centerY2 = (Game1.WindowHeight / 2) - ((keyBindTexture.Height * 1.8f) / 2);

                spriteBatch.Draw(keyBindTexture, new Vector2(centerX1, centerY1), null, Color.Gray, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                spriteBatch.Draw(keyBindTexture, new Vector2(centerX2, centerY2), null, Color.White, 0f, Vector2.Zero, 1.8f, SpriteEffects.None, 0f);
                
                string infoText = "Press the key to bind. Exit [Esc]";
                float textWidth = font.MeasureString(infoText).X;
                spriteBatch.DrawString(font, infoText, new Vector2(centerX2 + (((keyBindTexture.Width * 1.8f) / 2) - (textWidth / 2)), centerY2 + 5), Color.Black);

                float keyWidth = font.MeasureString(bindKey).X;
                spriteBatch.DrawString(font, bindKey, new Vector2(centerX2 + (((keyBindTexture.Width * 1.8f) / 2) - (keyWidth / 2)), centerY2 + 30), Color.Black, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            }

        }

    }
}
