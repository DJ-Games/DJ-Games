using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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



            switch (turnState)
            {
                case TurnState.SETUP:
                    CurrentCard = playerHand.RevealCard();
                    turnState = TurnState.TURN1;
                    

                    break;
                case TurnState.TURN1:
                    CurrentCard.HandleCard(player);



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


        public void DrawTurn(SpriteBatch sBatch, SpriteFont font)
        {
            switch (turnState)
            {
                case TurnState.SETUP:



                    break;
                case TurnState.TURN1:

                    CurrentCard.DrawCard(sBatch, font, 45, 40);



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
