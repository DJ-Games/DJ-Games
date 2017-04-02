using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniRogue
{
    class Card
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



        // Constructors
        public Card(string name, Texture2D cardTexture)
        {

            CardTexture = cardTexture;
            CardRectangle = new Rectangle(0, 0, 494, 708);

            this.Name = name;
            
        }

        // Methods

        public void DrawCard(SpriteBatch sBatch, int xPos, int yPos)
        {
            cardRectangle.X = xPos;
            cardRectangle.Y = yPos;
            sBatch.Draw(CardTexture, CardRectangle, Color.White);   
        }




    }
}
