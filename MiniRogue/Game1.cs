using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MiniRogue
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 

    enum Gamestate
    {
        TITILESCREEN,
        DIFFICULTY_SELECT,
        DELVE,
        BOSS,
        RECOUP,
        GAME_OVER,
        CREDITS,
    }

    enum CardPosition
    {
        POSITION1,
        POSITION2,
        POSITION3,
        POSITION4, 
        POSITION5, 
        POSITION6,
    }




    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Gamestate gamestate;
        KeyboardState kbState;

        Texture2D bossMonsterCard;
        Texture2D characterStatsCard;
        Texture2D eventCard;
        Texture2D merchantCard;
        Texture2D enemyCard;
        Texture2D restingCard;
        Texture2D theDungeonCard;
        Texture2D trapCard;
        Texture2D treasureCard;
        Texture2D titleScreen;
        SpriteFont font;

        Player player;
        Card currentCard;


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


            bossMonsterCard = Content.Load<Texture2D>("Boss_Monster");
            characterStatsCard = Content.Load<Texture2D>("Character_Stats");
            eventCard = Content.Load<Texture2D>("Event");
            merchantCard = Content.Load<Texture2D>("Merchant");
            enemyCard = Content.Load<Texture2D>("Monster");
            restingCard = Content.Load<Texture2D>("The_Dungeon");
            trapCard = Content.Load<Texture2D>("Trap");
            treasureCard = Content.Load<Texture2D>("Treasure");
            titleScreen = Content.Load<Texture2D>("Title");
            font = Content.Load<SpriteFont>("Font");
            gamestate = Gamestate.TITILESCREEN;
            

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

            kbState = Keyboard.GetState();

            switch (gamestate)
            {
                case Gamestate.TITILESCREEN:

                    if (kbState.IsKeyDown(Keys.Space)) 
                    {
                        gamestate = Gamestate.DIFFICULTY_SELECT;

                    }


                    break;

                case Gamestate.DIFFICULTY_SELECT:

                    if (kbState.IsKeyDown(Keys.D1))
                    {
                        player = new Player(1, 5, 5, 6);
                        gamestate = Gamestate.DELVE;
                    }

                    if (kbState.IsKeyDown(Keys.D2))
                    {
                        player = new Player(0, 5, 3, 6);
                        gamestate = Gamestate.DELVE;
                    }

                    if (kbState.IsKeyDown(Keys.D3))
                    {
                        player = new Player(0, 4, 2, 5);
                        gamestate = Gamestate.DELVE;
                    }

                    if (kbState.IsKeyDown(Keys.D4))
                    {
                        player = new Player(0, 3, 1, 3);
                        gamestate = Gamestate.DELVE;
                    }

                    break;

                case Gamestate.DELVE:

                    Hand playerHand = new Hand();
                    playerHand.DrawNewHand(enemyCard, eventCard, merchantCard, restingCard, trapCard, treasureCard);

                    currentCard = playerHand.RevealCard();





                    break;

                case Gamestate.BOSS:

                    break;

                case Gamestate.RECOUP:

                    break;

                case Gamestate.GAME_OVER:

                    break;

                case Gamestate.CREDITS:

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
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();


            switch (gamestate)
            {
                case Gamestate.TITILESCREEN:

                    spriteBatch.Draw(titleScreen, new Rectangle(300, 40, 250, 400), Color.White);
                    spriteBatch.DrawString(font, "Press Space to Begin", new Vector2(350, 300), Color.AntiqueWhite);


                    break;

                case Gamestate.DIFFICULTY_SELECT:

                    spriteBatch.DrawString(font, "Difficulty Select", new Vector2(300, 50), Color.White);
                    spriteBatch.DrawString(font, "1. Casual", new Vector2(300, 100), Color.White);
                    spriteBatch.DrawString(font, "2. Normal", new Vector2(300, 150), Color.White);
                    spriteBatch.DrawString(font, "3. Hard", new Vector2(300, 200), Color.White);
                    spriteBatch.DrawString(font, "4. Impossible", new Vector2(300, 250), Color.White);



                    break;

                case Gamestate.DELVE:

                    currentCard.DrawCard(spriteBatch, 200, 200);




                    break;

                case Gamestate.BOSS:

                    break;

                case Gamestate.RECOUP:

                    break;

                case Gamestate.GAME_OVER:

                    break;

                case Gamestate.CREDITS:

                    break;

                default:

                    break;
            }


            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
