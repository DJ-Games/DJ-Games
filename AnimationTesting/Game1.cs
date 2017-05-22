using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace AnimationTesting
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D front;
        Texture2D back;
        Vector2 scale;

        double sineValue = 0;
        bool frontFacing = false;



        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;  
            graphics.PreferredBackBufferHeight = 720;

            scale = new Vector2(1,1);



        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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

            // TODO: use this.Content to load your game content here

            front = Content.Load<Texture2D>("Merchant");
            back = Content.Load<Texture2D>("Door");            

            

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            //scale.X -= (float)Math.Sin(sineValue);
            //sineValue += .00f;

            if (frontFacing)
            {
                scale.X += .01f;
            }
            if (!frontFacing)
            {
                scale.X -= .01f;
            }
            if (scale.X > 1)
            {
                frontFacing = false;
            }
            if (scale.X < 0)
            {
                frontFacing = true;
            }






            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            if (frontFacing)
            {
                spriteBatch.Draw(back, new Vector2(10, 10), new Rectangle?(), Color.White, 0f, new Vector2(), scale, SpriteEffects.None, 1);
            }
            if (!frontFacing)
            {
                spriteBatch.Draw(front, new Vector2(10, 10), new Rectangle?(), Color.White, 0f, new Vector2(), scale, SpriteEffects.None, 1);
            }


            spriteBatch.End();




            base.Draw(gameTime);
        }
    }
}
