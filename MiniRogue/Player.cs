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

        private int experience;

        public int Experience
        {
            get { return experience; }
            set
            {
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

        public List<string> Spells { get; set; }

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
            Spells = new List<string>();
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

        public bool AddSpell(string spell)
        {
            if (Spells.Count < 2)
            {
                Spells.Add(spell);
                return true;
            }
            else return false; 
        }

        public bool RemoveSpell(string spell)
        {
            if (Spells.Count > 0)
            {
                Spells.Remove(spell);
                return true;
            }
            else return false; 

        }

        public void FallBelow()
        {
            switch (Rank)
            {
                case 1:

                    break;

                case 2:

                    DungeonLevel--;
                    DungeonArea -= 2;

                    break;

                case 3:

                    DungeonLevel--;
                    if (DungeonArea == 7)
                    {
                        DungeonArea -= 3;
                    }
                    else
                    {
                        DungeonArea -= 2;
                    }

                    break;

                case 4:

                    DungeonLevel--;
                    DungeonArea -= 3;

                    break;

                case 5:

                    DungeonLevel--;
                    if (DungeonArea == 14)
                    {
                        DungeonArea -= 4;
                    }
                    else
                    {
                        DungeonArea -= 3;
                    }

                    break;

                default:
                    break;
            }
        }







    }
}
