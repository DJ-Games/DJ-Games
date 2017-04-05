using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRogue
{
    class Player
    {

        public int Armor { get; set; }

        public int Health { get; set; }

        public int Gold { get; set; }

        public int Food { get; set; }

        public int Experience { get; set; }

        public int Rank { get; set; }

        public List<string> Spells { get; set; }

        public int Level { get; set; }

        public int DungeonLevel { get; set; }

        public int DungeonArea { get; set; }

        public Dice playerDice { get; set; }



        public Player(int armor, int health, int gold, int food)
        {

            Armor = armor;
            Health = health;
            Gold = gold;
            Food = food;
            Rank = 1; 

            DungeonLevel = 1;
            DungeonArea = 1;

            playerDice = new Dice();
            Spells = new List<string>();
        }

        public Player()
        {

        }









    }
}
