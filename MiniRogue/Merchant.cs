using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MiniRogue
{
    class Merchant : Card
    {


        // Properties
        public int SellCost { get; set; }

        public int BuyCost { get; set; }

        //Constructors

        public Merchant(string name, Texture2D cardTexture) : base(name, cardTexture)
        {

        }

        //---------------------- METHODS -----------------------------
        public override void HandleCard(Player player)
        {

        }

        public override void DrawCard(SpriteBatch sBatch, SpriteFont font, int xPos, int yPos)
        {
            XPos = xPos;
            YPos = yPos;
            sBatch.Draw(CardTexture, CardRectangle, Color.White);
            sBatch.DrawString(font, "What would you like to buy?", new Vector2(50, 800), Color.White);
        }






    }
}
