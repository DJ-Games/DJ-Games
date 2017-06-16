using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace MiniRogue
{

    enum BossTurnState
    {
        STARTCOMBAT,
        COMBAT,
        ROLLANIMATION,
        REWARDS,
        REMOVESPELL,
        REVIEW,
        COMPLETE,

    }

    class Boss : Enemy
    {

        public int GoldReward { get; set; }

        public bool GivesItem { get; set; }

        public int TreasureResult { get; set; }

        public string AwardedSpell { get; set; }

        public Die RewardDie { get; set; }

        public Dictionary<string, Texture2D> DieTextures { get; set; }

        BossTurnState bossTurnState = new BossTurnState();

        public Boss(string name, Texture2D cardTexture, Texture2D cardBack, Dictionary<string, Button> buttons,
            Dictionary<string, Die> combatDice, Dictionary<string, Texture2D> dieTextures, Dictionary<string, SoundEffect> dieSounds)
            : base(name, cardTexture, cardBack, buttons)
        {
            Buttons = buttons;
            CombatDice = combatDice;
            DieTextures = dieTextures;
            DieSounds = dieSounds;
            RewardDie = new Die(DieTextures, 840, 400);
        }

        public override bool HandleCard(Player player, MouseState current, MouseState previous, float xPos, float yPos)
        {

            XPos = xPos;
            YPos = yPos;
            CurrentMouseState = current;
            PreviousMouseState = previous;

            switch (bossTurnState)
            {
                case BossTurnState.STARTCOMBAT:
                    HandleButtons(player);
                    AnimationCounter = 0;
                    return false;

                case BossTurnState.COMBAT:

                    if (CurrentCombat.HandleCombat(player, CurrentMouseState, PreviousMouseState, XPos, yPos, true, false))
                    {
                        bossTurnState = BossTurnState.REWARDS;
                    }

                    return false;

                case BossTurnState.REWARDS:

                    HandleButtons(player);

                    return false;

                case BossTurnState.ROLLANIMATION:

                    if (AnimationCounter < 60)
                    {
                        RollAnimation();
                    }
                    else
                    {
                        TreasureResult = player.playerDice.RollDice();
                        RewardDie.CurrentTexture = RewardDie.DieTextures["Roll " + TreasureResult];
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
                            bossTurnState = BossTurnState.REMOVESPELL;
                        }
                        else if (TreasureResult > 2)
                        {
                            player.AddSpell(AwardedSpell);
                            bossTurnState = BossTurnState.REVIEW;
                        }
                        
                    }

                    return false;

                case BossTurnState.REMOVESPELL:

                    HandleButtons(player);

                    return false;

                case BossTurnState.REVIEW:

                    //if (TreasureResult > 2 && player.Spells.Count == 2)
                    //{
                    //    bossTurnState = BossTurnState.REMOVESPELL;
                    //}
                    //else if (TreasureResult > 2)
                    //{
                    //    player.AddSpell(AwardedSpell);
                    //}
                    HandleButtons(player);

                    return false;

                case BossTurnState.COMPLETE:


                    return true;

                default:
                    return false;
            }
        }

        public override void DrawCard(SpriteBatch sBatch, SpriteFont dungeonFont)
        {

            sBatch.Draw(CardTexture, new Vector2(274, 130), new Rectangle?(), Color.White, 0f, new Vector2(248, 0), .75f, SpriteEffects.None, 1);

            switch (bossTurnState)
            {
                case BossTurnState.STARTCOMBAT:

                    sBatch.Draw(Buttons["Combat Button"].ButtonTexture, new Vector2(770, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;

                case BossTurnState.COMBAT:

                    CurrentCombat.DrawCombat(sBatch, dungeonFont);

                    break;

                case BossTurnState.REWARDS:

                    sBatch.Draw(Buttons["Roll Die"].ButtonTexture, new Vector2(770, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    sBatch.DrawString(dungeonFont, "Roll to determine your reward.", new Vector2(620, 230), Color.White);

                    break;

                case BossTurnState.ROLLANIMATION:

                    RewardDie.DrawCombatDie(sBatch);

                    break;

                case BossTurnState.REMOVESPELL:

                    RewardDie.DrawCombatDie(sBatch);
                    sBatch.Draw(Buttons["Rewards"].ButtonTexture, new Vector2(650, 150), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    //sBatch.DrawString(dungeonFont, "You Rolled a:  " + TreasureResult, new Vector2(725, 200), Color.White);
                    sBatch.DrawString(dungeonFont, "Click a spell to remove or", new Vector2(650, 290), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                    sBatch.DrawString(dungeonFont, "click done to keep current spells", new Vector2(600, 340), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                    sBatch.Draw(Buttons["Done Button"].ButtonTexture, new Vector2(770, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;

                case BossTurnState.REVIEW:

                    RewardDie.DrawCombatDie(sBatch);
                    sBatch.Draw(Buttons["Rewards"].ButtonTexture, new Vector2(650, 150), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    sBatch.Draw(Buttons["Done Button"].ButtonTexture, new Vector2(770, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;

                case BossTurnState.COMPLETE:


                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        public new void HandleButtons(Player player)
        {
            if (SingleMouseClick())
            {
                switch (bossTurnState)
                {
                    case BossTurnState.STARTCOMBAT:


                        if (XPos > 770 && XPos < 1018 && YPos > 600 && YPos < 672)
                        {

                            CurrentCombat = new Combat(Buttons, CombatDice, DieSounds);
                            bossTurnState = BossTurnState.COMBAT;

                        }

                        break;
                    case BossTurnState.COMBAT:
                        break;

                    case BossTurnState.REWARDS:
                   
                        if (XPos > 770 && XPos < 1018 && YPos > 600 && YPos < 672)
                        {
                            bossTurnState = BossTurnState.ROLLANIMATION;
                        }
                        break;

                    case BossTurnState.ROLLANIMATION:



                        break;

                    case BossTurnState.REMOVESPELL:

                        if (XPos > 770 && XPos < 1018 && YPos > 600 && YPos < 672)
                        {
                            bossTurnState = BossTurnState.COMPLETE;
                        }

                        if (XPos > 1130 && XPos < 1175 && YPos > 20 && YPos < 65)
                        {
                            player.RemoveSpell(0);
                            player.AddSpell(AwardedSpell);
                            bossTurnState = BossTurnState.REVIEW;
                        }

                        if (player.Spells.Count == 2)
                        {
                            if (XPos > 1180 && XPos < 1225 && YPos > 20 && YPos < 65)
                            {
                                player.RemoveSpell(1);
                                player.AddSpell(AwardedSpell);
                                bossTurnState = BossTurnState.REVIEW;
                            }
                        }


                        break;


                    case BossTurnState.REVIEW:

                        if (XPos > 770 && XPos < 1018 && YPos > 600 && YPos < 672)
                        {
                            bossTurnState = BossTurnState.COMPLETE;
                        }

                        break;

                    case BossTurnState.COMPLETE:
                        break;
                    default:
                        break;
                }
            }
        }

        public void RollAnimation()
        {
            RewardDie.CurrentTexture = RewardDie.DieTextureList[Rng.Next(RewardDie.DieTextureList.Count - 1)];
            AnimationCounter += 5;
        }

    }
}
