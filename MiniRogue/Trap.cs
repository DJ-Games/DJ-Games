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

    enum TrapTurnState
    {
        ROLL_FOR_TRAP,
        SKILL_CHECK,
        REVIEW,
        COMPLETE,
    }





    class Trap : Card
    {

        private bool success;

        public bool Success
        {
            get { return success; }
            set { success = value; }
        }

        public int Result { get; set; }

        TrapTurnState trapTurnState;


        public Trap(string name, Texture2D cardTexture, Dictionary<string, Button> buttons) : base(name, cardTexture, buttons)
        {
            trapTurnState = new TrapTurnState();
            CurrentButtons = new List<Button>();
        }


        //---------------------- METHODS -----------------------------
        public override bool HandleCard(Player player, MouseState current, MouseState previous, float xPos, float yPos)
        {
            XPos = xPos;
            YPos = yPos;

            switch (trapTurnState)
            {
                case TrapTurnState.ROLL_FOR_TRAP:
                    HandleButtons(player);
                    return false;

                case TrapTurnState.SKILL_CHECK:
                    HandleButtons(player);
                    return false;

                case TrapTurnState.REVIEW:
                    HandleButtons(player);
                    return false;

                case TrapTurnState.COMPLETE:
                    HandleButtons(player);
                    return true;

                default:
                    return false;
            }
        }

        public void HandleButtons(Player player)
        {
            if (SingleMouseClick())
            {
                switch (trapTurnState)
                {
                    case TrapTurnState.SKILL_CHECK:

                        if (XPos > 700 && XPos < 948 && YPos > 275 && YPos < 348)
                        {
                            
                            if (player.playerDice.RollDice() <= player.Rank)
                            {
                                success = true;
                                trapTurnState = TrapTurnState.REVIEW;
                            }
                            else
                            {
                                success = false;
                                trapTurnState = TrapTurnState.ROLL_FOR_TRAP;
                            }
                        }

                        break;
                        
                    case TrapTurnState.ROLL_FOR_TRAP:

                        if (SingleKeyPress(Keys.Space))
                        {
                            Result = player.playerDice.RollDice();

                            switch (Result)
                            {
                                case 1:

                                    player.Food--;
                                    break;

                                case 2:

                                    player.Gold--;
                                    break;

                                case 3:

                                    if (player.Armor > 0)
                                    {
                                        player.Armor--;
                                    }
                                    player.Health -= 2;

                                    break;

                                case 4:

                                    player.Health--;
                                    break;

                                case 5:

                                    if (player.Experience > 1)
                                    {
                                        player.Experience--;
                                    }

                                    break;

                                case 6:

                                    player.Health -= 2;
                                    player.FallBelow();
                                    break;

                                default:
                                    break;
                            }

                            trapTurnState = TrapTurnState.COMPLETE;

                        }

                        break;

                    case TrapTurnState.REVIEW:

                        trapTurnState = TrapTurnState.COMPLETE;
                        break;


                    case TrapTurnState.COMPLETE:

                        break;

                    default:
                        break;
                }





            }
        }



        //public void TrapResult(int option, Player player)
        //{
        //    switch (option)
        //    {
        //        case 1:
        //            player.Food--;
        //            break;
        //        case 2:
        //            player.Gold--;
        //            break;
        //        case 3:
        //            if (player.Armor == 0)
        //            {
        //                player.Health -= 2;
        //            }
        //            else
        //            {
        //                player.Armor--;
        //            }
        //            break;
        //        case 4:
        //            player.Health--;
        //            break;
        //        case 5:
        //            player.Experience--;
        //            break;
        //        case 6:
        //            player.Health -= 2;
        //            // will need to make multi dimensional array to determine this
        //            break;



        //        default:
        //            break;
        //    }
        //}

        public override void DrawCard(SpriteBatch sBatch, SpriteFont font)
        {
            sBatch.Draw(CardTexture, new Vector2(100, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);
            sBatch.Draw(Buttons["Roll Die"].ButtonTexture, new Vector2(700, 275), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);

            switch (trapTurnState)
            {
                case TrapTurnState.SKILL_CHECK:

                    sBatch.DrawString(font, "Roll for skill check.", new Vector2(50, 800), Color.White);

                    break;

                case TrapTurnState.ROLL_FOR_TRAP:

                    break;

                case TrapTurnState.REVIEW:

                    if (success)
                    {

                    }
                    else
                    {

                    }

                    break;

                case TrapTurnState.COMPLETE:

                    break;

                default:
                    break;
            }





            sBatch.DrawString(font, "Press Space to roll for skill check.", new Vector2(50, 800), Color.White);
        }

    }
}
