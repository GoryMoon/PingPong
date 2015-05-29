using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using PingPong.GameObjects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.GamerServices;

namespace PingPong.GameScreens
{
    public class MainGameScreen: GameScreen
    {

        public PlayerPaddle player1;
        public PlayerPaddle player2;
        public ComputerPaddle computer;
        public Ball ball;
        public SoundEffect ping;
        public SpriteFont font;
        public int player1Score;
        public int opponentScore;

        public bool multi;
        public bool online;

        PacketReader packetReader = new PacketReader();
        PacketWriter packetWriter = new PacketWriter();

        public MainGameScreen(String name, bool multi, bool online) :base(name)
        {
            this.multi = multi;
            this.online = online;
        }

        public MainGameScreen(String name, bool multi)
            : this(name, multi, false)
        {

        }

        public MainGameScreen(String name)
            : this(name, false, false)
        {

        }

        public override void preInit()
        {
            add(new Property<PlayerPaddle>("player1"));
            if (multi && !online) add(new Property<PlayerPaddle>("player2"));
            if (!multi) add(new Property<ComputerPaddle>("computer"));
            add(new Property<Ball>("ball"));
            add(new Property<SoundEffect>("ping"));
            add(new Property<SpriteFont>("font"));
            add(new Property<int>("player1Score"));
            if (!online) add(new Property<int>("opponentScore"));
        }

        public override void init()
        {
            if (!online) player1 = put("player1", new PlayerPaddle(this, 25f, 100f, "P1"));
            if (multi && !online) player2 = put("player2", new PlayerPaddle(this, WindowWidth - 25f - 38f, 100f, "P2"));

            if (!multi) computer = put("computer", new ComputerPaddle(this, WindowWidth - 25f - 38f, 100f));

            ball = put("ball", new Ball(this, 300f, 300f));
            ping = put("ping", Content.Load<SoundEffect>("hit"));
            font = put("font", Content.Load<SpriteFont>("Font"));

            player1Score = put("player1Score", 0);
            if (!online) opponentScore = put("opponentScore", 0);

            if (!online) player1.LoadContent(Content);
            if (multi && !online) player2.LoadContent(Content);
            if (!multi) computer.LoadContent(Content);
            ball.LoadContent(Content);

            if (online)
            {
                foreach (NetworkGamer gamer in handler.game.lobby.networkSession.LocalGamers)
                {
                    PlayerPaddle player = gamer.Tag as PlayerPaddle;
                    player.LoadContent(Content);
                }
            }

        }

        public override void unload()
        {
            set("player1", player1);
            if (multi && !online) set("player2", player2);
            if (!multi) set("computer", computer);
            set("ball", ball);
            set("ping", ping);
            set("font", font);
            set("player1Score", player1Score);
            if (!online) set("computerScore", opponentScore);
        }

        /// <summary>
        /// Collision with the ball
        /// </summary>
        private void HandleCollisions()
        {
            if (!multi) HandleCollision(computer);
            if (!online) HandleCollision(player1);
            if (multi && !online) HandleCollision(player2);

            if (online)
            {
                foreach (NetworkGamer gamer in handler.game.lobby.networkSession.AllGamers)
                {
                    PlayerPaddle player = gamer.Tag as PlayerPaddle;
                    HandleCollision(player);
                }
            }

            //player.checkCollision(ball);

        }

        /// <summary>
        /// Helper function for collision handling
        /// </summary>
        private void HandleCollision(Paddle paddle)
        {
            Rectangle r = Rectangle.Intersect(paddle.Bounding, ball.Bounding);

            if (!r.IsEmpty)
            {
                ball.SpeedX *= -1;

                if (r.Y + r.Height == (paddle.Pos.Y + paddle.Height))
                {
                    ball.SpeedY = 5;
                }

                if (r.Y == paddle.Pos.Y)
                {
                    ball.SpeedY = -5;
                }

                ping.Play();
            }
        }

        public override void update(GameTime gameTime)
        {
            if (online)
            {
                foreach (SignedInGamer signedInGamer in
                    SignedInGamer.SignedInGamers)
                {
                    PlayerPaddle player = signedInGamer.Tag as PlayerPaddle;

                    updatePlayer(player, gameTime);
                }
                handler.game.lobby.networkSession.Update();
            }

            if (!online) player1.Update(gameTime, Window);
            if (multi && !online) player2.Update(gameTime, Window);
            if (!multi) computer.Update(gameTime, Window);
            ball.Update(gameTime, Window);
            HandleCollisions();
        }

        private void updatePlayer(PlayerPaddle player, GameTime gameTime)
        {
            foreach (LocalNetworkGamer gamer in handler.game.lobby.networkSession.LocalGamers)
            {
                receiveNetworkData(gamer, gameTime);

                player.Update(gameTime, handler.game.Window);

                packetWriter.Write(player.Pos);
                gamer.SendData(packetWriter, SendDataOptions.None);
            }
            
        }

        private void receiveNetworkData(LocalNetworkGamer gamer, GameTime gameTime)
        {
            while (gamer.IsDataAvailable)
            {
                NetworkGamer sender;
                gamer.ReceiveData(packetReader, out sender);

                if (!sender.IsLocal)
                {
                    PlayerPaddle player = sender.Tag as PlayerPaddle;
                    player.Pos = packetReader.ReadVector2();
                    player.Update(gameTime, handler.game.Window);
                }
            }
        }

        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (handler.game.lobby.networkSession != null)
            {
                PlayerPaddle player;
                foreach (NetworkGamer gamer in handler.game.lobby.networkSession.AllGamers)
                {
                    player = gamer.Tag as PlayerPaddle;
                    if (gamer.IsLocal)
                    {
                        player.Draw(gameTime, spriteBatch);
                    }
                    else
                    {
                        player.Draw(gameTime, spriteBatch);
                    }
                }
            }

            // Draw game objects
            if (!online) player1.Draw(gameTime, spriteBatch);
            if (multi && !online) player2.Draw(gameTime, spriteBatch);
            if (!multi) computer.Draw(gameTime, spriteBatch);
            ball.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(font, "Player 1 Score: " + player1Score, new Vector2(10, 10), Color.White);
            if (multi && !online) spriteBatch.DrawString(font, "Player 2 Score: " + opponentScore, new Vector2(Resolution.WindowSize.X - 240, 10), Color.White);
            if (!multi) spriteBatch.DrawString(font, "Computer Score: " + opponentScore, new Vector2(Resolution.WindowSize.X - 230, 10), Color.White);
        }

        public override bool CanBePaused
        {
            get
            {
                return true; ;
            }
        }

    }
}
