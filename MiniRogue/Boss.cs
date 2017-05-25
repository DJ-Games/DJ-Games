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
        COMPLETE,

    }

    class Boss : Enemy
    {

        public int GoldReward { get; set; }

        public bool GivesItem { get; set; }

        public int TreasureResult { get; set; }

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
                        bossTurnState = BossTurnState.COMPLETE;
                    }

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
                                player.SpellsString.Add("Fire Spell");
                                break;

                            case 4:
                                player.SpellsString.Add("Ice Spell");
                                break;

                            case 5:
                                player.SpellsString.Add("Poison Spell");
                                break;

                            case 6:
                                player.SpellsString.Add("Healing Spell");
                                break;
                            default:
                                break;
                        }
                        //Thread.Sleep(500);
                        //treasureTurnState = TreasureTurnState.REVIEW;
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
