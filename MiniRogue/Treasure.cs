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





    class Treasure : Card
    {

        public bool Guarded { get; set; }

        public bool Sucess { get; set; }

        public Treasure(string name, Texture2D cardTexture) : base(name, cardTexture)
        {

        }


        //---------------------- METHODS -----------------------------
        public override bool HandleCard(Player player)
        {
            PreviousKbState = CurrentKbState;

            // temp return till states enum is made.
            return false;

        }





        //public void TreasureResult(int option, Player player)
        //{
        //    switch (option)
        //    {
        //        case 1:
        //            player.Armor++;
        //            break;
        //        case 2:
        //            player.Experience += 2;
        //            break;
        //        case 3:
        //            player.Spells.Add("Fireball");
        //            break;
        //        case 4:
        //            player.Spells.Add("Freeze");
        //            break;
        //        case 5:
        //            player.Spells.Add("Poison");
        //            break;
        //        case 6:
        //            player.Spells.Add("Healing");
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
            sBatch.DrawString(font, "Press Space to roll for attempt.", new Vector2(50, 800), Color.White);
        }

    }
}
