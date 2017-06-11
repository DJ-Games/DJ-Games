using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
                    return;
                }
                if ((armor = value) > 5)
                {
                    armor = 5;
                    return;
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
                    return;
                }
                if ((health = value) > 20)
                {
                    health = 20;
                    return;
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
                    return;
                }
                if((gold = value) > 20)
                {
                    gold = 20;
                    return;
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
                    return;
                }
                if ((food = value) > 6)
                {
                    food = 6;
                    return;
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
                    return;
                }
                if ((experience = value) < 6)
                {
                    Rank = 1;
                    return;
                }
                if ((experience = value) >= 6 && (experience = value) < 18)
                {
                    Rank = 2;
                    return;
                }
                if ((experience = value) >= 18 && (experience = value) < 36)
                {
                    Rank = 3;
                    return;
                }
                //if ((experience = value) == 36)
                //{
                //    Rank = 4;
                //    return;
                //}
                if ((experience = value) >= 36)
                {
                    Rank = 4;
                    experience = 36;
                    Health += (value - 36);
                    return;
                }
            }
        }


        public int Rank { get; set; }

        public List<Spell> Spells { get; set; }

        public int Level { get; set; }

        public int DungeonLevel { get; set; }

        public int DungeonArea { get; set; }

        public BasicDie playerDice { get; set; }

        public bool HasFoughtMonster { get; set; }

        public Dictionary<string, Texture2D> SpellIcons { get; set; }



        public Player(int armor, int health, int gold, int food, Dictionary<string, Texture2D> spellIcons)
        {

            Armor = armor;
            Health = health;
            Gold = gold;
            Food = food;
            Rank = 1; 

            DungeonLevel = 1;
            DungeonArea = 1;

            HasFoughtMonster = false;

            playerDice = new BasicDie();
            Spells = new List<Spell>();
            SpellIcons = spellIcons;
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

        public void AddSpell(string spell)
        {
            if (Spells.Count == 0)
            {
                Spells.Add(new Spell(spell, SpellIcons[spell + " Spell Icon"]));
                Spells[0].IconXpos = 1130;
                Spells[0].IconYpos = 20;
            }
            else 
            {
                Spells.Add(new Spell(spell, SpellIcons[spell + " Spell Icon"]));
                Spells[1].IconXpos = 1180;
                Spells[1].IconYpos = 20;
            }
        }

        public void RemoveSpell(int index)
        {
            if (index == 0)
            {
                Spells.RemoveAt(0);
                if (Spells.Count == 1)
                {
                    Spells[0].IconXpos = 1130;
                }
            }
            else { Spells.RemoveAt(index); }
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
