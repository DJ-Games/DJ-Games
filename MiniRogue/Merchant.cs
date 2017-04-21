﻿using System;
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
        BUYSPELL,
        SELLSPELL,
        COMPLETE,
    }
        



    class Merchant : Card
    {


        // Properties
        public int SellCost { get; set; }

        public int BuyCost { get; set; }

        MerchantTurnState merchantTurnState;

        //Constructors

        public Merchant(string name, Texture2D cardTexture, Dictionary<string, Button> buttons) : base(name, cardTexture, buttons)
        {
            merchantTurnState = new MerchantTurnState();
            CurrentButtons = new List<Button>();
        }




        //---------------------- METHODS -----------------------------

        /// <summary>
        /// NEED TO ADD SPELL CODE LATER
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public override bool HandleCard(Player player, MouseState current, MouseState previous, float xPos, float yPos)
        {
            XPos = xPos;
            YPos = yPos;

            switch (merchantTurnState)
            {
                case MerchantTurnState.BUY:
                    LoadMerchantButtons();
                    HandleButtons(player);
                    

                    return false;

                case MerchantTurnState.BUYSPELL:
                    LoadSpellsButtons();

                    return false;

                case MerchantTurnState.SELLSPELL:
                    LoadSpellsButtons();

                    return false;


                case MerchantTurnState.COMPLETE:


                    return true;

                default:
                    return false;


            }
        }

        public override void DrawCard(SpriteBatch sBatch, SpriteFont font)
        {

            sBatch.Draw(CardTexture, new Vector2(100, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);

            switch (merchantTurnState)
            {
                         
                case MerchantTurnState.BUY:
                    sBatch.DrawString(font, "What would you like to buy or sell?", new Vector2(500, 200), Color.White);
                    int counter = 240;
                    foreach (var item in CurrentButtons)
                    {
                        sBatch.Draw(item.ButtonTexture, new Vector2(525, counter), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                        counter += 80;
                    }
                    sBatch.Draw(Buttons["Green Armor Piece Button"].ButtonTexture, new Vector2(800, 240), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    sBatch.Draw(Buttons["Green Spells Button"].ButtonTexture, new Vector2(800, 320), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);

                    break;
                case MerchantTurnState.BUYSPELL:
                    int counter2 = 240;
                    foreach (var item in CurrentButtons)
                    {
                        sBatch.Draw(item.ButtonTexture, new Vector2(525, counter2), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                        counter2 += 80;
                    }

                    break;
                case MerchantTurnState.SELLSPELL:


                    break;
                case MerchantTurnState.COMPLETE:
                    break;
                default:
                    break;
            }




        }


        public void LoadMerchantButtons()
        {
            CurrentButtons.Clear();
            CurrentButtons.Add(Buttons["Green Ration Button"]);
            CurrentButtons.Add(Buttons["Green Health Potion Button"]);
            CurrentButtons.Add(Buttons["Green Big Health Potion Button"]);
            CurrentButtons.Add(Buttons["Green Armor Piece Button"]);
            CurrentButtons.Add(Buttons["Green Spells Button"]);
        }

        public void LoadSpellsButtons()
        {
            CurrentButtons.Clear();
            CurrentButtons.Add(Buttons["Green Fireball Spell Button"]);
            CurrentButtons.Add(Buttons["Green Ice Spell Button"]);
            CurrentButtons.Add(Buttons["Green Poison Spell Button"]);
            CurrentButtons.Add(Buttons["Green Healing Spell Button"]);
        }

        public void HandleButtons(Player player)
        {
            if (SingleMouseClick())
            {
                switch (merchantTurnState)
                {
                    case MerchantTurnState.BUY:


                        if (XPos > 525 && XPos < 773 && YPos > 240 && YPos < 312)
                        {
                            player.Gold--;
                            player.Food++;
                        }

                        if (XPos > 525 && XPos < 773 && YPos > 320 && YPos < 392)
                        {
                            player.Gold--;
                            player.Health++;
                        }

                        if (XPos > 525 && XPos < 773 && YPos > 400 && YPos < 472)
                        {
                            player.Gold -= 3;
                            player.Health += 4;
                        }

                        if (XPos > 525 && XPos < 773 && YPos > 480 && YPos < 552)
                        {
                            player.Gold -= 6;
                            player.Armor++;
                        }

                        if (XPos > 525 && XPos < 773 && YPos > 560 && YPos < 632)
                        {
                            merchantTurnState = MerchantTurnState.BUYSPELL;
                        }

                        if (XPos > 800 && XPos < 1048 && YPos > 240 && YPos < 312)
                        {
                            player.Gold += 3;
                            player.Armor--;
                        }

                        if (XPos > 800 && XPos < 1048 && YPos > 320 && YPos < 392)
                        {

                        }


                        break;
                    case MerchantTurnState.BUYSPELL:

                    case MerchantTurnState.SELLSPELL:


                        break;


                        break;
                    case MerchantTurnState.COMPLETE:
                        break;
                    default:
                        break;
                }

            }
        }

    }
}
