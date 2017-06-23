using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Deep_Space_D_6
{
    class Ship
    {
        public List<Die> ThreatZone { get; set; }

        public List<Die> CommanderZone { get; set; }

        public List<Die> TacticalZone { get; set; }

        public List<Die> MedicZone { get; set; }

        public List<Die> ScienceZone { get; set; }

        public List<Die> EngineerZone { get; set; }


        public Ship()
        {
            ThreatZone = new List<Die>();
            CommanderZone = new List<Die>();
            TacticalZone = new List<Die>();
            MedicZone = new List<Die>();
            ScienceZone = new List<Die>();
            EngineerZone = new List<Die>();
        }

        public void AddDieToShip(Die die, string zone)
        {
            switch (zone)
            {
                case "Threat":

                    if (ThreatZone.Count == 0)
                    {
                        die.ShipXPos = 522;
                        die.ShipYPos = 48;
                    }
                    if (ThreatZone.Count == 1)
                    {
                        die.ShipXPos = 583;
                        die.ShipYPos = 48;
                    }
                    if (ThreatZone.Count == 2)
                    {
                        die.ShipXPos = 644;
                        die.ShipYPos = 48;
                    }
                    ThreatZone.Add(die);
                    die.OnShip = true;
                    return;

                case "Commander":

                    switch (CommanderZone.Count)
                    {
                        case 0:
                            die.ShipXPos = 550;
                            die.ShipYPos = 220;
                            break;

                        case 1:
                            die.ShipXPos = 605;
                            die.ShipYPos = 220;
                            break;

                        case 2:
                            die.ShipXPos = 660;
                            die.ShipYPos = 220;
                            break;

                        case 3:
                            die.ShipXPos = 550;
                            die.ShipYPos = 165;
                            break;

                        case 4:
                            die.ShipXPos = 605;
                            die.ShipYPos = 165;
                            break;

                        case 5:
                            die.ShipXPos = 660;
                            die.ShipYPos = 165;
                            break;

                        default:
                            break;
                    }

                    CommanderZone.Add(die);
                    die.OnShip = true;
                    return;

                case "Tactical":

                    switch (TacticalZone.Count)
                    {
                        case 0:
                            die.ShipXPos = 550;
                            die.ShipYPos = 345;
                            break;

                        case 1:
                            die.ShipXPos = 605;
                            die.ShipYPos = 345;
                            break;

                        case 2:
                            die.ShipXPos = 660;
                            die.ShipYPos = 345;
                            break;

                        case 3:
                            die.ShipXPos = 550;
                            die.ShipYPos = 290;
                            break;

                        case 4:
                            die.ShipXPos = 605;
                            die.ShipYPos = 290;
                            break;

                        case 5:
                            die.ShipXPos = 660;
                            die.ShipYPos = 290;
                            break;

                        default:
                            break;
                    }

                    TacticalZone.Add(die);
                    die.OnShip = true;
                    return;

                case "Medic":

                    switch (MedicZone.Count)
                    {
                        case 0:
                            die.ShipXPos = 485;
                            die.ShipYPos = 470;
                            break;

                        case 1:
                            die.ShipXPos = 540;
                            die.ShipYPos = 470;
                            break;

                        case 2:
                            die.ShipXPos = 595;
                            die.ShipYPos = 470;
                            break;

                        case 3:
                            die.ShipXPos = 485;
                            die.ShipYPos = 415;
                            break;

                        case 4:
                            die.ShipXPos = 540;
                            die.ShipYPos = 415;
                            break;

                        case 5:
                            die.ShipXPos = 595;
                            die.ShipYPos = 415;
                            break;

                        default:
                            break;
                    }

                    MedicZone.Add(die);
                    die.OnShip = true;
                    return;

                case "Science":

                    switch (ScienceZone.Count)
                    {
                        case 0:
                            die.ShipXPos = 660;
                            die.ShipYPos = 470;
                            break;

                        case 1:
                            die.ShipXPos = 715;
                            die.ShipYPos = 470;
                            break;

                        case 2:
                            die.ShipXPos = 770;
                            die.ShipYPos = 470;
                            break;

                        case 3:
                            die.ShipXPos = 660;
                            die.ShipYPos = 415;
                            break;

                        case 4:
                            die.ShipXPos = 715;
                            die.ShipYPos = 415;
                            break;

                        case 5:
                            die.ShipXPos = 770;
                            die.ShipYPos = 415;
                            break;

                        default:
                            break;
                    }

                    ScienceZone.Add(die);
                    die.OnShip = true;
                    return;

                case "Engineer":

                    switch (EngineerZone.Count)
                    {
                        case 0:
                            die.ShipXPos = 547;
                            die.ShipYPos = 578;
                            break;

                        case 1:
                            die.ShipXPos = 602;
                            die.ShipYPos = 578;
                            break;

                        case 2:
                            die.ShipXPos = 657;
                            die.ShipYPos = 578;
                            break;

                        case 3:
                            die.ShipXPos = 547;
                            die.ShipYPos = 523;
                            break;

                        case 4:
                            die.ShipXPos = 602;
                            die.ShipYPos = 523;
                            break;

                        case 5:
                            die.ShipXPos = 657;
                            die.ShipYPos = 523;
                            break;

                        default:
                            break;
                    }

                    EngineerZone.Add(die);
                    die.OnShip = true;
                    return;

                default:
                    break;
            }
        }

        //public void DrawShipDie(SpriteBatch sbatch)
        //{

        //}



    }
}
