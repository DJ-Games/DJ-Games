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
            Texture2D restingTex, Texture2D trapTex, Texture2D treasureTex, Dictionary<string, Button> buttons)
        {
            //playerHand.Add(new Enemy("Enemy", enemyTex, buttons));

            //playerHand.Add(new EventCard("EventCard", eventTex, buttons));

            //playerHand.Add(new Merchant("Merchant", merchantTex, buttons));

            playerHand.Add(new Resting("Resting", restingTex, buttons));

            //playerHand.Add(new Trap("Trap", trapTex, buttons));

            //playerHand.Add(new Treasure("Treasure", treasureTex, buttons));
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
