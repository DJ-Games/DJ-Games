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

    enum BossTurnState
    {
        STARTCOMBAT,
        COMBAT,
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

        BossTurnState bossTurnState = new BossTurnState();

        public Boss(string name, Texture2D cardTexture, Texture2D cardBack, Dictionary<string, Button> buttons, Dictionary<string, CombatDice> combatDice, Dictionary<string, CheckBox> checkBoxes) : base(name, cardTexture, cardBack, buttons)
        {
            Buttons = buttons;
            CombatDice = combatDice;
            CheckBoxes = checkBoxes;
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

                    return false;

                case BossTurnState.COMBAT:

                    if (CurrentCombat.HandleCombat(player, CurrentMouseState, PreviousMouseState, XPos, yPos, true))
                    {
                        bossTurnState = BossTurnState.REWARDS;
                    }

                    return false;

                case BossTurnState.REWARDS:

                    HandleButtons(player);

                    return false;

                case BossTurnState.REMOVESPELL:

                    HandleButtons(player);

                    return false;

                case BossTurnState.REVIEW:

                    if (TreasureResult > 2 && player.Spells.Count == 2)
                    {
                        bossTurnState = BossTurnState.REMOVESPELL;
                    }
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

            sBatch.Draw(CardTexture, new Vector2(100, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);

            switch (bossTurnState)
            {
                case BossTurnState.STARTCOMBAT:

                    sBatch.Draw(Buttons["Combat Button"].ButtonTexture, new Vector2(700, 500), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;

                case BossTurnState.COMBAT:

                    CurrentCombat.DrawCombat(sBatch, dungeonFont);

                    break;


                case BossTurnState.REWARDS:

                    sBatch.Draw(Buttons["Roll Die"].ButtonTexture, new Vector2(700, 275), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);
                    sBatch.DrawString(dungeonFont, "Roll to determine your reward.", new Vector2(520, 230), Color.White);

                    break;

                case BossTurnState.REMOVESPELL:

                    sBatch.DrawString(dungeonFont, "You Rolled a:  " + TreasureResult, new Vector2(725, 200), Color.White);
                    sBatch.DrawString(dungeonFont, "Click spell you would like to remove or done to keep current spells.", new Vector2(520, 230), Color.White);
                    sBatch.Draw(Buttons["Done Button"].ButtonTexture, new Vector2(700, 275), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);

                    break;

                case BossTurnState.REVIEW:

                    sBatch.DrawString(dungeonFont, "You Rolled a:  " + TreasureResult, new Vector2(725, 200), Color.White);
                    sBatch.Draw(Buttons["Done Button"].ButtonTexture, new Vector2(700, 275), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);

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


                        if (XPos > 700 && XPos < 948 && YPos > 500 && YPos < 572)
                        {

                            CurrentCombat = new Combat(Buttons, CombatDice, CheckBoxes);
                            bossTurnState = BossTurnState.COMBAT;

                        }

                        break;
                    case BossTurnState.COMBAT:
                        break;

                    case BossTurnState.REWARDS:
                        if (XPos > 700 && XPos < 948 && YPos > 275 && YPos < 348)
                        {
                            TreasureResult = player.playerDice.RollDice();
                        }

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
                        bossTurnState = BossTurnState.REVIEW;
                        break;

                    case BossTurnState.REMOVESPELL:

                        if (XPos > 700 && XPos < 948 && YPos > 275 && YPos < 348)
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

                        if (XPos > 700 && XPos < 948 && YPos > 275 && YPos < 348)
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
    }
}
