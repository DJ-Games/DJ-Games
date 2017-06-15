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
        ROLLANIMATION,
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

        public int SkillCheckResult { get; set; }

        public int AnimationCounter { get; set; }

        public Die TrapDie { get; set; }

        public Dictionary<string, Texture2D> DieTextures { get; set; }

        public bool SkillCheck { get; set; }

        TrapTurnState trapTurnState;


        public Trap(string name, Texture2D cardTexture, Texture2D cardBack, Dictionary<string, Button> buttons, Dictionary<string, Texture2D> dieTextures) : base(name, cardTexture, cardBack, buttons)
        {
           
            trapTurnState = TrapTurnState.ROLL_FOR_TRAP;
            CurrentButtons = new List<Button>();
            DieTextures = dieTextures;
            TrapDie = new Die(DieTextures, 840, 400);
        }


        //---------------------- METHODS -----------------------------
        public override bool HandleCard(Player player, MouseState current, MouseState previous, float xPos, float yPos)
        {
            XPos = xPos;
            YPos = yPos;
            CurrentMouseState = current;
            PreviousMouseState = previous;

            switch (trapTurnState)
            {
                case TrapTurnState.ROLL_FOR_TRAP:
                    SkillCheck = false;
                    HandleButtons(player);
                    return false;

                case TrapTurnState.ROLLANIMATION:

                    if (AnimationCounter < 60)
                    {
                        RollAnimation();
                    }
                    else
                    {
                        if (SkillCheck)
                        {
                            SkillCheckResult = player.playerDice.RollDice();
                            TrapDie.CurrentTexture = TrapDie.DieTextures["Roll " + SkillCheckResult];
                            if (SkillCheckResult <= player.Rank)
                            {
                                success = true;

                                trapTurnState = TrapTurnState.REVIEW;
                            }

                            else
                            {
                                success = false;

                                switch (TrapResult)
                                {
                                    case 1:
                                        if (player.Food > 0)
                                        {
                                            player.Food -= 1;
                                        }
                                       else player.Health -= 2; 
                                        break;

                                    case 2:
                                        if (player.Gold > 0)
                                        {
                                            player.Gold -= 1;
                                        }
                                       else player.Health -= 2;         
                                        break;

                                    case 3:
                                        if (player.Armor > 0)
                                        {
                                            player.Armor -= 1;
                                        }
                                       else player.Health -= 2;

                                        break;

                                    case 4:

                                        player.Health -= 1;
                                        break;

                                    case 5:

                                        if (player.Experience > 1)
                                        {
                                            player.Experience -= 1;
                                        }
                                       else player.Health -= 2;
                                        break;

                                    case 6:
                                        player.FallBelow();
                                        break;

                                    default:
                                        break;
                                }

                                trapTurnState = TrapTurnState.REVIEW;

                            }
                        }
                        else
                        {
                            TrapResult = player.playerDice.RollDice();
                            TrapDie.CurrentTexture = TrapDie.DieTextures["Roll " + TrapResult];
                            trapTurnState = TrapTurnState.SKILL_CHECK;
                        }
                    }

                    return false;

                case TrapTurnState.SKILL_CHECK:
                    AnimationCounter = 0;
                    HandleButtons(player);
                    return false;

                case TrapTurnState.RESOLVE_TRAP:
                    HandleButtons(player);
                    return false; 

                case TrapTurnState.REVIEW:
                    AnimationCounter = 0;
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
                    case TrapTurnState.ROLL_FOR_TRAP:
                        if (XPos > 770 && XPos < 1018 && YPos > 600 && YPos < 672)
                        {
                            trapTurnState = TrapTurnState.ROLLANIMATION;
                        }
                       
                        break;
                   
                    case TrapTurnState.SKILL_CHECK:
 
                        if (XPos > 770 && XPos < 1018 && YPos > 600 && YPos < 672)
                        {
                            trapTurnState = TrapTurnState.ROLLANIMATION;
                            SkillCheck = true;
                        }

                        break;                       

                    case TrapTurnState.REVIEW:
                        if (XPos > 770 && XPos < 1018 && YPos > 600 && YPos < 672)
                        {
                            trapTurnState = TrapTurnState.COMPLETE;
                        }
                        
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
            sBatch.Draw(CardTexture, new Vector2(274, 130), new Rectangle?(), Color.White, 0f, new Vector2(248, 0), .75f, SpriteEffects.None, 1);
            

            switch (trapTurnState)
            {
                case TrapTurnState.ROLL_FOR_TRAP:
                    sBatch.Draw(Buttons["Roll Die"].ButtonTexture, new Vector2(770, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    sBatch.DrawString(font, "Roll to determine the trap you face", new Vector2(600, 200), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                    break;

                case TrapTurnState.ROLLANIMATION:

                    TrapDie.DrawCombatDie(sBatch);

                    break;

                case TrapTurnState.SKILL_CHECK:

                    sBatch.DrawString(font, "Roll a skill check to evade the trap", new Vector2(600, 250), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                    sBatch.Draw(Buttons["Roll Die"].ButtonTexture, new Vector2(770, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    switch (TrapResult)
                    {
                        case 1:
                            sBatch.DrawString(font, "Mildew: You Lose a Food", new Vector2(600, 200), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                            break;

                        case 2:
                            sBatch.DrawString(font, "TripWire: You Lose a Gold", new Vector2(600, 200), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                            break;

                        case 3:
                            sBatch.DrawString(font, "Acid Mist: Lose an Armor or Two Health", new Vector2(520, 200), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                            break;

                        case 4:
                            sBatch.DrawString(font, "Spring Blades: You Lose a Health", new Vector2(600, 200), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                            break;

                        case 5:
                            sBatch.DrawString(font, "Moving Walls: You Lose a XP", new Vector2(600, 200), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                            break;

                        case 6:
                            sBatch.DrawString(font, "Pit: Lose Two Health and fall an Area Below", new Vector2(525, 200), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                            break;

                        default:
                            break;
                    }
                    TrapDie.DrawCombatDie(sBatch);

                    break;

                case TrapTurnState.REVIEW:

                    sBatch.Draw(Buttons["Done Button"].ButtonTexture, new Vector2(770, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    if (success)
                    {
                        sBatch.DrawString(font, "Success! You avoided the trap", new Vector2(600, 200), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                    }

                    else
                    {
                        sBatch.DrawString(font, "You Failed, accept your fate", new Vector2(650, 200), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0f);
                    }
                    TrapDie.DrawCombatDie(sBatch);
                    break;

                case TrapTurnState.COMPLETE:
                    
                    break;

                default:
                    break;
            }
     
        }

        public void RollAnimation()
        {
            TrapDie.CurrentTexture = TrapDie.DieTextureList[Rng.Next(TrapDie.DieTextureList.Count - 1)];
            AnimationCounter += 5;
        }


    }
}
