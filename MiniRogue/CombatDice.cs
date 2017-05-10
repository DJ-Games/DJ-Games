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

        public Dictionary<string, Texture2D> DieTextures { get; set; }


        public CombatDice(Dictionary<string, Texture2D> dieTextures)
        {
            DieTextures = dieTextures;
        }
    }
}
