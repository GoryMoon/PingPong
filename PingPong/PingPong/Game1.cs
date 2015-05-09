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

using PingPong.GameScreens;
using PingPong.Ui;

using PingPong.GameObjects;
using PingPong.Menus;

namespace PingPong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GameScreenHandler handler;
        public MenuHandler menuHandler;
        private bool loadedFirst;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //SetFrameRate(graphics, 144);

            Components.Add(new FrameRateCounter(this));
        }

        public void SetFrameRate(GraphicsDeviceManager manager, int frames)
        {
            double dt = Math.Floor((double)1000 / (double)frames);
            manager.SynchronizeWithVerticalRetrace = false;
            this.TargetElapsedTime = TimeSpan.FromMilliseconds(dt);
            manager.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            handler = new GameScreenHandler(this);
            handler.registerGameScreen(new MainGameScreen());
            handler.registerGameScreen(new MenuGameScreen());
            handler.setLoadingScreen(new LoadingGameScreen());

            menuHandler = new MenuHandler(this);
            menuHandler.registerMenu("Main", new MainMenu(300, 60, null));
            
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (!loadedFirst)
            {
                loadedFirst = true;
                handler.changeTo("Menu");
            }

            handler.updateGameScreen(gameTime);
            menuHandler.updateMenu(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.GhostWhite);

            this.spriteBatch.Begin();

            handler.drawGameScreen(gameTime, spriteBatch);
            menuHandler.drawMenu(gameTime, spriteBatch);

            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
