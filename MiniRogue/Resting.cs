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


        public Resting(string name, Texture2D cardTexture, Dictionary<string, Button> buttons) : base(name, cardTexture, buttons)
        {
            restingTurnState = RestingTurnState.SELECTION;

        }

        //---------------------- METHODS -----------------------------
        public override bool HandleCard(Player player, MouseState current, MouseState previous, float xPos, float yPos)
        {
            PreviousKbState = CurrentKbState;

            switch (restingTurnState)
            {
                case RestingTurnState.SELECTION:


                    if (SingleKeyPress(Keys.D1))
                    {
                        player.Experience++;
                        restingTurnState = RestingTurnState.REVIEW;
                    }
                    if (SingleKeyPress(Keys.D2))
                    {
                        player.Food++;
                        restingTurnState = RestingTurnState.REVIEW;
                    }
                    if (SingleKeyPress(Keys.D3))
                    {
                        player.Health += 2;
                        restingTurnState = RestingTurnState.REVIEW;
                    }
                    return false;

                case RestingTurnState.REVIEW:
                    return false;

                case RestingTurnState.COMPLETE:
                    return true;

                default:
                    return false;
            }




        }

        public override void DrawCard(SpriteBatch sBatch, SpriteFont font, int xPos, int yPos)
        {

            XPos = xPos;
            YPos = yPos;
            sBatch.Draw(CardTexture, new Vector2(100, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);
            sBatch.DrawString(font, "Select a number.", new Vector2(50, 800), Color.White);

            switch (restingTurnState)
            {
                case RestingTurnState.SELECTION:
                    sBatch.DrawString(font, "1:  +1XP  2:  +1FOOD   3:  +2HP", new Vector2(50, 825), Color.White);
                    break;
                case RestingTurnState.REVIEW:
                    break;
                case RestingTurnState.COMPLETE:
                    break;
                default:
                    break;
            }




        }

    }
}
