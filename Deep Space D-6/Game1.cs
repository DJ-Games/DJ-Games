using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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

        GameState gameState;

        Texture2D ship;
        Texture2D dieCommander;
        Texture2D dieEngineer;
        Texture2D dieMedic;
        Texture2D dieScience;
        Texture2D dieTactical;
        Texture2D dieThreat;

        Dictionary<string, Texture2D> dieTextures;
        Dictionary<string, Die>






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
            
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ship = Content.Load<Texture2D>("ship");
            dieCommander = Content.Load<Texture2D>("dieCommander");
            dieEngineer = Content.Load<Texture2D>("dieEngineer");
            dieMedic = Content.Load<Texture2D>("dieMedic");
            dieScience = Content.Load<Texture2D>("dieScience");
            dieTactical = Content.Load<Texture2D>("dieTactical");
            dieThreat = Content.Load<Texture2D>("dieThreat");

            
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

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

                    break;

                default:
                    break;
            }

            





            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
