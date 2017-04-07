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

    enum TrapTurnState
    {

    }





    class Trap : Card
    {

        private bool success;

        public bool Success
        {
            get { return success; }
            set { success = value; }
        }


        public Trap(string name, Texture2D cardTexture) : base(name, cardTexture)
        {



        }


        //---------------------- METHODS -----------------------------
        public override bool HandleCard(Player player)
        {
            PreviousKbState = CurrentKbState;

            // temp return till states enum is made.
            return false;

        }

        //public void TrapResult(int option, Player player)
        //{
        //    switch (option)
        //    {
        //        case 1:
        //            player.Food--;
        //            break;
        //        case 2:
        //            player.Gold--;
        //            break;
        //        case 3:
        //            if (player.Armor == 0)
        //            {
        //                player.Health -= 2;
        //            }
        //            else
        //            {
        //                player.Armor--;
        //            }
        //            break;
        //        case 4:
        //            player.Health--;
        //            break;
        //        case 5:
        //            player.Experience--;
        //            break;
        //        case 6:
        //            player.Health -= 2;
        //            // will need to make multi dimensional array to determine this
        //            break;



        //        default:
        //            break;
        //    }
        //}

        public override void DrawCard(SpriteBatch sBatch, SpriteFont font, int xPos, int yPos)
        {
            XPos = xPos;
            YPos = yPos;
            sBatch.Draw(CardTexture, CardRectangle, Color.White);
            sBatch.DrawString(font, "Press Space to roll for skill check.", new Vector2(50, 800), Color.White);
        }

    }
}
