﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRogue
{
    class Treasure : Card
    {

        public bool Guarded { get; set; }

        public bool Sucess { get; set; }

        public Treasure(string name) : base(name)
        {

        }

        public void TreasureResult(int option, Player player)
        {
            switch (option)
            {
                case 1:
                    player.Armor++;
                    break;
                case 2:
                    player.Experience += 2;
                    break;
                case 3:
                    player.Spells.Add("Fireball");
                    break;
                case 4:
                    player.Spells.Add("Freeze");
                    break;
                case 5:
                    player.Spells.Add("Poison");
                    break;
                case 6:
                    player.Spells.Add("Healing");
                    break;
                default:
                    break;
            }
        }







    }
}
