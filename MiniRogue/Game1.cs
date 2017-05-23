using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System.Collections.Generic;


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
        HACKANDSLASH,
        DELVING,
        GAME_OVER,
        CREDITS,
        COMBATTESTING,
    }

    enum CurrentTurnState
    {
        PRETURN1,
        TURN1,
        PRETURN2,
        TURN2,
        PRETURN3,
        TURN3,
        PRETURN4,
        TURN4,
        PREBOSS,
        BOSS,

    }


    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Dictionary<string, Button> buttonDictionay;

        Gamestate gamestate;
        CurrentTurnState currentTurnState;
        KeyboardState kbState;
        KeyboardState prevKbState;
        MouseState mouseState;
        MouseState prevMouseState;

        // Initialize textures
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
        Texture2D difficultyScreen;
        Texture2D door;
        Texture2D doorGrayed;
        Texture2D die1;
        Texture2D die2;
        Texture2D die3;
        Texture2D die4;
        Texture2D die5;
        Texture2D die6;
        Texture2D rollDieBtnTex;
        Texture2D foundLootBtnTex;
        Texture2D foundLootBtnHLTex;
        Texture2D foundRationBtnTex;
        Texture2D foundRationBtnHLTex;
        Texture2D foundShieldBtnTex;
        Texture2D foundShieldBtnHLTex;
        Texture2D healthPotionBtnTex;
        Texture2D healthPotionBtnHLTex;
        Texture2D monsterBtnTex;
        Texture2D monsterBtnHLTex;
        Texture2D whetstoneBtnTex;
        Texture2D whetstoneBtnHLTex;
        Texture2D rationButtonTex;
        Texture2D healthPotionButtonTex;
        Texture2D bigHealthPotionButtonTex;
        Texture2D ArmorPieceButtonTex;
        Texture2D fireballSpellButtonTex;
        Texture2D iceSpellSpellButtonTex;
        Texture2D poisonSpellButtonTex;
        Texture2D healingSpellButtonTex;
        Texture2D spellsButtonTex;
        Texture2D confirmPurchaseMenu;
        Texture2D confirmSaleMenu;
        Texture2D reinforceButton;
        Texture2D healButton;
        Texture2D doneButton;
        Texture2D checkBoxEmpty;
        Texture2D checkBoxFull;
        Texture2D checkBoxGray;
        Texture2D dieBlank;
        Texture2D useFeatButton;
        Texture2D acceptButton;
        Texture2D useSpellButton;
        Texture2D spend2HPButton;
        Texture2D spend1XPButton;
        Texture2D combatButton;
        Texture2D cardBack;
        SpriteFont font;
        SpriteFont dungeonFont;
        Vector2 position;



        Player player;
        Hand playerHand;
        Dice playerDice;
        Card currentCard;
        Difficulty difficulty;

        int deviceWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int deviceHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        //----------------------- FOR TESTING COMBAT --------------------------

        Combat combat;
        Dictionary<string, Texture2D> dieTextures;
        Dictionary<string, CombatDice> combatDice;
        Dictionary<string, CheckBox> checkBoxes;

        // --------------------------------------------------------------------


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 720; // set this value to the desired height of your window

            //Sets resolution to resolution of current device. 
            //graphics.PreferredBackBufferWidth = deviceWidth;  
            //graphics.PreferredBackBufferHeight = deviceHeight;   

            //graphics.IsFullScreen = true;;

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
            // TODD: Add your initialization logic here

            base.Initialize();
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODD: use this.Content to load your game content here

            //Load Content
            bossMonsterCard = Content.Load<Texture2D>("Boss_Monster");
            characterStatsCard = Content.Load<Texture2D>("Character_Stats");
            eventCard = Content.Load<Texture2D>("Event");
            merchantCard = Content.Load<Texture2D>("Merchant");
            enemyCard = Content.Load<Texture2D>("Monster");
            restingCard = Content.Load<Texture2D>("Resting");
            trapCard = Content.Load<Texture2D>("Trap");
            treasureCard = Content.Load<Texture2D>("Treasure");
            titleScreen = Content.Load<Texture2D>("TitleScreen");
            difficultyScreen = Content.Load<Texture2D>("DifficultySelectScreen");
            door = Content.Load<Texture2D>("Door");
            doorGrayed = Content.Load<Texture2D>("DoorGrayed");
            die1 = Content.Load<Texture2D>("Die1");
            die2 = Content.Load<Texture2D>("Die2");
            die3 = Content.Load<Texture2D>("Die3");
            die4 = Content.Load<Texture2D>("Die4");
            die5 = Content.Load<Texture2D>("Die5");
            die6 = Content.Load<Texture2D>("Die6");
            dieBlank = Content.Load<Texture2D>("DieBlank");
            rollDieBtnTex = Content.Load<Texture2D>("RollDieButton");
            foundLootBtnTex = Content.Load<Texture2D>("Found_Loot_Button");
            foundLootBtnHLTex = Content.Load<Texture2D>("Found_Loot_Button_Highlight");
            foundRationBtnTex = Content.Load<Texture2D>("Found_Ration_Button");
            foundRationBtnHLTex = Content.Load<Texture2D>("Found_Ration_Button_Highlight");
            foundShieldBtnTex = Content.Load<Texture2D>("Found_Shield_Button");
            foundShieldBtnHLTex = Content.Load<Texture2D>("Found_Shield_Button_Highlight");
            healthPotionBtnTex = Content.Load<Texture2D>("Health_Potion_Button");
            healthPotionBtnHLTex = Content.Load<Texture2D>("Health_Potion_Button_Highlight");
            monsterBtnTex = Content.Load<Texture2D>("Monster_Button");
            monsterBtnHLTex = Content.Load<Texture2D>("Monster_Button_Highlight");
            whetstoneBtnTex = Content.Load<Texture2D>("Whetstone_Button");
            whetstoneBtnHLTex = Content.Load<Texture2D>("Whetstone_Button_Highlight");
            rationButtonTex = Content.Load<Texture2D>("RationButton");
            healthPotionButtonTex = Content.Load<Texture2D>("HealthPotionButton");
            bigHealthPotionButtonTex = Content.Load<Texture2D>("BigHPPotionButton");
            ArmorPieceButtonTex = Content.Load<Texture2D>("ArmorPieceButton");
            fireballSpellButtonTex = Content.Load<Texture2D>("FireSpellButton");
            iceSpellSpellButtonTex = Content.Load<Texture2D>("IceSpellButton");
            poisonSpellButtonTex = Content.Load<Texture2D>("PoisonSpellButton");
            healingSpellButtonTex = Content.Load<Texture2D>("HealingSpellButton");
            spellsButtonTex = Content.Load<Texture2D>("SpellsButton");
            confirmPurchaseMenu = Content.Load<Texture2D>("Confirm_Purchase_menu");
            confirmSaleMenu = Content.Load<Texture2D>("Confirm_Sale_Menu");
            reinforceButton = Content.Load<Texture2D>("ReinforceButton");
            healButton = Content.Load<Texture2D>("HealButton");
            useFeatButton = Content.Load<Texture2D>("UseFeatButton");
            acceptButton = Content.Load<Texture2D>("AcceptButton");
            useSpellButton = Content.Load<Texture2D>("UseSpellButton");
            spend2HPButton = Content.Load<Texture2D>("Spend2HPButton");
            spend1XPButton = Content.Load<Texture2D>("Spend1XPButton");
            doneButton = Content.Load<Texture2D>("DoneButton");
            checkBoxFull = Content.Load<Texture2D>("CheckFull");
            checkBoxEmpty = Content.Load<Texture2D>("CheckEmpty");
            checkBoxGray = Content.Load<Texture2D>("CheckGrayed");
            combatButton = Content.Load<Texture2D>("CombatButton");
            cardBack = Content.Load<Texture2D>("CardBack");
            font = Content.Load<SpriteFont>("Font");
            dungeonFont = Content.Load<SpriteFont>("Dungeon");


            position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                graphics.GraphicsDevice.Viewport.Height / 2);
            playerDice = new Dice();
            difficulty = new Difficulty();
            playerHand = new Hand();
            buttonDictionay = new Dictionary<string, Button>();
            gamestate = Gamestate.TITILESCREEN;
            currentTurnState = CurrentTurnState.PRETURN1;


            //----------------------- FOR TESTING COMBAT --------------------------


            dieTextures = new Dictionary<string, Texture2D>();

            combatDice = new Dictionary<string, CombatDice>();

            checkBoxes = new Dictionary<string, CheckBox>();

            dieTextures.Add("Roll 1", die1);
            dieTextures.Add("Roll 2", die2);
            dieTextures.Add("Roll 3", die3);
            dieTextures.Add("Roll 4", die4);
            dieTextures.Add("Roll 5", die5);
            dieTextures.Add("Roll 6", die6);
            dieTextures.Add("Blank", dieBlank);

            combatDice.Add("Combat Die 1", new CombatDice(dieTextures, 250, 450));
            combatDice.Add("Combat Die 2", new CombatDice(dieTextures, 450, 450));
            combatDice.Add("Combat Die 3", new CombatDice(dieTextures, 650, 450));
            combatDice.Add("Combat Die 4", new CombatDice(dieTextures, 850, 450));

            checkBoxes.Add("Check Box 1", new CheckBox(checkBoxFull, checkBoxEmpty, checkBoxGray, 275, 380));
            checkBoxes.Add("Check Box 2", new CheckBox(checkBoxFull, checkBoxEmpty, checkBoxGray, 475, 380));
            checkBoxes.Add("Check Box 3", new CheckBox(checkBoxFull, checkBoxEmpty, checkBoxGray, 675, 380));
            checkBoxes.Add("Check Box 4", new CheckBox(checkBoxFull, checkBoxEmpty, checkBoxGray, 875, 380));

            // --------------------------------------------------------------------

            // Fill Button Dictionary
            buttonDictionay.Add("Roll Die", new Button(rollDieBtnTex, "Roll Die"));
            buttonDictionay.Add("Found Loot", new Button(foundLootBtnTex, "Found Loot"));
            buttonDictionay.Add("Found Loot Highlight", new Button(foundLootBtnHLTex, "Found Loot Highlight"));
            buttonDictionay.Add("Found Ration", new Button(foundRationBtnTex, "Found Ration"));
            buttonDictionay.Add("Found Ration Highlight", new Button(foundRationBtnHLTex, "Found Ration Highlight"));
            buttonDictionay.Add("Found Shield", new Button(foundShieldBtnTex, "Found Shield"));
            buttonDictionay.Add("Found Shield Highlight", new Button(foundShieldBtnHLTex, "Found Shield Highlight"));
            buttonDictionay.Add("Health Potion", new Button(healthPotionBtnTex, "Health Potion"));
            buttonDictionay.Add("Health Potion Highlight", new Button(healthPotionBtnHLTex, "Health Potion Highlight"));
            buttonDictionay.Add("Whetstone", new Button(whetstoneBtnTex, "Whetstone"));
            buttonDictionay.Add("Whetstone Highlight", new Button(whetstoneBtnHLTex, "Whetstone Highlight"));
            buttonDictionay.Add("Monster", new Button(monsterBtnTex, "Monster"));
            buttonDictionay.Add("Monster Highlight", new Button(monsterBtnHLTex, "Monster Highlight"));
            buttonDictionay.Add("Green Ration Button", new Button(rationButtonTex, "Green Ration Button"));
            buttonDictionay.Add("Green Health Potion Button", new Button(healthPotionButtonTex, "Green Health Potion Button"));
            buttonDictionay.Add("Green Big Health Potion Button", new Button(bigHealthPotionButtonTex, "Green Big Health Potion Button"));
            buttonDictionay.Add("Green Armor Piece Button", new Button(ArmorPieceButtonTex, "Green Armor Piece Button"));
            buttonDictionay.Add("Green Fireball Spell Button", new Button(fireballSpellButtonTex, "Green Fireball Spell Button"));
            buttonDictionay.Add("Green Ice Spell Button", new Button(iceSpellSpellButtonTex, "Green Ice Spell Button"));
            buttonDictionay.Add("Green Poison Spell Button", new Button(poisonSpellButtonTex, "Green Poison Spell Button"));
            buttonDictionay.Add("Green Healing Spell Button", new Button(healingSpellButtonTex, "Green Health Spell Button"));
            buttonDictionay.Add("Green Spells Button", new Button(spellsButtonTex, "Green Spells Button"));
            buttonDictionay.Add("Confirm Purchase Menu", new Button(confirmPurchaseMenu, "Confirm Purchase Menu"));
            buttonDictionay.Add("Confirm Sale Menu", new Button(confirmSaleMenu, "Confirm Sale Menu"));
            buttonDictionay.Add("Reinforce Button", new Button(reinforceButton, "Reinforce Button"));
            buttonDictionay.Add("Heal Button", new Button(healButton, "Heal Button"));
            buttonDictionay.Add("Done Button", new Button(doneButton, "Done Button"));
            buttonDictionay.Add("Die Roll 1", new Button(die1, "Die Roll 1"));
            buttonDictionay.Add("Die Roll 2", new Button(die2, "Die Roll 2"));
            buttonDictionay.Add("Die Roll 3", new Button(die3, "Die Roll 3"));
            buttonDictionay.Add("Die Roll 4", new Button(die4, "Die Roll 4"));
            buttonDictionay.Add("Die Roll 5", new Button(die5, "Die Roll 5"));
            buttonDictionay.Add("Die Roll 6", new Button(die6, "Die Roll 6"));
            buttonDictionay.Add("Check Box Empty", new Button(checkBoxEmpty, "Check Box Empty"));
            buttonDictionay.Add("Check Box Full", new Button(checkBoxFull, "Check Box Full"));
            buttonDictionay.Add("Use Feat Button", new Button(useFeatButton, "Use Feat Button"));
            buttonDictionay.Add("Accept Button", new Button(acceptButton, "Accept Button"));
            buttonDictionay.Add("Use Spell Button", new Button(useSpellButton, "Use Spell Button"));
            buttonDictionay.Add("Spend 2 HP Button", new Button(spend2HPButton, "Spend 2 HP Button"));
            buttonDictionay.Add("Spend 1 XP Button", new Button(spend1XPButton, "Spend 1 XP Button"));
            buttonDictionay.Add("Combat Button", new Button(combatButton, "Combat Button"));


        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODD: Unload any non ContentManager content here
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

            // TODD: Add your update logic here

            mouseState = Mouse.GetState();
            position.X = mouseState.X;
            position.Y = mouseState.Y;

            if (gamestate == Gamestate.HACKANDSLASH)
            {
                if (player.Health <= 0)
                {
                    gamestate = Gamestate.GAME_OVER;
                }
            }



            //------------------- Switch for gamestates ------------------------
            switch (gamestate)
            {
                case Gamestate.TITILESCREEN:

                    if (SingleMouseClick())
                    {
                        if (position.X > 800 && position.X < 1050 && position.Y > 372 && position.Y < 446)
                        {
                            gamestate = Gamestate.DIFFICULTY_SELECT;
                        }
                    }

                    //----------------------- FOR TESTING COMBAT --------------------------

                    if (SingleMouseClick())
                    {
                        if (position.X > 0 && position.X < 100 && position.Y > 0 && position.Y < 100)
                        {
                            combat = new Combat(buttonDictionay, combatDice, checkBoxes);
                            player = new Player(0, 10, 10, 10);
                            player.Rank = 2;
                            gamestate = Gamestate.COMBATTESTING;
                        }
                    }

                    // --------------------------------------------------------------------

                    break;

                // -------------- Difficulty Select Screen Game State Update -------------------
                // 
                // Player is created, hand is created, new hand is draw, and player stats
                // set based on difficulty selection. 


                //_________________________________________________________________
                //  MAYBE MAKE A DICTIONARY OF TEXTURES TO SEND IN INSTEAD
                //  OF INDIVIDUAL TEXTURES???
                //_________________________________________________________________

                case Gamestate.DIFFICULTY_SELECT:

                    //----- Begin Mouse controls -----

                    if (SingleMouseClick())
                    {
                        if (position.X > 800 && position.X < 1050 && position.Y > 167 && position.Y < 241)
                        {
                            player = new Player(1, 5, 5, 6);
                            DrawNewHand();
                            gamestate = Gamestate.HACKANDSLASH;
                        }

                        if (position.X > 800 && position.X < 1050 && position.Y > 270 && position.Y < 344)
                        {
                            player = new Player(0, 5, 3, 6);
                            DrawNewHand();
                            gamestate = Gamestate.HACKANDSLASH;
                        }

                        if (position.X > 800 && position.X < 1050 && position.Y > 373 && position.Y < 447)
                        {
                            player = new Player(0, 4, 2, 5);
                            DrawNewHand();
                            gamestate = Gamestate.HACKANDSLASH;
                        }

                        if (position.X > 800 && position.X < 1050 && position.Y > 476 && position.Y < 550)
                        {
                            player = new Player(0, 3, 1, 3);
                            DrawNewHand();
                            gamestate = Gamestate.HACKANDSLASH;
                        }
                    }

                    break;

                //----- End Mouse Controls -----

                // -------------- Delve Game State Update -------------------

                case Gamestate.HACKANDSLASH:

                    switch (currentTurnState)
                    {
                        case CurrentTurnState.PRETURN1:

                            FlipCard(playerHand.Card1);

                            //-----------------

                            if (SingleMouseClick())
                            {
                                if (position.X > 75 && position.X < 270 && position.Y > 260 && position.Y < 535)
                                {
                                    currentCard = playerHand.Card1;
                                    currentTurnState = CurrentTurnState.TURN1;
                                }
                            }

                            break;

                        case CurrentTurnState.TURN1:
                            if(currentCard.HandleCard(player, mouseState, prevMouseState, position.X, position.Y))
                            {
                                currentTurnState = CurrentTurnState.PRETURN2;
                            }

                            break;

                        case CurrentTurnState.PRETURN2:

                            FlipCard(playerHand.Card2);
                            FlipCard(playerHand.Card3);


                            if (SingleMouseClick())
                            {
                                if (position.X > 300 && position.X < 495 && position.Y > 100 && position.Y < 375)
                                {
                                    currentCard = playerHand.Card2;
                                    currentTurnState = CurrentTurnState.TURN2;

                                }

                                if (position.X > 300 && position.X < 495 && position.Y > 400 && position.Y < 575)
                                {
                                    currentCard = playerHand.Card3;
                                    currentTurnState = CurrentTurnState.TURN2;
                                }
                            }



                            break;          

                        case CurrentTurnState.TURN2:

                            if (currentCard.HandleCard(player, mouseState, prevMouseState, position.X, position.Y))
                            {
                                currentTurnState = CurrentTurnState.PRETURN3;
                            }

                            break;

                        case CurrentTurnState.PRETURN3:

                            FlipCard(playerHand.Card4);

                            if (SingleMouseClick())
                            {
                                if (position.X > 525 && position.X < 720 && position.Y > 260 && position.Y < 535)
                                {
                                    currentCard = playerHand.Card4;
                                    currentTurnState = CurrentTurnState.TURN3;
                                }
                            }

                            break;

                        case CurrentTurnState.TURN3:

                            if (currentCard.HandleCard(player, mouseState, prevMouseState, position.X, position.Y))
                            {
                                currentTurnState = CurrentTurnState.PRETURN4;
                            }

                            break;

                        case CurrentTurnState.PRETURN4:

                            FlipCard(playerHand.Card5);
                            FlipCard(playerHand.Card6);

                            if (SingleMouseClick())
                            {
                                if (position.X > 750 && position.X < 945 && position.Y > 100 && position.Y < 375)
                                {
                                    currentCard = playerHand.Card5;
                                    currentTurnState = CurrentTurnState.TURN4;

                                }


                                if (position.X > 750 && position.X < 945 && position.Y > 400 && position.Y < 575)
                                {
                                    currentCard = playerHand.Card6;
                                    currentTurnState = CurrentTurnState.TURN4;
                                }
                            }


                            break;


                        case CurrentTurnState.TURN4:

                            if (currentCard.HandleCard(player, mouseState, prevMouseState, position.X, position.Y))
                            {
                                if (player.DungeonArea == 2 || player.DungeonArea == 4 || player.DungeonArea == 7 || player.DungeonArea == 10 || player.DungeonArea == 14)
                                {
                                    currentTurnState = CurrentTurnState.PREBOSS;
                                }
                                else
                                {
                                    gamestate = Gamestate.DELVING;
                                }
                                
                            }

                            break;

                        case CurrentTurnState.PREBOSS:

                            FlipCard(playerHand.Card7);

                            if (SingleMouseClick())
                            {
                                if (position.X > 925 && position.X < 1120 && position.Y > 260 && position.Y < 535)
                                {
                                    currentCard = playerHand.Card7;
                                    currentTurnState = CurrentTurnState.BOSS;
                                }
                            }

                            break;


                        case CurrentTurnState.BOSS:

                            if (currentCard.HandleCard(player, mouseState, prevMouseState, position.X, position.Y))
                            {
                                gamestate = Gamestate.DELVING;
                            }

                            break;


                        default:
                            break;
                    
            }

                    break;

                case Gamestate.DELVING:

                    if (SingleMouseClick())
                    {
                        if (position.X > 600 && position.X < 848 && position.Y > 500 && position.Y < 572)
                        {
                            player.DungeonArea++;

                            if (player.DungeonArea == 3)
                            {
                                player.DungeonLevel = 2;
                            }
                            if (player.DungeonArea == 5)
                            {
                                player.DungeonLevel = 3;
                            }
                            if (player.DungeonArea == 8)
                            {
                                player.DungeonLevel = 4;
                            }
                            if (player.DungeonArea == 11)
                            {
                                player.DungeonLevel = 5;
                            }
                            player.HasFoughtMonster = false;
                            playerHand = new Hand();
                            DrawNewHand();
                            //playerHand.ShuffleHand();
                            playerHand.Card1.Flipped = false;
                            playerHand.Card2.Flipped = false;
                            playerHand.Card3.Flipped = false;
                            playerHand.Card4.Flipped = false;
                            playerHand.Card5.Flipped = false;
                            playerHand.Card6.Flipped = false;
                            //playerHand.Card7.Flipped = false;
                            gamestate = Gamestate.HACKANDSLASH;
                            currentTurnState = CurrentTurnState.PRETURN1;
                        }

                    }

                            break;

                case Gamestate.GAME_OVER:

                    break;

                case Gamestate.CREDITS:

                    break;

                //----------------------- FOR TESTING COMBAT --------------------------

                case Gamestate.COMBATTESTING:

                    if (combat.HandleCombat(player, mouseState, prevMouseState, position.X, position.Y, true))
                    {
                        gamestate = Gamestate.TITILESCREEN;
                    }
                    


                    break;

                // --------------------------------------------------------------------

                default:

                    break;
            }

            prevKbState = kbState;
            prevMouseState = mouseState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODD: Add your drawing code here

            spriteBatch.Begin();


            switch (gamestate)
            {
                case Gamestate.TITILESCREEN:


                    spriteBatch.Draw(titleScreen, new Vector2(0, 0), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);                 


                    break;


                // -------------- Difficulty Select Screen Game State Draw -----------------

                case Gamestate.DIFFICULTY_SELECT:

                    spriteBatch.Draw(difficultyScreen, new Vector2(0, 0), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);


                    break;

                // -------------- Delve Game State Draw -------------------

                case Gamestate.HACKANDSLASH:

                    spriteBatch.DrawString(font, "Health: " + player.Health, new Vector2(20, 20), Color.White);
                    spriteBatch.DrawString(font, "Armor: " + player.Armor, new Vector2(120, 20), Color.White);
                    spriteBatch.DrawString(font, "Gold: " + player.Gold, new Vector2(220, 20), Color.White);
                    spriteBatch.DrawString(font, "Food: " + player.Food, new Vector2(320, 20), Color.White);
                    spriteBatch.DrawString(font, "XP: " + player.Experience, new Vector2(420, 20), Color.White);
                    spriteBatch.DrawString(font, "Rank: " + player.Rank, new Vector2(520, 20), Color.White);
                    spriteBatch.DrawString(font, "Spell1: ", new Vector2(620, 20), Color.White);
                    spriteBatch.DrawString(font, "Spell2: ", new Vector2(820, 20), Color.White);
                    spriteBatch.DrawString(font, "Dungeon Level: " + player.DungeonLevel, new Vector2(1120, 20), Color.White);
                    spriteBatch.DrawString(font, "Dungeon Area: " + player.DungeonArea, new Vector2(1120, 40), Color.White);

                    if (player.SpellsString.Count == 1)
                    {
                        if (player.SpellsString[0]== "Fire Spell")
                        {
                            spriteBatch.Draw(fireballSpellButtonTex, new Vector2(700, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.SpellsString[0] == "Ice Spell")
                        {
                            spriteBatch.Draw(iceSpellSpellButtonTex, new Vector2(700, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.SpellsString[0] == "Poison Spell")
                        {
                            spriteBatch.Draw(poisonSpellButtonTex, new Vector2(700, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.SpellsString[0] == "Healing Spell")
                        {
                            spriteBatch.Draw(healingSpellButtonTex, new Vector2(700, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                   }

                    if (player.SpellsString.Count == 2)
                    {
                        if (player.SpellsString[0] == "Fire Spell")
                        {
                            spriteBatch.Draw(fireballSpellButtonTex, new Vector2(700, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.SpellsString[0] == "Ice Spell")
                        {
                            spriteBatch.Draw(iceSpellSpellButtonTex, new Vector2(700, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.SpellsString[0] == "Poison Spell")
                        {
                            spriteBatch.Draw(poisonSpellButtonTex, new Vector2(700, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.SpellsString[0] == "Healing Spell")
                        {
                            spriteBatch.Draw(healingSpellButtonTex, new Vector2(700, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.SpellsString[1] == "Fire Spell")
                        {
                            spriteBatch.Draw(fireballSpellButtonTex, new Vector2(900, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.SpellsString[1] == "Ice Spell")
                        {
                            spriteBatch.Draw(iceSpellSpellButtonTex, new Vector2(900, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.SpellsString[1] == "Poison Spell")
                        {
                            spriteBatch.Draw(poisonSpellButtonTex, new Vector2(900, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.SpellsString[1] == "Healing Spell")
                        {
                            spriteBatch.Draw(healingSpellButtonTex, new Vector2(900, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }
                    }


                    switch (currentTurnState)
                    {
                        case CurrentTurnState.PRETURN1:

                            playerHand.DrawHand(spriteBatch);

                            break;
                        case CurrentTurnState.TURN1:
                            currentCard.DrawCard(spriteBatch, dungeonFont);
                            break;

                        case CurrentTurnState.PRETURN2:

                            playerHand.DrawHand(spriteBatch);

                            break;

                        case CurrentTurnState.TURN2:
                            currentCard.DrawCard(spriteBatch, dungeonFont);
                            break;

                        case CurrentTurnState.PRETURN3:


                            playerHand.DrawHand(spriteBatch);

                            break;

                        case CurrentTurnState.TURN3:


                            currentCard.DrawCard(spriteBatch, dungeonFont);

                            break;

                        case CurrentTurnState.PRETURN4:

                            playerHand.DrawHand(spriteBatch);

                            break;

                        case CurrentTurnState.TURN4:


                            currentCard.DrawCard(spriteBatch, dungeonFont);

                            break;

                        case CurrentTurnState.PREBOSS:

                            playerHand.DrawHand(spriteBatch);

                            break;

                        case CurrentTurnState.BOSS:

                            currentCard.DrawCard(spriteBatch, dungeonFont);

                            break;

                        default:
                            break;
                    }

                    break;


                case Gamestate.DELVING:

                    spriteBatch.DrawString(font, "You have completed area " + player.DungeonArea + "!", new Vector2(660, 200), Color.White);
                    spriteBatch.Draw(acceptButton, new Vector2(600, 500), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;

                case Gamestate.GAME_OVER:

                    spriteBatch.DrawString(font, "You died. Game Over. ", new Vector2(660, 200), Color.White);

                    break;

                case Gamestate.CREDITS:

                    break;


                //----------------------- FOR TESTING COMBAT --------------------------

                case Gamestate.COMBATTESTING:

                    combat.DrawCombat(spriteBatch, dungeonFont);
                    spriteBatch.DrawString(font, "Health: " + player.Health, new Vector2(20, 20), Color.White);
                    spriteBatch.DrawString(font, "Armor: " + player.Armor, new Vector2(120, 20), Color.White);
                    spriteBatch.DrawString(font, "Gold: " + player.Gold, new Vector2(220, 20), Color.White);
                    spriteBatch.DrawString(font, "Food: " + player.Food, new Vector2(320, 20), Color.White);
                    spriteBatch.DrawString(font, "XP: " + player.Experience, new Vector2(420, 20), Color.White);
                    spriteBatch.DrawString(font, "Rank: " + player.Rank, new Vector2(520, 20), Color.White);
                    spriteBatch.DrawString(font, "Spell1: ", new Vector2(620, 20), Color.White);
                    spriteBatch.DrawString(font, "Spell2: ", new Vector2(820, 20), Color.White);
                    spriteBatch.DrawString(font, "Dungeon Level: " + player.DungeonLevel, new Vector2(1120, 20), Color.White);
                    spriteBatch.DrawString(font, "Dungeon Area: " + player.DungeonArea, new Vector2(1120, 40), Color.White);


                    break;

                // --------------------------------------------------------------------

                default:

                    break;
            }


            spriteBatch.End();


            base.Draw(gameTime);
        }


        private bool SingleKeyPress(Keys key)
        {
            kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(key) && prevKbState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }

        private bool SingleMouseClick()
        {
            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }

        public void DrawNewHand()
        {

            playerHand.DrawNewHand(enemyCard, eventCard, merchantCard, restingCard, trapCard, treasureCard, bossMonsterCard, cardBack, buttonDictionay, combatDice, checkBoxes);


        } 

        public void PreparePhase()
        {
            playerHand.ShuffleHand();
            currentTurnState = CurrentTurnState.PRETURN1;
            
        }

        private void FlipCard(Card card)
        {
            if (!card.Flipped)
            {
                card.ScaleVector = new Vector2(card.ScaleVector.X - .02f, card.ScaleVector.Y);
            }
            if (card.ScaleVector.X < 0)
            {
                card.Flipped = true;
            }
            if (card.Flipped && card.ScaleVector.X < .43f)
            {
                card.ScaleVector = new Vector2(card.ScaleVector.X + .02f, card.ScaleVector.Y);
            }

        }



    }
}
