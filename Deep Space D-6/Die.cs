using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Deep_Space_D_6.Content
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


    }
}
