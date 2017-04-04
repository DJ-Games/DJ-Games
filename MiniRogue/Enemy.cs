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
    class Enemy : Card
    {

        //---------------------- PROPERTIES --------------------------

        public int Damage { get; set; }

        public int ExpReward { get; set; }

        public int Health { get; set; }

        //----------------------CONSTRUCTORS -------------------------

        public Enemy(string name, Texture2D cardTexture) : base(name, cardTexture)
        {

        }




        //---------------------- METHODS -----------------------------

        public override void HandleCard(Player player)
        {
            if (SingleKeyPress(Keys.Space))
            {
                Health = 10; // player.DungeonArea + player.playerDice.RollDice(1);
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
