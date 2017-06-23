using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

namespace Deep_Space_D_6
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 

    enum GameState
    {
        MAINPLAY,
    }




    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MouseState currentMouseState;
        MouseState previousMouseState;
        Vector2 position;


        GameState gameState;

        Texture2D ship;
        Texture2D dieCommander;
        Texture2D dieEngineer;
        Texture2D dieMedic;
        Texture2D dieScience;
        Texture2D dieTactical;
        Texture2D dieThreat;

        Dictionary<string, Texture2D> dieTextures;
        Dictionary<string, Die> playerDice;

        int activeDieNumber;
        bool noDieInHand;

        // For Testing
        bool dieRolled = false;




        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;  
            graphics.PreferredBackBufferHeight = 720; 
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            gameState = GameState.MAINPLAY;

            base.Initialize();
            this.IsMouseVisible = true;

        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            dieTextures = new Dictionary<string, Texture2D>();
            playerDice = new Dictionary<string, Die>();

            ship = Content.Load<Texture2D>("ship");
            dieCommander = Content.Load<Texture2D>("dieCommander");
            dieEngineer = Content.Load<Texture2D>("dieEngineer");
            dieMedic = Content.Load<Texture2D>("dieMedic");
            dieScience = Content.Load<Texture2D>("dieScience");
            dieTactical = Content.Load<Texture2D>("dieTactical");
            dieThreat = Content.Load<Texture2D>("dieThreat");

            dieTextures.Add("dieCommander", dieCommander);
            dieTextures.Add("dieEngineer", dieEngineer);
            dieTextures.Add("dieMedic", dieMedic);
            dieTextures.Add("dieScience", dieScience);
            dieTextures.Add("dieTactical", dieTactical);
            dieTextures.Add("dieThreat", dieThreat);

            playerDice.Add("Die1", new Die(dieTextures, 200, 118));
            Thread.Sleep(20);
            playerDice.Add("Die2", new Die(dieTextures, 277, 118));
            Thread.Sleep(20);
            playerDice.Add("Die3", new Die(dieTextures, 200, 191));
            Thread.Sleep(20);
            playerDice.Add("Die4", new Die(dieTextures, 277, 191));
            Thread.Sleep(20);
            playerDice.Add("Die5", new Die(dieTextures, 200, 266));
            Thread.Sleep(20);
            playerDice.Add("Die6", new Die(dieTextures, 277, 266));

            position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                graphics.GraphicsDevice.Viewport.Height / 2);


        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {

            currentMouseState = Mouse.GetState();

            position.X = currentMouseState.X;
            position.Y = currentMouseState.Y;


            if (!playerDice["Die1"].InHand && !playerDice["Die2"].InHand && !playerDice["Die3"].InHand &&
                !playerDice["Die4"].InHand && !playerDice["Die5"].InHand && !playerDice["Die6"].InHand)
            {
                noDieInHand = true;
            }
            else { noDieInHand = false; }

            if (!dieRolled)
            {
                for (int i = 1; i < 7; i++)
                {
                    playerDice["Die" + i].RollDie();
                    dieRolled = true;
                }
            }

            if (LeftClickPressed() && noDieInHand)
            {
                if (position.X > 200 && position.X < 267 && position.Y > 118 && position.Y < 185)
                {
                    playerDice["Die1"].InHand = true;
                }

                if (position.X > 277 && position.X < 344 && position.Y > 118 && position.Y < 185)
                {
                    playerDice["Die2"].InHand = true;
                }

                if (position.X > 200 && position.X < 267 && position.Y > 191 && position.Y < 258)
                {
                    playerDice["Die3"].InHand = true;
                }

                if (position.X > 277 && position.X < 344 && position.Y > 191 && position.Y < 258)
                {
                    playerDice["Die4"].InHand = true;
                }

                if (position.X > 200 && position.X < 267 && position.Y > 266 && position.Y < 333)
                {
                    playerDice["Die5"].InHand = true;
                }

                if (position.X > 277 && position.X < 344 && position.Y > 266 && position.Y < 333)
                {
                    playerDice["Die6"].InHand = true;
                }
            }

            if (!LeftClickHeld())
            {
                for (int i = 1; i < 7; i++)
                {
                    playerDice["Die" + i].InHand = false;
                }
            }

            for (int i = 1; i < 7; i++)
            {
                if (playerDice["Die" + i].InHand)
                {
                    playerDice["Die" + i].InHandXPos = currentMouseState.X;
                    playerDice["Die" + i].InHandYPos = currentMouseState.Y;

                }
            }

            if (playerDice["Die1"].InHand)
            {
                playerDice["Die1"].InHandXPos = currentMouseState.X;
                playerDice["Die1"].InHandYPos = currentMouseState.Y;

            }





            previousMouseState = currentMouseState;

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.MAINPLAY:

                    spriteBatch.Draw(ship, new Vector2(393, 23), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1f);
                    for (int i = 1; i < 7; i++)
                    {
                        playerDice["Die" + i].DrawDie(spriteBatch);
                    }



                    break;

                default:
                    break;
            }

            





            spriteBatch.End();

            base.Draw(gameTime);
        }

        public bool SingleMouseClick()
        {
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }

        public bool LeftClickHeld()
        {
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        public bool LeftClickPressed()
        {
            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        public bool LeftClickReleased()
        {
            if (currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
    }

    
}
