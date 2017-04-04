using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace MiniRogue
{
    enum TurnState
    {
        SETUP,
        TURN1,
        TURN2,
        TURN3,
        TURN4,
    }

    class Turn
    {


        TurnState turnState = new TurnState();

        public  KeyboardState CurrentKBState { get; set; }

        public KeyboardState PreviousKBState { get; set; }

        public Card CurrentCard { get; set; }



        public Turn()
        {
            turnState = TurnState.SETUP;
        }





        public void ResolveTurn(KeyboardState current, KeyboardState previous, Player player, Hand playerHand)
        {

            CurrentKBState = current;
            PreviousKBState = previous;

            switch (turnState)
            {
                case TurnState.SETUP:
                    CurrentCard = playerHand.RevealCard();
                    turnState = TurnState.TURN1;

                    break;
                case TurnState.TURN1:
                    CurrentCard.HandleCard(player);
                    //CurrentCard.DrawCard();

                    break;
                case TurnState.TURN2:
                    break;
                case TurnState.TURN3:
                    break;
                case TurnState.TURN4:
                    break;
                default:
                    break;
            }





        }



        private bool SingleKeyPress(Keys key)
        {
            if (CurrentKBState.IsKeyDown(key) && PreviousKBState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }

    }
}
