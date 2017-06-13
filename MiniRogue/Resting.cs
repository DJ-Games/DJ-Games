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
    enum RestingTurnState
    {
        SELECTION,
        //REVIEW,
        COMPLETE,
        
    }

    class Resting : Card
    {


        public string PlayerChoice { get; set; }

        RestingTurnState restingTurnState = new RestingTurnState();

        // Constuctor
        public Resting(string name, Texture2D cardTexture, Texture2D cardBack, Dictionary<string, Button> buttons) : base(name, cardTexture, cardBack, buttons)
        {
            restingTurnState = RestingTurnState.SELECTION;
            CurrentButtons = new List<Button>();

        }

        //---------------------- METHODS -----------------------------
        public override bool HandleCard(Player player, MouseState current, MouseState previous, float xPos, float yPos)
        {
            XPos = xPos;
            YPos = yPos;
            CurrentMouseState = current;
            PreviousMouseState = previous;


            switch (restingTurnState)
            {
                case RestingTurnState.SELECTION:
                    LoadRestingButtons();
                    HandleButtons(player);

                    return false;

                //case RestingTurnState.REVIEW:
                //    HandleButtons(player);

                    //return false;

                case RestingTurnState.COMPLETE:
                    return true;

                default:
                    return false;
            }




        }

        public override void DrawCard(SpriteBatch sBatch, SpriteFont font)
        {


            sBatch.Draw(CardTexture, new Vector2(274, 130), new Rectangle?(), Color.White, 0f, new Vector2(248, 0), .75f, SpriteEffects.None, 1);


            switch (restingTurnState)
            {
                case RestingTurnState.SELECTION:


                    sBatch.DrawString(font, "You are resting. Choose an option.", new Vector2(575, 100), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
                    int counter = 240; 
                    foreach (var item in CurrentButtons)
                    {
                        sBatch.Draw(item.ButtonTexture, new Vector2(770, counter), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
                        counter += 150; 
                    }
                    break;

                //case RestingTurnState.REVIEW:
                //    //sbatch.draw(buttons["confirm purchase menu"].buttontexture, new vector2(580, 320), new rectangle?(), color.white, 0f, new vector2(), 1f, spriteeffects.none, 1);
                //    break;

                case RestingTurnState.COMPLETE:
                    break;
                default:
                    break;
            }




        }


        public void LoadRestingButtons()
        {
            CurrentButtons.Clear();
            CurrentButtons.Add(Buttons["Reinforce Button"]);
            CurrentButtons.Add(Buttons["Green Ration Button"]);
            CurrentButtons.Add(Buttons["Heal Button"]);
        }

        public void HandleButtons(Player player)
        {
            if (SingleMouseClick())
            {
                switch (restingTurnState)
                {
                    case RestingTurnState.SELECTION:

                        if (XPos > 770 && XPos < 1018 && YPos > 240 && YPos < 312)
                        {
                            PlayerChoice = "Reinforce Weapon";
                            player.Experience += 1; 
                            restingTurnState = RestingTurnState.COMPLETE;
                        }

                        if (XPos > 770 && XPos < 1018 && YPos > 390 && YPos < 462)
                        {
                            PlayerChoice = "Ration";
                            player.Food += 1; 
                            restingTurnState = RestingTurnState.COMPLETE;
                        }

                        if (XPos > 770 && XPos < 1018 && YPos > 540 && YPos < 612)
                        {
                            PlayerChoice = "Heal";
                            player.Health += 1; 
                            restingTurnState = RestingTurnState.COMPLETE;
                        }

                        break;

                    //case RestingTurnState.REVIEW:

                    //    if (XPos > 614 && XPos < 860 && YPos > 420 && YPos < 490)
                    //    {
                    //        switch (PlayerChoice)
                    //        {
                    //            case "Reinforce Weapon":

                    //                player.Experience++;
                    //                restingTurnState = RestingTurnState.COMPLETE;
                    //                break;

                    //            case "Ration":

                    //                player.Food++;
                    //                restingTurnState = RestingTurnState.COMPLETE;
                    //                break;

                    //            case "Heal":

                    //                player.Health+=2;
                    //                restingTurnState = RestingTurnState.COMPLETE;
                    //                break;
                    //        }
                    //    }

                    //    if (XPos > 900 && XPos < 1150 && YPos > 420 && YPos < 490)
                    //    {
                    //        restingTurnState = RestingTurnState.SELECTION;
                    //    }


                    //    break;


                }
            }
        }




    }
}
