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
                        die.ShipXPos = 517;
                        die.ShipYPos = 44;
                    }
                    if (ThreatZone.Count == 1)
                    {
                        die.ShipXPos = 579;
                        die.ShipYPos = 44;
                    }
                    if (ThreatZone.Count == 2)
                    {
                        die.ShipXPos = 639;
                        die.ShipYPos = 44;
                    }
                    ThreatZone.Add(die);
                    die.OnShip = true;
                    return;

                case "Commander":


                    return;

                case "Tactical":


                    return;

                case "Medic":


                    return;

                case "Science":


                    return;

                case "Engineer":


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
