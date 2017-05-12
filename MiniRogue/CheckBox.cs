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
    class CheckBox
    {

        public bool Active { get; set; }

        public bool Checked { get; set; }

        public Texture2D CurrentTexture { get; set; }

        public Texture2D CheckedTexture { get; set; }

        public Texture2D UncheckedTexture { get; set; }

        public int Xpos { get; set; }

        public int Ypos { get; set; }

        public CheckBox(Texture2D checkedTexture, Texture2D uncheckedTexture, int xPos, int yPos)
        {
            CheckedTexture = checkedTexture;
            UncheckedTexture = uncheckedTexture;
            CurrentTexture = UncheckedTexture;
            Xpos = xPos;
            Ypos = yPos;

        }

        public void HandleCheckBoxes()
        {

        }

        public void DrawCheckBoxes(SpriteBatch sBatch)
        {
            if (Active)
            {
                sBatch.Draw(CurrentTexture, new Vector2(Xpos, Ypos), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
            }
            
        }

    }
}
