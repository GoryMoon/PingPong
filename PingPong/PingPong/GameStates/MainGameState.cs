using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.GameObjects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace PingPong.GameStates
{
    public class MainGameState: GameState
    {

        public MainGameState()
            : base("Main")
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
            set("player", new PlayerPaddle(this, 100f, 100f));
            set("computer", new ComputerPaddle(this, 700f, 100f));
            set("ball", new Ball(this, 300f, 300f));
            set("ping", Content.Load<SoundEffect>("PingPongSound"));
            set("font", Content.Load<SpriteFont>("Font"));

            get<PlayerPaddle>("player").LoadContent(Content);
            get<ComputerPaddle>("computer").LoadContent(Content);
            get<Ball>("ball").LoadContent(Content);
        }

        public override void unload()
        {

        }

        /// <summary>
        /// Collision with the ball
        /// </summary>
        private void HandleCollisions()
        {
            HandleCollision(get<ComputerPaddle>("computer"));
            HandleCollision(get<PlayerPaddle>("player"));
        }

        /// <summary>
        /// Helper function for collision handling
        /// </summary>
        private void HandleCollision(Paddle paddle)
        {
            Ball ball = get<Ball>("ball");
            Rectangle r = Rectangle.Intersect(paddle.BoundingBox, ball.BoundingBox);

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

                set("ball", ball);
                get<SoundEffect>("ping").Play();
            }
        }

        public override void update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            get<PlayerPaddle>("player").Update(gameTime, Window);
            get<ComputerPaddle>("computer").Update(gameTime, Window);
            get<Ball>("ball").Update(gameTime, Window);
            HandleCollisions();
        }

        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw game objects
            get<PlayerPaddle>("player").Draw(gameTime, spriteBatch);
            get<ComputerPaddle>("computer").Draw(gameTime, spriteBatch);
            get<Ball>("ball").Draw(gameTime, spriteBatch);

            SpriteFont font = get<SpriteFont>("font");

            spriteBatch.DrawString(font, "Player Score: " + get<int>("playerScore"), new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(font, "Computer Score: " + get<int>("computerScore"), new Vector2(Window.ClientBounds.Width - 230, 10), Color.Black);
        }

    }
}
