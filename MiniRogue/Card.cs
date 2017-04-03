using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MiniRogue
{
    class Card
    {


        // Properties
        public string Name { get; set; }

        public bool Visible { get; set; }



        private Rectangle cardRectangle;

        public Rectangle CardRectangle
        {
            get { return cardRectangle; }
            set { cardRectangle = value; }
        }


        public Texture2D CardTexture { get; set; }

        private int xPos;

        public int XPos
        {
            get { return xPos; }
            set { cardRectangle.X = value; }
        }

        private int yPos;

        public int YPos
        {
            get { return yPos; }
            set { cardRectangle.Y = value; }
        }

        KeyboardState kbState;
        KeyboardState previousKbState;
        Dice dice = new Dice();
        int currentRoll;



        // Constructors
        public Card(string name, Texture2D cardTexture)
        {

            CardTexture = cardTexture;
            CardRectangle = new Rectangle(0, 0, 494, 708);

            this.Name = name;
            
        }

        // Methods

        public void DrawCard(SpriteBatch sBatch, int xPos, int yPos)
        {
            cardRectangle.X = xPos;
            cardRectangle.Y = yPos;
            sBatch.Draw(CardTexture, CardRectangle, Color.White);   
        }

        public void HandleCard(string cardName, Player player, Phase phase)
        {
            switch (cardName)
            {
                case "EventCard":

                    if (SingleKeyPress(Keys.Space))
                    {
                        currentRoll = dice.RollDice(1);
                    }

                    switch (currentRoll)
                    {

                        case 1:
                            player.Food++;
                            Thread.Sleep(250);
                            phase.CurrentPhase++;
                            break;

                        case 2:
                            player.Health += 2;
                            Thread.Sleep(250);
                            phase.CurrentPhase++;
                            break;

                        case 3:
                            player.Gold++;
                            Thread.Sleep(250);
                            phase.CurrentPhase++;
                            break;

                        case 4:
                            player.Experience += 2;
                            Thread.Sleep(250);
                            phase.CurrentPhase++;
                            break;

                        case 5:
                            player.Armor++;
                            Thread.Sleep(250);
                            phase.CurrentPhase++;
                            break;

                        case 6:

                            // Copy code from moster state here, then make something better here later.
                            Thread.Sleep(250);
                            phase.CurrentPhase++;
                            break;

                        default:
                            break;
                    }

                    break;

                case "Resting":
                    int playerChoice;

                    if (SingleKeyPress(Keys.D1))
                    {
                        playerChoice = 1;
                    }
                    else if (SingleKeyPress(Keys.D2))
                    {
                        playerChoice = 2;
                    }
                    else if (SingleKeyPress(Keys.D3))
                    {
                        playerChoice = 3;
                    }
                    else
                    {
                        playerChoice = 0;
                    }


                    switch (playerChoice)
                    {
                        case 1:
                            player.Experience++;
                            phase.CurrentPhase++;
                            break;

                        case 2:
                            player.Food++;
                            phase.CurrentPhase++;
                            break;

                        case 3:
                            player.Health += 2;
                            phase.CurrentPhase++;
                            break;

                        default:
                            break;
                    }
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
        }

        private bool SingleKeyPress(Keys key)
        {
            kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(key) && previousKbState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }



    }
}
