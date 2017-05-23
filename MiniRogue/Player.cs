using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRogue
{
    class Player
    {

        private int armor;

        public int Armor
        {
            get { return armor; }
            set
            {
                if ((armor = value) < 0)
                {
                    armor = 0;
                }
                if ((armor = value) > 5)
                {
                    armor = 5;
                }
            }
        }

        private int health;

        public int Health
        {
            get { return health; }
            set
            {
                if ((health = value) < 0)
                {
                    health = 0;
                }
                if ((health = value) > 20)
                {
                    health = 20;
                }
            }
        }

        private int gold;

        public int Gold
        {
            get { return gold; }
            set
            {
                if ((gold = value) < 0)
                {
                    gold = 0;
                }
                if((gold = value) > 20)
                {
                    gold = 20;
                }
            }
        }

        private int food;

        public int Food
        {
            get { return food; }
            set
            {
                if ((food = value) < 0)
                {
                    food = 0;
                }
                if ((food = value) > 6)
                {
                    food = 6;
                }
            }
        }


        private int experience;

        public int Experience
        {
            get { return experience; }
            set
            {
                if ((experience = value) < 0)
                {
                    experience = 0;
                }
                if ((experience = value) > 18)
                {
                    experience = 18;
                }
                if ((experience = value) < 6)
                {
                    Rank = 1;
                    experience = value;
                }
                if ((experience = value) >= 6 && (experience = value) < 12)
                {
                    Rank = 2;
                    experience = value;
                }
                if ((experience = value) >= 12 && (experience = value) < 18)
                {
                    Rank = 3;
                    experience = value;
                }
                if ((experience = value) >= 18)
                {
                    Rank = 4;
                    experience = value;
                }
            }
        }


        public int Rank { get; set; }

        public List<string> SpellsString { get; set; }

        public List<Spell> Spells { get; set; }

        public int Level { get; set; }

        public int DungeonLevel { get; set; }

        public int DungeonArea { get; set; }

        public Dice playerDice { get; set; }

        public bool HasFoughtMonster { get; set; }



        public Player(int armor, int health, int gold, int food)
        {

            Armor = armor;
            Health = health;
            Gold = gold;
            Food = food;
            Rank = 1; 

            DungeonLevel = 1;
            DungeonArea = 1;

            HasFoughtMonster = false;

            playerDice = new Dice();
            SpellsString = new List<string>();
        }

        public Player()
        {

        }

        // Methods

        public bool SpendGold(int cost)
        {
            if (Gold >= cost)
            {
                Gold -= cost;
                return true; 
                
            }

            return false; 
        }

        public bool RemoveArmor()
        {
            if (Armor >= 1)
            {
                Armor--;
                return true;
            }
            else return false;
        }

        public bool AddSpellString(string spell)
        {
            if (SpellsString.Count < 2)
            {
                SpellsString.Add(spell);
                return true;
            }
            else return false; 
        }

        public bool RemoveSpellString(string spell)
        {
            if (SpellsString.Count > 0)
            {
                SpellsString.Remove(spell);
                return true;
            }
            else return false; 

        }

        public void FallBelow()
        {
            Health -= 2; 

            switch (DungeonLevel)
            {
                case 1:

                    DungeonArea += 2;
                    DungeonLevel++;

                    break;

                case 2:

                    DungeonArea += 2;
                    DungeonLevel++;

                    break;

                case 3:

                    DungeonArea += 3;
                    DungeonLevel++;

                    break;

                case 4:

                    DungeonArea += 3;
                    DungeonLevel++;

                    break;

                default:
                    break;
            }
        }







    }
}
