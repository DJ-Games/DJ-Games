using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Bob's Big Give Away");
            Console.WriteLine("Choose a Door: 1, 2 or 3:  ");
            string userValue = Console.ReadLine();

            string message = "";

            if (userValue == "1")
            {
                message = "You Won a CAR!!";
            }

            else if (userValue == "2")
            {
                message = "You won a new boat!";
                // Console.WriteLine(message); 
            }

            else if (userValue == "3")
            {
                message = "You won a cat!";
                // Console.WriteLine(message);
            }

            else
            {
                message = "Sorry, we didn't understand";
                //Console.WriteLine(message);  
            }

            Console.WriteLine(message); 
            Console.ReadLine(); 

        }
    }
}
