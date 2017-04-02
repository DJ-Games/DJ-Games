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

    enum TurnPhase
    {
        SETUPPHASE,
        PHASE1,
        PHASE2,
        PHASE3,
        PHASE4,
        BOSSPHASE,
    }

    //enum CardPosition
    //{
    //    POSITION1,
    //    POSITION2,
    //    POSITION3,
    //    POSITION4, 
    //    POSITION5, 
    //    POSITION6,
    //}




    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Gamestate gamestate;
        TurnPhase turnPhase;
        KeyboardState kbState;
        KeyboardState PrevKbState;

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
        Hand playerHand;
        Card currentCard;
        Dice playerDice;
        int currentRoll;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 576;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 960;   // set this value to the desired height of your window
            graphics.ApplyChanges();
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
            playerDice = new Dice();
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

            switch (gamestate)
            {
                case Gamestate.TITILESCREEN:

                    if (SingleKeyPress(Keys.Space)) 
                    {
                        gamestate = Gamestate.DIFFICULTY_SELECT;

                    }


                    break;

                case Gamestate.DIFFICULTY_SELECT:

                    if (SingleKeyPress(Keys.D1))
                    {
                        player = new Player(1, 5, 5, 6);
                        gamestate = Gamestate.DELVE;

                    }

                    if (SingleKeyPress(Keys.D2))
                    {
                        player = new Player(0, 5, 3, 6);
                        gamestate = Gamestate.DELVE;
                    }

                    if (SingleKeyPress(Keys.D3))
                    {
                        player = new Player(0, 4, 2, 5);
                        gamestate = Gamestate.DELVE;
                    }

                    if (SingleKeyPress(Keys.D4))
                    {
                        player = new Player(0, 3, 1, 3);
                        gamestate = Gamestate.DELVE;
                    }

                    break;

                case Gamestate.DELVE:

                    switch (turnPhase)
                    {
                        case TurnPhase.SETUPPHASE:
                            playerHand = new Hand();
                            playerHand.DrawNewHand(enemyCard, eventCard, merchantCard, restingCard, trapCard, treasureCard);
                            currentCard = playerHand.RevealCard();
                            turnPhase = TurnPhase.PHASE1;

                            break;
                            
                        case TurnPhase.PHASE1:

                            if (SingleKeyPress(Keys.Space))
                            {
                                currentRoll = playerDice.RollDice(1);
                            }



                            break;
                        case TurnPhase.PHASE2:
                            break;
                        case TurnPhase.PHASE3:
                            break;
                        case TurnPhase.PHASE4:
                            break;
                        case TurnPhase.BOSSPHASE:
                            break;
                        default:
                            break;
                    }

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

            PrevKbState = kbState;

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

                    spriteBatch.Draw(titleScreen, new Rectangle(45, 40, 494, 708), Color.White);
                    spriteBatch.DrawString(font, "Press Space to Begin", new Vector2(210, 800), Color.AntiqueWhite);


                    break;

                case Gamestate.DIFFICULTY_SELECT:

                    spriteBatch.DrawString(font, "Difficulty Select", new Vector2(220, 200), Color.White);
                    spriteBatch.DrawString(font, "1. Casual", new Vector2(220, 250), Color.White);
                    spriteBatch.DrawString(font, "2. Normal", new Vector2(220, 300), Color.White);
                    spriteBatch.DrawString(font, "3. Hard", new Vector2(220, 350), Color.White);
                    spriteBatch.DrawString(font, "4. Impossible", new Vector2(220, 400), Color.White);



                    break;

                case Gamestate.DELVE:

                    switch (turnPhase)
                    {
                        case TurnPhase.SETUPPHASE:
                            break;
                        case TurnPhase.PHASE1:

                            currentCard.DrawCard(spriteBatch, 45, 40);
                            spriteBatch.DrawString(font, "XP: " + player.Experience, new Vector2(50, 900), Color.White);
                            spriteBatch.DrawString(font, "Armor: " + player.Armor, new Vector2(150, 900), Color.White);
                            spriteBatch.DrawString(font, "HP: " + player.Health, new Vector2(250, 900), Color.White);
                            spriteBatch.DrawString(font, "Gold: " + player.Gold, new Vector2(350, 900), Color.White);

                            spriteBatch.DrawString(font, "Press space to roll die.", new Vector2(50, 800), Color.White);
                            spriteBatch.DrawString(font, currentRoll.ToString(), new Vector2(400, 800), Color.White);


                            break;
                        case TurnPhase.PHASE2:
                            break;
                        case TurnPhase.PHASE3:
                            break;
                        case TurnPhase.PHASE4:
                            break;
                        case TurnPhase.BOSSPHASE:
                            break;
                        default:
                            break;
                    }




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


        private bool SingleKeyPress(Keys key)
        {
            kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(key) && PrevKbState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }


    }
}
