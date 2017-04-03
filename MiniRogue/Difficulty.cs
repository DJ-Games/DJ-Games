using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace MiniRogue
{
    class Difficulty
    {


        public Player Select(KeyboardState current, KeyboardState previous)
        {
            if (SingleKeyPress(current, previous, Keys.D1))
            {
                return new Player(1, 5, 5, 6);
            }
            else if (SingleKeyPress(current, previous, Keys.D2))
            {
                return new Player(0, 5, 3, 6);
            }
            else if (SingleKeyPress(current, previous, Keys.D3))
            {
                return new Player(0, 4, 2, 5);
            }
            else if (SingleKeyPress(current, previous, Keys.D4))
            {
                return new Player(0, 3, 1, 3);
            }
            else { return null; }
            
         

        }


        private bool SingleKeyPress(KeyboardState current, KeyboardState previous, Keys key)
        {
            current = Keyboard.GetState();
            if (current.IsKeyDown(key) && previous.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }




    }
}
