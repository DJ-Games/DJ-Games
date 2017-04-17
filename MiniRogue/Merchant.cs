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

    enum MerchantTurnState
    {
        BUY,
        REVIEW,
        COMPLETE,
    }
        




    class Merchant : Card
    {


        // Properties
        public int SellCost { get; set; }

        public int BuyCost { get; set; }

        MerchantTurnState merchantTurnState;

        //Constructors

        public Merchant(string name, Texture2D cardTexture) : base(name, cardTexture)
        {
            merchantTurnState = new MerchantTurnState();
        }




        //---------------------- METHODS -----------------------------

        /// <summary>
        /// NEED TO ADD SPELL CODE LATER
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public override bool HandleCard(Player player, MouseState current, MouseState previous, float xPos, float yPos)
        {
            PreviousKbState = CurrentKbState;

            switch (merchantTurnState)
            {
                case MerchantTurnState.BUY:

                    if (SingleKeyPress(Keys.D1))
                    {
                        if (player.Gold >= 1)
                        {
                            player.Food++;
                            player.Gold--;
                        }
                    }
                    if (SingleKeyPress(Keys.D2))
                    {
                        if (player.Gold >= 1)
                        {
                            player.Health++;
                            player.Gold--;
                        }

                    }
                    if (SingleKeyPress(Keys.D3))
                    {
                        if (player.Gold >= 3)
                        {
                            player.Health += 4;
                            player.Gold -= 3;
                        }
                    }
                    if (SingleKeyPress(Keys.D4))
                    {
                        if (player.Gold >= 6)
                        {
                            player.Armor++;
                            player.Gold -= 6;
                        }
                    }
                    if (SingleKeyPress(Keys.D5))
                    {
                        //Add spell code later
                    }
                    if (SingleKeyPress(Keys.D6))
                    {
                        if (player.Armor >= 1)
                        {
                            player.Armor--;
                            player.Gold += 3;
                        }
                    }
                    if (SingleKeyPress(Keys.D7))
                    {
                        // Add spell code later
                    }
                    if (SingleKeyPress(Keys.Space))
                    {
                        merchantTurnState = MerchantTurnState.REVIEW;
                    }


                    return false;

                    break;
                case MerchantTurnState.REVIEW:

                    merchantTurnState = MerchantTurnState.COMPLETE;
                    return false;

                    break;

                case MerchantTurnState.COMPLETE:

                    return true;
                    break;


                default:
                    return false;
                    break;

            }
        }

        public override void DrawCard(SpriteBatch sBatch, SpriteFont font, int xPos, int yPos)
        {
            XPos = xPos;
            YPos = yPos;
            sBatch.Draw(CardTexture, new Vector2(100, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);

            switch (merchantTurnState)
            {
                         
                case MerchantTurnState.BUY:
                    sBatch.DrawString(font, "What would you like to buy or sell?", new Vector2(50, 800), Color.White);
                    sBatch.DrawString(font, "BUY  1: +1 Food(1G)  2: +1HP(1G)  3: +4HP(3G)", new Vector2(50, 820), Color.White);
                    sBatch.DrawString(font, "4: +1 Armor(6G)  5: +Spell(8G)", new Vector2(50, 840), Color.White);
                    sBatch.DrawString(font, "SPELL 6: -1 Armor(3G)  7: -Spell(4)", new Vector2(50, 860), Color.White);
                    sBatch.DrawString(font, "Press space when complete.", new Vector2(50, 880), Color.White);

                    break;
                case MerchantTurnState.REVIEW:
                    break;
                case MerchantTurnState.COMPLETE:
                    break;
                default:
                    break;
            }




        }






    }
}
