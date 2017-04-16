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

        public Treasure(string name, Texture2D cardTexture) : base(name, cardTexture)
        {
            treasureTurnState = TreasureTurnState.GOLD_AWARD;
        }


        //---------------------- METHODS -----------------------------
        public override bool HandleCard(Player player)
        {
            PreviousKbState = CurrentKbState;

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

                    if (SingleKeyPress(Keys.Space))
                    {
                        ExtTreasureResult = player.playerDice.RollDice();

                        if (ExtTreasureResult >= 5)
                        {
                            treasureTurnState = TreasureTurnState.TREASURE_ROLL;
                        }
                        else
                        {
                            treasureTurnState = TreasureTurnState.REVIEW;
                        }

                    }

                    return false;

                case TreasureTurnState.TREASURE_ROLL:

                    if (SingleKeyPress(Keys.Space))
                    {
                        TreasureResult = player.playerDice.RollDice();

                        switch (TreasureResult)
                        {

                            case 1:
                                player.Armor++;
                                break;

                            case 2:
                                player.Experience += 2;
                                break;

                            case 3:
                                player.Spells.Add("Fireball");
                                break;
                            case 4:
                                player.Spells.Add("Freeze");
                                break;
                            case 5:
                                player.Spells.Add("Poison");
                                break;
                            case 6:
                                player.Spells.Add("Healing");
                                break;
                            default:
                                break;


                        }
                        treasureTurnState = TreasureTurnState.REVIEW;

                    }

                    return false;

                case TreasureTurnState.REVIEW:

                    treasureTurnState = TreasureTurnState.COMPLETE;

                    return false;

                case TreasureTurnState.COMPLETE:

                    return true;

                default:

                    return false;
            }


        }




        public override void DrawCard(SpriteBatch sBatch, SpriteFont font, int xPos, int yPos)
        {
            XPos = xPos;
            YPos = yPos;
            sBatch.Draw(CardTexture, new Vector2(100, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);

            switch (treasureTurnState)
            {
                case TreasureTurnState.GOLD_AWARD:
                    break;
                case TreasureTurnState.EXTRA_TREASURE_ROLL:

                    sBatch.DrawString(font, "Press Space. Roll 5+ to revceive extra treasure.", new Vector2(50, 800), Color.White);

                    break;
                case TreasureTurnState.TREASURE_ROLL:

                    sBatch.DrawString(font, "Press Space to roll for Treasure.", new Vector2(50, 800), Color.White);

                    break;
                case TreasureTurnState.REVIEW:
                    break;
                case TreasureTurnState.COMPLETE:
                    break;
                default:
                    break;
            }

        }

    }
}
