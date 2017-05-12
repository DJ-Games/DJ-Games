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
    class CombatDice
    {

        public String Name { get; set; }

        public int Roll { get; set; }

        public Texture2D CurrentTexture { get; set; }

        public Dictionary<string, Texture2D> DieTextures { get; set; }

        public bool Active { get; set; }

        public int Xpos { get; set; }

        public int Ypos { get; set; }

        public bool FeatUsed { get; set; }




        public CombatDice(Dictionary<string, Texture2D> dieTextures, int xPos, int yPos)
        {
            DieTextures = dieTextures;
            CurrentTexture = dieTextures["Blank"];
            Xpos = xPos;
            Ypos = yPos;

        }

        public void HandleCombatDie()
        {

        }

        public void DrawCombatDie(SpriteBatch sBatch)
        {
            sBatch.Draw(CurrentTexture, new Vector2(Xpos, Ypos), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
        }


    }
}
