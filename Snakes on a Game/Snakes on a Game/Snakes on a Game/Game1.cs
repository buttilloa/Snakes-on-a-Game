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
        Snake snake1;
        Snake snake2;
        public Rectangle Pellet;
        Random rand = new Random();
        float timeRemaining = 0.0f;
        float timeTotal = 0.2f;
        Song Om;
        SoundEffect Pew;
        SoundEffect Sneeze;
        SoundEffect Bwaaaah;
        Color[] colors = { Color.Red, Color.Green, Color.Blue ,Color.Yellow, Color.Brown, Color.Wheat, Color.Transparent, Color.Tomato, Color.Peru, Color.Azure, Color.Aquamarine, Color.Firebrick};
 
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
            snake1 = new Snake(600, 200, 22);
            snake2 = new Snake( 200, 200, 22);
            Om = Content.Load<Song>("OM");
            Pew = Content.Load<SoundEffect>("Pew");
            Sneeze = Content.Load<SoundEffect>("Sneeze");
            Bwaaaah = Content.Load<SoundEffect>("Bwaaaah");
            Bwaaaah.Play();
            MediaPlayer.Play(Om);
            snake1.effect = Pew;
            snake2.effect = Sneeze;
            NewPellet();

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) this.Exit();
            KeyboardState keyState = Keyboard.GetState();


            if (keyState.IsKeyDown(Keys.Down) && snake1.Facing != 2) snake1.Facing = 0;
            if (keyState.IsKeyDown(Keys.Right) && snake1.Facing != 3) snake1.Facing = 1;
            if (keyState.IsKeyDown(Keys.Up) && snake1.Facing != 0) snake1.Facing = 2;
            if (keyState.IsKeyDown(Keys.Left) && snake1.Facing != 1) snake1.Facing = 3;

            if (keyState.IsKeyDown(Keys.S) && snake2.Facing != 2) snake2.Facing = 0;
            if (keyState.IsKeyDown(Keys.D) && snake2.Facing != 3) snake2.Facing = 1;
            if (keyState.IsKeyDown(Keys.W) && snake2.Facing != 0) snake2.Facing = 2;
            if (keyState.IsKeyDown(Keys.A) && snake2.Facing != 1) snake2.Facing = 3;

            if (snake1.CheckCollisions(snake2.getFront(), ref snake2, this.Window)) snake1.isAlive = false;
            if (snake2.CheckCollisions(snake1.getFront(), ref snake1, this.Window)) snake2.isAlive = false;
            
            if (timeRemaining == 0.0f)
            {
                snake1.Update();
                snake2.Update();

                if (snake1.DidEatPellet(Pellet) || snake2.DidEatPellet(Pellet)) NewPellet();

                timeRemaining = timeTotal;
            }
            timeRemaining = MathHelper.Max(0, timeRemaining -
           (float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }
        public void NewPellet()
        {
            Pellet = new Rectangle(
               rand.Next(25, this.Window.ClientBounds.Width - 25),
               rand.Next(25, this.Window.ClientBounds.Height - 25),
               16, 16);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            int count = snake1.Count;
            int count2 = snake2.Count;
            spriteBatch.Begin();
            for (int i = 0; i < count; i++)
            {

                spriteBatch.Draw(
                   snakeTexture,
                   new Rectangle((int)snake1.GetInstance(i).X, (int)snake1.GetInstance(i).Y, snake1.DrawSize, snake1.DrawSize),
                   new Rectangle(0, 0, 16, 16),
                   Color.BlanchedAlmond);
            }
            for (int i = 0; i < count2; i++)
            {

                spriteBatch.Draw(
                   snakeTexture,
                   new Rectangle((int)snake2.GetInstance(i).X, (int)snake2.GetInstance(i).Y, snake2.DrawSize, snake2.DrawSize),
                   new Rectangle(0, 0, 16, 16),
                   Color.SlateGray);    
            }
            spriteBatch.Draw(
               snakeTexture,
               Pellet,
               colors[rand.Next(0,colors.Count()-1)]);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
