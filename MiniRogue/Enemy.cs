using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MiniRogue
{
    enum EnemyTurnState
    {
        MONSTERHEALTHROLL,
        DAMAGEROLL1,
        COMPLETE,

    }

    class Enemy : Card
    {

        //---------------------- PROPERTIES --------------------------

        public int Damage { get; set; }

        public int ExpReward { get; set; }

        public int Health { get; set; }

        EnemyTurnState enemyTurnState = new EnemyTurnState();

        //----------------------CONSTRUCTORS -------------------------

        public Enemy(string name, Texture2D cardTexture) : base(name, cardTexture)
        {

        }




        //---------------------- METHODS -----------------------------

        public override bool HandleCard(Player player)
        {
            PreviousKbState = CurrentKbState;




            switch (enemyTurnState)
            {
                case EnemyTurnState.MONSTERHEALTHROLL:

                    if (SingleKeyPress(Keys.Space))
                    {
                        Health = player.DungeonArea + player.playerDice.RollDice(1);
                        enemyTurnState = EnemyTurnState.DAMAGEROLL1;
                    }
                    return false;

                case EnemyTurnState.DAMAGEROLL1:

                    // Temp auto move to COMPLETE state till code is finished
                    enemyTurnState = EnemyTurnState.COMPLETE;
                    return false;

                case EnemyTurnState.COMPLETE:
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
