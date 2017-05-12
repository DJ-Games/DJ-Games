using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MiniRogue
{

    enum TrapTurnState
    {
        ROLL_FOR_TRAP,
        SKILL_CHECK,
        RESOLVE_TRAP,
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
        public int TrapResult { get; set; }
        TrapTurnState trapTurnState;


        public Trap(string name, Texture2D cardTexture, Dictionary<string, Button> buttons) : base(name, cardTexture, buttons)
        {
           
            trapTurnState = TrapTurnState.ROLL_FOR_TRAP;
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
                    
                    return false;

                case TrapTurnState.RESOLVE_TRAP:
                    
                    return false; 

                case TrapTurnState.REVIEW:
                  
                    return false;

                case TrapTurnState.COMPLETE:
                   
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
                    case TrapTurnState.ROLL_FOR_TRAP:
                        if (XPos > 700 && XPos < 948 && YPos > 275 && YPos < 348)
                        {
                            TrapResult = player.playerDice.RollDice();
                            trapTurnState = TrapTurnState.SKILL_CHECK; 
                        }
                        break;

                    case TrapTurnState.SKILL_CHECK:
                        

                        if (XPos > 700 && XPos < 948 && YPos > 375 && YPos < 448)
                        {
                            
                            if (player.playerDice.RollDice() <= player.Rank)
                            {
                                success = true;
                                trapTurnState = TrapTurnState.REVIEW;
                            }
                            else
                            {
                                success = false;
                                trapTurnState = TrapTurnState.RESOLVE_TRAP;
                            }
                        }

                        break;
                        
                    case TrapTurnState.RESOLVE_TRAP:
                    
         

                        switch (TrapResult)
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




        public override void DrawCard(SpriteBatch sBatch, SpriteFont font)
        {
            sBatch.Draw(CardTexture, new Vector2(100, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);
            sBatch.Draw(Buttons["Roll Die"].ButtonTexture, new Vector2(700, 275), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);

            switch (trapTurnState)
            {
                case TrapTurnState.ROLL_FOR_TRAP:
                    sBatch.DrawString(font, "Roll to determine the trap you face", new Vector2(680, 200), Color.White);
                    
                    break;

                case TrapTurnState.SKILL_CHECK:
                    
                    sBatch.DrawString(font, "You Rolled a:  " + TrapResult, new Vector2(725, 200), Color.White);
                    sBatch.DrawString(font, "Roll a skill check to evade the trap", new Vector2(680, 50), Color.White);

                    switch (TrapResult)
                    {
                        case 1:
                            sBatch.DrawString(font, "Mildew: You Lose a Food", new Vector2(750, 225), Color.White);
                            break;

                        case 2:
                            sBatch.DrawString(font, "TripWire: You Lose a Gold", new Vector2(750, 225), Color.White);
                            break;

                        case 3:
                            sBatch.DrawString(font, "Acid Mist: You Lose a Armor Piece or Two Health", new Vector2(750, 225), Color.White);
                            break;

                        case 4:
                            sBatch.DrawString(font, "Spring Blades: You Lose a Health", new Vector2(750, 225), Color.White);
                            break;

                        case 5:
                            sBatch.DrawString(font, "Moving Walls: You Lose a XP", new Vector2(750, 225), Color.White);
                            break;

                        case 6:
                            sBatch.DrawString(font, "Pit: You Lose Two Health and have fallen deeper into the darkness", new Vector2(750, 225), Color.White);
                            break;

                        default:
                            break;
                    }
                   
                    sBatch.DrawString(font, "You Rolled a:  " + TrapResult, new Vector2(725, 200), Color.White);
                    trapTurnState = TrapTurnState.RESOLVE_TRAP; 
                    break;

                case TrapTurnState.RESOLVE_TRAP:
                    
                    break; 

                case TrapTurnState.REVIEW:

                    if (success)
                    {

                    }
                    else
                    {

                    }

                    trapTurnState = TrapTurnState.COMPLETE;
                    sBatch.Draw(Buttons["Done Button"].ButtonTexture, new Vector2(700, 75), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);
                    break;

             

                    break;

                default:
                    break;
            }





        
        }

    }
}
