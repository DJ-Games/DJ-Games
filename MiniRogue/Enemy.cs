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
    enum EnemyTurnState
    {
        STARTCOMBAT,
        COMBAT,
        COMPLETE,

    }

    class Enemy : Card
    {

        //---------------------- PROPERTIES --------------------------

        public Combat CurrentCombat { get; set; }

        public Dictionary<string, CombatDice> CombatDice { get; set; }

        public Dictionary<string, CheckBox> CheckBoxes { get; set; }


        EnemyTurnState enemyTurnState = new EnemyTurnState();

        //----------------------CONSTRUCTORS -------------------------

        public Enemy(string name, Texture2D cardTexture, Texture2D cardBack, Dictionary<string, Button> buttons) : base(name, cardTexture, cardBack, buttons)
        {

        }

        public Enemy(string name, Texture2D cardTexture, Texture2D cardBack, Dictionary<string, Button> buttons, Dictionary<string, CombatDice> combatDice, Dictionary<string, CheckBox> checkBoxes) : base(name, cardTexture, cardBack, buttons)
        {
            Buttons = buttons;
            CombatDice = combatDice;
            CheckBoxes = checkBoxes;

        }


        //---------------------- METHODS -----------------------------


        /// <summary>
        /// Handles Enemy card turn.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public override bool HandleCard(Player player, MouseState current, MouseState previous, float xPos, float yPos)
        {

            XPos = xPos;
            YPos = yPos;
            PreviousMouseState = CurrentMouseState;

            switch (enemyTurnState)
            {

                case EnemyTurnState.STARTCOMBAT:
                    HandleButtons(player);
                    return false;

                case EnemyTurnState.COMBAT:

                    if (CurrentCombat.HandleCombat(player, CurrentMouseState, PreviousMouseState, XPos, yPos, false))
                    {
                        enemyTurnState = EnemyTurnState.COMPLETE;
                    }
                    


                    return false;


                case EnemyTurnState.COMPLETE:
                    player.HasFoughtMonster = true;
                    return true;

                default:
                    return false;
            }

        }


        public override void DrawCard(SpriteBatch sBatch,SpriteFont dungeonFont)
        {

            sBatch.Draw(CardTexture, new Vector2(100, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);

            switch (enemyTurnState)
            {
                case EnemyTurnState.STARTCOMBAT:

                    sBatch.Draw(Buttons["Combat Button"].ButtonTexture, new Vector2(700, 500), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    

                    break;

                case EnemyTurnState.COMBAT:

                    CurrentCombat.DrawCombat(sBatch, dungeonFont);
                    break;
                case EnemyTurnState.COMPLETE:
                    break;
                default:
                    break;
            }

        }

        public void HandleButtons(Player player)
        {

            if (SingleMouseClick())
            {
                switch (enemyTurnState)
                {
                    case EnemyTurnState.STARTCOMBAT:


                        if (XPos > 700 && XPos < 948 && YPos > 500 && YPos < 572)
                        {

                            CurrentCombat = new Combat(Buttons, CombatDice, CheckBoxes);
                            enemyTurnState = EnemyTurnState.COMBAT;

                        }

                        break;
                    case EnemyTurnState.COMBAT:
                        break;
                    case EnemyTurnState.COMPLETE:
                        break;
                    default:
                        break;
                }
            }

       }




    }
}
