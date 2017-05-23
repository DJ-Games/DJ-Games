using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MiniRogue
{

    enum MerchantTurnState
    {
        BUYSELL,
        BUYSPELL,
        CONFIRMBUY,
        SELLSPELL,
        CONFIRMSELL,
        INSUFFICENTFUNDS,
        COMPLETE,
    }
        

    class Merchant : Card
    {


        // Properties
        public int SellCost { get; set; }

        public int BuyCost { get; set; }

        public string Selection { get; set; }

       

        MerchantTurnState merchantTurnState;

        //Constructors

        public Merchant(string name, Texture2D cardTexture, Texture2D cardBack, Dictionary<string, Button> buttons) : base(name, cardTexture, cardBack, buttons)
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
            PreviousMouseState = CurrentMouseState;


            switch (merchantTurnState)
            {
              
                case MerchantTurnState.BUYSELL:
                    LoadMerchantButtons();
                    HandleButtons(player);
                    
                    return false;


                case MerchantTurnState.INSUFFICENTFUNDS:
                    Thread.Sleep(500);

                    merchantTurnState = MerchantTurnState.BUYSELL;
                    return false;


                case MerchantTurnState.BUYSPELL:
                    LoadSpellsButtons();
                    HandleButtons(player);

                    return false;

                case MerchantTurnState.SELLSPELL:
                    LoadSpellsButtons();
                    HandleButtons(player);

                    return false;

                case MerchantTurnState.CONFIRMBUY:
                    HandleButtons(player);

                    if (player.Gold < 0)
                    {
                      return true;
                    }

                    else 
                    return false;

                case MerchantTurnState.CONFIRMSELL:
                    HandleButtons(player);

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

                case MerchantTurnState.BUYSELL:
                    sBatch.Draw(Buttons["Done Button"].ButtonTexture, new Vector2(660, 100), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);  
                    sBatch.DrawString(font, "Would you like to buy or sell?", new Vector2(500, 200), Color.White);
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
                case MerchantTurnState.CONFIRMBUY:
                    sBatch.Draw(Buttons["Confirm Purchase Menu"].ButtonTexture, new Vector2(500, 320), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    break;
                case MerchantTurnState.CONFIRMSELL:
                    sBatch.Draw(Buttons["Confirm Sale Menu"].ButtonTexture, new Vector2(500, 320), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                    break;
                case MerchantTurnState.COMPLETE:
                    break;
                default:
                    break;
            }




        }

        /// <summary>
        /// Load Merchant Buttons Method
        /// </summary>
        public void LoadMerchantButtons()
        {
            CurrentButtons.Clear();
            CurrentButtons.Add(Buttons["Green Ration Button"]);
            CurrentButtons.Add(Buttons["Green Health Potion Button"]);
            CurrentButtons.Add(Buttons["Green Big Health Potion Button"]);
            CurrentButtons.Add(Buttons["Green Armor Piece Button"]);
            CurrentButtons.Add(Buttons["Green Spells Button"]);
            
        }

        /// <summary>
        /// Load Spells Buttons Method
        /// </summary>
        public void LoadSpellsButtons()
        {
            CurrentButtons.Clear();
            CurrentButtons.Add(Buttons["Green Fireball Spell Button"]);
            CurrentButtons.Add(Buttons["Green Ice Spell Button"]);
            CurrentButtons.Add(Buttons["Green Poison Spell Button"]);
            CurrentButtons.Add(Buttons["Green Healing Spell Button"]);
        }

        /// <summary>
        /// Handle Buttons Method
        /// </summary>
        /// <param name="player"></param>
        public void HandleButtons(Player player)
        {
            // If player clicks left mouse button. 
            if (SingleMouseClick())
            {
                switch (merchantTurnState)
                {
                    // ------- BUY / SELL STATE -------
                    case MerchantTurnState.BUYSELL:

                        if (XPos > 660 && XPos < 908 && YPos > 100 && YPos < 172)
                        {
                            merchantTurnState = MerchantTurnState.COMPLETE; 

                        }
                        if (XPos > 525 && XPos < 773 && YPos > 240 && YPos < 312)
                        {
                            Selection = "Buy Ration";
                            merchantTurnState = MerchantTurnState.CONFIRMBUY;
                            
                        }

                        if (XPos > 525 && XPos < 773 && YPos > 320 && YPos < 392)
                        {
                            Selection = "Buy Potion";
                            merchantTurnState = MerchantTurnState.CONFIRMBUY;
                        }

                        if (XPos > 525 && XPos < 773 && YPos > 400 && YPos < 472)
                        {
                            Selection = "Buy Big Potion";
                            merchantTurnState = MerchantTurnState.CONFIRMBUY;
                        }

                        if (XPos > 525 && XPos < 773 && YPos > 480 && YPos < 552)
                        {
                            Selection = "Buy Armor";
                            merchantTurnState = MerchantTurnState.CONFIRMBUY;
                        }

                        if (XPos > 525 && XPos < 773 && YPos > 560 && YPos < 632)
                        {
                            merchantTurnState = MerchantTurnState.BUYSPELL;
                        }

                        if (XPos > 800 && XPos < 1048 && YPos > 240 && YPos < 312)
                        {
                            Selection = "Sell Armor";
                            merchantTurnState = MerchantTurnState.CONFIRMSELL;
                        }

                        if (XPos > 800 && XPos < 1048 && YPos > 320 && YPos < 392)
                        {
                            if (player.SpellsString.Count > 0)
                            {
                                merchantTurnState = MerchantTurnState.SELLSPELL;
                            }
                            else merchantTurnState = MerchantTurnState.INSUFFICENTFUNDS;
                        }
               
                        break;
                 
                    // ------- CONFIRM BUY STATE -------
                    case MerchantTurnState.CONFIRMBUY:

                        if (XPos > 534 && XPos < 780 && YPos > 420 && YPos < 490)
                        {
                            switch (Selection)
                            {
                                case "Buy Ration":

                                 
                                    if (player.SpendGold(1))
                                    {
                                        player.Food++;
                                        merchantTurnState = MerchantTurnState.BUYSELL;
                                    }

                                    else merchantTurnState = MerchantTurnState.INSUFFICENTFUNDS;

                                    break;

                                case "Buy Potion":
                                    if (player.SpendGold(1))
                                    {
                                        player.Health++;
                                        merchantTurnState = MerchantTurnState.BUYSELL;
                                    }

                                    else merchantTurnState = MerchantTurnState.INSUFFICENTFUNDS;

                                    break;

                                case "Buy Big Potion":
                                    if (player.SpendGold(3))
                                    {
                                        player.Health+=4;
                                        merchantTurnState = MerchantTurnState.BUYSELL;
                                    }
                                   
                                   else merchantTurnState = MerchantTurnState.INSUFFICENTFUNDS;

                                    break;

                                case "Buy Armor":
                                    if (player.SpendGold(6))
                                    {
                                        player.Armor += 1;
                                        merchantTurnState = MerchantTurnState.BUYSELL;
                                    }

                                    else merchantTurnState = MerchantTurnState.INSUFFICENTFUNDS;

                                    break;

                                case "Buy Fire Spell":

                                    if (player.SpendGold(8))
                                    {
                                        player.AddSpellString("Fire Spell");
                                        merchantTurnState = MerchantTurnState.BUYSELL;
                                    }
                                    else merchantTurnState = MerchantTurnState.INSUFFICENTFUNDS;

                                    break;

                                case "Buy Ice Spell":

                                    if (player.SpendGold(8))
                                    {
                                        player.AddSpellString("Ice Spell");
                                        merchantTurnState = MerchantTurnState.BUYSELL;
                                    }
                                    else merchantTurnState = MerchantTurnState.INSUFFICENTFUNDS;

                                    break;

                                case "Buy Poison Spell":

                                    if (player.SpendGold(8))
                                    {
                                        player.AddSpellString("Poison Spell");
                                        merchantTurnState = MerchantTurnState.BUYSELL;
                                    }
                                    else merchantTurnState = MerchantTurnState.INSUFFICENTFUNDS;

                                    break;

                                case "Buy Healing Spell":

                                    if (player.SpendGold(8))
                                    {
                                        player.AddSpellString("Healing Spell");
                                        merchantTurnState = MerchantTurnState.BUYSELL;
                                    }
                                    else merchantTurnState = MerchantTurnState.INSUFFICENTFUNDS;

                                    break;

                                default:
                                    break;
                            }
                            Thread.Sleep(500);
                        }

                        if (XPos > 820 && XPos < 1070 && YPos > 420 && YPos < 490)
                        {
                            merchantTurnState = MerchantTurnState.BUYSELL;
                        }

                        break;
                    
                    // ------- BUY SPELL STATE -------
                    case MerchantTurnState.BUYSPELL:

                        if (XPos > 525 && XPos < 773 && YPos > 240 && YPos < 312)
                        {
                            Selection = "Buy Fire Spell";
                            merchantTurnState = MerchantTurnState.CONFIRMBUY;

                        }

                        if (XPos > 525 && XPos < 773 && YPos > 320 && YPos < 392)
                        {
                            Selection = "Buy Ice Spell";
                            merchantTurnState = MerchantTurnState.CONFIRMBUY;
                        }

                        if (XPos > 525 && XPos < 773 && YPos > 400 && YPos < 472)
                        {
                            Selection = "Buy Poison Spell";
                            merchantTurnState = MerchantTurnState.CONFIRMBUY;
                        }

                        if (XPos > 525 && XPos < 773 && YPos > 480 && YPos < 552)
                        {
                            Selection = "Buy Healing Spell";
                            merchantTurnState = MerchantTurnState.CONFIRMBUY;
                        }

                        break;

                    // ------- SELL SPELL STATE -------
                    // if there is no second spell but someone clicks the x&y space an out of bounds exception exists

                    case MerchantTurnState.SELLSPELL:

                        if (XPos > 700 && XPos < 807 && YPos > 15 && YPos < 46)
                        {
                            Selection = "Sell " + player.SpellsString[0];
                            merchantTurnState = MerchantTurnState.CONFIRMSELL; 
                        }

                        if (XPos > 900 && XPos < 1007 && YPos > 15 && YPos < 46)
                        {
                            Selection = "Sell " + player.SpellsString[1];
                            merchantTurnState = MerchantTurnState.CONFIRMSELL;
                        }

                        break;

                    // ------- CONFIRM SALE STATE -------
                    case MerchantTurnState.CONFIRMSELL:
                        if (XPos > 534 && XPos < 780 && YPos > 420 && YPos < 490)
                        {
                            switch (Selection)
                            {
                                case "Sell Armor":

                                    if (player.RemoveArmor())
                                    {
                                        player.Gold += 3; 
                                        merchantTurnState = MerchantTurnState.BUYSELL;

                                    }
                                    else merchantTurnState = MerchantTurnState.INSUFFICENTFUNDS;

                                    break;

                                case "Sell Fire Spell":
                                    if (player.SpellsString.Contains("Fire Spell"))
                                    {
                                        player.RemoveSpellString("Fire Spell");
                                        player.Gold += 4;
                                        merchantTurnState = MerchantTurnState.BUYSELL;
                                    }

                                    else merchantTurnState = MerchantTurnState.INSUFFICENTFUNDS;

                                    break;

                                case "Sell Ice Spell":
                                    if (player.SpellsString.Contains("Ice Spell"))
                                    {
                                        player.RemoveSpellString("Ice Spell");
                                        player.Gold += 4;
                                        merchantTurnState = MerchantTurnState.BUYSELL;
                                    }

                                    else merchantTurnState = MerchantTurnState.INSUFFICENTFUNDS;

                                    break;

                                case "Sell Poison Spell":
                                    if (player.SpellsString.Contains("Poison Spell"))
                                    {
                                        player.RemoveSpellString("Poison Spell");
                                        player.Gold += 4;
                                        merchantTurnState = MerchantTurnState.BUYSELL;
                                    }

                                    else merchantTurnState = MerchantTurnState.INSUFFICENTFUNDS;

                                    break;

                                case "Sell Healing Spell":

                                    if (player.SpellsString.Contains("Healing Spell"))
                                    {
                                        player.RemoveSpellString("Healing Spell");
                                        player.Gold += 4;
                                        merchantTurnState = MerchantTurnState.BUYSELL;
                                    }
                                    else merchantTurnState = MerchantTurnState.INSUFFICENTFUNDS;

                                    break;

                               
                                default:
                                    break;
                            }
                            Thread.Sleep(500);
                        }

                        if (XPos > 820 && XPos < 1070 && YPos > 420 && YPos < 490)
                        {
                            merchantTurnState = MerchantTurnState.BUYSELL;
                        }

                        break;


                    // ------- COMPLETE -------
                    case MerchantTurnState.COMPLETE:
                        break;
                    default:
                        break;
                }

            }
        }

    }
}
