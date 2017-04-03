using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniRogue
{
    class Phase
    {

        public int CurrentPhase { get; set; }

        public Card CurrentCard { get; set; }




        public void HandlePhase(Player player,  Hand playerHand)
        {


            switch (CurrentPhase)
            {
                case 1:
                    CurrentCard = playerHand.RevealCard();
                    CurrentCard.HandleCard(CurrentCard.Name, player, this);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                default:
                    break;
            }

        }

        public void DrawPhase(SpriteBatch sBatch, SpriteFont font, Player player)
        {
            switch (CurrentPhase)
            {
                case 1:

                    CurrentCard.DrawCard(sBatch, 45, 40);
                    sBatch.DrawString(font, "XP: " + player.Experience, new Vector2(50, 900), Color.White);
                    sBatch.DrawString(font, "Armor: " + player.Armor, new Vector2(150, 900), Color.White);
                    sBatch.DrawString(font, "HP: " + player.Health, new Vector2(250, 900), Color.White);
                    sBatch.DrawString(font, "Gold: " + player.Gold, new Vector2(350, 900), Color.White);

                    switch (CurrentCard.Name)
                    {

                        case "EventCard":
                            sBatch.DrawString(font, "Press space to roll die.", new Vector2(50, 800), Color.White);
                            //sBatch.DrawString(font, CurrentRoll.ToString(), new Vector2(400, 800), Color.White);
                            break;

                        case "Resting":
                            sBatch.DrawString(font, "1: XP", new Vector2(50, 825), Color.White);
                            sBatch.DrawString(font, "2: Food", new Vector2(50, 850), Color.White);
                            sBatch.DrawString(font, "3: Health", new Vector2(50, 875), Color.White);



                            break;


                        case "Treasure":

                            break;

                        case "Monster":

                            break;

                        case "Trap":

                            break;

                        case "Merchant":

                            break;



                        default:
                            break;
                    }





                    break;


                case 2:

                    CurrentCard.DrawCard(sBatch, 45, 40);
                    sBatch.DrawString(font, "XP: " + player.Experience, new Vector2(50, 900), Color.White);
                    sBatch.DrawString(font, "Armor: " + player.Armor, new Vector2(150, 900), Color.White);
                    sBatch.DrawString(font, "HP: " + player.Health, new Vector2(250, 900), Color.White);
                    sBatch.DrawString(font, "Gold: " + player.Gold, new Vector2(350, 900), Color.White);



                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                default:
                    break;
            }
        }
    }
}
