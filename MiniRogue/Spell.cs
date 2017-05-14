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

        public Spell()
        {

        }

    }
}
