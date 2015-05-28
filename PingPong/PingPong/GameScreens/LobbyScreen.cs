using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using PingPong.GameObjects;

namespace PingPong.GameScreens
{
    public enum State
    {
        Lobby,
        Playing
    }

    public class LobbyScreen: GameScreen
    {

        public NetworkSession networkSession;
        public AvailableNetworkSessionCollection availableSessions;

        public SpriteFont font;

        public State currentState = State.Lobby;

        public LobbyScreen() : base("Lobby")
        {
            SignedInGamer.SignedIn +=
                                    new EventHandler<SignedInEventArgs>(SignedInGamer_SignedIn);
        }

        void SignedInGamer_SignedIn(object sender, SignedInEventArgs e)
        {
            e.Gamer.Tag = new PlayerPaddle(handler.game.onlineMainScreen, 0, 0, "P1");
        }

        public override void preInit()
        {

        }

        public override void init()
        {
            font = Content.Load<SpriteFont>("Font");
            handler.game.showCursor = true;
        }

        public override void update(GameTime gameTime)
        {
            if (!Guide.IsVisible)
            {
                foreach (SignedInGamer gamer in SignedInGamer.SignedInGamers)
                {
                    PlayerPaddle player = gamer.Tag as PlayerPaddle;

                    if (networkSession != null)
                    {
                        if (currentState == State.Lobby)
                        {
                            if (player.X == 0 && player.Y == 0)
                            {
                                if (networkSession.IsHost)
                                {
                                    player.Pos = new Vector2(25f, 100f);
                                }
                                else
                                {
                                    player.Pos = new Vector2(WindowWidth - 25f - 38f, 100f);
                                }
                            }
                            
                        }
                        if (currentState == State.Playing)
                        {
                            handler.game.menuHandler.changeTo("None");
                            handler.changeTo("MainMultiOnline");
                            currentState = State.Lobby;
                        }
                    }
                    else if (availableSessions != null)
                    {
                        
                    }
                    else
                    {
                        if (handler.game.onlineHost)
                        {
                            createSession();
                        }
                        else
                        {
                            availableSessions = NetworkSession.Find(NetworkSessionType.SystemLink, 1, null);
                        }
                    }

                }
            }
        }

        private void createSession()
        {
            networkSession = NetworkSession.Create(NetworkSessionType.SystemLink, 1, 8, 2, null);
            networkSession.AllowHostMigration = true;
            networkSession.AllowJoinInProgress = true;

            hookSessionEvents();
        }

        public void hookSessionEvents()
        {
            networkSession.GamerJoined += new EventHandler<GamerJoinedEventArgs>(networkSession_gamerJoined);
        }

        private void networkSession_gamerJoined(object sender, GamerJoinedEventArgs e)
        {
            if (!e.Gamer.IsLocal)
            {
                e.Gamer.Tag = new PlayerPaddle(handler.game.onlineMainScreen, 0, 0, "P1");
            }
            else
            {
                e.Gamer.Tag = getPlayer(e.Gamer.Gamertag);
            }
        }

        private PlayerPaddle getPlayer(string tag)
        {
            foreach (SignedInGamer gamer in SignedInGamer.SignedInGamers)
            {
                if (gamer.Gamertag == tag)
                {
                    return gamer.Tag as PlayerPaddle;
                }
            }

            return new PlayerPaddle(handler.game.onlineMainScreen, 0, 0, "P1");
        }

        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (networkSession != null)
            {
                DrawLobby(spriteBatch);
            }
            else if (availableSessions != null)
            {
                DrawAvailableSessions(spriteBatch);
            }
        }

        private void DrawLobby(SpriteBatch spriteBatch)
        {
            float y = 100;

            spriteBatch.DrawString(font,
                "Players Joined",
                new Vector2(100, y), Color.White);

            y += font.LineSpacing * 2;

            foreach (NetworkGamer gamer in networkSession.AllGamers)
            {
                string text = gamer.Gamertag;

                PlayerPaddle player = gamer.Tag as PlayerPaddle;

                if (player.picture == null)
                {
                    try
                    {
                        GamerProfile gamerProfile = gamer.GetProfile();
                        player.picture = Texture2D.FromStream(handler.game.GraphicsDevice, gamerProfile.GetGamerPicture());
                    }
                    catch (Exception ex)
                    {
                        player.picture = Content.Load<Texture2D>("profile");
                        //player.picture = Texture2D
                    }
                }

                if (gamer.IsReady)
                    text += " - ready!";

                spriteBatch.Draw(player.picture, new Vector2(100, y),
                    Color.White);
                spriteBatch.DrawString(font, text, new Vector2(170, y),
                    Color.White);

                y += font.LineSpacing + 64;
            }

        }

        private void DrawAvailableSessions(SpriteBatch spriteBatch)
        {
            float y = 100;

            spriteBatch.DrawString(font,
                "Available games",
                new Vector2(100, y), Color.White);

            y += font.LineSpacing * 2;

            int selectedSessionIndex = 0;

            for (int sessionIndex = 0; sessionIndex < availableSessions.Count; sessionIndex++)
            {
                Color color = Color.White;

                if (sessionIndex == selectedSessionIndex)
                    color = Color.Yellow;

                spriteBatch.DrawString(font,
                    availableSessions[sessionIndex].HostGamertag,
                    new Vector2(100, y), color);

                y += font.LineSpacing;
            }
        }

        public override void unload()
        {
            handler.game.showCursor = false;
        }
    }
}
