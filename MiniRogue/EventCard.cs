using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MiniRogue
{
    class EventCard : Card
    {


        public bool Success { get; set; }


        public EventCard(string name, Texture2D cardTexture) : base(name, cardTexture)
        {

        }

        //---------------------- METHODS -----------------------------
        public override void HandleCard()
        {
            

            //switch ()
            //{

            //    case 1:
            //        player.Food++;
            //        Thread.Sleep(250);
            //        phase.CurrentPhase++;
            //        break;

            //    case 2:
            //        player.Health += 2;
            //        Thread.Sleep(250);
            //        phase.CurrentPhase++;
            //        break;

            //    case 3:
            //        player.Gold++;
            //        Thread.Sleep(250);
            //        phase.CurrentPhase++;
            //        break;

            //    case 4:
            //        player.Experience += 2;
            //        Thread.Sleep(250);
            //        phase.CurrentPhase++;
            //        break;

            //    case 5:
            //        player.Armor++;
            //        Thread.Sleep(250);
            //        phase.CurrentPhase++;
            //        break;

            //    case 6:

            //        // Copy code from moster state here, then make something better here later.
            //        Thread.Sleep(250);
            //        phase.CurrentPhase++;
            //        break;

            //    default:
            //        break;
            //}
        }

        public override void DrawCard(SpriteBatch sBatch, SpriteFont font, int xPos, int yPos)
        {
            XPos = xPos;
            YPos = yPos;
            sBatch.Draw(CardTexture, CardRectangle, Color.White);
            sBatch.DrawString(font, "Press Space to roll for event.", new Vector2(50, 800), Color.White);
        }


    }
}
