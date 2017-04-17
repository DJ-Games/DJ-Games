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

    enum EventCardTurnState
    {
        INITIAL_ROLL,
        SKILL_CHECK,
        ADJUSTOPTION,
        HANDLE_EVENT,
        FIGHT_MONSTER,
        COMPLETE,
    }




    class EventCard : Card
    {


        public bool Success { get; set; }

        public int Option { get; set; }

        EventCardTurnState eventCardTurnState;

        public EventCard(string name, Texture2D cardTexture) : base(name, cardTexture)
        {
            eventCardTurnState = new EventCardTurnState();
        }

        //---------------------- METHODS -----------------------------


        /// <summary>
        /// Event card handler.!!! Come back to add option for skill check later!!!!
        /// Add Monster event and recognition of results on skill check and 
        /// complete state.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public override bool HandleCard(Player player, MouseState current, MouseState previous, float xPos, float yPos)
        {
            PreviousKbState = CurrentKbState;

            switch (eventCardTurnState)
            {
                case EventCardTurnState.INITIAL_ROLL:


                    if (SingleMouseClick())
                    {
                        if (xPos > 700 && xPos < 948 && yPos > 575 && yPos < 648)
                        {


                            switch (player.playerDice.RollDice())
                            {

                                case 1:
                                    Option = 1;
                                    break;

                                case 2:
                                    Option = 2;
                                    break;

                                case 3:
                                    Option = 3;
                                    break;

                                case 4:
                                    Option = 4;
                                    break;

                                case 5:
                                    Option = 5;
                                    break;

                                case 6:
                                    Option = 6;
                                    break;

                                default:
                                    break;
                            }
                            eventCardTurnState = EventCardTurnState.SKILL_CHECK;

                        }
                    }


                    break;
                case EventCardTurnState.SKILL_CHECK:


                    if (SingleMouseClick())
                    {
                        if (xPos > 700 && xPos < 948 && yPos > 575 && yPos < 648)
                        {

                            if (player.playerDice.RollDice() <= player.Rank)
                            {

                                eventCardTurnState = EventCardTurnState.ADJUSTOPTION;

                            }
                            else
                            {
                                eventCardTurnState = EventCardTurnState.HANDLE_EVENT;
                            }
                        }
                    }


                    break;

                case EventCardTurnState.ADJUSTOPTION:

                    if (SingleMouseClick())
                    {
                        if (xPos > 700 && xPos < 948 && yPos > 575 && yPos < 648)
                        {



                        }
                    }


                            break;

                case EventCardTurnState.HANDLE_EVENT:

                    switch (player.playerDice.RollDice())
                    {

                        case 1:
                            player.Food++;
                            break;

                        case 2:
                            player.Health += 2;
                            break;

                        case 3:
                            player.Gold++;
                            break;

                        case 4:
                            player.Experience += 2;
                            break;

                        case 5:
                            player.Armor++;
                            break;

                        case 6:
                            eventCardTurnState = EventCardTurnState.FIGHT_MONSTER;



                            break;

                        default:
                            break;
                    }
                    eventCardTurnState = EventCardTurnState.COMPLETE;
                    break;

                case EventCardTurnState.FIGHT_MONSTER:



                    







                    break;

                case EventCardTurnState.COMPLETE:
                    break;


                default:
                    break;
            }

            return true;




        }

        public override void DrawCard(SpriteBatch sBatch, SpriteFont font, int xPos, int yPos)
        {
            XPos = xPos;
            YPos = yPos;
            sBatch.Draw(CardTexture, new Vector2(100, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);
            switch (eventCardTurnState)
            {
                case EventCardTurnState.INITIAL_ROLL:
                    sBatch.DrawString(font, "Press Space to roll for event.", new Vector2(500, 200), Color.White);
                    break;
                case EventCardTurnState.SKILL_CHECK:
                    sBatch.DrawString(font, "Press Space to roll for skill check.", new Vector2(500, 200), Color.White);
                    break;

                case EventCardTurnState.ADJUSTOPTION:


                    break

                case EventCardTurnState.HANDLE_EVENT:
                    sBatch.DrawString(font, "Press + to add 1 and - to subtract 1.", new Vector2(500, 200), Color.White);
                    break;

                case EventCardTurnState.FIGHT_MONSTER:

                    break;


                case EventCardTurnState.COMPLETE:
                    break;
                default:
                    break;
            }

        }


    }
}
