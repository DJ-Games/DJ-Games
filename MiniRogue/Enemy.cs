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
        MONSTERHEALTHROLL,
        DAMAGEROLL,
        COMPLETE,

    }

    class Enemy : Card
    {

        //---------------------- PROPERTIES --------------------------

        public int Damage { get; set; }

        public int ExpReward { get; set; }

        private int health;

        public int Health
        {
            get { return health; }
            set
            {
                if (health-value<0)
                {
                    health = 0;
                }
                

            }
        }


        EnemyTurnState enemyTurnState = new EnemyTurnState();

        //----------------------CONSTRUCTORS -------------------------

        public Enemy(string name, Texture2D cardTexture) : base(name, cardTexture)
        {

        }




        //---------------------- METHODS -----------------------------

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
                case EnemyTurnState.MONSTERHEALTHROLL:

                    if (SingleKeyPress(Keys.Space))
                    {
                        health = player.DungeonArea + player.playerDice.RollCombatDice(1);
                        enemyTurnState = EnemyTurnState.DAMAGEROLL;
                    }
                    return false;

                case EnemyTurnState.DAMAGEROLL:

                    if (SingleKeyPress(Keys.Space))
                    {
                        while (Health > 0)
                        {
                            switch (player.Rank)
                            {
                                case 1:

                                    Health -= player.playerDice.RollCombatDice(1);

                                    break;


                                case 2:

                                    Health -= player.playerDice.RollCombatDice(2);

                                    break;


                                case 3:

                                    Health -= player.playerDice.RollCombatDice(3);

                                    break;


                                case 4:

                                    Health -= player.playerDice.RollCombatDice(4);

                                    break;

                                default:
                                    break;
                            }

                            if (Health > 0)
                            {
                                player.Health -= Damage;


                            }


                        }
                    }


                 


                    enemyTurnState = EnemyTurnState.COMPLETE;

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
        }



    }
}
