using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MiniRogue
{
    abstract class Card
    {


        // Properties
        public string Name { get; set; }

        public bool Flipped { get; set; }


        private Rectangle cardRectangle;

        public Rectangle CardRectangle
        {
            get { return cardRectangle; }
            set { cardRectangle = value; }
        }

        public Texture2D CardTexture { get; set; }

        public float XPos { get; set; }

        public float YPos { get; set; }

        public KeyboardState CurrentKbState { get; set; }

        public KeyboardState PreviousKbState { get; set; }

        public MouseState CurrentMouseState { get; set; }

        public MouseState PreviousMouseState { get; set; }

        public Dice Dice { get; set; }

        public Dictionary<string, Button> Buttons { get; set; }

        public List<Button> CurrentButtons { get; set; }


        // Constructors
        public Card(string name, Texture2D cardTexture, Dictionary<string, Button> buttons)
        {

            CardTexture = cardTexture;
            CardRectangle = new Rectangle(0, 0, 494, 708);
            Dice = new Dice();
            Name = name;
            Buttons = buttons;
            
        }

        // Methods

        public abstract void DrawCard(SpriteBatch sBatch,SpriteFont font);

        public abstract bool HandleCard(Player player, MouseState current, MouseState previous, float xPos, float yPos);


        public bool SingleKeyPress(Keys key)
        {
            CurrentKbState = Keyboard.GetState();
            if (CurrentKbState.IsKeyDown(key) && PreviousKbState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }

        public bool SingleMouseClick()
        {
            CurrentMouseState = Mouse.GetState();
            if (CurrentMouseState.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }






    }
}
