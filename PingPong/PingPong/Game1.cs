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

namespace PingPong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ballTexture;
        Texture2D padTexture;
        Vector2 ballCoord;
        Vector2 padCoord;
        Vector2 padCoord2;
        int speed;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            this.speed = 5;
            this.ballCoord = new Vector2(300.0f, 100.0f);
            this.padCoord = new Vector2(100.0f, 100.0f);
            this.padCoord2 = new Vector2(700.0f, 100.0f);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.ballTexture = this.Content.Load<Texture2D>("ball");
            this.padTexture = this.Content.Load<Texture2D>("pad");

            // TODO: use this.Content to load your game content here
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

            if (this.ballCoord.X > 800)
            {
                this.speed *= -1;
            }
            else if (this.ballCoord.X < 0)
            {
                this.speed *= -1;
            }

            this.ballCoord.X += this.speed;


            
            this.padCoord2.Y -= 2;

            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Up) && this.padCoord.Y > 0)
            {
                this.padCoord.Y -= 2;
            }

            if (state.IsKeyDown(Keys.Down) && this.padCoord.Y < 480-150)
            {
                this.padCoord.Y += 2;
            }


            base.Update(gameTime);

        

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //ball

            this.spriteBatch.Begin();
            this.spriteBatch.Draw(this.ballTexture, this.ballCoord, Color.White);

            //pad 1

            this.spriteBatch.Draw(this.padTexture, this.padCoord , Color.White);

            //pad 2

            this.spriteBatch.Draw(this.padTexture, this.padCoord2, Color.White);

            this.spriteBatch.End();

           

            base.Draw(gameTime);
        }
    }
}
