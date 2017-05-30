using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRogue
{
    class BasicDie
    {

        public int roll { get; set; }

        public Random rGen { get; set; }

        public BasicDie()
        {
            rGen = new Random();
        }

        public int RollDice()
        {
            return rGen.Next(6) + 1;
        }

    }
}

