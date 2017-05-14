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
        RESOLVEDIE,
        USEFEAT,
        DEALDAMAGE,
        USESPELL,
        DAMAGEPLAYER,
    }

    class Combat
    {

        public float XPos { get; set; }

        public float YPos { get; set; }

        public Random Rng { get; set; }

        public MouseState CurrentMouseState { get; set; }

        public MouseState PreviousMouseState { get; set; }

        public Dictionary<string, Button> CombatButtons { get; set; }

        public Dictionary<string, CombatDice> CombatDice { get; set; }

        public Dictionary<string, CheckBox> CheckBoxes { get; set; }

        public int ActiveDie { get; set; }

        public int Damage { get; set; }

        public int ExpReward { get; set; }

        public bool InsufficientHealth { get; set; }

        public bool InsufficientXP { get; set; }

        public bool OneBoxChecked { get; set; }

        private int monsterHealth;

        public int Health
        {
            get { return monsterHealth; }
            set
            {
                if ((monsterHealth = value) < 0)
                {
                    monsterHealth = 0;
                }
                else
                {
                    monsterHealth = value;
                }
            }
        }

        CombatState combatState = new CombatState();


        //----------------------CONSTRUCTORS -------------------------

        public Combat(Dictionary<string, Button> combatButtons, Dictionary<string, CombatDice> combatDice, Dictionary<string, CheckBox> checkBoxes)
        {
            CombatButtons = combatButtons;
            CombatDice = combatDice;
            CheckBoxes = checkBoxes;
            Rng = new Random();
            combatDice["Combat Die 1"].Active = true;
            CheckBoxes["Check Box 1"].Active = true;


        }

        //---------------------- METHODS ------------------------

        public void HandleCombat(Player player, MouseState current, MouseState previous, float xPos, float yPos)
        {
            XPos = xPos;
            YPos = yPos;
            ActiveDie = player.Rank;
            SetDieActivations();
            PreviousMouseState = CurrentMouseState;

            // Switch for setting enemy damage.
            switch (player.DungeonLevel)
            {
                case 1:

                    Damage = 2;

                    break;

                case 2:

                    Damage = 4;

                    break;

                case 3:

                    Damage = 6;

                    break;

                case 4:

                    Damage = 8;

                    break;

                case 5:

                    Damage = 10;

                    break;


                default:
                    break;
            }

            switch (combatState)
            {
                case CombatState.ENEMYHEALTHROLL:

                    HandleButtons(player);

                    break;
                case CombatState.ROLLDIE:

                    HandleButtons(player);

                    break;
                case CombatState.RESOLVEDIE:

                    HandleButtons(player);

                    break;
                case CombatState.USEFEAT:

                    HandleButtons(player);


                    break;
                case CombatState.DEALDAMAGE:

                    Health -= (CombatDice["Combat Die 1"].Roll + CombatDice["Combat Die 2"].Roll + CombatDice["Combat Die 3"].Roll + CombatDice["Combat Die 4"].Roll);
                    combatState = CombatState.USESPELL;


                    break;
                case CombatState.USESPELL:

                    combatState = CombatState.DAMAGEPLAYER;

                    break;

                case CombatState.DAMAGEPLAYER:

                    player.Health -= Damage;

                    if (player.Health > 0)
                    {
                        combatState = CombatState.ROLLDIE;
                    }


                    break;
                
                default:
                    break;
            }

        }

        public void DrawCombat(SpriteBatch sBatch, SpriteFont dungeonFont)
        {
       
            sBatch.DrawString(dungeonFont, "Monster Health: " + monsterHealth, new Vector2(425, 50), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);


            switch (combatState)
            {
                case CombatState.ENEMYHEALTHROLL:

                    sBatch.Draw(CombatButtons["Roll Die"].ButtonTexture, new Vector2(450, 250), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    sBatch.DrawString(dungeonFont, "Roll For Monster Health", new Vector2(365, 100), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);

                    break;
                case CombatState.ROLLDIE:

                    sBatch.Draw(CombatButtons["Roll Die"].ButtonTexture, new Vector2(450, 250), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    sBatch.DrawString(dungeonFont, "Roll Combat Dice", new Vector2(400, 100), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);



                    break;
                case CombatState.RESOLVEDIE:

                    // Develop a foreach type method for this type of thing since a normal foreach
                    // will not let you call methods (LINQ?)

                    CombatDice["Combat Die 1"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 2"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 3"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 4"].DrawCombatDie(sBatch);

                    sBatch.Draw(CombatButtons["Use Feat Button"].ButtonTexture, new Vector2(300, 250), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    sBatch.Draw(CombatButtons["Accept Button"].ButtonTexture, new Vector2(650, 250), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    if (CombatDice["Combat Die 1"].Active)
                    {
                        sBatch.DrawString(dungeonFont, CombatDice["Combat Die 1"].Roll.ToString(), new Vector2(250, 600), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);
                    }

                    if (CombatDice["Combat Die 2"].Active)
                    {
                        sBatch.DrawString(dungeonFont, CombatDice["Combat Die 2"].Roll.ToString(), new Vector2(450, 600), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);
                    }

                    if (CombatDice["Combat Die 3"].Active)
                    {
                        sBatch.DrawString(dungeonFont, CombatDice["Combat Die 3"].Roll.ToString(), new Vector2(650, 600), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);
                    }

                    if (CombatDice["Combat Die 4"].Active)
                    {
                        sBatch.DrawString(dungeonFont, CombatDice["Combat Die 4"].Roll.ToString(), new Vector2(850, 600), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 1);
                    }


                    break;
                case CombatState.USEFEAT:

                    CombatDice["Combat Die 1"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 2"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 3"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 4"].DrawCombatDie(sBatch);

                    CheckBoxes["Check Box 1"].DrawCheckBoxes(sBatch);
                    CheckBoxes["Check Box 2"].DrawCheckBoxes(sBatch);
                    CheckBoxes["Check Box 3"].DrawCheckBoxes(sBatch);
                    CheckBoxes["Check Box 4"].DrawCheckBoxes(sBatch);

                    sBatch.Draw(CombatButtons["Spend 2 HP Button"].ButtonTexture, new Vector2(300, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    sBatch.Draw(CombatButtons["Spend 1 XP Button"].ButtonTexture, new Vector2(650, 600), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;
                case CombatState.DEALDAMAGE:
                    break;
                case CombatState.USESPELL:
                    break;

                case CombatState.DAMAGEPLAYER:

                    break;


                default:
                    break;
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


        public void HandleButtons(Player player)
        {

            if (SingleMouseClick())
            {

                switch (combatState)
                {
                    case CombatState.ENEMYHEALTHROLL:

                        if (XPos > 450 && XPos < 698 && YPos > 250 && YPos < 322)
                        {
                            // 100 Helath added to monster health for testing
                            monsterHealth = player.DungeonArea + player.playerDice.RollDice() + 100;
                            Thread.Sleep(500);
                            combatState = CombatState.ROLLDIE;

                        }

                        break;
                    case CombatState.ROLLDIE:

                        if (XPos > 450 && XPos < 698 && YPos > 250 && YPos < 322)
                        {

                            int die1Roll;
                            int die2Roll;
                            int die3Roll;
                            int die4Roll;

                            if (ActiveDie > 0 && CheckBoxes["Check Box 1"].Checked)
                            {
                                die1Roll = Rng.Next(6) + 1;
                                CombatDice["Combat Die 1"].Roll = die1Roll;
                                CombatDice["Combat Die 1"].CurrentTexture = CombatDice["Combat Die 1"].DieTextures["Roll " + die1Roll];
                                CheckBoxes["Check Box 1"].Checked = false;
                            }

                            if (ActiveDie > 1 && CheckBoxes["Check Box 2"].Checked)
                            {
                                die2Roll = Rng.Next(6) + 1;
                                CombatDice["Combat Die 2"].Roll = die2Roll;
                                CombatDice["Combat Die 2"].CurrentTexture = CombatDice["Combat Die 2"].DieTextures["Roll " + die2Roll];
                                CheckBoxes["Check Box 2"].Checked = false;
                            }
                            if (ActiveDie > 2 && CheckBoxes["Check Box 3"].Checked)
                            {
                                die3Roll = Rng.Next(6) + 1;
                                CombatDice["Combat Die 3"].Roll = die3Roll;
                                CombatDice["Combat Die 3"].CurrentTexture = CombatDice["Combat Die 3"].DieTextures["Roll " + die3Roll];
                                CheckBoxes["Check Box 3"].Checked = false;
                            }
                            if (ActiveDie > 3 && CheckBoxes["Check Box 4"].Checked)
                            {
                                die4Roll = Rng.Next(6) + 1;
                                CombatDice["Combat Die 4"].Roll = die4Roll;
                                CombatDice["Combat Die 4"].CurrentTexture = CombatDice["Combat Die 4"].DieTextures["Roll " + die4Roll];
                                CheckBoxes["Check Box 4"].Checked = false;
                            }

                            combatState = CombatState.RESOLVEDIE;

                        }

                        break;
                    case CombatState.RESOLVEDIE:


                        //-------------- This can be shortened after making change to check box ----------------------
                        CheckBoxes["Check Box 1"].Checked = false;
                        CheckBoxes["Check Box 2"].Checked = false;
                        CheckBoxes["Check Box 3"].Checked = false;
                        CheckBoxes["Check Box 4"].Checked = false;

                        CheckBoxes["Check Box 1"].CurrentTexture = CheckBoxes["Check Box 1"].UncheckedTexture;
                        CheckBoxes["Check Box 2"].CurrentTexture = CheckBoxes["Check Box 2"].UncheckedTexture;
                        CheckBoxes["Check Box 3"].CurrentTexture = CheckBoxes["Check Box 3"].UncheckedTexture;
                        CheckBoxes["Check Box 4"].CurrentTexture = CheckBoxes["Check Box 4"].UncheckedTexture;
                        //---------------------------------------------------------------------------------------------

                        if (XPos > 300 && XPos < 548 && YPos > 250 && YPos < 322)
                        {
                            combatState = CombatState.USEFEAT;
                        }

                        if (XPos > 650 && XPos < 898 && YPos > 250 && YPos < 322)
                        {
                            combatState = CombatState.DEALDAMAGE;
                        }



                        break;
                    case CombatState.USEFEAT:


                        if (XPos > 275 && XPos < 325 && YPos > 380 && YPos < 430)
                        {
                            if (CheckBoxes["Check Box 1"].CurrentTexture == CheckBoxes["Check Box 1"].UncheckedTexture)
                            {
                                if (!OneBoxChecked)
                                {
                                    CheckBoxes["Check Box 1"].CurrentTexture = CheckBoxes["Check Box 1"].CheckedTexture;
                                    CheckBoxes["Check Box 1"].Checked = true;
                                    OneBoxChecked = true;
                                }

                            }
                            else
                            {
                                CheckBoxes["Check Box 1"].CurrentTexture = CheckBoxes["Check Box 1"].UncheckedTexture;
                                CheckBoxes["Check Box 1"].Checked = false;
                                OneBoxChecked = false;
                            }

                            
                        }


                        if (XPos > 475 && XPos < 525 && YPos > 380 && YPos < 430)
                        {
                            if (CheckBoxes["Check Box 2"].CurrentTexture == CheckBoxes["Check Box 2"].UncheckedTexture)
                            {
                                if (!OneBoxChecked)
                                {
                                    CheckBoxes["Check Box 2"].CurrentTexture = CheckBoxes["Check Box 2"].CheckedTexture;
                                    CheckBoxes["Check Box 2"].Checked = true;
                                    OneBoxChecked = true;
                                }

                            }
                            else
                            {
                                CheckBoxes["Check Box 2"].CurrentTexture = CheckBoxes["Check Box 2"].UncheckedTexture;
                                CheckBoxes["Check Box 2"].Checked = false;
                                OneBoxChecked = false;
                            }
                            
                            
                        }

                        if (XPos > 675 && XPos < 725 && YPos > 380 && YPos < 430)
                        {
                            if (CheckBoxes["Check Box 3"].CurrentTexture == CheckBoxes["Check Box 3"].UncheckedTexture)
                            {
                                if (!OneBoxChecked)
                                {
                                    CheckBoxes["Check Box 3"].CurrentTexture = CheckBoxes["Check Box 3"].CheckedTexture;
                                    CheckBoxes["Check Box 3"].Checked = true;
                                    OneBoxChecked = true;
                                }

                            }
                            else
                            {
                                CheckBoxes["Check Box 3"].CurrentTexture = CheckBoxes["Check Box 3"].UncheckedTexture;
                                CheckBoxes["Check Box 3"].Checked = false;
                                OneBoxChecked = false;
                            }
                            
                            
                        }

                        if (XPos > 875 && XPos < 925 && YPos > 380 && YPos < 430)
                        {
                            if (CheckBoxes["Check Box 4"].CurrentTexture == CheckBoxes["Check Box 4"].UncheckedTexture)
                            {
                                if (!OneBoxChecked)
                                {
                                    CheckBoxes["Check Box 4"].CurrentTexture = CheckBoxes["Check Box 4"].CheckedTexture;
                                    CheckBoxes["Check Box 4"].Checked = true;
                                    OneBoxChecked = true;
                                }

                            }
                            else
                            {
                                CheckBoxes["Check Box 4"].CurrentTexture = CheckBoxes["Check Box 4"].UncheckedTexture;
                                CheckBoxes["Check Box 4"].Checked = false;
                                OneBoxChecked = false;
                            }


                        }

                        if (XPos > 300 && XPos < 548 && YPos > 600 && YPos < 672)
                        {
                            if (player.Health > 2)
                            {
                                player.Health -= 2;
                                RerollDie();
                            }
                            else { InsufficientHealth = true; }
                        }

                        if (XPos > 650 && XPos < 898 && YPos > 600 && YPos < 672)
                        {
                            if (player.Experience > 1)
                            {
                                player.Experience --;
                                RerollDie();
                            }
                            else { InsufficientXP = true; }
                        }


                        break;
                    case CombatState.DEALDAMAGE:





                        break;
                    case CombatState.USESPELL:
                        break;

                    case CombatState.DAMAGEPLAYER:

                        break;

                    default:
                        break;
                }



            }
        }

        public void SetDieActivations()
        {
            // Set die 2-4 to false
            for (int i = 0; i < 3; i++)
            {
                CombatDice["Combat Die " + (i+2)].Active = false;
                CheckBoxes["Check Box " + (i+2)].Active = false;
            }

            // Activate die and check boxes based on number of active die
            if (ActiveDie > 0)
            {
                CombatDice["Combat Die 1"].Active = true;
                CheckBoxes["Check Box 1"].Active = true;
            }

            if (ActiveDie > 1)
            {
                CombatDice["Combat Die 2"].Active = true;
                CheckBoxes["Check Box 2"].Active = true;
            }
            if (ActiveDie > 2)
            {
                CombatDice["Combat Die 3"].Active = true;
                CheckBoxes["Check Box 3"].Active = true;
            }
            if (ActiveDie > 3)
            {
                CombatDice["Combat Die 4"].Active = true;
                CheckBoxes["Check Box 4"].Active = true;
            }
        }

        public void RerollDie()
        {
            int die1Roll;
            int die2Roll;
            int die3Roll;
            int die4Roll;

            if (ActiveDie > 0 && CheckBoxes["Check Box 1"].Checked)
            {
                die1Roll = Rng.Next(6) + 1;
                if (CombatDice["Combat Die 1"].Roll == 6)
                {
                    CombatDice["Combat Die 1"].Roll += die1Roll;
                }
                else
                {
                    CombatDice["Combat Die 1"].Roll = die1Roll;
                }
                CombatDice["Combat Die 1"].CurrentTexture = CombatDice["Combat Die 1"].DieTextures["Roll " + die1Roll];
                CheckBoxes["Check Box 1"].Checked = false;
            }

            if (ActiveDie > 1 && CheckBoxes["Check Box 2"].Checked)
            {
                die2Roll = Rng.Next(6) + 1;
                if (CombatDice["Combat Die 2"].Roll == 6)
                {
                    CombatDice["Combat Die 2"].Roll += die2Roll;
                }
                else
                {
                    CombatDice["Combat Die 2"].Roll = die2Roll;
                }
                CombatDice["Combat Die 2"].CurrentTexture = CombatDice["Combat Die 2"].DieTextures["Roll " + die2Roll];
                CheckBoxes["Check Box 2"].Checked = false;

            }
            if (ActiveDie > 2 && CheckBoxes["Check Box 3"].Checked)
            {
                die3Roll = Rng.Next(6) + 1;
                if (CombatDice["Combat Die 3"].Roll == 6)
                {
                    CombatDice["Combat Die 3"].Roll += die3Roll;
                }
                else
                {
                    CombatDice["Combat Die 3"].Roll = die3Roll;
                }
                CombatDice["Combat Die 3"].CurrentTexture = CombatDice["Combat Die 3"].DieTextures["Roll " + die3Roll];
                CheckBoxes["Check Box 3"].Checked = false;
            }
            if (ActiveDie > 3 && CheckBoxes["Check Box 4"].Checked)
            {
                die4Roll = Rng.Next(6) + 1;
                if (CombatDice["Combat Die 4"].Roll == 6)
                {
                    CombatDice["Combat Die 4"].Roll += die4Roll;
                }
                else
                {
                    CombatDice["Combat Die 4"].Roll = die4Roll;
                }
                CombatDice["Combat Die 4"].CurrentTexture = CombatDice["Combat Die 4"].DieTextures["Roll " + die4Roll];
                CheckBoxes["Check Box 4"].Checked = false;
            }

            combatState = CombatState.RESOLVEDIE;

        }


    }
}
