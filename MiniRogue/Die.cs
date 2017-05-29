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
    class Die
    {

        public String Name { get; set; }

        private int roll;

        public int Roll
        {
            get { return roll; }
            set
            {
                if (value == 1)
                {
                    roll = 0;
                }
                else { roll = value; }
            }
        }

        public Texture2D CurrentTexture { get; set; }

        public Dictionary<string, Texture2D> DieTextures { get; set; }

        public List<Texture2D> DieTextureList { get; set; }

        public bool Active { get; set; }

        public int Xpos { get; set; }

        public int Ypos { get; set; }

        public bool FeatUsed { get; set; }

        public Random Rng { get; set; }



        public Die(Dictionary<string, Texture2D> dieTextures, int xPos, int yPos)
        {
            DieTextures = dieTextures;
            CurrentTexture = dieTextures["Blank"];
            Xpos = xPos;
            Ypos = yPos;
            Rng = new Random();
            DieTextureList = new List<Texture2D>();
            for (int i = 1; i < 7; i++)
            {
                DieTextureList.Add(dieTextures["Roll " + i]);
            }
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
