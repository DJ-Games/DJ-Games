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

    enum CombatState
    {
        ENEMYHEALTHROLL,
        ROLLDIE,
        ROLLANIMATION,
        RESOLVEDIE,
        USEFEAT,
        DEALDAMAGE,
        USESPELL,
        DAMAGEPLAYER,
        RESULTS,
        COMPLETE,
    }

    class Combat
    {

        public float XPos { get; set; }

        public float YPos { get; set; }

        public Random Rng { get; set; }

        public MouseState CurrentMouseState { get; set; }

        public MouseState PreviousMouseState { get; set; }

        public Dictionary<string, Button> CombatButtons { get; set; }

        public Dictionary<string, Die> CombatDice { get; set; }

        public int ActiveDie { get; set; }

        public int Damage { get; set; }

        public int ExpReward { get; set; }

        public int GoldReward { get; set; }

        private int monsterHealth;

        public int MonsterHealth
        {
            get { return monsterHealth; }
            set
            {
                if ((monsterHealth = value) < 0)
                {
                    monsterHealth = 0;
                    return;
                }
            }
        }

        public bool BossFight { get; set; }

        public bool MonsterPoisoned { get; set; }

        public bool MonsterFrozen { get; set; }

        CombatState combatState = new CombatState();

        public int AnimationCounter { get; set; }

        public bool HealthFeatAvailable { get; set; }

        public bool ExperienceFeatAvailable { get; set; }


        //----------------------CONSTRUCTORS -------------------------

        public Combat(Dictionary<string, Button> combatButtons, Dictionary<string, Die> combatDice, Dictionary<string, CheckBox> checkBoxes)
        {
            CombatButtons = combatButtons;
            CombatDice = combatDice;
            Rng = new Random();


        }

        //---------------------- METHODS ------------------------

        public bool HandleCombat(Player player, MouseState current, MouseState previous, float xPos, float yPos, bool bossFight)
        {
            XPos = xPos;
            YPos = yPos;
            CurrentMouseState = current;
            PreviousMouseState = previous;

            switch (combatState)
            {
                case CombatState.ENEMYHEALTHROLL:

                    MonsterPoisoned = false;
                    ActiveDie = player.Rank;
                    BossFight = bossFight;
                    SetMonsterStats(player);


                    if (BossFight)
                    {
                        combatState = CombatState.ROLLDIE;
                        return false;
                    }

                    HandleButtons(player);
                    return false;

                case CombatState.ROLLDIE:

                    SetDieActivations();
                    AnimationCounter = 0;
                    MonsterFrozen = false;
                    HandleButtons(player);
                    return false;

                case CombatState.ROLLANIMATION:

                    if (AnimationCounter < 60)
                    {
                        RollAnimation();
                    }
                    else
                    {
                        RollDie();
                        combatState = CombatState.RESOLVEDIE;
                    }

                    return false;

                case CombatState.RESOLVEDIE:

                    HandleButtons(player);

                    return false;

                case CombatState.USEFEAT:

                    HandleButtons(player);

                    return false;

                case CombatState.DEALDAMAGE:
                    MonsterHealth -= (CombatDice["Combat Die 1"].Roll + CombatDice["Combat Die 2"].Roll + CombatDice["Combat Die 3"].Roll + CombatDice["Combat Die 4"].Roll);
                    if (MonsterPoisoned)
                    {
                        MonsterHealth -= 5;
                    }
                    if (MonsterHealth <= 0)
                    {
                        player.Experience += ExpReward;
                        player.Gold += GoldReward;
                        combatState = CombatState.RESULTS;
                    }
                    else { combatState = CombatState.USESPELL; } 
                    return false;

                case CombatState.USESPELL:

                    HandleButtons(player);

                    return false;

                case CombatState.DAMAGEPLAYER:
                    if (MonsterFrozen)
                    {

                    }
                    else { player.Health -= (Damage - player.Armor); }

                    if (player.Health > 0)
                    {
                        combatState = CombatState.ROLLDIE;
                    }

                    else
                    {
                        combatState = CombatState.COMPLETE;
                    }
                    return false;

                case CombatState.RESULTS:

                    HandleButtons(player);

                    return false;

                case CombatState.COMPLETE:
                    HealthFeatAvailable = false;
                    ExperienceFeatAvailable = false;
                    return true;

                default:
                    return false;
            }
        }

        public void DrawCombat(SpriteBatch sBatch, SpriteFont dungeonFont)
        {     
            sBatch.DrawString(dungeonFont, "Monster Health: " + monsterHealth, new Vector2(700, 100), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);

            switch (combatState)
            {
                case CombatState.ENEMYHEALTHROLL:

                    sBatch.Draw(CombatButtons["Roll Die"].ButtonTexture, new Vector2(770, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    sBatch.DrawString(dungeonFont, "Roll For Monster Health", new Vector2(650, 150), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);

                    break;
                case CombatState.ROLLDIE:

                    sBatch.Draw(CombatButtons["Roll Die"].ButtonTexture, new Vector2(770, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    sBatch.DrawString(dungeonFont, "Roll Combat Dice", new Vector2(700, 150), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);

                    break;

                case CombatState.ROLLANIMATION:

                    CombatDice["Combat Die 1"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 2"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 3"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 4"].DrawCombatDie(sBatch);

                    break;

                case CombatState.RESOLVEDIE:

                    CombatDice["Combat Die 1"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 2"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 3"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 4"].DrawCombatDie(sBatch);
                  
                    sBatch.Draw(CombatButtons["Accept Button"].ButtonTexture, new Vector2(770, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    if (CombatDice["Combat Die 1"].Active)
                    {
                        sBatch.DrawString(dungeonFont, CombatDice["Combat Die 1"].Roll.ToString(), new Vector2(585, 400), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);
                    }

                    if (CombatDice["Combat Die 2"].Active)
                    {
                        sBatch.DrawString(dungeonFont, CombatDice["Combat Die 2"].Roll.ToString(), new Vector2(770, 400), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);
                    }

                    if (CombatDice["Combat Die 3"].Active)
                    {
                        sBatch.DrawString(dungeonFont, CombatDice["Combat Die 3"].Roll.ToString(), new Vector2(955, 400), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);
                    }

                    if (CombatDice["Combat Die 4"].Active)
                    {
                        sBatch.DrawString(dungeonFont, CombatDice["Combat Die 4"].Roll.ToString(), new Vector2(1140, 400), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);
                    }

                    break;

                case CombatState.USEFEAT:

                    CombatDice["Combat Die 1"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 2"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 3"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 4"].DrawCombatDie(sBatch);

                    if (CombatDice["Combat Die 1"].Active)
                    {
                        sBatch.DrawString(dungeonFont, CombatDice["Combat Die 1"].Roll.ToString(), new Vector2(585, 400), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);
                    }

                    if (CombatDice["Combat Die 2"].Active)
                    {
                        sBatch.DrawString(dungeonFont, CombatDice["Combat Die 2"].Roll.ToString(), new Vector2(770, 400), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);
                    }

                    if (CombatDice["Combat Die 3"].Active)
                    {
                        sBatch.DrawString(dungeonFont, CombatDice["Combat Die 3"].Roll.ToString(), new Vector2(955, 400), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);
                    }

                    if (CombatDice["Combat Die 4"].Active)
                    {
                        sBatch.DrawString(dungeonFont, CombatDice["Combat Die 4"].Roll.ToString(), new Vector2(1140, 400), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);
                    }

                    if (HealthFeatAvailable)
                    {
                        sBatch.Draw(CombatButtons["Spend 2 HP Button"].ButtonTexture, new Vector2(300, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    }

                    if (ExperienceFeatAvailable)
                    {
                        sBatch.Draw(CombatButtons["Spend 1 XP Button"].ButtonTexture, new Vector2(600, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    }

                    if (CombatDice["Combat Die 1"].CritRollAvailable || CombatDice["Combat Die 2"].CritRollAvailable || CombatDice["Combat Die 3"].CritRollAvailable || CombatDice["Combat Die 4"].CritRollAvailable)
                    {
                        sBatch.Draw(CombatButtons["Crit Roll Button"].ButtonTexture, new Vector2(900, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    }
                    
                    break;

                case CombatState.DEALDAMAGE:

                    break;

                case CombatState.USESPELL:

                    sBatch.DrawString(dungeonFont, "Click on a spell icon to use.", new Vector2(650, 250), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);
                    sBatch.Draw(CombatButtons["Accept Button"].ButtonTexture, new Vector2(770, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;

                case CombatState.DAMAGEPLAYER:

                    break;

                case CombatState.RESULTS:

                    sBatch.DrawString(dungeonFont, "You killed the monster", new Vector2(650, 250), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);
                    sBatch.DrawString(dungeonFont, "You gained " + ExpReward + " EXP!", new Vector2(650, 300), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);
                    sBatch.Draw(CombatButtons["Accept Button"].ButtonTexture, new Vector2(770, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    break;

                default:
                    break;
            }
        }

        public void HandleButtons(Player player)
        {

            if (SingleMouseClick())
            {

                switch (combatState)
                {
                    case CombatState.ENEMYHEALTHROLL:

                        if (XPos > 770 && XPos < 1018 && YPos > 600 && YPos < 672)
                        {                          
                            monsterHealth = player.DungeonArea + player.playerDice.RollDice();
                            combatState = CombatState.ROLLDIE;
                        }

                        break;

                    case CombatState.ROLLDIE:

                        if (XPos > 770 && XPos < 1018 && YPos > 600 && YPos < 672)
                        {

                            combatState = CombatState.ROLLANIMATION;
                        }

                        break;
                    
                    case CombatState.RESOLVEDIE:

                        if (player.Health >= 3)
                        {
                            HealthFeatAvailable = true;
                        }

                        if (player.Experience >= 1)
                        {
                            ExperienceFeatAvailable = true;
                        }

                        if (CombatDice["Combat Die 1"].Active)
                        {
                            if (XPos > 550 && XPos < 650 && YPos > 300 && YPos < 400 && !CombatDice["Combat Die 1"].FeatUsed)
                            {
                                if (CombatDice["Combat Die 1"].Roll % 6 == 0 && CombatDice["Combat Die 1"].Roll != 0)
                                {
                                    CombatDice["Combat Die 1"].CritRollAvailable = true;
                                }
                                else { CombatDice["Combat Die 1"].NeedsRoll = true; }
                                CombatDice["Combat Die 1"].FeatUsed = true;
                                combatState = CombatState.USEFEAT;
                            }
                        }

                        if (CombatDice["Combat Die 2"].Active)
                        {
                            if (XPos > 735 && XPos < 835 && YPos > 300 && YPos < 400 && !CombatDice["Combat Die 2"].FeatUsed)
                            {
                                if (CombatDice["Combat Die 2"].Roll % 6 == 0 && CombatDice["Combat Die 2"].Roll != 0)
                                {
                                    CombatDice["Combat Die 2"].CritRollAvailable = true;
                                }
                                else { CombatDice["Combat Die 2"].NeedsRoll = true; }
                                CombatDice["Combat Die 2"].FeatUsed = true;
                                combatState = CombatState.USEFEAT;
                            }
                        }

                        if (CombatDice["Combat Die 3"].Active)
                        {
                            if (XPos > 920 && XPos < 1020 && YPos > 300 && YPos < 400 && !CombatDice["Combat Die 3"].FeatUsed)
                            {
                                if (CombatDice["Combat Die 3"].Roll % 6 == 0 && CombatDice["Combat Die 3"].Roll != 0)
                                {
                                    CombatDice["Combat Die 3"].CritRollAvailable = true;
                                }
                                else { CombatDice["Combat Die 3"].NeedsRoll = true; }
                                CombatDice["Combat Die 3"].FeatUsed = true;
                                combatState = CombatState.USEFEAT;
                            }
                        }

                        if (CombatDice["Combat Die 4"].Active)
                        {
                            if (XPos > 1105 && XPos < 1205 && YPos > 300 && YPos < 400 && !CombatDice["Combat Die 4"].FeatUsed)
                            {
                                if (CombatDice["Combat Die 4"].Roll % 6 == 0 && CombatDice["Combat Die 4"].Roll != 0)
                                {
                                    CombatDice["Combat Die 4"].CritRollAvailable = true;
                                }
                                else { CombatDice["Combat Die 4"].NeedsRoll = true; }
                                CombatDice["Combat Die 4"].FeatUsed = true;
                                combatState = CombatState.USEFEAT;
                            }
                        }

                        if (XPos > 770 && XPos < 1018 && YPos > 600 && YPos < 672)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                CombatDice["Combat Die " + (i + 1)].FeatUsed = false;
                            }
                            combatState = CombatState.DEALDAMAGE;
                        }

                        break;

                    case CombatState.USEFEAT:

                        AnimationCounter = 0;

                        if (CombatDice["Combat Die 1"].Active)
                        {
                            if (XPos > 550 && XPos < 650 && YPos > 300 && YPos < 400)
                            {
                                CombatDice["Combat Die 1"].FeatUsed = false;
                                CombatDice["Combat Die 1"].NeedsRoll = false;
                                combatState = CombatState.RESOLVEDIE;
                            }
                        }

                        if (CombatDice["Combat Die 2"].Active)
                        {
                            if (XPos > 735 && XPos < 835 && YPos > 300 && YPos < 400)
                            {
                                CombatDice["Combat Die 2"].FeatUsed = false;
                                CombatDice["Combat Die 2"].NeedsRoll = false;
                                combatState = CombatState.RESOLVEDIE;
                            }
                        }

                        if (CombatDice["Combat Die 3"].Active)
                        {
                            if (XPos > 920 && XPos < 1020 && YPos > 300 && YPos < 400)
                            {
                                CombatDice["Combat Die 3"].FeatUsed = false;
                                CombatDice["Combat Die 3"].NeedsRoll = false;
                                combatState = CombatState.RESOLVEDIE;
                            }
                        }

                        if (CombatDice["Combat Die 4"].Active)
                        {
                            if (XPos > 1105 && XPos < 1205 && YPos > 300 && YPos < 400)
                            {
                                CombatDice["Combat Die 4"].FeatUsed = false;
                                CombatDice["Combat Die 4"].NeedsRoll = false;
                                combatState = CombatState.RESOLVEDIE;
                            }
                        }

                        if (HealthFeatAvailable)
                        {
                            if (XPos > 300 && XPos < 548 && YPos > 600 && YPos < 672)
                            {
                                player.Health -= 2;
                                combatState = CombatState.ROLLANIMATION;
                            }
                        }

                        if (ExperienceFeatAvailable)
                        {
                            if (XPos > 600 && XPos < 848 && YPos > 600 && YPos < 672)
                            {
                                player.Experience--;
                                combatState = CombatState.ROLLANIMATION;
                            }
                        }

                        if (CombatDice["Combat Die 1"].CritRollAvailable || CombatDice["Combat Die 2"].CritRollAvailable || CombatDice["Combat Die 3"].CritRollAvailable || CombatDice["Combat Die 4"].CritRollAvailable)
                        {
                            if (XPos > 900 && XPos < 1148 && YPos > 600 && YPos < 672)
                            {
                                combatState = CombatState.ROLLANIMATION;
                            }
                        }

                        break;

                    case CombatState.DEALDAMAGE:

                        break;

                    case CombatState.USESPELL:

                        if (XPos > 770 && XPos < 1018 && YPos > 600 && YPos < 672)
                        {

                            combatState = CombatState.DAMAGEPLAYER; 
                        }

                        if (XPos > 1130 && XPos < 1175 && YPos > 20 && YPos < 65)
                        {
                            CastSpell(player.Spells[0].Name, player);
                            player.RemoveSpell(0);
                            if (MonsterHealth <= 0)
                            {
                                player.Experience += ExpReward;
                                player.Gold += GoldReward;
                                combatState = CombatState.RESULTS;
                            }
                        }

                        if (player.Spells.Count == 2)
                        {
                            if (XPos > 1180 && XPos < 1225 && YPos > 20 && YPos < 65)
                            {
                                CastSpell(player.Spells[1].Name, player);
                                player.RemoveSpell(1);
                                if (MonsterHealth <= 0)
                                {
                                    player.Experience += ExpReward;
                                    player.Gold += GoldReward; 
                                    combatState = CombatState.RESULTS;
                                }
                            }
                        }



                        break;

                    case CombatState.DAMAGEPLAYER:

                        break;

                    case CombatState.RESULTS:

                        if (XPos > 770 && XPos < 1018 && YPos > 600 && YPos < 672)
                        {
                            combatState = CombatState.COMPLETE;
                        }
                            break;


                    default:
                        break;
                }
            }
        }

        public bool SingleMouseClick()
        {
            CurrentMouseState = Mouse.GetState();
            if (CurrentMouseState.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }

        public void SetDieActivations()
        {   
            
            for (int i = 0; i < 4; i++)
            {
                //CombatDice["Combat Die " + (i + 1)].FeatUsed = false;
                CombatDice["Combat Die " + (i + 1)].Active = false;
                CombatDice["Combat Die " + (i + 1)].Roll = 0;
            }

            if (ActiveDie > 0)
            {
                CombatDice["Combat Die 1"].Active = true;
                CombatDice["Combat Die 1"].NeedsRoll = true;
            }

            if (ActiveDie > 1)
            {
                CombatDice["Combat Die 2"].Active = true;
                CombatDice["Combat Die 2"].NeedsRoll = true;
            }
            if (ActiveDie > 2)
            {
                CombatDice["Combat Die 3"].Active = true;
                CombatDice["Combat Die 3"].NeedsRoll = true;
            }
            if (ActiveDie > 3)
            {
                CombatDice["Combat Die 4"].Active = true;
                CombatDice["Combat Die 4"].NeedsRoll = true;
            }
        }

        public void SetMonsterStats(Player player)
        {
            switch (player.DungeonLevel)
            {
                case 1:

                    if (BossFight)
                    {
                        MonsterHealth = 10;
                        Damage = 3;
                        GoldReward = 2;
                        ExpReward = 2;

                    }
                    else
                    {
                        Damage = 2;
                        ExpReward = 1;
                        GoldReward = 0;
                    }

                    break;

                case 2:

                    if (BossFight)
                    {
                        MonsterHealth = 15;
                        Damage = 5;
                        GoldReward = 2;
                        ExpReward = 3;
                    }
                    else
                    {
                        Damage = 4;
                        ExpReward = 1;
                        GoldReward = 0;
                    }


                    break;

                case 3:

                    if (BossFight)
                    {
                        MonsterHealth = 20;
                        Damage = 7;
                        GoldReward = 3;
                        ExpReward = 4;
                    }
                    else
                    {
                        Damage = 6;
                        ExpReward = 2;
                        GoldReward = 0;
                    }


                    break;

                case 4:

                    if (BossFight)
                    {
                        MonsterHealth = 25;
                        Damage = 9;
                        GoldReward = 3;
                        ExpReward = 5;
                    }
                    else
                    {
                        Damage = 8;
                        ExpReward = 2;
                        GoldReward = 0;
                    }

                    break;

                case 5:

                    if (BossFight)
                    {
                        MonsterHealth = 30;
                        Damage = 12;
                        GoldReward = 0;
                        ExpReward = 0;
                    }
                    else
                    {
                        Damage = 10;
                        ExpReward = 3;
                        GoldReward = 0;
                    }


                    break;


                default:
                    break;
            }
        }


        public void CastSpell(string spell, Player player)
        {
            switch (spell)
            {
                case "Fire":
                    monsterHealth -= 8;

                    break;

                case "Healing":
                    player.Health += 8;

                    break;

                case "Ice":
                    MonsterFrozen = true;

                    break;

                case "Poison":
                    MonsterHealth -= 5;
                    MonsterPoisoned = true;

                    break;


                default:
                    break;
            }
        }

        public void RollAnimation()
        {
            if (ActiveDie > 0 && (CombatDice["Combat Die 1"].NeedsRoll || CombatDice["Combat Die 1"].CritRollAvailable))
            {
                CombatDice["Combat Die 1"].CurrentTexture = CombatDice["Combat Die 1"].DieTextureList[Rng.Next(CombatDice["Combat Die 1"].DieTextureList.Count - 1)];
            }
            if (ActiveDie > 1 && (CombatDice["Combat Die 2"].NeedsRoll || CombatDice["Combat Die 2"].CritRollAvailable))
            {
                CombatDice["Combat Die 2"].CurrentTexture = CombatDice["Combat Die 2"].DieTextureList[Rng.Next(CombatDice["Combat Die 2"].DieTextureList.Count - 1)];
            }
            if (ActiveDie > 2 && (CombatDice["Combat Die 3"].NeedsRoll || CombatDice["Combat Die 3"].CritRollAvailable))
            {
                CombatDice["Combat Die 3"].CurrentTexture = CombatDice["Combat Die 3"].DieTextureList[Rng.Next(CombatDice["Combat Die 3"].DieTextureList.Count - 1)];
            }
            if (ActiveDie > 3 && (CombatDice["Combat Die 4"].NeedsRoll || CombatDice["Combat Die 4"].CritRollAvailable))
            {
                CombatDice["Combat Die 4"].CurrentTexture = CombatDice["Combat Die 4"].DieTextureList[Rng.Next(CombatDice["Combat Die 4"].DieTextureList.Count - 1)];
            }
            AnimationCounter+=5;
        }

        public void RollDie()
        {
            int die1Roll;
            int die2Roll;
            int die3Roll;
            int die4Roll;

            if (ActiveDie > 0 && (CombatDice["Combat Die 1"].NeedsRoll || CombatDice["Combat Die 1"].CritRollAvailable))
            {
                die1Roll = Rng.Next(6) + 1;
                if (die1Roll == 1)
                {
                    CombatDice["Combat Die 1"].Roll = die1Roll;
                }
                else if (CombatDice["Combat Die 1"].Roll % 6 == 0 && CombatDice["Combat Die 1"].Roll != 0)
                {
                    CombatDice["Combat Die 1"].Roll += die1Roll;
                }
                else
                {
                    CombatDice["Combat Die 1"].Roll = die1Roll;
                }
                CombatDice["Combat Die 1"].CurrentTexture = CombatDice["Combat Die 1"].DieTextures["Roll " + die1Roll];
                //CombatDice["Combat Die 1"].FeatUsed = true;
                CombatDice["Combat Die 1"].NeedsRoll = false;
            }

            if (ActiveDie > 1 && (CombatDice["Combat Die 2"].NeedsRoll || CombatDice["Combat Die 2"].CritRollAvailable))
            {
                die2Roll = Rng.Next(6) + 1;
                if (die2Roll == 1)
                {
                    CombatDice["Combat Die 1"].Roll = die2Roll;
                }
                else if (CombatDice["Combat Die 2"].Roll % 6 == 0 && CombatDice["Combat Die 2"].Roll != 0)
                {
                    CombatDice["Combat Die 2"].Roll += die2Roll;
                }
                else
                {
                    CombatDice["Combat Die 2"].Roll = die2Roll;
                }
                CombatDice["Combat Die 2"].CurrentTexture = CombatDice["Combat Die 2"].DieTextures["Roll " + die2Roll];
                //CombatDice["Combat Die 2"].FeatUsed = true;
                CombatDice["Combat Die 2"].NeedsRoll = false;
            }
            if (ActiveDie > 0 && (CombatDice["Combat Die 3"].NeedsRoll || CombatDice["Combat Die 3"].CritRollAvailable))
            {
                die3Roll = Rng.Next(6) + 1;
                if (die3Roll == 1)
                {
                    CombatDice["Combat Die 1"].Roll = die3Roll;
                }
                else if (CombatDice["Combat Die 3"].Roll % 6 == 0 && CombatDice["Combat Die 3"].Roll != 0)
                {
                    CombatDice["Combat Die 3"].Roll += die3Roll;
                }
                else
                {
                    CombatDice["Combat Die 3"].Roll = die3Roll;
                }
                CombatDice["Combat Die 3"].CurrentTexture = CombatDice["Combat Die 3"].DieTextures["Roll " + die3Roll];
                //CombatDice["Combat Die 3"].FeatUsed = true;
                CombatDice["Combat Die 3"].NeedsRoll = false;
            }
            if (ActiveDie > 0 && (CombatDice["Combat Die 4"].NeedsRoll || CombatDice["Combat Die 4"].CritRollAvailable))
            {
                die4Roll = Rng.Next(6) + 1;
                if (die4Roll == 1)
                {
                    CombatDice["Combat Die 1"].Roll = die4Roll;
                }
                else if (CombatDice["Combat Die 4"].Roll % 6 == 0 && CombatDice["Combat Die 4"].Roll != 0)
                {
                    CombatDice["Combat Die 4"].Roll += die4Roll;
                }
                else
                {
                    CombatDice["Combat Die 4"].Roll = die4Roll;
                }
                CombatDice["Combat Die 4"].CurrentTexture = CombatDice["Combat Die 4"].DieTextures["Roll " + die4Roll];
                //CombatDice["Combat Die 4"].FeatUsed = true;
                CombatDice["Combat Die 4"].NeedsRoll = false;
            }

            CombatDice["Combat Die 1"].CritRollAvailable = false;
            CombatDice["Combat Die 2"].CritRollAvailable = false;
            CombatDice["Combat Die 3"].CritRollAvailable = false;
            CombatDice["Combat Die 4"].CritRollAvailable = false;

        }


    }
}
