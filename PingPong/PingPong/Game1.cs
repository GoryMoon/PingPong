using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using PingPong.GameObjects;

namespace PingPong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Game objects
        PlayerPaddle playerPaddle;
        ComputerPaddle computerPaddle;
        public Ball ball;

        SoundEffect pingPongSound;
        SpriteFont highScore;

        public static Game1 instance;

        public int playerScore = 0;
        public int computerScore = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Create game objects
            this.playerPaddle = new PlayerPaddle(this, 100f, 100f);
            this.computerPaddle = new ComputerPaddle(this, 700f, 100f);
            this.ball = new Ball(this, 100f, 300f);

            instance = this;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.playerPaddle.LoadContent();
            this.computerPaddle.LoadContent();
            this.ball.LoadContent();
            pingPongSound = Content.Load<SoundEffect>("PingPongSound");
            highScore = Content.Load<SpriteFont>("HighScore");
        }

        /// <summary>
        /// Sprite
        /// </summary>
        private void HandleCollisions()
        {
            HandleCollision(computerPaddle);
            HandleCollision(playerPaddle);
        }

        private void HandleCollision(Paddle paddle)
        {
            Rectangle r = Rectangle.Intersect(paddle.BoundingBox, ball.BoundingBox);

            if (!r.IsEmpty)
            {
                this.ball.SpeedX *= -1;

                if (r.Y + r.Height == (paddle.Pos.Y + paddle.Height))
                {
                    this.ball.SpeedY = 5;
                }

                if (r.Y == paddle.Pos.Y)
                {
                    this.ball.SpeedY = -5;
                }

                pingPongSound.Play();
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            this.playerPaddle.Update(gameTime, Window);
            this.computerPaddle.Update(gameTime, Window);
            this.ball.Update(gameTime, Window);
            base.Update(gameTime);
            HandleCollisions();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();

            // Draw game objects
            this.playerPaddle.Draw(gameTime, this.spriteBatch);
            this.computerPaddle.Draw(gameTime, this.spriteBatch);
            this.ball.Draw(gameTime, this.spriteBatch);

            spriteBatch.DrawString(highScore, "Player Score: " + playerScore, new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(highScore, "Computer Score: " + computerScore, new Vector2(Window.ClientBounds.Width - 230, 10), Color.Black);

            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
