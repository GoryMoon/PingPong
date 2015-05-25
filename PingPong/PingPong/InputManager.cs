using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PingPong
{
    public class InputManager : IGameComponent, IUpdateable
    {
        static private MouseState currentMouseState;
        static private MouseState previousMouseState;

        static private KeyboardState currentKeyboardState;
        static private KeyboardState previousKeyboardState;

        public void Update(GameTime gameTime)
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }

        static public bool isKeyClicked(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
        }

        static public bool isKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        static public bool isLeftMouseClicked()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
        }

        static public Vector2 MousePosition
        {
            get { return new Vector2(MathHelper.remapX(currentMouseState.X), MathHelper.remapY(currentMouseState.Y)); }
        }

        static public Vector2 RawMousePosition
        {
            get { return new Vector2(currentMouseState.X, currentMouseState.Y); }
        }

        public bool Enabled
        {
            get { return true; }
        }

        public event EventHandler<EventArgs> EnabledChanged;

        public int UpdateOrder
        {
            get { return 0; }
        }

        public event EventHandler<EventArgs> UpdateOrderChanged;

        public void Initialize()
        {

        }
    }
}
