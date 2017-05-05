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

    enum TreasureTurnState
    {
        GOLD_AWARD,
        EXTRA_TREASURE_ROLL,
        TREASURE_ROLL,
        REVIEW,
        COMPLETE,
    }


    class Treasure : Card
    {

        public bool Guarded { get; set; }

        public bool Sucess { get; set; }

        public int ExtTreasureResult { get; set; }

        public int TreasureResult { get; set; }



        TreasureTurnState treasureTurnState;

        public Treasure(string name, Texture2D cardTexture, Dictionary<string, Button> buttons) : base(name, cardTexture, buttons)
        {
            treasureTurnState = TreasureTurnState.GOLD_AWARD;
            CurrentButtons = new List<Button>(); 
        }


        //---------------------- METHODS -----------------------------
        public override bool HandleCard(Player player, MouseState current, MouseState previous, float xPos, float yPos)
        {
            XPos = xPos;
            YPos = yPos;

            switch (treasureTurnState)
            {
                case TreasureTurnState.GOLD_AWARD:

                    if (player.HasFoughtMonster)
                    {
                        player.Gold += 2;
                    }
                    else
                    {
                        player.Gold++;
                    }
                    treasureTurnState = TreasureTurnState.EXTRA_TREASURE_ROLL;

                    return false;

                case TreasureTurnState.EXTRA_TREASURE_ROLL:
                    HandleButtons(player);


                    return false;
            
                case TreasureTurnState.TREASURE_ROLL:
                    HandleButtons(player);


                   

                    return false;

                case TreasureTurnState.REVIEW:
                    HandleButtons(player);


                    return false;

                case TreasureTurnState.COMPLETE:
                    HandleButtons(player);

                    return true;

                default:

                    return false;
            }


        }




        public override void DrawCard(SpriteBatch sBatch, SpriteFont font)
        {
            sBatch.Draw(CardTexture, new Vector2(100, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);
            sBatch.Draw(Buttons["Roll Die"].ButtonTexture, new Vector2(700, 575), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);
            sBatch.Draw(Buttons["Done Button"].ButtonTexture, new Vector2(700, 775), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);

            switch (treasureTurnState)
            {
                case TreasureTurnState.GOLD_AWARD:
                    break;
                case TreasureTurnState.EXTRA_TREASURE_ROLL:

                    sBatch.DrawString(font, "Press Space. Roll 5+ to revceive extra treasure.", new Vector2(700, 400), Color.White);

                    break;
                case TreasureTurnState.TREASURE_ROLL:

                    sBatch.DrawString(font, "Press Space to roll for Treasure.", new Vector2(700, 400), Color.White);

                    break;
                case TreasureTurnState.REVIEW:
                    break;
                case TreasureTurnState.COMPLETE:
                    break;
                default:
                    break;

            }

        }

        public void HandleButtons(Player player)
        {
            switch (treasureTurnState)
            {
                case TreasureTurnState.GOLD_AWARD:
                    break;
                case TreasureTurnState.EXTRA_TREASURE_ROLL:

                    if (SingleMouseClick())
                    {
                        if (XPos > 700 && YPos < 948 && YPos > 575 && XPos < 648)
                        {
                            ExtTreasureResult = player.playerDice.RollDice();
                        }


                        if (ExtTreasureResult >= 5)
                        {
                            treasureTurnState = TreasureTurnState.TREASURE_ROLL;
                        }
                        else
                        {
                            treasureTurnState = TreasureTurnState.REVIEW;
                        }

                    }
                    break;

                case TreasureTurnState.TREASURE_ROLL:

                    if (SingleMouseClick())
                    {
                        if (XPos > 700 && XPos < 948 && YPos > 575 && YPos < 648)
                        {
                            TreasureResult = player.playerDice.RollDice();
                        }
                    }
                    switch (TreasureResult)
                    {

                        case 1:
                            player.Armor++;
                            break;

                        case 2:
                            player.Experience += 2;
                            break;

                        case 3:
                            player.Spells.Add("Fire Spell");
                            break;

                        case 4:
                            player.Spells.Add("Ice Spell");
                            break;

                        case 5:
                            player.Spells.Add("Poison Spell");
                            break;

                        case 6:
                            player.Spells.Add("Healing Spell");
                            break;
                        default:
                            break;
                    }

                    treasureTurnState = TreasureTurnState.REVIEW;
                    break;
            
        
            case TreasureTurnState.REVIEW:
            if (SingleMouseClick())
            {
                if (XPos > 700 && XPos < 948 && YPos > 775 && YPos < 848)
                {
                    treasureTurnState = TreasureTurnState.COMPLETE;
                }
            }

            break;

                case TreasureTurnState.COMPLETE:
                    break;
                default:
                    break;
            }
        }




    }
}
