using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            playerHand.Add(new Enemy("Enemy"));

            playerHand.Add(new EventCard("EventCard"));

            playerHand.Add(new Merchant("Merchant"));

            playerHand.Add(new Resting("Resting"));

            playerHand.Add(new Trap("Trap"));

            playerHand.Add(new Treasure("Treasure"));

            rGen = new Random();
        }

        public Card RevealCard()
        {
            int tempInt;

            tempInt = rGen.Next(playerHand.Count);

            return playerHand[tempInt];

            playerHand.RemoveAt(tempInt);

        
        }



    }
}
