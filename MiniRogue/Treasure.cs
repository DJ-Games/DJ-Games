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

    enum TreasureTurnState
    {
        CARDFLIP,
        GOLD_AWARD,
        EXTRA_TREASURE_ROLL,
        REMOVESPELL,
        REVIEW,
        COMPLETE,
    }


    class Treasure : Card
    {

        public bool Guarded { get; set; }

        public bool Sucess { get; set; }

        public int GoldAward { get; set; }

        public int ExtTreasureResult { get; set; }

        public int TreasureResult { get; set; }

        public string AwardedSpell { get; set; }

        public bool TreasureAwarded { get; set; }


        TreasureTurnState treasureTurnState;

        public Treasure(string name, Texture2D cardTexture, Texture2D cardBack,  Dictionary<string, Button> buttons) : base(name, cardTexture, cardBack, buttons)
        {
            treasureTurnState = TreasureTurnState.GOLD_AWARD;
            CurrentButtons = new List<Button>(); 
        }


        //---------------------- METHODS -----------------------------
        public override bool HandleCard(Player player, MouseState current, MouseState previous, float xPos, float yPos)
        {
            XPos = xPos;
            YPos = yPos;
            CurrentMouseState = current;
            PreviousMouseState = previous;


            switch (treasureTurnState)
            {
                case TreasureTurnState.GOLD_AWARD:
                    TreasureAwarded = false;
                    if (player.HasFoughtMonster)
                    {
                        GoldAward = 2;
                    }
                    else { GoldAward = 1; }
                    HandleButtons(player);

                    return false;

                case TreasureTurnState.EXTRA_TREASURE_ROLL:
                    HandleButtons(player);

                    return false;

                case TreasureTurnState.REMOVESPELL:
                    HandleButtons(player);

                    return false;

                case TreasureTurnState.REVIEW:
                    if (TreasureResult > 2 && !TreasureAwarded)
                    {
                        player.AddSpell(AwardedSpell);
                        TreasureAwarded = true;
                    }
                    HandleButtons(player);

                    return false;

                case TreasureTurnState.COMPLETE:
                    HandleButtons(player);

                    return true;

                default:

                    return false;
            }


        }




        public override void DrawCard(SpriteBatch sBatch, SpriteFont font)
        {
            sBatch.Draw(CardTexture, new Vector2(100, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);
    
            switch (treasureTurnState)
            {
                case TreasureTurnState.GOLD_AWARD:

                    sBatch.DrawString(font, "You gained " + GoldAward + " gold!", new Vector2(730, 200), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    sBatch.DrawString(font, "Roll 5+ to find a treasure.", new Vector2(680, 250), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    sBatch.Draw(Buttons["Roll Die"].ButtonTexture, new Vector2(770, 540), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);



                    break;
                case TreasureTurnState.EXTRA_TREASURE_ROLL:

                    sBatch.DrawString(font, "You Rolled a:  " + ExtTreasureResult, new Vector2(725, 200), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    sBatch.DrawString(font, "You found a treasure!", new Vector2(670, 250), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    sBatch.DrawString(font, "Roll for treasure.", new Vector2(715, 300), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    sBatch.Draw(Buttons["Roll Die"].ButtonTexture, new Vector2(770, 540), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;

                case TreasureTurnState.REMOVESPELL:

                    sBatch.DrawString(font, "You Rolled a:  " + TreasureResult, new Vector2(725, 200), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    sBatch.DrawString(font, "Click spell you would like to remove or done to keep current spells.", new Vector2(520, 250), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    sBatch.Draw(Buttons["Done Button"].ButtonTexture, new Vector2(770, 540), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;

                case TreasureTurnState.REVIEW:
                    
                    if (TreasureResult > 2)
                    {
                        sBatch.DrawString(font, "You gained a " + AwardedSpell + " Spell.", new Vector2(725, 200), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    }

                    else if(TreasureResult == 1)
                    {
                        sBatch.DrawString(font, "You gained 1 armor.", new Vector2(725, 200), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    }
                    else
                    {
                        sBatch.DrawString(font, "You gained 2 Experience.", new Vector2(725, 200), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    }
                    sBatch.Draw(Buttons["Done Button"].ButtonTexture, new Vector2(770, 540), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;

                case TreasureTurnState.COMPLETE:
                    break;
                default:
                    break;

            }

        }

        public void HandleButtons(Player player)
        {
            if (SingleMouseClick())
            {
                switch (treasureTurnState)
                {
                    case TreasureTurnState.GOLD_AWARD:

                        player.Gold += GoldAward;
                        if (XPos > 770 && XPos < 1018 && YPos > 540 && YPos < 612)
                        {
                            // die animation (probably make this in player?)
                            ExtTreasureResult = player.playerDice.RollDice();
                            // Set to zero for testing
                            if (ExtTreasureResult >= 0)
                            {
                                treasureTurnState = TreasureTurnState.EXTRA_TREASURE_ROLL;
                            }
                            else { treasureTurnState = TreasureTurnState.REVIEW; }
                            
                        }
                            break;

                    case TreasureTurnState.EXTRA_TREASURE_ROLL:
                        
                        if (XPos > 770 && XPos < 1018 && YPos > 540 && YPos < 612)
                        {
                            // add Die roll animation
                            TreasureResult = player.playerDice.RollDice();

                            switch (TreasureResult)
                            {
                                case 1:
                                    player.Armor++;
                                    break;

                                case 2:
                                    player.Experience += 2;
                                    break;

                                case 3:
                                    AwardedSpell = "Fire";
                                    break;

                                case 4:
                                    AwardedSpell = "Ice";
                                    break;

                                case 5:
                                    AwardedSpell = "Poison";
                                    break;

                                case 6:
                                    AwardedSpell = "Healing";
                                    break;
                                default:
                                    break;
                            }

                            if (TreasureResult > 2 && player.Spells.Count == 2)
                            {
                                treasureTurnState = TreasureTurnState.REMOVESPELL;
                            }
                            else { treasureTurnState = TreasureTurnState.REVIEW; }
                        }

                        break;

                    case TreasureTurnState.REMOVESPELL:

                        if (XPos > 700 && XPos < 948 && YPos > 275 && YPos < 348)
                        {
                            treasureTurnState = TreasureTurnState.COMPLETE;
                        }

                        if (XPos > 1130 && XPos < 1175 && YPos > 20 && YPos < 65)
                        {
                            player.RemoveSpell(0);
                            player.AddSpell(AwardedSpell);
                            TreasureAwarded = true;
                            treasureTurnState = TreasureTurnState.REVIEW;
                        }

                        if (player.Spells.Count == 2)
                        {
                            if (XPos > 1180 && XPos < 1225 && YPos > 20 && YPos < 65)
                            {
                                player.RemoveSpell(1);
                                player.AddSpell(AwardedSpell);
                                TreasureAwarded = true;
                                treasureTurnState = TreasureTurnState.REVIEW;
                            }
                        }
                            break;

                    case TreasureTurnState.REVIEW:
                        
                            if (XPos > 770 && XPos < 1018 && YPos > 540 && YPos < 612)
                            {
                                treasureTurnState = TreasureTurnState.COMPLETE;
                            }


                        break;

                    case TreasureTurnState.COMPLETE:
                        break;
                    default:
                        break;
                }
            }
        }



    }
}
