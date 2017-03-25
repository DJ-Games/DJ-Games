using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRogue
{
    class Boss : Enemy
    {


        public int GoldReward { get; set; }

        public bool GivesItem { get; set; }



        public Boss(string name) : base(name)
        {

        }




    }
}
