using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Snakes_on_a_Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D snakeTexture;
        Random rand = new Random();
        Texture2D squareTexture;
        Rectangle currentSquare;
        
        List<Vector2> snake = new List<Vector2>();
        float timeRemaining = 0.0f;
        float timeTotal = 0.3f;
        float TimePerSquare = 2.00f;
        int direction = 2; // 0= Down, 1= right, 2= up, 3= left \\
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            snake.Add(new Vector2(400, 200));
            snake.Add(new Vector2(400, 221));
            snake.Add(new Vector2(400, 242));
            snake.Add(new Vector2(400, 263));
            snake.Add(new Vector2(400, 284));

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
            snakeTexture = Content.Load<Texture2D>(@"SQUARE");
            squareTexture = Content.Load<Texture2D>(@"SQUARE");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
             KeyboardState keyState = Keyboard.GetState();
            // Allows the game to exit
             if (keyState.IsKeyDown(Keys.Down)&& direction != 2)direction = 0;
             if (keyState.IsKeyDown(Keys.Right) && direction != 3)direction = 1;
             if (keyState.IsKeyDown(Keys.Up)&& direction != 0)direction = 2;
             if (keyState.IsKeyDown(Keys.Left) && direction != 1)direction = 3;
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (timeRemaining == 0.0f)
            {
                for(int i=snake.Count-1;i > 0;i--)
                {
                    snake[i] = snake[i - 1];
                }
                if(direction == 0)
                snake[0] = new Vector2(snake[0].X, (snake[0].Y)+21);
                if (direction == 1)
                    snake[0] = new Vector2((snake[0].X +21), (snake[0].Y));
                if (direction == 2)
                    snake[0] = new Vector2(snake[0].X, (snake[0].Y)-21);
                if (direction == 3)
                    snake[0] = new Vector2((snake[0].X -21),(snake[0].Y));

                timeRemaining = timeTotal;
            }
            timeRemaining = MathHelper.Max(0, timeRemaining -
           (float)gameTime.ElapsedGameTime.TotalSeconds);
            Window.Title = "Time " + timeRemaining; 
            base.Update(gameTime);

            if (timeRemaining == 0.0f)
            {
                currentSquare = new Rectangle(
                rand.Next(0, this.Window.ClientBounds.Width - 25),
                rand.Next(0, this.Window.ClientBounds.Height - 25),
                );
                timeRemaining = TimePerSquare;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            for (int i = 0; i < snake.Count; i++)
            {
                spriteBatch.Draw(
                   snakeTexture,
                   new Rectangle((int)snake[i].X, (int)snake[i].Y, 20, 20),
                   new Rectangle(0, 0, 16, 16),
                   Color.PapayaWhip);
            }
          
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
