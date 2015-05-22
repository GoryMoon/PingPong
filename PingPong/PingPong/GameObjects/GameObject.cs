using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.GameScreens;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PingPong.GameObjects
{
    /// <summary>
    /// Base class for all game objects on screen
    /// </summary>
    public abstract class GameObject
    {
        private Vector2 position;
        protected GameScreen gameScreen;
        protected Texture2D spriteTexture;

        /// <summary>
        /// Initialize a new game object
        /// </summary>
        /// <param name="game"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public GameObject(GameScreen game, float x, float y)
        {
            this.gameScreen = game;
            this.position.X = x;
            this.position.Y = y;
        }

        /// <summary>
        /// Load the sprites and store them somewhere
        /// </summary>
        public abstract void LoadContent(ContentManager content);

        /// <summary>
        /// Update the game logic. Move object, etc.
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime, GameWindow window);

        /// <summary>
        /// Draw the game object on screen using spriteBatch
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.spriteTexture, this.Pos, Color.White);
        }

        public Vector2 Pos { get { return position; } }

        public float X { get { return position.X; } set { position.X = value; } }
        public float Y { get { return position.Y; } set { position.Y = value; } }
        public float Width { get { return spriteTexture.Width; } }
        public float Height { get { return spriteTexture.Height; } }

        public float Bottom { get { return Y + Height; } }
        public float Top { get { return Y; } }
        public float Left { get { return X; } }
        public float Right { get { return X + Width; } }

        public virtual Rectangle Bounding { get { return new Rectangle((int)X, (int)Y, (int)Width, (int)Height); } }
    }
}
