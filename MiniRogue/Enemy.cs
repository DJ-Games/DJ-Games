﻿using System;
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

        private int health;

        public int DamageRoll1 { get; set; }
        public int DamageRoll2 { get; set; }
        public int DamageRoll3 { get; set; }
        public int DamageRoll4 { get; set; }

        public int Health
        {
            get { return health; }
            set
            {
                if((health = value) < 0)
                {
                    health = 0;
                }
                else
                {
                    health = value;
                }
                

            }
        }


        EnemyTurnState enemyTurnState = new EnemyTurnState();

        //----------------------CONSTRUCTORS -------------------------

        public Enemy(string name, Texture2D cardTexture) : base(name, cardTexture)
        {

        }




        //---------------------- METHODS -----------------------------





        /// <summary>
        /// Handles Enemy card turn.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public override bool HandleCard(Player player)
        {
            PreviousKbState = CurrentKbState;

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

                    if (SingleKeyPress(Keys.Space))
                    {
                        health = player.DungeonArea + player.playerDice.RollDice();
                        enemyTurnState = EnemyTurnState.DAMAGE_ROLL;
                    }
                    return false;

                case EnemyTurnState.DAMAGE_ROLL:


                    if (Health > 0)
                    {

                        if (SingleKeyPress(Keys.Space))
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
                    } 
                    return false;
                case EnemyTurnState.DAMAGE_ROLL_MAXIMIZE:

                    







                    if (Health > 0)
                    {
                        player.Health -= Damage;
                    }
        
                    else
                    {
                        switch (player.DungeonLevel)
                        {
                            case 1:

                                player.Experience += 1;

                                break;

                            case 2:

                                player.Experience += 2; 

                                break;

                            case 3:

                                player.Experience += 2;

                                break;

                            case 4:

                                player.Experience += 3;

                                break;

                            case 5:

                                player.Experience += 3;

                                break;

                            default:
                                break;
                        }
    player.HasFoughtMonster = true;
                        enemyTurnState = EnemyTurnState.COMPLETE;
                    }

                    return false; 

                case EnemyTurnState.REVIEW:
                    return false;


                case EnemyTurnState.COMPLETE:
                    Thread.Sleep(2000);
                    return true;

                default:
                    return false;
            }

        }












        public override void DrawCard(SpriteBatch sBatch,SpriteFont font, int xPos, int yPos)
        {
            XPos = xPos;
            YPos = yPos;
            sBatch.Draw(CardTexture, CardRectangle, Color.White);
            sBatch.DrawString(font, "Press Space to roll for monster difficulty.", new Vector2(50, 800), Color.White);
            sBatch.DrawString(font, "Monster Health: " + Health, new Vector2(50, 825), Color.White);

            switch (enemyTurnState)
            {
                case EnemyTurnState.MONSTER_HEALTH_ROLL:
                    break;
                case EnemyTurnState.DAMAGE_ROLL:
                    break;
                case EnemyTurnState.DAMAGE_ROLL_MAXIMIZE:
                    
                    //sBatch.DrawString(font, "Player Die")



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
