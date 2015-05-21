using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
        public SpriteBatch spriteBatch;
        public Camera2D camera;

        public GameScreenHandler screenHandler;
        public MenuHandler menuHandler;
        private bool loadedFirst;
        public Settings settings;

        public static bool showDebug;
        private Debug debug;

        private KeyboardState lastKeyboardState;
        private KeyboardState keyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //SetFrameRate(graphics, 144);

            Form gameForm = (Form)Form.FromHandle(Window.Handle);
            gameForm.FormClosing += onClose;

            graphics.PreferMultiSampling = true;

            settings = new Settings("PingPongGame/pingpong.conf");

            /*graphics.PreferredBackBufferHeight = 1200;
            graphics.PreferredBackBufferWidth = 1600;
            graphics.ApplyChanges();

            Components.Add(new FrameRateCounter(this));
            camera = new Camera2D();
            camera.Pos = new Vector2(800, 600);*/
            //camera.Zoom = 2f;
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
            screenHandler = new GameScreenHandler(this);
            screenHandler.registerGameScreen(new MainGameScreen());
            screenHandler.registerGameScreen(new MenuGameScreen());
            screenHandler.setLoadingScreen(new LoadingGameScreen());

            menuHandler = new MenuHandler(this);
            menuHandler.registerMenu("Main", new PingPong.Menus.MainMenu(300, 60));
            menuHandler.registerMenu("Options", new OptionsMenu(300, 60));

            debug = new Debug();
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
            debug.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void onClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            settings.save();
        }

        public void ExitApplication()
        {
            settings.save();
            Exit();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (!loadedFirst)
            {
                loadedFirst = true;
                screenHandler.changeTo("Menu");
            }

            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            if (lastKeyboardState != null && lastKeyboardState.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.F2) && keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F2))
            {
                showDebug = !showDebug;
            }

            if (showDebug)
            {
                debug.Update(gameTime, Window);
            }

            screenHandler.updateGameScreen(gameTime);
            menuHandler.updateMenu(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //this.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, camera.get_transformation(GraphicsDevice));
            this.spriteBatch.Begin();

            screenHandler.drawGameScreen(gameTime, spriteBatch);
            menuHandler.drawMenu(gameTime, spriteBatch);

            if (showDebug)
            {
                debug.Draw(gameTime, spriteBatch);
            }

            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
