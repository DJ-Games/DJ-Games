using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniRogue
{
    class Enemy :Card
    {

        // Properties
        public int Damage { get; set; }

        public int ExpReward { get; set; }

        public int Health { get; set; }

        // Consructors
        public Enemy(string name, Texture2D cardTexture) : base(name, cardTexture)
        {

        }



    }
}
