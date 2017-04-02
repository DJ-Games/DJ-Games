using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniRogue
{
    class EventCard : Card
    {


        public bool Success { get; set; }


        public EventCard(string name, Texture2D cardTexture) : base(name, cardTexture)
        {

        }




    }
}
