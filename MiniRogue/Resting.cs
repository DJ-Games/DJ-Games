using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniRogue
{
    class Resting : Card
    {




        public Resting(string name, Texture2D cardTexture) : base(name, cardTexture)
        {


        }

        //---------------------- METHODS -----------------------------
        public override void HandleCard()
        {
        //    int playerChoice;

        //    if (SingleKeyPress(Keys.D1))
        //    {
        //        playerChoice = 1;
        //    }
        //    else if (SingleKeyPress(Keys.D2))
        //    {
        //        playerChoice = 2;
        //    }
        //    else if (SingleKeyPress(Keys.D3))
        //    {
        //        playerChoice = 3;
        //    }
        //    else
        //    {
        //        playerChoice = 0;
        //    }


        //    switch (playerChoice)
        //    {
        //        case 1:
        //            player.Experience++;
        //            phase.CurrentPhase++;
        //            break;

        //        case 2:
        //            player.Food++;
        //            phase.CurrentPhase++;
        //            break;

        //        case 3:
        //            player.Health += 2;
        //            phase.CurrentPhase++;
        //            break;

        //        default:
        //            break;
        //    }
        }

        public override void DrawCard(SpriteBatch sBatch, SpriteFont font, int xPos, int yPos)
        {
            XPos = xPos;
            YPos = yPos;
            sBatch.Draw(CardTexture, CardRectangle, Color.White);
            sBatch.DrawString(font, "Select a number.", new Vector2(50, 800), Color.White);
        }

    }
}
