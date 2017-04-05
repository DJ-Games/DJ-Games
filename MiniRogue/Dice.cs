using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRogue
{
    class Dice
    {

        public int roll { get; set; }

        public Random rGen { get; set; }

        public Dice()
        {
            rGen = new Random();
        }

        public int RollCombatDice(int number)
        {
            switch (number)
            {
                case (1):
                    return rGen.Next(6) + 1;
                    break;

                case (2):
                    return rGen.Next(6) + rGen.Next() + 2;
                    break;

                case (3):
                    return rGen.Next() + rGen.Next() + rGen.Next() + 3;
                    break;

                case (4):
                    return rGen.Next() + rGen.Next() + rGen.Next() + rGen.Next() + 4;
                    break;

                default:
                    return 0;
                    break;
            }
        }

        public int RollDice()
        {
            return rGen.Next(6) + 1;
        }
         
    }
}

