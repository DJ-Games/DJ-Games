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
        GOLD_AWARD,
        ROLLANIMATION,
        EXTRA_TREASURE_ROLL,
        REMOVESPELL,
        REVIEW,
        COMPLETE,
    }


    class Treasure : Card
    {

        public bool Guarded { get; set; }

        public bool Success { get; set; }

        public int GoldAward { get; set; }

        public int ExtTreasureResult { get; set; }

        public int TreasureResult { get; set; }

        public string AwardedSpell { get; set; }

        public bool TreasureAwarded { get; set; }

        public Dictionary<string, Texture2D> DieTextures { get; set; }

        public Die TreasureDie { get; set; }

        public int AnimationCounter { get; set; }

        public bool TreasureRoll { get; set; }

        TreasureTurnState treasureTurnState;

        public Treasure(string name, Texture2D cardTexture, Texture2D cardBack,  Dictionary<string, Button> buttons, Dictionary<string, Texture2D> dieTextures) : base(name, cardTexture, cardBack, buttons)
        {
            treasureTurnState = TreasureTurnState.GOLD_AWARD;
            CurrentButtons = new List<Button>();
            DieTextures = dieTextures;
            TreasureDie = new Die(DieTextures,840, 400);
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
                    Success = false;
                    TreasureAwarded = false;
                    TreasureRoll = false;
                    if (player.HasFoughtMonster)
                    {
                        GoldAward = 2;
                    }
                    else { GoldAward = 1; }
                    HandleButtons(player);

                    return false;

                case TreasureTurnState.ROLLANIMATION:

                    if (AnimationCounter < 60)
                    {
                        RollAnimation();
                    }
                    else
                    {
                        if (TreasureRoll)
                        {
                            AnimationCounter = 0;
                            TreasureResult = player.playerDice.RollDice();
                            TreasureDie.CurrentTexture = TreasureDie.DieTextures["Roll " + TreasureResult];


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
                        else
                        {
                            AnimationCounter = 0;
                            ExtTreasureResult = player.playerDice.RollDice();
                            TreasureDie.CurrentTexture = TreasureDie.DieTextures["Roll " + ExtTreasureResult];
                            if (ExtTreasureResult >= 5)
                            {
                                Success = true;
                                treasureTurnState = TreasureTurnState.EXTRA_TREASURE_ROLL;
                            }
                            else { treasureTurnState = TreasureTurnState.REVIEW; }
                        }
                    }

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

                    return true;

                default:

                    return false;
            }


        }




        public override void DrawCard(SpriteBatch sBatch, SpriteFont font)
        {
            sBatch.Draw(CardTexture, new Vector2(100, 130), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);
    
            switch (treasureTurnState)
            {
                case TreasureTurnState.GOLD_AWARD:

                    sBatch.DrawString(font, "You gained " + GoldAward + " gold!", new Vector2(730, 200), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    sBatch.DrawString(font, "Roll 5+ to find a treasure.", new Vector2(680, 250), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    sBatch.Draw(Buttons["Roll Die"].ButtonTexture, new Vector2(770, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;

                case TreasureTurnState.ROLLANIMATION:

                    TreasureDie.DrawCombatDie(sBatch);
                   
                    break;

                case TreasureTurnState.EXTRA_TREASURE_ROLL:

                    TreasureDie.DrawCombatDie(sBatch);
                    sBatch.DrawString(font, "You Rolled a:  " + ExtTreasureResult, new Vector2(725, 200), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    sBatch.DrawString(font, "You found a treasure!", new Vector2(670, 250), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    sBatch.DrawString(font, "Roll for treasure.", new Vector2(715, 300), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    sBatch.Draw(Buttons["Roll Die"].ButtonTexture, new Vector2(770, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;

                case TreasureTurnState.REMOVESPELL:

                    TreasureDie.DrawCombatDie(sBatch);
                    sBatch.DrawString(font, "You Rolled a:  " + TreasureResult, new Vector2(725, 200), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    sBatch.DrawString(font, "Click a spell to remove or", new Vector2(650, 250), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    sBatch.DrawString(font, "click done to keep current spells", new Vector2(600, 300), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    sBatch.Draw(Buttons["Done Button"].ButtonTexture, new Vector2(770, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;

                case TreasureTurnState.REVIEW:

                    if (!Success)
                    {
                        sBatch.DrawString(font, "You did not find treasure.", new Vector2(700, 200), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    }

                    TreasureDie.DrawCombatDie(sBatch);
                    if (TreasureResult == 3 || TreasureResult == 5 || TreasureResult == 6)
                    {
                        sBatch.DrawString(font, "You gained a " + AwardedSpell + " Spell.", new Vector2(700, 200), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    }
                    if (TreasureResult == 4)
                    {
                        sBatch.DrawString(font, "You gained an " + AwardedSpell + " Spell.", new Vector2(700, 200), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    }
                    if(TreasureResult == 1)
                    {
                        sBatch.DrawString(font, "You gained 1 armor.", new Vector2(725, 200), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    }
                    if (TreasureResult == 2)
                    {
                        sBatch.DrawString(font, "You gained 2 Experience.", new Vector2(725, 200), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    }

                   
                    sBatch.Draw(Buttons["Done Button"].ButtonTexture, new Vector2(770, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

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
                        if (XPos > 770 && XPos < 1018 && YPos > 600 && YPos < 672)
                        {
                            treasureTurnState = TreasureTurnState.ROLLANIMATION;                    
                        }
                            break;

                    case TreasureTurnState.EXTRA_TREASURE_ROLL:
                        
                        if (XPos > 770 && XPos < 1018 && YPos > 600 && YPos < 672)
                        {
                            TreasureRoll = true;
                            treasureTurnState = TreasureTurnState.ROLLANIMATION;
                        }

                        break;

                    case TreasureTurnState.REMOVESPELL:

                        if (XPos > 770 && XPos < 1018 && YPos > 600 && YPos < 672)
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
                        
                            if (XPos > 770 && XPos < 1018 && YPos > 600 && YPos < 672)
                            {
                                treasureTurnState = TreasureTurnState.COMPLETE;
                            }


                        break;

                    default:
                        break;
                }
            }
        }

        public void RollAnimation()
        {
            TreasureDie.CurrentTexture = TreasureDie.DieTextureList[Rng.Next(TreasureDie.DieTextureList.Count - 1)];
            AnimationCounter += 5;
        }

    }
}
