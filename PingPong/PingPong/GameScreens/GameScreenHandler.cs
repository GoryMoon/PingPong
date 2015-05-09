using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong.GameScreens
{
    public class GameScreenHandler
    {

        public Game1 game;
        public GameScreen activeGameScreen;
        public GameScreen oldGameScreen;
        private Dictionary<String, GameScreen> gameScreens;
        private GameScreen loaderScreen;

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
            this.loaderScreen.init();
        }

        public bool changeTo(String gameScreen)
        {
            GameScreen temp = gameScreens[gameScreen];

            if (temp != null)
            {
                oldGameScreen = activeGameScreen;
                activeGameScreen = loaderScreen;
                new Task(() => { loadNewScreen(temp); }).Start();
                return true;
            }

            return false;
        }

        private void loadNewScreen(GameScreen screen)
        {
            if (oldGameScreen != null)
            {
                oldGameScreen.unload();
                oldGameScreen.transfer(screen);
            }
            screen.initScreen();
            activeGameScreen = screen;
        }

        public void updateGameScreen(GameTime time)
        {
            activeGameScreen.update(time);
        }

        public void drawGameScreen(GameTime gameTime, SpriteBatch batch)
        {
            activeGameScreen.draw(gameTime, batch);
        }

    }
}
