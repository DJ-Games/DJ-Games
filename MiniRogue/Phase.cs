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
        //------------------ PROPERTIES --------------------------

        public int CurrentPhase { get; set; }

        public Card CurrentCard { get; set; }

        //-------------------- METHODS ---------------------------

        public void HandlePhase(Player player,  Hand playerHand)
        {


            switch (CurrentPhase)
            {
                case 1:
                    CurrentCard = playerHand.RevealCard();
                    CurrentCard.HandleCard(player);
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

            sBatch.DrawString(font, "XP: " + player.Experience, new Vector2(50, 900), Color.White);
            sBatch.DrawString(font, "Armor: " + player.Armor, new Vector2(150, 900), Color.White);
            sBatch.DrawString(font, "HP: " + player.Health, new Vector2(250, 900), Color.White);
            sBatch.DrawString(font, "Gold: " + player.Gold, new Vector2(350, 900), Color.White);
            CurrentCard.DrawCard(sBatch, font, 45, 40);

            switch (CurrentPhase)
            {
                case 1:




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
    }
}
