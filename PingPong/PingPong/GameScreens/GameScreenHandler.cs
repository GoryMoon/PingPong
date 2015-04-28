using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong.GameStates
{
    public class GameScreenHandler
    {

        public volatile Game1 game;
        public GameState activeGameState;
        public GameState oldGameState;
        private Dictionary<String, GameState> gameStates;
        private GameState loaderState;

        public GameScreenHandler(Game1 game)
        {
            this.game = game;
            this.gameStates = new Dictionary<string, GameState>();
        }

        public void registerGameState(GameState gameState)
        {
            gameState.addHandler(this);
            gameStates.Add(gameState.name, gameState);
        }

        public void setLoadingState(GameState state)
        {
            state.addHandler(this);
            this.loaderState = state;
            this.loaderState.init();
        }

        public bool changeTo(String gameState)
        {
            GameState temp = gameStates[gameState];

            if (temp != null)
            {
                oldGameState = activeGameState;
                activeGameState = loaderState;
                new Task(() => { loadNewState(temp); }).Start();
                return true;
            }

            return false;
        }

        private void loadNewState(GameState state)
        {
            if (oldGameState != null)
            {
                oldGameState.unload();
                oldGameState.transfer(state);
            }
            state.initState();
            activeGameState = state;
        }

        public void updateGameState(GameTime time)
        {
            activeGameState.update(time);
        }

        public void drawGameState(GameTime gameTime, SpriteBatch batch)
        {
            activeGameState.draw(gameTime, batch);
        }

    }
}
