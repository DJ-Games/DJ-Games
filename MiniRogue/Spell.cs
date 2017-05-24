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
    class Spell
    {

        public string Name { get; set; }

        public Texture2D ButtonTexture { get; set; }

        public Texture2D IconTexture { get; set; }

        public int ButtonXpos { get; set; }

        public int ButtonYpos { get; set; }

        public int IconXpos { get; set; }

        public int IconYpos { get; set; }

        public Spell(string name, Texture2D spellIcon)
        {
            Name = name;
            IconTexture = spellIcon;
        }

        public void RemoveSpell(Player player, string spell)
        {
            
        }

        public void DrawIcons(SpriteBatch sBatch)
        {
            sBatch.Draw(IconTexture, new Vector2(IconXpos, IconYpos), new Rectangle?(), Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1);
        }



    }
}
