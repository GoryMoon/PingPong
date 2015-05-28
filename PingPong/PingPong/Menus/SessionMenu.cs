using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.Ui;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Net;

namespace PingPong.Menus
{
    public class SessionMenu: Menu
    {

        public Button[] btns;
        public SpriteFont font;

        public SessionMenu(Game1 game, int x, int y)
            : base(game, x, y, "Available Games")
        {

        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);

            addButton(new Button("Leave"));
            font = Content.Load<SpriteFont>("ButtonFont");
        }

        public override void Update(GameTime gameTime, GameWindow window)
        {
            base.Update(gameTime, window);

            if (btns == null)
            {
                int y = 120;
                btns = new Button[game.lobby.availableSessions.Count];

                for (int sessionIndex = 0; sessionIndex < game.lobby.availableSessions.Count; sessionIndex++)
                {
                    btns[sessionIndex] = new Button(sessionIndex, game.screenHandler.activeGameScreen, 100f, y + 5 + 60 * sessionIndex, game.lobby.availableSessions[sessionIndex].HostGamertag, font);
                    btns[sessionIndex].Click += sessionBtnClick;
                }
            }

            foreach (Button btn in btns)
            {
                btn.Update(gameTime, game.Window);
            }
        }

        public void sessionBtnClick(object button, EventArgs e)
        {
            if (button != null && button.GetType() == typeof(Button))
            {
                Button btn = button as Button;
                game.lobby.networkSession = NetworkSession.Join(game.lobby.availableSessions[btn.id]);
                game.lobby.hookSessionEvents();

                game.lobby.availableSessions.Dispose();
                game.lobby.availableSessions = null;
                handler.changeTo("Lobby");
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            foreach (Button btn in btns)
            { 
                btn.Draw(gameTime, spriteBatch);
            }
        }

        public override void onButtonClick(Button btn)
        {
            switch (btn.id)
            {
                case 0:

                    handler.returnToLast();
                    if (game.lobby.availableSessions != null) game.lobby.availableSessions.Dispose();
                    game.lobby.availableSessions = null;

                    break;
                default:
                    break;
            }
        }
    }
}
