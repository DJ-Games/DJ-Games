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

        public  MouseState MouseState { get; set; }

        public MouseState PreviousMouseState { get; set; }

        public Card CurrentCard { get; set; }



        public Turn()
        {
            turnState = TurnState.SETUP;
        }





        public void ResolveTurn(MouseState current, MouseState previous, Player player, Hand playerHand)
        {
            MouseState = current;
            PreviousMouseState = previous;


            switch (turnState)
            {
                case TurnState.SETUP:

                    CurrentCard = playerHand.RevealCard();
                    turnState = TurnState.TURN1;

                    break;

                case TurnState.TURN1:

                    if (CurrentCard.HandleCard(player)) 
                    {
                        turnState = TurnState.TURN2;
                        CurrentCard = playerHand.RevealCard();
                    }

                    break;

                case TurnState.TURN2:

                    if (CurrentCard.HandleCard(player))
                    {
                        turnState = TurnState.TURN3;
                        CurrentCard = playerHand.RevealCard();
                    }

                    break;

                case TurnState.TURN3:

                    if (CurrentCard.HandleCard(player))
                    {
                        turnState = TurnState.TURN4;
                        CurrentCard = playerHand.RevealCard();
                    }

                    break;

                case TurnState.TURN4:

                    // I think this is where we will be drawing a new hand. Make sure
                    // to set player.HasFoughtMonster to false when you do this. 


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

                    CurrentCard.DrawCard(sBatch, font, 45, 40);

                    break;
                case TurnState.TURN3:

                    CurrentCard.DrawCard(sBatch, font, 45, 40);

                    break;
                case TurnState.TURN4:

                    CurrentCard.DrawCard(sBatch, font, 45, 40);

                    break;
                default:
                    break;
            }

        }


        private bool SingleMouseClick()
        {
            if (MouseState.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }



        //private bool SingleKeyPress(Keys key)
        //{
        //    if (CurrentKBState.IsKeyDown(key) && PreviousKBState.IsKeyUp(key))
        //    {
        //        return true;
        //    }
        //    return false;
        //}




    }
}
