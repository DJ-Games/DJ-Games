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

        public bool Visible { get; set; }



        private Rectangle cardRectangle;

        public Rectangle CardRectangle
        {
            get { return cardRectangle; }
            set { cardRectangle = value; }
        }


        public Texture2D CardTexture { get; set; }

        private int xPos;

        public int XPos
        {
            get { return xPos; }
            set { cardRectangle.X = value; }
        }

        private int yPos;

        public int YPos
        {
            get { return yPos; }
            set { cardRectangle.Y = value; }
        }

        public KeyboardState CurrentKbState { get; set; }

        public KeyboardState PreviousKbState { get; set; }

        public Dice Dice { get; set; }


        // Constructors
        public Card(string name, Texture2D cardTexture)
        {

            CardTexture = cardTexture;
            CardRectangle = new Rectangle(0, 0, 494, 708);
            Dice = new Dice();
            Name = name;
            
        }

        // Methods

        public abstract void DrawCard(SpriteBatch sBatch,SpriteFont font, int xPos, int yPos);

        public abstract void HandleCard(Player player);


        public bool SingleKeyPress(Keys key)
        {
            CurrentKbState = Keyboard.GetState();
            if (CurrentKbState.IsKeyDown(key) && PreviousKbState.IsKeyUp(key))
            {
                PreviousKbState = CurrentKbState;
                return true;
            }
            PreviousKbState = CurrentKbState;
            return false;
        }

    }
}
