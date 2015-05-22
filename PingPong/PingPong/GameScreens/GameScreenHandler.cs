using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using PingPong.Ui;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PingPong.GameScreens
{
    public class GameScreenHandler
    {

        public Game1 game;
        public GameScreen activeGameScreen;
        public GameScreen oldGameScreen;
        private Dictionary<String, GameScreen> gameScreens;
        private GameScreen loaderScreen;
        private Stopwatch sw = new Stopwatch();

        private bool isChangingScreen;
        public bool isPaused = false;

        public GameScreenHandler(Game1 game)
        {
            this.game = game;
            this.gameScreens = new Dictionary<String, GameScreen>();
        }

        public void registerGameScreen(GameScreen gameScreen)
        {
            gameScreen.addHandler(this);
            gameScreens.Add(gameScreen.name, gameScreen);
        }

        public void setLoadingScreen(GameScreen screen)
        {
            screen.addHandler(this);
            this.loaderScreen = screen;
            this.loaderScreen.preInit();
            this.loaderScreen.init();
        }

        public bool changeTo(String gameScreen)
        {
            if (!isChangingScreen)
            {
                isChangingScreen = true;
                GameScreen temp = gameScreens[gameScreen];

                if (temp != null)
                {
                    if (isPaused)
                    {
                        isPaused = false;
                    }

                    oldGameScreen = activeGameScreen;
                    activeGameScreen = loaderScreen;
                    Log.debug("GameScreens: Changing GameScreen to [{0}]", gameScreen);
                    new Task(() => { loadNewScreen(temp); }).Start();
                    return true;
                }    
            }
            
            Log.debug("GameScreens: Invalid GameScreen [{0}]", gameScreen);
            return false;
        }

        private void loadNewScreen(GameScreen screen)
        {
            screen.preInit();

            if (oldGameScreen != null)
            {
                oldGameScreen.unload();
                oldGameScreen.transfer(screen);
            }
            Log.debug("GameScreens: GameScreen [{0}] is now loading", screen.name);

            sw.Reset();
            sw.Start();

            screen.initScreen();
            activeGameScreen = screen;

            sw.Stop();

            isChangingScreen = false;
            Log.debug("GameScreens: GameScreen [{0}] loaded in {1}", screen.name, sw.Elapsed);
        }

        public void updateGameScreen(GameTime time)
        {
            if (InputManager.isKeyClicked(Keys.Escape) && activeGameScreen.CanBePaused && !game.menuHandler.isTransitionRunning)
            {
                if (!isPaused)
                {
                    isPaused = true;
                    game.showCursor = true;
                    game.menuHandler.changeTo("Pause", TransitionType.SLIDEDOWN);
                }
                else if (game.menuHandler.canBeUnPaused)
                {
                    isPaused = false;
                    game.showCursor = false;
                    game.menuHandler.changeTo("None", TransitionType.SLIDEUP);
                }
            }

            if (!isPaused)
            {
                activeGameScreen.update(time);
            }
            else
            {

            }
            
        }

        public void drawGameScreen(GameTime gameTime, SpriteBatch batch)
        {
            activeGameScreen.draw(gameTime, batch);
        }

    }
}
