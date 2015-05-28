using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework;

using PingPong.Ui;

namespace PingPong.Menus
{
    class LobbyMenu: Menu
    {
        Button readyButton;
        Button startGame;

        public LobbyMenu(Game1 game, int x, int y) :base(game, x, y, "Lobby", true)
        {

        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            base.LoadContent(Content);
            readyButton = new Button("Ready?");
            addButton(readyButton);
            addButton(new Button("Leave Lobby"));

        }

        public override void transitionDone()
        {

        }

        public override void Update(GameTime gameTime, GameWindow window)
        {
            base.Update(gameTime, window);

            if (game.lobby.networkSession != null)
            {
                if (startGame == null && game.lobby.networkSession.IsHost)
                {
                    startGame = new Button("Start");
                    startGame.isVisible = false;
                    textHeight = (name != null && name != "" && drawName) ? buttonFont.MeasureString(name).Y : 0;
                    startGame.font = buttonFont;
                    startGame.id = 2;
                    startGame.Y = Y + textHeight + 5 + 2 * 60;
                    startGame.X = X;

                    startGame.LoadContent(game.Content);

                    startGame.LoadContent(game.Content);
                    addButton(startGame);
                }

                if (game.lobby.networkSession.IsEveryoneReady)
                {
                    if (game.lobby.networkSession.IsHost)
                    {
                        startGame.isVisible = true;
                    }
                }
                else
                {
                    if (game.lobby.networkSession.IsHost)
                    {
                        startGame.isVisible = false;
                    }
                }
            }
        }

        public override void onButtonClick(Button btn)
        {
            switch (btn.id)
            {
                case 0:
                    foreach (LocalNetworkGamer gamer in game.lobby.networkSession.LocalGamers)
                    {
                        gamer.IsReady = !gamer.IsReady;
                        if (gamer.IsReady)
                        {
                            readyButton.text = "Unready?";
                        }
                        else
                        {
                            readyButton.text = "Ready?";
                        }
                    }
                    break;
                case 1:
                    if (game.lobby.networkSession != null)
                    {
                        if (game.lobby.networkSession.IsHost)
                        {
                            game.lobby.networkSession.Dispose();
                            game.lobby.networkSession = null;
                            game.lobby.availableSessions = null;
                        }
                        handler.changeTo("Main");
                        handler.game.screenHandler.changeTo("Menu");
                    }
                    break;
                case 2:
                    if (game.lobby.networkSession.IsHost)
                    {
                        Log.log("Start!");
                        game.lobby.currentState = GameScreens.State.Playing;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
