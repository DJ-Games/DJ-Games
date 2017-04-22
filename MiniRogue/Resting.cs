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
        REVIEW, 
        COMPLETE,
        
    }

    class Resting : Card
    {


        public int PlayerChoice { get; set; }

        RestingTurnState restingTurnState = new RestingTurnState();

        // Constuctor
        public Resting(string name, Texture2D cardTexture, Dictionary<string, Button> buttons) : base(name, cardTexture, buttons)
        {
            restingTurnState = RestingTurnState.SELECTION;

        }

        //---------------------- METHODS -----------------------------
        public override bool HandleCard(Player player, MouseState current, MouseState previous, float xPos, float yPos)
        {
            PreviousKbState = CurrentKbState;

            switch (restingTurnState)
            {
                case RestingTurnState.SELECTION:
                    LoadRestingButtons(); 

                    
                    return false;

                case RestingTurnState.REVIEW:
                    return false;

                case RestingTurnState.COMPLETE:
                    return true;

                default:
                    return false;
            }




        }

        public override void DrawCard(SpriteBatch sBatch, SpriteFont font)
        {


            sBatch.Draw(CardTexture, new Vector2(100, 100), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);


            switch (restingTurnState)
            {
                case RestingTurnState.SELECTION:
                    int counter = 200; 
                    //foreach (var item in CurrentButtons)
                    //{
                    //    sBatch.Draw(item.ButtonTexture, new Vector2(800, counter), new Rectangle?(), Color.White, 0f, new Vector2(), .75f, SpriteEffects.None, 1);
                    //    counter += 80; 
                    //}
                    break;
                case RestingTurnState.REVIEW:
                    break;
                case RestingTurnState.COMPLETE:
                    break;
                default:
                    break;
            }




        }


        public void LoadRestingButtons()
        {
            CurrentButtons.Add(Buttons["Reinforce"]);
            CurrentButtons.Add(Buttons["Green Ration Button"]);
            CurrentButtons.Add(Buttons["Heal"]);





        }
    }
}
