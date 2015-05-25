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
        
        public GameScreenHandler screenHandler;
        public MenuHandler menuHandler;
        private bool loadedFirst;
        public Settings settings;

        public static bool showDebug;
        private Debug debug;

        private KeyboardState lastKeyboardState;
        private KeyboardState keyboardState;

        private Texture2D cursor;
        private Vector2 cursorPos;
        public bool showCursor;

        private static float windowWidth;
        private static float windowHeight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = false;
            //SetFrameRate(graphics, 144);

            Form gameForm = (Form)Form.FromHandle(Window.Handle);
            gameForm.FormClosing += onClose;
            gameForm.MinimizeBox = false;

            settings = new Settings("PingPongGame/pingpong.conf");

            graphics.PreferMultiSampling = true;
            Resolution.Init(ref graphics);

            Point p = settings.getResolution("res");
            bool fullScreen = Convert.ToBoolean(settings.get("fullscreen"));
            Resolution.SetVirtualResolution(800, 600);
            Resolution.SetResolution((int)p.X, (int)p.Y, fullScreen);

            Components.Add(new FrameRateCounter(this));
            Components.Add(new InputManager());

            windowWidth = MathHelper.remapX(GraphicsDevice.Viewport.Width);
            windowHeight = MathHelper.remapY(GraphicsDevice.Viewport.Height);
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
            menuHandler.registerMenu("Main", new PingPong.Menus.MainMenu(this, 300, 60));
            menuHandler.registerMenu("Options", new OptionsMenu(this, 300, 60));
            menuHandler.registerMenu("Pause", new PauseMenu(this, 300, 60));
            menuHandler.registerMenu("MultiSub", new MultiSubMenu(this, 300, 60));

            debug = new Debug(this);
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
            cursor = Content.Load<Texture2D>("cursor");
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
            Game1.WindowWidth = Resolution.WindowSize.X;
            Game1.WindowHeight = Resolution.WindowSize.Y;

            cursorPos = InputManager.MousePosition;

            if (cursorPos.X < 0)
            {
                cursorPos.X = 0;
            }
            else if (InputManager.MousePosition.X > Resolution.WindowSize.X)
            {
                cursorPos.X = Resolution.WindowSize.X - 1;
            }

            if (cursorPos.Y < 0)
            {
                cursorPos.Y = 0;
            }
            else if (InputManager.MousePosition.Y > Resolution.WindowSize.Y)
            {
                cursorPos.Y = Resolution.WindowSize.Y - 1;
            }

            if (!loadedFirst)
            {
                loadedFirst = true;
                screenHandler.changeTo("Menu");
                Mouse.SetPosition(GraphicsDevice.Viewport.X + GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Y + GraphicsDevice.Viewport.Height / 2);
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
            Resolution.BeginDraw();

            GraphicsDevice.Clear(Color.Black);

            this.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Resolution.getTransformationMatrix());
            //this.spriteBatch.Begin();

            screenHandler.drawGameScreen(gameTime, spriteBatch);
            menuHandler.drawMenu(gameTime, spriteBatch);

            if (showDebug)
            {
                debug.Draw(gameTime, spriteBatch);
            }

            if (showCursor) spriteBatch.Draw(cursor, cursorPos, Color.White);
            this.spriteBatch.End();
            
            base.Draw(gameTime);
        }

        public static float WindowWidth { get { return windowWidth; } set { windowWidth = value; } }
        public static float WindowHeight { get { return windowHeight; } set { windowHeight = value; } }
    }
}
