using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;


namespace MiniRogueAndroid
{

    // Game and turn state enumerators    

    enum Gamestate
    {
        TITILESCREEN,
        DIFFICULTY_SELECT,
        HACKANDSLASH,
        DELVING,
        GAME_OVER,
        CREDITS,
        COMBATTESTING,
    }

    enum TurnState
    {
        PRETURN1,
        ANIMATECARD1,
        TURN1,
        PRETURN2,
        ANIMATECARD2,
        TURN2,
        PRETURN3,
        ANIMATECARD3,
        TURN3,
        PRETURN4,
        ANIMATECARD4,
        TURN4,
        PREBOSS,
        ANIMATEBOSSCARD,
        BOSS,

    }


    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Resolution Independence
        Vector2 virtualScreen = new Vector2(1280, 720);
        Vector3 ScalingFactor;
        Matrix Scale;

        // Touch
        TouchCollection touchState;

        //States
        Gamestate gameState;
        TurnState turnState;

        // Title Screen Animation
        bool doorsOpen;
        bool eyesOnScreen;
        int counter;
        float eyeOpacity = 0.0f;
        Vector2 rightdoor = new Vector2(261, 141);
        Vector2 leftDoor = new Vector2(99, 140);
        Vector2 guy = new Vector2(-450, 250);

        // Initialize Textures
        Texture2D titleBlank;
        Texture2D titleRightDoor;
        Texture2D titleLeftDoor;
        Texture2D titleEyes;
        Texture2D titleGuy;
        Texture2D titleBlack;



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

            // Title Screen Animation
            titleBlank = Content.Load<Texture2D>("Title_Screen");
            titleRightDoor = Content.Load<Texture2D>("Title_Screen_Right_Door");
            titleLeftDoor = Content.Load<Texture2D>("Title_Screen_Left_Door");
            titleEyes = Content.Load<Texture2D>("Title_Screen_Eyes");
            titleGuy = Content.Load<Texture2D>("Title_Screen_Guy");
            titleBlack = Content.Load<Texture2D>("Title_Screen_Background");




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

            // Calculate ScalingFactor
            float widthScale = (float)GraphicsDevice.PresentationParameters.BackBufferWidth / virtualScreen.X;
            float heightScale = (float)GraphicsDevice.PresentationParameters.BackBufferHeight / virtualScreen.Y;
            ScalingFactor = new Vector3(widthScale, heightScale, 1);
            Scale = Matrix.CreateScale(ScalingFactor);

            // Touch update and transfrom
            touchState = TouchPanel.GetState();
            
                      

            switch (gameState)
            {
                case Gamestate.TITILESCREEN:

                    TitleScreenAnim();

                    if (TouchControl(800, 1050, 372, 446))
                    {
                        gameState = Gamestate.DIFFICULTY_SELECT;
                    }

                    break;
                case Gamestate.DIFFICULTY_SELECT:
                    break;
                case Gamestate.HACKANDSLASH:
                    break;
                case Gamestate.DELVING:
                    break;
                case Gamestate.GAME_OVER:
                    break;
                case Gamestate.CREDITS:
                    break;
                case Gamestate.COMBATTESTING:
                    break;
                default:
                    break;
            }






            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Scale);

            switch (gameState)
            {
                case Gamestate.TITILESCREEN:

                    spriteBatch.Draw(titleBlack, new Vector2(0, 0), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    spriteBatch.Draw(titleRightDoor, rightdoor, new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    spriteBatch.Draw(titleLeftDoor, leftDoor, new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    spriteBatch.Draw(titleBlank, new Vector2(0, 0), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    spriteBatch.Draw(titleEyes, new Vector2(165, 110), new Rectangle?(), Color.White * eyeOpacity, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    spriteBatch.Draw(titleGuy, guy, new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;
                case Gamestate.DIFFICULTY_SELECT:
                    break;
                case Gamestate.HACKANDSLASH:
                    break;
                case Gamestate.DELVING:
                    break;
                case Gamestate.GAME_OVER:
                    break;
                case Gamestate.CREDITS:
                    break;
                case Gamestate.COMBATTESTING:
                    break;
                default:
                    break;
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }

        public bool TouchControl(int xPosMin, int xPosMax, int yPosMin, int yPosMax)
        {
            foreach (var touch in touchState)
            {
                if (touch.State == TouchLocationState.Pressed && (touch.Position.X / ScalingFactor.X)
                    < xPosMax && (touch.Position.X / ScalingFactor.X) > xPosMin && (touch.Position.Y / ScalingFactor.Y)
                    < yPosMax && (touch.Position.Y / ScalingFactor.Y) > yPosMin)
                {
                    return true;
                }
            }
            return false;
        }

        public void TitleScreenAnim()
        {
            counter++;
            if (counter > 60 && !doorsOpen && !(leftDoor.X < -60))
            {
                leftDoor.X -= 3;
                rightdoor.X += 3;
            }
            if (leftDoor.X < -60)
            {
                doorsOpen = true;
            }
            if (doorsOpen && !eyesOnScreen)
            {
                eyeOpacity += .03f;
            }
            if (eyeOpacity > 1)
            {
                eyesOnScreen = true;
            }
            if (eyesOnScreen && guy.X < -20)
            {
                guy.X += 10;
            }
        }

    }
}
