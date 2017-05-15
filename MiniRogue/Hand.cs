﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniRogue
{
    class Hand
    {

        private List<Card> playerHand;

        public List<Card> PlayerHand
        {
            get { return playerHand; }
            set { playerHand = value; }
        }

        public Random rGen { get; set; }

        public Card Card1 { get; set; }

        public Card Card2 { get; set; }

        public Card Card3 { get; set; }

        public Card Card4 { get; set; }

        public Card Card5 { get; set; }

        public Card Card6 { get; set; }

        public Card Card7 { get; set; }

        public Hand ()
        {
            rGen = new Random();
            playerHand = new List<Card>();
        }



        public void DrawNewHand(Texture2D enemyTex, Texture2D eventTex, Texture2D merchantTex,
            Texture2D restingTex, Texture2D trapTex, Texture2D treasureTex, Texture2D bossTex, Dictionary<string, Button> buttons,
            Dictionary<string, CombatDice> combatDice, Dictionary<string, CheckBox> checkBoxes)
        {
            playerHand.Add(new Enemy("Enemy", enemyTex, buttons, combatDice, checkBoxes));

            playerHand.Add(new EventCard("EventCard", eventTex, buttons));

            playerHand.Add(new Merchant("Merchant", merchantTex, buttons));

            playerHand.Add(new Resting("Resting", restingTex, buttons));

            playerHand.Add(new Trap("Trap", trapTex, buttons));

            playerHand.Add(new Treasure("Treasure", treasureTex, buttons));

            ShuffleHand();

            PlayerHand.Add(new Boss("Boss", bossTex, buttons, combatDice, checkBoxes));
        }


        public void ShuffleHand()
        {
           
            for (int i = 0; i < 20; i++)
            {
                int TempRNumber = rGen.Next(PlayerHand.Count - 1);
                Card tempCard = playerHand[TempRNumber];
                PlayerHand.RemoveAt(TempRNumber);
                playerHand.Insert(rGen.Next(playerHand.Count - 1), tempCard);
            }

            Card1 = playerHand[0];
            Card2 = playerHand[1];
            Card3 = playerHand[2];
            Card4 = playerHand[3];
            Card5 = playerHand[4];
            Card6 = playerHand[5];

            Card1.LevelXpos = 75;
            Card1.LevelYPos = 260;
            Card2.LevelXpos = 300;
            Card2.LevelYPos = 100;
            Card3.LevelXpos = 300;
            Card3.LevelYPos = 400;
            Card4.LevelXpos = 525;
            Card4.LevelYPos = 260;
            Card5.LevelXpos = 750;
            Card5.LevelYPos = 100;
            Card6.LevelXpos = 750;
            Card6.LevelYPos = 400;
            //Card7 = playerHand[6];
        }
    



        public Card RevealCard()
        {
            Card tempCard;
            int tempInt = rGen.Next(playerHand.Count);
            tempCard = playerHand[tempInt];
            playerHand.RemoveAt(tempInt);
            return tempCard;
        }

        public void DrawHand(SpriteBatch sBatch)
        {
            foreach (var item in playerHand)
            {
                if (item.Flipped)
                {
                    sBatch.Draw(item.CardTexture, new Vector2(item.LevelXpos, item.LevelYPos), new Rectangle?(), Color.White, 0f, new Vector2(), .40f, SpriteEffects.None, 1);
                }

            }
        }



    }
}
