using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        WINSCREEN,
        COMBATTESTING,
    }

    enum CurrentTurnState
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

        // Title screen animation 
        bool doorsOpen;
        bool eyesOnScreen;
        int counter;
        float eyeOpacity = 0.0f;
        Vector2 rightdoor = new Vector2(0,0);
        Vector2 leftDoor = new Vector2(0,0);
        Vector2 guy = new Vector2(-450,0);
        //-----------------------

        Dictionary<string, Button> buttonDictionay;

        Gamestate gamestate;
        CurrentTurnState currentTurnState;
        MouseState mouseState;
        MouseState prevMouseState;

        // Initialize textures
        Texture2D bossMonsterCard;
        Texture2D characterStatsCard;
        Texture2D eventCard;
        Texture2D merchantCard;
        Texture2D enemyCard;
        Texture2D restingCard;
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
        Texture2D titleBlank;
        Texture2D titleRightDoor;
        Texture2D titleLeftDoor;
        Texture2D titleEyes;
        Texture2D titleGuy;
        Texture2D titleBlack;
        Texture2D gameScreen;
        Texture2D fireSpellIcon;
        Texture2D healthSpellIcon;
        Texture2D iceSpellIcon;
        Texture2D poisonSpellIcon;
        Texture2D critRollButton;
        Texture2D blackSewers;
        Texture2D PoisonousDungeon;
        Texture2D undeadCatacombs;
        Texture2D flamingUnderworld;
        Texture2D sunkenKeepOfOg;
        Texture2D mapHighlight;
        Texture2D rewards;
        Texture2D gameBackground;
        Texture2D characterStats;
        Texture2D dungeon;
        Texture2D dieHighlight;
        Texture2D winScreenGuy;
        Texture2D eyes;
        SpriteFont font;
        SpriteFont highTower;
        SpriteFont dungeonFont;
        Vector2 position;

        Song song;

        Player player;
        Hand playerHand;
        BasicDie playerDice;
        Card currentCard;
        int currentCardNumber;
        float colorMultiplyer = 1;

        int deviceWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int deviceHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        //----------------------- FOR TESTING COMBAT --------------------------

        Combat combat;
        Dictionary<string, Texture2D> dieTextures;
        Dictionary<string, Die> combatDice;
        Dictionary<string, CheckBox> checkBoxes;
        Dictionary<string, Texture2D> spellIcons;

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
            titleBlank = Content.Load<Texture2D>("TitleScreenBase");
            titleRightDoor = Content.Load<Texture2D>("TitleScreenRightDoor");
            titleLeftDoor = Content.Load<Texture2D>("TitleScreenLeftDoor");
            titleEyes = Content.Load<Texture2D>("TitleScreenEyes");
            titleGuy = Content.Load<Texture2D>("TitleScreenGuy");
            titleBlack = Content.Load<Texture2D>("TitleScreenBlack");
            gameScreen = Content.Load<Texture2D>("GameScreen2");
            fireSpellIcon = Content.Load<Texture2D>("FireSpellIcon");
            healthSpellIcon = Content.Load<Texture2D>("HealthSpellIcon");
            iceSpellIcon = Content.Load<Texture2D>("IceSpellIcon");
            poisonSpellIcon = Content.Load<Texture2D>("PoisonSpellIcon");
            critRollButton = Content.Load<Texture2D>("CritRollButton");
            blackSewers = Content.Load<Texture2D>("BlackSewers");
            PoisonousDungeon = Content.Load<Texture2D>("PoisonousDungeon");
            undeadCatacombs = Content.Load<Texture2D>("UndeadCatacombs");
            flamingUnderworld = Content.Load<Texture2D>("FlamingUnderworld");
            sunkenKeepOfOg = Content.Load<Texture2D>("SunkenKeepOfOg");
            mapHighlight = Content.Load<Texture2D>("MapHighlight");
            rewards = Content.Load<Texture2D>("Rewards");
            gameBackground = Content.Load<Texture2D>("GameBackground");
            characterStats = Content.Load<Texture2D>("CharacterStats");
            dungeon = Content.Load<Texture2D>("Dungeon");
            dieHighlight = Content.Load<Texture2D>("Die Highlight");
            eyes = Content.Load<Texture2D>("Eyes");
            winScreenGuy = Content.Load<Texture2D>("WinScreenGuy");
            font = Content.Load<SpriteFont>("Font");
            highTower = Content.Load<SpriteFont>("HighTower");
            dungeonFont = Content.Load<SpriteFont>("MorrisRoman");
            song = Content.Load <Song>("Music");
            MediaPlayer.Volume -= 0.7f;
            //MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                graphics.GraphicsDevice.Viewport.Height / 2);
            playerDice = new BasicDie();
            playerHand = new Hand();
            buttonDictionay = new Dictionary<string, Button>();
            gamestate = Gamestate.TITILESCREEN;
            currentTurnState = CurrentTurnState.PRETURN1;



            dieTextures = new Dictionary<string, Texture2D>();

            combatDice = new Dictionary<string, Die>();

            checkBoxes = new Dictionary<string, CheckBox>();

            spellIcons = new Dictionary<string, Texture2D>();

            dieTextures.Add("Roll 1", die1);
            dieTextures.Add("Roll 2", die2);
            dieTextures.Add("Roll 3", die3);
            dieTextures.Add("Roll 4", die4);
            dieTextures.Add("Roll 5", die5);
            dieTextures.Add("Roll 6", die6);
            dieTextures.Add("Blank", dieBlank);

            combatDice.Add("Combat Die 1", new Die(dieTextures, 550, 300));
            combatDice.Add("Combat Die 2", new Die(dieTextures, 735, 300));
            combatDice.Add("Combat Die 3", new Die(dieTextures, 920, 300));
            combatDice.Add("Combat Die 4", new Die(dieTextures, 1105, 300));

            checkBoxes.Add("Check Box 1", new CheckBox(checkBoxFull, checkBoxEmpty, checkBoxGray, 275, 380));
            checkBoxes.Add("Check Box 2", new CheckBox(checkBoxFull, checkBoxEmpty, checkBoxGray, 475, 380));
            checkBoxes.Add("Check Box 3", new CheckBox(checkBoxFull, checkBoxEmpty, checkBoxGray, 675, 380));
            checkBoxes.Add("Check Box 4", new CheckBox(checkBoxFull, checkBoxEmpty, checkBoxGray, 875, 380));

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
            buttonDictionay.Add("Combat Button", new Button(combatButton, "Combat Button"));
            buttonDictionay.Add("Spend 2 HP Button", new Button(spend2HPButton, "Spend 2 HP Button"));
            buttonDictionay.Add("Spend 1 XP Button", new Button(spend1XPButton, "Spend 1 XP Button"));
            buttonDictionay.Add("Crit Roll Button", new Button(critRollButton, "Crit Roll Button"));
            buttonDictionay.Add("Rewards", new Button(rewards, "Rewards"));
            buttonDictionay.Add("Die Highlight", new Button(dieHighlight, "Die Highlight"));

            spellIcons.Add("Fire Spell Icon", fireSpellIcon);
            spellIcons.Add("Healing Spell Icon", healthSpellIcon);
            spellIcons.Add("Ice Spell Icon", iceSpellIcon);
            spellIcons.Add("Poison Spell Icon", poisonSpellIcon);
 
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

                    TitleScreenAnim();

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
                            player = new Player(0, 10, 10, 10, spellIcons);
                            player.Experience = 3;
                            player.Rank = 4;
                            gamestate = Gamestate.COMBATTESTING;
                        }
                    }

                    // --------------------------------------------------------------------

                    break;

                // -------------- Difficulty Select Screen Game State Update -------------------
                // 
                // Player is created, hand is created, new hand is draw, and player stats
                // set based on difficulty selection. 


                case Gamestate.DIFFICULTY_SELECT:

                    if (SingleMouseClick())
                    {
                        if (position.X > 800 && position.X < 1050 && position.Y > 167 && position.Y < 241)
                        {
                            player = new Player(1, 5, 20, 6, spellIcons);
                            DrawNewHand();
                            gamestate = Gamestate.HACKANDSLASH;
                        }

                        if (position.X > 800 && position.X < 1050 && position.Y > 270 && position.Y < 344)
                        {
                            player = new Player(0, 5, 3, 6, spellIcons);
                            DrawNewHand();
                            gamestate = Gamestate.HACKANDSLASH;
                        }

                        if (position.X > 800 && position.X < 1050 && position.Y > 373 && position.Y < 447)
                        {
                            player = new Player(0, 4, 2, 5, spellIcons);
                            DrawNewHand();
                            gamestate = Gamestate.HACKANDSLASH;
                        }

                        if (position.X > 800 && position.X < 1050 && position.Y > 476 && position.Y < 550)
                        {
                            player = new Player(0, 3, 1, 3, spellIcons);
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

                            if (SingleMouseClick())
                            {
                                if (position.X > 92 && position.X < 290 && position.Y > 260 && position.Y < 542)
                                {
                                    currentCard = playerHand.Card1;
                                    currentCardNumber = 1;
                                    playerHand.Card1.Moving = true;
                                    currentTurnState = CurrentTurnState.ANIMATECARD1;
                                }
                            }

                            HandleHealingSpells();

                            break;


                        case CurrentTurnState.ANIMATECARD1:

                            if (playerHand.Card1.Moving)
                            {
                                playerHand.Card1.SlideCard(currentCardNumber);
                            }
                            else
                            {
                                currentTurnState = CurrentTurnState.TURN1;
                            }

                            break;

                        case CurrentTurnState.TURN1:
                            currentCard.ScaleVector = new Vector2(.40f, .40f);
                            currentCard.LevelXpos = 191;
                            currentCard.LevelYPos = 260;
                            colorMultiplyer = 1f;
                            if (currentCard.HandleCard(player, mouseState, prevMouseState, position.X, position.Y))
                            {
                                currentTurnState = CurrentTurnState.PRETURN2;
                            }

                            break;

                        case CurrentTurnState.PRETURN2:

                            FlipCard(playerHand.Card2);
                            FlipCard(playerHand.Card3);


                            if (SingleMouseClick())
                            {
                                if (position.X > 317 && position.X < 515 && position.Y > 100 && position.Y < 382)
                                {
                                    currentCard = playerHand.Card2;
                                    currentCardNumber = 2;
                                    playerHand.Card2.Moving = true;
                                    currentTurnState = CurrentTurnState.ANIMATECARD2;

                                }

                                if (position.X > 317 && position.X < 515 && position.Y > 410 && position.Y < 692)
                                {
                                    currentCard = playerHand.Card3;
                                    currentCardNumber = 3;
                                    playerHand.Card3.Moving = true;
                                    currentTurnState = CurrentTurnState.ANIMATECARD2;
                                }
                            }

                            HandleHealingSpells();

                            break;

                        case CurrentTurnState.ANIMATECARD2:

                            if (playerHand.Card2.Moving)
                            {
                                playerHand.Card2.SlideCard(currentCardNumber);
                            }
                            else if (playerHand.Card3.Moving)
                            {
                                playerHand.Card3.SlideCard(currentCardNumber);
                            }
                            else
                            {
                                currentTurnState = CurrentTurnState.TURN2;
                            }

                            break;    

                        case CurrentTurnState.TURN2:
                            currentCard.ScaleVector = new Vector2(.40f, .40f);
                            colorMultiplyer = 1f;
                            if (currentCardNumber == 2)
                            {
                                currentCard.LevelXpos = 416;
                                currentCard.LevelYPos = 100;
                            }
                            if (currentCardNumber == 3)
                            {
                                currentCard.LevelXpos = 416;
                                currentCard.LevelYPos = 410;
                            }


                            if (currentCard.HandleCard(player, mouseState, prevMouseState, position.X, position.Y))
                            {
                                currentTurnState = CurrentTurnState.PRETURN3;
                            }

                            break;

                        case CurrentTurnState.PRETURN3:

                            FlipCard(playerHand.Card4);

                            if (SingleMouseClick())
                            {
                                if (position.X > 542 && position.X < 740 && position.Y > 260 && position.Y < 542)
                                {
                                    currentCard = playerHand.Card4;
                                    currentCardNumber = 4;
                                    playerHand.Card4.Moving = true;
                                    currentTurnState = CurrentTurnState.ANIMATECARD3;
                                }
                            }

                            HandleHealingSpells();

                            break;

                        case CurrentTurnState.ANIMATECARD3:

                            if (playerHand.Card4.Moving)
                            {
                                playerHand.Card4.SlideCard(currentCardNumber);
                            }
                            else
                            {
                                currentTurnState = CurrentTurnState.TURN3;
                            }

                            break;

                        case CurrentTurnState.TURN3:
                            currentCard.ScaleVector = new Vector2(.40f, .40f);
                            currentCard.LevelXpos = 641;
                            currentCard.LevelYPos = 260;
                            colorMultiplyer = 1f;
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
                                if (position.X > 767 && position.X < 965 && position.Y > 100 && position.Y < 382)
                                {
                                    currentCard = playerHand.Card5;
                                    currentCardNumber = 5;
                                    playerHand.Card5.Moving = true;
                                    currentTurnState = CurrentTurnState.ANIMATECARD4;

                                }


                                if (position.X > 767 && position.X < 965 && position.Y > 410 && position.Y < 692)
                                {
                                    currentCard = playerHand.Card6;
                                    currentCardNumber = 6;
                                    playerHand.Card6.Moving = true;
                                    currentTurnState = CurrentTurnState.ANIMATECARD4;
                                }
                            }

                            HandleHealingSpells();

                            break;

                        case CurrentTurnState.ANIMATECARD4:

                            if (playerHand.Card5.Moving)
                            {
                                playerHand.Card5.SlideCard(currentCardNumber);
                            }
                            else if (playerHand.Card6.Moving)
                            {
                                playerHand.Card6.SlideCard(currentCardNumber);
                            }
                            else
                            {
                                currentTurnState = CurrentTurnState.TURN4;
                            }

                            break;

                        case CurrentTurnState.TURN4:
                            currentCard.ScaleVector = new Vector2(.40f, .40f);
                            colorMultiplyer = 1f;
                            if (currentCardNumber == 5)
                            {
                                currentCard.LevelXpos = 866;
                                currentCard.LevelYPos = 100;
                            }
                            if (currentCardNumber == 6)
                            {
                                currentCard.LevelXpos = 866;
                                currentCard.LevelYPos = 410;
                            }
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
                                if (position.X > 992 && position.X < 1190 && position.Y > 260 && position.Y < 542)
                                {
                                    currentCard = playerHand.Card7;
                                    currentCardNumber = 7;
                                    playerHand.Card7.Moving = true;
                                    currentTurnState = CurrentTurnState.ANIMATEBOSSCARD;
                                }
                            }

                            HandleHealingSpells();
                            break;

                        case CurrentTurnState.ANIMATEBOSSCARD:

                            if (playerHand.Card7.Moving)
                            {
                                playerHand.Card7.SlideCard(currentCardNumber);
                            }
                            else
                            {
                                currentTurnState = CurrentTurnState.BOSS;
                            }

                            break;

                        case CurrentTurnState.BOSS:
                            currentCard.ScaleVector = new Vector2(.40f, .40f);
                            currentCard.LevelXpos = 1091;
                            currentCard.LevelYPos = 260;
                            colorMultiplyer = 1f;
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
                        if (position.X > 770 && position.X < 1018 && position.Y > 450 && position.Y < 522)
                        {
                            player.DungeonArea++;

                            if (player.DungeonArea == 15)
                            {
                                gamestate = Gamestate.WINSCREEN;
                            }
                            player.HasFoughtMonster = false;
                            playerHand = new Hand();
                            DrawNewHand();
                            player.Food--;

                            if (player.Food <= 0)
                            {
                                player.Health -= 2;
                            }

                            if (gamestate != Gamestate.WINSCREEN)
                            {
                                gamestate = Gamestate.HACKANDSLASH;
                                currentTurnState = CurrentTurnState.PRETURN1;
                            }
                            
                        }

                    }

                            break;

                case Gamestate.GAME_OVER:
                    if (SingleMouseClick())
                    {
                        if (position.X > 770 && position.X < 1018 && position.Y > 450 && position.Y < 522)
                        {
                            ResetGame();
                            gamestate = Gamestate.TITILESCREEN;
                        }
                    }

                        break;

                case Gamestate.WINSCREEN:

                    if (SingleMouseClick())
                    {
                        if (position.X > 770 && position.X < 1018 && position.Y > 450 && position.Y < 522)
                        {
                            ResetGame();
                            gamestate = Gamestate.TITILESCREEN;
                        }
                    }

                    break;

                //----------------------- FOR TESTING COMBAT --------------------------

                case Gamestate.COMBATTESTING:

                    if (combat.HandleCombat(player, mouseState, prevMouseState, position.X, position.Y, true, false))
                    {
                        gamestate = Gamestate.TITILESCREEN;
                    }
                    


                    break;

                // --------------------------------------------------------------------

                default:

                    break;
            }

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

                    spriteBatch.Draw(titleBlack, new Vector2(0,0), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    spriteBatch.Draw(titleRightDoor, rightdoor, new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    spriteBatch.Draw(titleLeftDoor, leftDoor, new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    spriteBatch.Draw(titleBlank, new Vector2(0, 0), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    spriteBatch.Draw(titleEyes, new Vector2(0, 0), new Rectangle?(), Color.White * eyeOpacity, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    spriteBatch.Draw(titleGuy, guy, new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;

                // -------------- Difficulty Select Screen Game State Draw -----------------

                case Gamestate.DIFFICULTY_SELECT:

                    spriteBatch.Draw(difficultyScreen, new Vector2(0, 0), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);


                    break;

                // -------------- Delve Game State Draw -------------------

                case Gamestate.HACKANDSLASH:

                    //spriteBatch.Draw(gameScreen, new Vector2(0, 0), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    spriteBatch.Draw(gameBackground, new Vector2(0, 0), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    spriteBatch.Draw(characterStats, new Vector2(8, 8), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    spriteBatch.DrawString(highTower, player.Health.ToString(), new Vector2(742, 26), Color.Black);
                    spriteBatch.DrawString(highTower, player.Armor.ToString(), new Vector2(623, 26), Color.Black);
                    spriteBatch.DrawString(highTower, player.Gold.ToString(), new Vector2(880, 26), Color.Black);
                    spriteBatch.DrawString(highTower, player.Food.ToString(), new Vector2(1007, 26), Color.White);
                    spriteBatch.DrawString(highTower, player.Experience.ToString(), new Vector2(380, 26), Color.Red);
                    spriteBatch.DrawString(highTower, player.Rank.ToString(), new Vector2(485, 26), Color.Red);


                    if (player.DungeonArea <= 2)
                    {
                        spriteBatch.Draw(blackSewers, new Vector2(5, 665), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                    }
                    if (player.DungeonArea >= 3 && player.DungeonArea <= 4)
                    {
                        spriteBatch.Draw(PoisonousDungeon, new Vector2(5, 665), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                    }
                    if (player.DungeonArea >= 5 && player.DungeonArea <= 7)
                    {
                        spriteBatch.Draw(undeadCatacombs, new Vector2(5, 665), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                    }
                    if (player.DungeonArea >= 8 && player.DungeonArea <= 10)
                    {
                        spriteBatch.Draw(flamingUnderworld, new Vector2(5, 665), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                    }
                    if (player.DungeonArea >= 11 && player.DungeonArea <= 14)
                    {
                        spriteBatch.Draw(sunkenKeepOfOg, new Vector2(5, 665), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                    }

                    switch (player.DungeonArea)
                    {
                        case 1:
                            spriteBatch.Draw(mapHighlight, new Vector2(11, 681), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                            break;

                        case 2:
                            spriteBatch.Draw(mapHighlight, new Vector2(40, 681), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                            break;

                        case 3:
                            spriteBatch.Draw(mapHighlight, new Vector2(13, 681), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                            break;

                        case 4:
                            spriteBatch.Draw(mapHighlight, new Vector2(41, 681), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                            break;

                        case 5:
                            spriteBatch.Draw(mapHighlight, new Vector2(11, 683), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                            break;

                        case 6:
                            spriteBatch.Draw(mapHighlight, new Vector2(40, 683), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                            break;

                        case 7:
                            spriteBatch.Draw(mapHighlight, new Vector2(69, 683), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                            break;

                        case 8:
                            spriteBatch.Draw(mapHighlight, new Vector2(11, 682), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                            break;

                        case 9:
                            spriteBatch.Draw(mapHighlight, new Vector2(40, 681), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                            break;

                        case 10:
                            spriteBatch.Draw(mapHighlight, new Vector2(68, 681), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                            break;

                        case 11:
                            spriteBatch.Draw(mapHighlight, new Vector2(13, 681), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                            break;

                        case 12:
                            spriteBatch.Draw(mapHighlight, new Vector2(41, 681), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                            break;

                        case 13:
                            spriteBatch.Draw(mapHighlight, new Vector2(69, 681), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                            break;

                        case 14:
                            spriteBatch.Draw(mapHighlight, new Vector2(98, 681), new Rectangle?(), Color.White, 0f, new Vector2(), .5f, SpriteEffects.None, 1);
                            break;

                        default:
                            break;
                    }

                    foreach (var item in player.Spells)
                    {
                        item.DrawIcons(spriteBatch);
                    }

                    switch (currentTurnState)
                    {
                        case CurrentTurnState.PRETURN1:

                            playerHand.DrawHand(spriteBatch);

                            break;

                        case CurrentTurnState.ANIMATECARD1:

                            DrawSlidingCards();

                            break;

                        case CurrentTurnState.TURN1:
                            currentCard.DrawCard(spriteBatch, dungeonFont);
                            break;

                        case CurrentTurnState.PRETURN2:

                            playerHand.DrawHand(spriteBatch);

                            break;

                        case CurrentTurnState.ANIMATECARD2:

                            DrawSlidingCards();

                            break;

                        case CurrentTurnState.TURN2:
                            currentCard.DrawCard(spriteBatch, dungeonFont);
                            break;

                        case CurrentTurnState.PRETURN3:


                            playerHand.DrawHand(spriteBatch);

                            break;

                        case CurrentTurnState.ANIMATECARD3:

                            DrawSlidingCards();

                            break;

                        case CurrentTurnState.TURN3:


                            currentCard.DrawCard(spriteBatch, dungeonFont);

                            break;

                        case CurrentTurnState.PRETURN4:

                            playerHand.DrawHand(spriteBatch);

                            break;

                        case CurrentTurnState.ANIMATECARD4:

                            DrawSlidingCards();

                            break;

                        case CurrentTurnState.TURN4:


                            currentCard.DrawCard(spriteBatch, dungeonFont);

                            break;

                        case CurrentTurnState.PREBOSS:

                            playerHand.DrawHand(spriteBatch);

                            break;

                        case CurrentTurnState.ANIMATEBOSSCARD:

                            DrawSlidingCards();

                            break;

                        case CurrentTurnState.BOSS:

                            currentCard.DrawCard(spriteBatch, dungeonFont);

                            break;

                        default:
                            break;
                    }

                    break;


                case Gamestate.DELVING:

                    spriteBatch.Draw(gameBackground, new Vector2(0, 0), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    spriteBatch.Draw(dungeon, new Vector2(150, 150), new Rectangle?(), Color.White, 0f, new Vector2(), 1.3f, SpriteEffects.None, 1);
                    spriteBatch.DrawString(dungeonFont, "You have completed area " + player.DungeonArea + "!", new Vector2(780, 200), Color.White);
                    spriteBatch.Draw(acceptButton, new Vector2(770, 450), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;

                case Gamestate.GAME_OVER:

                    spriteBatch.Draw(titleBlack, new Vector2(0, 0), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    spriteBatch.DrawString(dungeonFont, "You died. Game Over.", new Vector2(790, 200), Color.White);
                    spriteBatch.Draw(eyes, new Vector2(150, 170), new Rectangle?(), Color.White, 0f, new Vector2(), 2.7f, SpriteEffects.None, 1);
                    spriteBatch.Draw(acceptButton, new Vector2(770, 450), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;

                case Gamestate.WINSCREEN:

                    spriteBatch.Draw(titleBlack, new Vector2(0, 0), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    spriteBatch.DrawString(dungeonFont, "You win!", new Vector2(820, 200), Color.White);
                    spriteBatch.Draw(winScreenGuy, new Vector2(-20, 150), new Rectangle?(), Color.White, 0f, new Vector2(), 1.5f, SpriteEffects.None, 1);
                    spriteBatch.Draw(acceptButton, new Vector2(770, 450), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
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

            playerHand.DrawNewHand(enemyCard, eventCard, merchantCard, restingCard, trapCard, treasureCard, bossMonsterCard, cardBack, buttonDictionay, combatDice, checkBoxes, dieTextures);


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
            if (card.Flipped && card.ScaleVector.X < .40f)
            {
                card.ScaleVector = new Vector2(card.ScaleVector.X + .02f, card.ScaleVector.Y);
            }

        }

        public void TitleScreenAnim()
        {
            counter++;
            if (counter > 30 && !doorsOpen && !(leftDoor.X < -160) && !(rightdoor.X > 160))
            {
                leftDoor.X -= 3;
                rightdoor.X += 3;
            }
            if (leftDoor.X < -160)
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
            if (eyesOnScreen && guy.X < 0)
            {
                guy.X += 10;
            }
        }

        public void DrawSlidingCards()
        {
            colorMultiplyer -= .033f;
            switch (currentCardNumber)
            {

                case 1:

                    spriteBatch.Draw(playerHand.Card2.CurrentTexture, new Vector2(playerHand.Card2.LevelXpos, playerHand.Card2.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card2.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card3.CurrentTexture, new Vector2(playerHand.Card3.LevelXpos, playerHand.Card3.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card3.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card4.CurrentTexture, new Vector2(playerHand.Card4.LevelXpos, playerHand.Card4.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card4.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card5.CurrentTexture, new Vector2(playerHand.Card5.LevelXpos, playerHand.Card5.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card5.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card6.CurrentTexture, new Vector2(playerHand.Card6.LevelXpos, playerHand.Card6.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card6.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card7.CurrentTexture, new Vector2(playerHand.Card7.LevelXpos, playerHand.Card7.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card7.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card1.CurrentTexture, new Vector2(playerHand.Card1.LevelXpos, playerHand.Card1.LevelYPos), new Rectangle?(), Color.White, 0f, new Vector2(248, 0), playerHand.Card1.ScaleVector, SpriteEffects.None, 1);

                    break;

                case 2:

                    spriteBatch.Draw(playerHand.Card1.CurrentTexture, new Vector2(playerHand.Card1.LevelXpos, playerHand.Card1.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card1.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card3.CurrentTexture, new Vector2(playerHand.Card3.LevelXpos, playerHand.Card3.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card3.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card4.CurrentTexture, new Vector2(playerHand.Card4.LevelXpos, playerHand.Card4.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card4.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card5.CurrentTexture, new Vector2(playerHand.Card5.LevelXpos, playerHand.Card5.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card5.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card6.CurrentTexture, new Vector2(playerHand.Card6.LevelXpos, playerHand.Card6.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card6.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card7.CurrentTexture, new Vector2(playerHand.Card7.LevelXpos, playerHand.Card7.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card7.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card2.CurrentTexture, new Vector2(playerHand.Card2.LevelXpos, playerHand.Card2.LevelYPos), new Rectangle?(), Color.White, 0f, new Vector2(248, 0), playerHand.Card2.ScaleVector, SpriteEffects.None, 1);

                    break;

                case 3:

                    spriteBatch.Draw(playerHand.Card1.CurrentTexture, new Vector2(playerHand.Card1.LevelXpos, playerHand.Card1.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card1.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card2.CurrentTexture, new Vector2(playerHand.Card2.LevelXpos, playerHand.Card2.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card2.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card4.CurrentTexture, new Vector2(playerHand.Card4.LevelXpos, playerHand.Card4.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card4.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card5.CurrentTexture, new Vector2(playerHand.Card5.LevelXpos, playerHand.Card5.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card5.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card6.CurrentTexture, new Vector2(playerHand.Card6.LevelXpos, playerHand.Card6.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card6.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card7.CurrentTexture, new Vector2(playerHand.Card7.LevelXpos, playerHand.Card7.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card7.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card3.CurrentTexture, new Vector2(playerHand.Card3.LevelXpos, playerHand.Card3.LevelYPos), new Rectangle?(), Color.White, 0f, new Vector2(248, 0), playerHand.Card3.ScaleVector, SpriteEffects.None, 1);

                    break;

                case 4:

                    spriteBatch.Draw(playerHand.Card1.CurrentTexture, new Vector2(playerHand.Card1.LevelXpos, playerHand.Card1.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card1.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card2.CurrentTexture, new Vector2(playerHand.Card2.LevelXpos, playerHand.Card2.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card2.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card3.CurrentTexture, new Vector2(playerHand.Card3.LevelXpos, playerHand.Card3.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card3.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card5.CurrentTexture, new Vector2(playerHand.Card5.LevelXpos, playerHand.Card5.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card5.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card6.CurrentTexture, new Vector2(playerHand.Card6.LevelXpos, playerHand.Card6.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card6.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card7.CurrentTexture, new Vector2(playerHand.Card7.LevelXpos, playerHand.Card7.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card7.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card4.CurrentTexture, new Vector2(playerHand.Card4.LevelXpos, playerHand.Card4.LevelYPos), new Rectangle?(), Color.White, 0f, new Vector2(248, 0), playerHand.Card4.ScaleVector, SpriteEffects.None, 1);

                    break;

                case 5:

                    spriteBatch.Draw(playerHand.Card1.CurrentTexture, new Vector2(playerHand.Card1.LevelXpos, playerHand.Card1.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card1.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card2.CurrentTexture, new Vector2(playerHand.Card2.LevelXpos, playerHand.Card2.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card2.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card3.CurrentTexture, new Vector2(playerHand.Card3.LevelXpos, playerHand.Card3.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card3.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card4.CurrentTexture, new Vector2(playerHand.Card4.LevelXpos, playerHand.Card4.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card4.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card6.CurrentTexture, new Vector2(playerHand.Card6.LevelXpos, playerHand.Card6.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card6.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card7.CurrentTexture, new Vector2(playerHand.Card7.LevelXpos, playerHand.Card7.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card7.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card5.CurrentTexture, new Vector2(playerHand.Card5.LevelXpos, playerHand.Card5.LevelYPos), new Rectangle?(), Color.White, 0f, new Vector2(248, 0), playerHand.Card5.ScaleVector, SpriteEffects.None, 1);

                    break;

                case 6:

                    spriteBatch.Draw(playerHand.Card1.CurrentTexture, new Vector2(playerHand.Card1.LevelXpos, playerHand.Card1.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card1.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card2.CurrentTexture, new Vector2(playerHand.Card2.LevelXpos, playerHand.Card2.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card2.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card3.CurrentTexture, new Vector2(playerHand.Card3.LevelXpos, playerHand.Card3.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card3.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card4.CurrentTexture, new Vector2(playerHand.Card4.LevelXpos, playerHand.Card4.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card4.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card5.CurrentTexture, new Vector2(playerHand.Card5.LevelXpos, playerHand.Card5.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card5.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card7.CurrentTexture, new Vector2(playerHand.Card7.LevelXpos, playerHand.Card7.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card7.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card6.CurrentTexture, new Vector2(playerHand.Card6.LevelXpos, playerHand.Card6.LevelYPos), new Rectangle?(), Color.White, 0f, new Vector2(248, 0), playerHand.Card6.ScaleVector, SpriteEffects.None, 1);

                    break;

                case 7:

                    spriteBatch.Draw(playerHand.Card1.CurrentTexture, new Vector2(playerHand.Card1.LevelXpos, playerHand.Card1.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card1.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card2.CurrentTexture, new Vector2(playerHand.Card2.LevelXpos, playerHand.Card2.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card2.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card3.CurrentTexture, new Vector2(playerHand.Card3.LevelXpos, playerHand.Card3.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card3.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card4.CurrentTexture, new Vector2(playerHand.Card4.LevelXpos, playerHand.Card4.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card4.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card5.CurrentTexture, new Vector2(playerHand.Card5.LevelXpos, playerHand.Card5.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card5.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card6.CurrentTexture, new Vector2(playerHand.Card6.LevelXpos, playerHand.Card6.LevelYPos), new Rectangle?(), Color.White * colorMultiplyer, 0f, new Vector2(248, 0), playerHand.Card6.ScaleVector, SpriteEffects.None, 1);
                    spriteBatch.Draw(playerHand.Card7.CurrentTexture, new Vector2(playerHand.Card7.LevelXpos, playerHand.Card7.LevelYPos), new Rectangle?(), Color.White, 0f, new Vector2(248, 0), playerHand.Card7.ScaleVector, SpriteEffects.None, 1);

                    break;

                default:
                    break;
            }
        }

        public void HandleHealingSpells()
        {
            if (player.Spells.Count >= 1 && player.Spells[0].Name == "Healing")
            {
                if (SingleMouseClick())
                {
                    if (position.X > 1130 && position.X < 1175 && position.Y > 20 && position.Y < 65)
                    {
                        player.Health += 8;
                        player.RemoveSpell(0);
                    }
                }
            }
            if (player.Spells.Count == 2 && player.Spells[1].Name == "Healing")
            {
                if (SingleMouseClick())
                {
                    if (position.X > 1180 && position.X < 1225 && position.Y > 20 && position.Y < 65)
                    {
                        player.Health += 8;
                        player.RemoveSpell(1);
                    }
                }
            }
        }

        public void ResetGame()
        {
            currentTurnState = CurrentTurnState.PRETURN1;
            currentCard = null;
            player = null;

            for (int i = 0; i < 7; i++)
            {
                playerHand.PlayerHand.Clear();
            }

        }

    }
}
