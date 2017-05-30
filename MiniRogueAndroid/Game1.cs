using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace MiniRogueAndroid
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        // Resolution Independence
        Vector2 virtualScreen = new Vector2(1280, 720);
        Vector3 ScalingFactor;
        Matrix Scale;

        // Touch
        TouchCollection currentTouchState;
        TouchCollection previousTouchState;



        Texture2D backGround;
        Texture2D titleScreen;

        bool title;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
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

            backGround = Content.Load<Texture2D>("GameScreen");
            titleScreen = Content.Load<Texture2D>("TitleScreen");

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            // TODO: Add your update logic here

            // Calculate ScalingFactor
            float widthScale = (float)GraphicsDevice.PresentationParameters.BackBufferWidth / virtualScreen.X;
            float heightScale = (float)GraphicsDevice.PresentationParameters.BackBufferHeight / virtualScreen.Y;
            ScalingFactor = new Vector3(widthScale, heightScale, 1);
            Scale = Matrix.CreateScale(ScalingFactor);

            currentTouchState = TouchPanel.GetState();

            if (TouchControl())
            {
                if (title)
                {
                    title = false;
                }
                else { title = true; }
            }



            previousTouchState = currentTouchState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Scale);

            if (title)
            {
                spriteBatch.Draw(titleScreen, new Vector2(0, 0), Color.White);
            }
            else
            {
                spriteBatch.Draw(backGround, new Vector2(0, 0), Color.White);
            }



            spriteBatch.End();

            base.Draw(gameTime);
        }

        public bool TouchControl()
        {
            foreach (var touch in currentTouchState)
            {
                if (touch.State == TouchLocationState.Pressed && touch.Position.X > 100 && touch.Position.Y > 100)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
