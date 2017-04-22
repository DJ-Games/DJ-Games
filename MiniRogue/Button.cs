using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MiniRogue
{
    class Button
    {

        public String Name { get; set; }

        public Texture2D ButtonTexture { get; set; }







        public Button(Texture2D bTexture, string name)
        {
            Name = name;
            ButtonTexture = bTexture;
        }


    }
}
