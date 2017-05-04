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
        DELVE,
        BOSS,
        RECOUP,
        GAME_OVER,
        CREDITS,
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
        SpriteFont font;
        Vector2 position;


        Player player;
        Hand playerHand;
        Dice playerDice;
        Card currentCard;
        Difficulty difficulty;
        int currentRoll;

        int deviceWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int deviceHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;




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
            font = Content.Load<SpriteFont>("Font");
            position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                graphics.GraphicsDevice.Viewport.Height / 2);
            playerDice = new Dice();
            difficulty = new Difficulty();
            buttonDictionay = new Dictionary<string, Button>();
            gamestate = Gamestate.TITILESCREEN;
            currentTurnState = CurrentTurnState.PRETURN1;


            // Fill Button dictionary

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


            //------------------- Switch for gamestates ------------------------
            switch (gamestate)
            {
                case Gamestate.TITILESCREEN:

                    if (SingleKeyPress(Keys.Space)) 
                    {
                        gamestate = Gamestate.DIFFICULTY_SELECT;

                    }

                    if (SingleMouseClick())
                    {
                        if (position.X > 800 && position.X < 1050 && position.Y > 372 && position.Y < 446)
                        {
                            gamestate = Gamestate.DIFFICULTY_SELECT;
                        }
                    }

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
                            player = new Player(1, 5, 20, 6);
                            playerHand = new Hand();
                            playerHand.DrawNewHand(enemyCard, eventCard, merchantCard, restingCard, trapCard, treasureCard, buttonDictionay);
                            gamestate = Gamestate.DELVE;
                        }
                    }

                    if (SingleMouseClick())
                    {
                        if (position.X > 800 && position.X < 1050 && position.Y > 270 && position.Y < 344)
                        {
                            player = new Player(0, 5, 3, 6);
                            playerHand = new Hand();
                            playerHand.DrawNewHand(enemyCard, eventCard, merchantCard, restingCard, trapCard, treasureCard, buttonDictionay);
                            gamestate = Gamestate.DELVE;
                        }
                    }

                    if (SingleMouseClick())
                    {
                        if (position.X > 800 && position.X < 1050 && position.Y > 373 && position.Y < 447)
                        {
                            player = new Player(0, 4, 2, 5);
                            playerHand = new Hand();
                            playerHand.DrawNewHand(enemyCard, eventCard, merchantCard, restingCard, trapCard, treasureCard, buttonDictionay);
                            gamestate = Gamestate.DELVE;
                        }
                    }

                    if (SingleMouseClick())
                    {
                        if (position.X > 800 && position.X < 1050 && position.Y > 476 && position.Y < 550)
                        {
                            player = new Player(0, 3, 1, 3);
                            playerHand = new Hand();
                            playerHand.DrawNewHand(enemyCard, eventCard, merchantCard, restingCard, trapCard, treasureCard, buttonDictionay);
                            gamestate = Gamestate.DELVE;
                        }
                    }

                    break;

                //----- End Mouse Controls -----

                // -------------- Delve Game State Update -------------------

                case Gamestate.DELVE:

                    switch (currentTurnState)
                    {
                        case CurrentTurnState.PRETURN1:

                            if (SingleMouseClick())
                            {
                                if (position.X > 75 && position.X < 270 && position.Y > 260 && position.Y < 535)
                                {

                                    currentCard = playerHand.RevealCard();
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

                           // if (SingleMouseClick())
                           // {
                           //     if (position.X > 300 && position.X < 495 && position.Y > 100 && position.Y < 375)
                           //     {
                          //          currentCard = playerHand.RevealCard();
                           //         //currentTurnState = CurrentTurnState.TURN2;

                           //     }
                          //  }

                           // if (position.X > 300 && position.X < 495 && position.Y > 400 && position.Y < 575)
                          //  {
                           //     currentCard = playerHand.RevealCard();
                          //  }
                            
                            break;
                    

                   

                        case CurrentTurnState.TURN2:
                            break;
                        case CurrentTurnState.TURN3:
                            break;
                        case CurrentTurnState.TURN4:
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

                case Gamestate.DELVE:

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

                    if (player.Spells.Count == 1)
                    {
                        if (player.Spells[0]== "Fire Spell")
                        {
                            spriteBatch.Draw(fireballSpellButtonTex, new Vector2(700, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.Spells[0] == "Ice Spell")
                        {
                            spriteBatch.Draw(iceSpellSpellButtonTex, new Vector2(700, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.Spells[0] == "Poison Spell")
                        {
                            spriteBatch.Draw(poisonSpellButtonTex, new Vector2(700, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.Spells[0] == "Healing Spell")
                        {
                            spriteBatch.Draw(healingSpellButtonTex, new Vector2(700, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                   }

                    if (player.Spells.Count == 2)
                    {
                        if (player.Spells[0] == "Fire Spell")
                        {
                            spriteBatch.Draw(fireballSpellButtonTex, new Vector2(700, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.Spells[0] == "Ice Spell")
                        {
                            spriteBatch.Draw(iceSpellSpellButtonTex, new Vector2(700, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.Spells[0] == "Poison Spell")
                        {
                            spriteBatch.Draw(poisonSpellButtonTex, new Vector2(700, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.Spells[0] == "Healing Spell")
                        {
                            spriteBatch.Draw(healingSpellButtonTex, new Vector2(700, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.Spells[1] == "Fire Spell")
                        {
                            spriteBatch.Draw(fireballSpellButtonTex, new Vector2(900, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.Spells[1] == "Ice Spell")
                        {
                            spriteBatch.Draw(iceSpellSpellButtonTex, new Vector2(900, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.Spells[1] == "Poison Spell")
                        {
                            spriteBatch.Draw(poisonSpellButtonTex, new Vector2(900, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }

                        if (player.Spells[1] == "Healing Spell")
                        {
                            spriteBatch.Draw(healingSpellButtonTex, new Vector2(900, 15), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                        }
                    }


                    switch (currentTurnState)
                    {
                        case CurrentTurnState.PRETURN1:

                            spriteBatch.Draw(door, new Vector2(75, 260), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(300, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(300, 400), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(525, 260), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(750, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(750, 400), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(975, 260), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);

                            break;
                        case CurrentTurnState.TURN1:
                            currentCard.DrawCard(spriteBatch, font);

                            spriteBatch.Draw(die1, new Vector2(1130, 100), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                            spriteBatch.Draw(die2, new Vector2(1130, 250), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                            spriteBatch.Draw(die3, new Vector2(1130, 400), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                            spriteBatch.Draw(die4, new Vector2(1130, 550), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                            break;

                        case CurrentTurnState.PRETURN2:

                            spriteBatch.Draw(doorGrayed, new Vector2(75, 260), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(door, new Vector2(300, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(door, new Vector2(300, 400), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(525, 260), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(750, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(750, 400), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(975, 260), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);

                            break;

                        case CurrentTurnState.TURN2:
                            break;

                        case CurrentTurnState.PRETURN3:

                            spriteBatch.Draw(doorGrayed, new Vector2(75, 260), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(300, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(300, 400), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(door, new Vector2(525, 260), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(750, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(750, 400), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(975, 260), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);

                            break;

                        case CurrentTurnState.TURN3:
                            break;

                        case CurrentTurnState.PRETURN4:

                            spriteBatch.Draw(doorGrayed, new Vector2(75, 260), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(300, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(300, 400), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(525, 260), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(door, new Vector2(750, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(door, new Vector2(750, 400), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);
                            spriteBatch.Draw(doorGrayed, new Vector2(975, 260), new Rectangle?(), Color.White, 0f, new Vector2(), .43f, SpriteEffects.None, 1);

                            break;

                        case CurrentTurnState.TURN4:
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


    }
}
