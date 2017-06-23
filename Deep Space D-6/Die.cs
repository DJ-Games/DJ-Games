using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Deep_Space_D_6
{
    class Die
    {
        public Dictionary<string, Texture2D> DieTextures { get; set; }

        public Texture2D CurrentTexture { get; set; }

        private int dieValue;
                
        public int DieValue
        {
            get { return dieValue; }
            set
            {
                if ((dieValue = value) == 1)
                {
                    CurrentTexture = DieTextures["dieCommander"];
                    return;
                }
                if ((dieValue = value) == 2)
                {
                    CurrentTexture = DieTextures["dieTactical"];
                    return;
                }
                if ((dieValue = value) == 3)
                {
                    CurrentTexture = DieTextures["dieMedic"];
                    return;
                }
                if ((dieValue = value) == 4)
                {
                    CurrentTexture = DieTextures["dieScience"];
                    return;
                }
                if ((dieValue = value) == 5)
                {
                    CurrentTexture = DieTextures["dieEngineer"];
                    return;
                }
                if ((dieValue = value) == 6)
                {
                    CurrentTexture = DieTextures["dieThreat"];
                    return;
                }
            }
        }


        public Random RNG { get; set; }

        public int ReturnedXPos { get; set; }

        public int ReturnedYPos { get; set; }

        public int InHandXPos { get; set; }

        public int InHandYPos { get; set; }

        public int ShipXPos { get; set; }

        public int ShipYPos { get; set; }

        public bool OnShip { get; set; }

        public bool InHand { get; set; }



        public Die(Dictionary<string, Texture2D> dieTextures, int returnedXPos, int returnedYPos)
        {
            DieTextures = dieTextures;
            ReturnedXPos = returnedXPos;
            ReturnedYPos = returnedYPos;
            RNG = new Random();
        }

        public void RollDie()
        {
            DieValue = RNG.Next(6)+1;
            
            
        }

        public void DrawDie(SpriteBatch sBatch)
        {
            if (!OnShip && !InHand)
            {
                sBatch.Draw(CurrentTexture, new Vector2(ReturnedXPos, ReturnedYPos), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1f);
            }
            else
            {
                sBatch.Draw(CurrentTexture, new Vector2(InHandXPos, InHandYPos), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1f);
            }
        }
    }
}
