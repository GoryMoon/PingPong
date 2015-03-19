using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PingPong.GameObjects
{
    /// <summary>
    /// Base class for all game objects on screen
    /// </summary>
    public abstract class GameObject : Sprite
    {
        protected Vector2 position;
        protected Game game;

        /// <summary>
        /// Initialize a new game object
        /// </summary>
        /// <param name="game"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public GameObject(Game game, float x, float y, int width, int height) : base(new Vector2(x, y), width, height)
        {
            this.game = game;
            this.position.X = x;
            this.position.Y = y;
        }

        /// <summary>
        /// Load the sprites and store them somewhere
        /// </summary>
        public abstract void LoadContent();

        /// <summary>
        /// Update the game logic. Move object, etc.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            // Do nothing...
        }

        /// <summary>
        /// Draw the game object on screen using spriteBatch
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
