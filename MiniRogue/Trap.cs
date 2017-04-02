using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniRogue
{
    class Trap : Card
    {

        private bool success;

        public bool Success
        {
            get { return success; }
            set { success = value; }
        }


        public Trap(string name, Texture2D cardTexture) : base(name, cardTexture)
        {

        }


        public void TrapResult(int option, Player player)
        {
            switch (option)
            {
                case 1:
                    player.Food--;
                    break;
                case 2:
                    player.Gold--;
                    break;
                case 3:
                    if (player.Armor == 0)
                    {
                        player.Health -= 2;
                    }
                    else
                    {
                        player.Armor--;
                    }
                    break;
                case 4:
                    player.Health--;
                    break;
                case 5:
                    player.Experience--;
                    break;
                case 6:
                    player.Health -= 2;
                    // will need to make multi dimensional array to determine this
                    break;



                default:
                    break;
            }
        }







    }
}
