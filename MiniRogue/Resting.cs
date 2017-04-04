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
    enum RestingTurnState
    {
        SELECTION,
        REVIEW, 
        COMPLETE,
        
    }

    class Resting : Card
    {


        public int PlayerChoice { get; set; }

        RestingTurnState restingTurnState = new RestingTurnState();


        public Resting(string name, Texture2D cardTexture) : base(name, cardTexture)
        {
            restingTurnState = RestingTurnState.SELECTION;

        }

        //---------------------- METHODS -----------------------------
        public override bool HandleCard(Player player)
        {
            PreviousKbState = CurrentKbState;

            switch (restingTurnState)
            {
                case RestingTurnState.SELECTION:


                    if (SingleKeyPress(Keys.D1))
                    {
                        player.Experience++;
                        //restingTurnState = RestingTurnState.REVIEW;
                    }
                    if (SingleKeyPress(Keys.D2))
                    {
                        player.Food++;
                        //restingTurnState = RestingTurnState.REVIEW;
                    }
                    if (SingleKeyPress(Keys.D3))
                    {
                        player.Health += 2;
                        //restingTurnState = RestingTurnState.REVIEW;
                    }
                    return false;

                //case RestingTurnState.REVIEW:
                //    return false;

                //case RestingTurnState.COMPLETE:
                //    return true;

                default:
                    return false;
            }




        }

        public override void DrawCard(SpriteBatch sBatch, SpriteFont font, int xPos, int yPos)
        {

                    XPos = xPos;
                    YPos = yPos;
                    sBatch.Draw(CardTexture, CardRectangle, Color.White);
                    sBatch.DrawString(font, "Select a number.", new Vector2(50, 800), Color.White);
                    sBatch.DrawString(font, "1:  +1XP  2:  +1FOOD   3:  +2HP", new Vector2(50, 825), Color.White);

        }

    }
}
