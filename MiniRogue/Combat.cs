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
                    break;
                case CombatState.USESPELL:
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

                    sBatch.Draw(CombatButtons["Roll Die"].ButtonTexture, new Vector2(700, 500), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;
                case CombatState.ROLLDIE:

                    sBatch.Draw(CombatButtons["Roll Die"].ButtonTexture, new Vector2(100, 100), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);



                    break;
                case CombatState.RESOLVEDIE:

                    // Develop a foreach type method for this type of thing since a normal foreach
                    // will not let you call methods (LINQ?)

                    CombatDice["Combat Die 1"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 2"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 3"].DrawCombatDie(sBatch);
                    CombatDice["Combat Die 4"].DrawCombatDie(sBatch);

                    sBatch.Draw(CombatButtons["Use Feat Button"].ButtonTexture, new Vector2(500, 250), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);



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


                    break;
                case CombatState.DEALDAMAGE:
                    break;
                case CombatState.USESPELL:
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

                        if (XPos > 700 && XPos < 948 && YPos > 500 && YPos < 572)
                        {
                            monsterHealth = player.DungeonArea + player.playerDice.RollDice();
                            Thread.Sleep(500);
                            combatState = CombatState.ROLLDIE;

                        }

                        break;
                    case CombatState.ROLLDIE:

                        if (XPos > 100 && XPos < 348 && YPos > 100 && YPos < 172)
                        {

                            int die1Roll;
                            int die2Roll;
                            int die3Roll;
                            int die4Roll;

                            die1Roll = Rng.Next(6) + 1;
                            CombatDice["Combat Die 1"].Roll = die1Roll;
                            CombatDice["Combat Die 1"].CurrentTexture = CombatDice["Combat Die 1"].DieTextures["Roll " + die1Roll];

                            if (ActiveDie > 1)
                            {
                                die2Roll = Rng.Next(6) + 1;
                                CombatDice["Combat Die 2"].Roll = die2Roll;
                                CombatDice["Combat Die 2"].CurrentTexture = CombatDice["Combat Die 2"].DieTextures["Roll " + die2Roll];
                            }
                            if (ActiveDie > 2)
                            {
                                die3Roll = Rng.Next(6) + 1;
                                CombatDice["Combat Die 3"].Roll = die3Roll;
                                CombatDice["Combat Die 3"].CurrentTexture = CombatDice["Combat Die 3"].DieTextures["Roll " + die3Roll];
                            }
                            if (ActiveDie > 3)
                            {
                                die4Roll = Rng.Next(6) + 1;
                                CombatDice["Combat Die 4"].Roll = die4Roll;
                                CombatDice["Combat Die 4"].CurrentTexture = CombatDice["Combat Die 4"].DieTextures["Roll " + die4Roll];
                            }

                            combatState = CombatState.RESOLVEDIE;

                        }

                        break;
                    case CombatState.RESOLVEDIE:

                        if (XPos > 500 && XPos < 748 && YPos > 250 && YPos < 322)
                        {
                            combatState = CombatState.USEFEAT;
                        }

                            break;
                    case CombatState.USEFEAT:


                        if (XPos > 275 && XPos < 300 && YPos > 380 && YPos < 430)
                        {
                            if (CheckBoxes["Check Box 1"].CurrentTexture == CheckBoxes["Check Box 1"].UncheckedTexture)
                            {
                                CheckBoxes["Check Box 1"].CurrentTexture = CheckBoxes["Check Box 1"].CheckedTexture;
                                CheckBoxes["Check Box 1"].Checked = true;
                            }
                            else { CheckBoxes["Check Box 1"].CurrentTexture = CheckBoxes["Check Box 1"].UncheckedTexture; }
                            CheckBoxes["Check Box 1"].Checked = false;
                            
                        }


                        if (XPos > 475 && XPos < 500 && YPos > 380 && YPos < 430)
                        {
                            if (CheckBoxes["Check Box 2"].CurrentTexture == CheckBoxes["Check Box 2"].UncheckedTexture)
                            {
                                CheckBoxes["Check Box 2"].CurrentTexture = CheckBoxes["Check Box 2"].CheckedTexture;
                                CheckBoxes["Check Box 2"].Checked = true;
                            }
                            else { CheckBoxes["Check Box 2"].CurrentTexture = CheckBoxes["Check Box 2"].UncheckedTexture; }
                            CheckBoxes["Check Box 2"].Checked = false;
                            
                        }

                        if (XPos > 675 && XPos < 700 && YPos > 380 && YPos < 430)
                        {
                            if (CheckBoxes["Check Box 3"].CurrentTexture == CheckBoxes["Check Box 3"].UncheckedTexture)
                            {
                                CheckBoxes["Check Box 3"].CurrentTexture = CheckBoxes["Check Box 3"].CheckedTexture;
                                CheckBoxes["Check Box 3"].Checked = true;
                            }
                            else { CheckBoxes["Check Box 3"].CurrentTexture = CheckBoxes["Check Box 3"].UncheckedTexture; }
                            CheckBoxes["Check Box 3"].Checked = false;
                            
                        }

                        if (XPos > 875 && XPos < 900 && YPos > 380 && YPos < 430)
                        {
                            if (CheckBoxes["Check Box 4"].CurrentTexture == CheckBoxes["Check Box 4"].UncheckedTexture)
                            {
                                CheckBoxes["Check Box 4"].CurrentTexture = CheckBoxes["Check Box 4"].CheckedTexture;
                                CheckBoxes["Check Box 4"].Checked = true;
                            }
                            else { CheckBoxes["Check Box 4"].CurrentTexture = CheckBoxes["Check Box 4"].UncheckedTexture; }
                            CheckBoxes["Check Box 4"].Checked = false;
                            
                        }

                        break;
                    case CombatState.DEALDAMAGE:
                        break;
                    case CombatState.USESPELL:
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
                CheckBoxes["Check Box " + (i + 2)].Active = false;
            }

            // Activate die and check boxes based on number of active die
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
    }
}
