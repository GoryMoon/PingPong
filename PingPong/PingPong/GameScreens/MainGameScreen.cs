using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using PingPong.GameObjects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace PingPong.GameScreens
{
    public class MainGameScreen: GameScreen
    {

        public PlayerPaddle player;
        public ComputerPaddle computer;
        public Ball ball;
        public SoundEffect ping;
        public SpriteFont font;
        public int playerScore;
        public int computerScore;

        public MainGameScreen() :base("Main")
        {
            
        }

        public override void preInit()
        {
            add(new Property<PlayerPaddle>("player"));
            add(new Property<ComputerPaddle>("computer"));
            add(new Property<Ball>("ball"));
            add(new Property<SoundEffect>("ping"));
            add(new Property<SpriteFont>("font"));
            add(new Property<int>("playerScore"));
            add(new Property<int>("computerScore"));
        }

        public override void init()
        {
            player = put("player", new PlayerPaddle(this, 25f, 100f));
            computer = put("computer", new ComputerPaddle(this, WindowWidth - 25f - 38f, 100f));
            ball = put("ball", new Ball(this, 300f, 300f));
            ping = put("ping", Content.Load<SoundEffect>("PingPongSound"));
            font = put("font", Content.Load<SpriteFont>("Font"));
            playerScore = put("playerScore", 0);
            computerScore = put("computerScore", 0);

            player.LoadContent(Content);
            computer.LoadContent(Content);
            ball.LoadContent(Content);

        }

        public override void unload()
        {
            set("player", player);
            set("computer", computer);
            set("ball", ball);
            set("ping", ping);
            set("font", font);
            set("playerScore", playerScore);
            set("computerScore", computerScore);
        }

        /// <summary>
        /// Collision with the ball
        /// </summary>
        private void HandleCollisions()
        {
            HandleCollision(computer);
            HandleCollision(player);

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

                get<SoundEffect>("ping").Play();
            }
        }

        public override void update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            player.Update(gameTime, Window);
            computer.Update(gameTime, Window);
            ball.Update(gameTime, Window);
            HandleCollisions();
        }

        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw game objects
            player.Draw(gameTime, spriteBatch);
            computer.Draw(gameTime, spriteBatch);
            ball.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(font, "Player Score: " + playerScore, new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(font, "Computer Score: " + computerScore, new Vector2(Window.ClientBounds.Width - 230, 10), Color.White);
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
