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

        public Random RNG { get; set; }

        public int ReturnedXPos { get; set; }

        public int ReturnedYPos { get; set; }

        public int ShipXPos { get; set; }

        public int ShipYPos { get; set; }

        public bool OnShip { get; set; }

        public Die(Dictionary<string, Texture2D> dieTextures, int returnedXPos, int returnedYPos)
        {
            DieTextures = dieTextures;
            ReturnedXPos = returnedXPos;
            ReturnedYPos = returnedYPos;
            CurrentTexture = DieTextures["dieTactical"];
        }

        public void DrawDie(SpriteBatch sBatch)
        {
            if (!OnShip)
            {
                sBatch.Draw(CurrentTexture, new Vector2(ReturnedXPos, ReturnedYPos), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1f);
            }
        }
    }
}
