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
        MONSTER_HEALTH_ROLL,
        DAMAGE_ROLL,
        DAMAGE_ROLL_MAXIMIZE,
        REVIEW,
        COMPLETE,

    }

    class Enemy : Card
    {

        //---------------------- PROPERTIES --------------------------

        public int Damage { get; set; }

        public int ExpReward { get; set; }

        private int monsterHealth;

        public int DamageRoll1 { get; set; }
        public int DamageRoll2 { get; set; }
        public int DamageRoll3 { get; set; }
        public int DamageRoll4 { get; set; }

        public int Health
        {
            get { return monsterHealth; }
            set
            {
                if((monsterHealth = value) < 0)
                {
                    monsterHealth = 0;
                }
                else
                {
                    monsterHealth = value;
                }
                

            }
        }


        EnemyTurnState enemyTurnState = new EnemyTurnState();

        //----------------------CONSTRUCTORS -------------------------

        public Enemy(string name, Texture2D cardTexture, Dictionary<string, Button> buttons) : base(name, cardTexture, buttons)
        {

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

            





            switch (enemyTurnState)
            {
                case EnemyTurnState.MONSTER_HEALTH_ROLL:

                    HandleButtons(player);

                    return false;

                case EnemyTurnState.DAMAGE_ROLL:

                    HandleButtons(player);


                    return false;







                    
                case EnemyTurnState.DAMAGE_ROLL_MAXIMIZE:






                    return false;


               

                case EnemyTurnState.REVIEW:
                    return false;


                case EnemyTurnState.COMPLETE:

                    return true;

                default:
                    return false;
            }

        }












        public override void DrawCard(SpriteBatch sBatch,SpriteFont dungeonFont)
        {

            sBatch.Draw(CardTexture, new Vector2(100, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);
            sBatch.DrawString(dungeonFont, "Monster Health: " + monsterHealth, new Vector2(600, 50), Color.Green, 0f, new Vector2(), 2f, SpriteEffects.None, 1);

            switch (enemyTurnState)
            {
                case EnemyTurnState.MONSTER_HEALTH_ROLL:

                    sBatch.DrawString(dungeonFont, "Roll for moster hit points", new Vector2(575, 100), Color.Green, 0f, new Vector2(), 2f, SpriteEffects.None, 1);
                    sBatch.Draw(Buttons["Roll Die"].ButtonTexture, new Vector2(700, 500), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;

                case EnemyTurnState.DAMAGE_ROLL:

                    sBatch.DrawString(dungeonFont, "Roll for damage", new Vector2(575, 100), Color.Green, 0f, new Vector2(), 2f, SpriteEffects.None, 1);
                    sBatch.Draw(Buttons["Roll Die"].ButtonTexture, new Vector2(700, 500), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;


                case EnemyTurnState.DAMAGE_ROLL_MAXIMIZE:
                   





                    break;
                case EnemyTurnState.REVIEW:
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
                    case EnemyTurnState.MONSTER_HEALTH_ROLL:


                        if (XPos > 700 && XPos < 948 && YPos > 500 && YPos < 572)
                        {
                            monsterHealth = player.DungeonArea + player.playerDice.RollDice();
                            enemyTurnState = EnemyTurnState.DAMAGE_ROLL;

                        }

                        break;
                    case EnemyTurnState.DAMAGE_ROLL:

                        if (XPos > 700 && XPos < 948 && YPos > 500 && YPos < 572)
                        {
                            switch (player.Rank)
                            {
                                case 1:
                                    DamageRoll1 = player.playerDice.RollDice();
                                    break;

                                case 2:
                                    DamageRoll1 = player.playerDice.RollDice();
                                    DamageRoll2 = player.playerDice.RollDice();

                                    break;

                                case 3:
                                    DamageRoll1 = player.playerDice.RollDice();
                                    DamageRoll2 = player.playerDice.RollDice();
                                    DamageRoll3 = player.playerDice.RollDice();

                                    break;

                                case 4:
                                    DamageRoll1 = player.playerDice.RollDice();
                                    DamageRoll2 = player.playerDice.RollDice();
                                    DamageRoll3 = player.playerDice.RollDice();
                                    DamageRoll4 = player.playerDice.RollDice();

                                    break;
                                    
                                default:
                                    break;
                            }

                        }

                        break;

                    case EnemyTurnState.DAMAGE_ROLL_MAXIMIZE:





                        break;
                    case EnemyTurnState.REVIEW:
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
