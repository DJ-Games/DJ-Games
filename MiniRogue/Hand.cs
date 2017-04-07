using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniRogue
{
    class Hand
    {

        private List<Card> playerHand;

        public List<Card> PlayerHand
        {
            get { return playerHand; }
            set { playerHand = value; }
        }

        public Random rGen { get; set; }




        public Hand ()
        {
            rGen = new Random();
            playerHand = new List<Card>();
        }



        public void DrawNewHand(Texture2D enemyTex, Texture2D eventTex, Texture2D merchantTex,
            Texture2D restingTex, Texture2D trapTex, Texture2D treasureTex)
        {
            //playerHand.Add(new Enemy("Enemy", enemyTex ));

            //playerHand.Add(new EventCard("EventCard", eventTex));

            playerHand.Add(new Merchant("Merchant", merchantTex));

            //playerHand.Add(new Resting("Resting", restingTex));

            //playerHand.Add(new Trap("Trap", trapTex));

            //playerHand.Add(new Treasure("Treasure", treasureTex));
        }





        public Card RevealCard()
        {
            Card tempCard;
            int tempInt = rGen.Next(playerHand.Count);
            tempCard = playerHand[tempInt];
            playerHand.RemoveAt(tempInt);
            return tempCard;
        }



    }
}
