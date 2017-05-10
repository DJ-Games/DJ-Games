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

        public MouseState CurrentMouseState { get; set; }

        public MouseState PreviousMouseState { get; set; }

        public int Die1 { get; set; }

        public int Die2 { get; set; }

        public int Die3 { get; set; }

        public int Die4 { get; set; }

        public Dictionary<string, Button> CombatButtons { get; set; }

        public Dictionary<string, CombatDice> CombatDice { get; set; }


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

        public Combat(Dictionary<string, Button> combatButtons, Dictionary<string, CombatDice> combatDice)
        {
            CombatButtons = combatButtons;
            CombatDice = combatDice;
        }

        //---------------------- METHODS ------------------------

        public void HandleCombat(Player player, MouseState current, MouseState previous, float xPos, float yPos)
        {
            XPos = xPos;
            YPos = yPos;


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
                    break;
                case CombatState.USEFEAT:
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


                    sBatch.Draw(CombatDice["Combat Die 1"].DieTextures["Roll 1"], new Vector2(250, 450), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    sBatch.Draw(CombatDice["Combat Die 2"].DieTextures["Blank"], new Vector2(450, 450), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    sBatch.Draw(CombatDice["Combat Die 3"].DieTextures["Blank"], new Vector2(650, 450), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    sBatch.Draw(CombatDice["Combat Die 4"].DieTextures["Blank"], new Vector2(850, 450), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);


                    break;
                case CombatState.USEFEAT:
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
                            combatState = CombatState.RESOLVEDIE;

                        }

                        break;
                    case CombatState.RESOLVEDIE:
                        break;
                    case CombatState.USEFEAT:
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
    }
}
