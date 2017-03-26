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

        Random rGen = new Random();

        public Hand ()
        {
            playerHand.Add(new Enemy("Enemy"));

            playerHand.Add(new EventCard("EventCard"));

            playerHand.Add(new Merchant("Merchant"));

            playerHand.Add(new Resting("Resting"));

            playerHand.Add(new Trap("Trap"));

            playerHand.Add(new Treasure("Treasure"));


        }

        public void dealHand()
        {
            







        }



    }
}
