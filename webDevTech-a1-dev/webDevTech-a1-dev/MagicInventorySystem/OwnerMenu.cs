using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class OwnerMenu
    {
        public void Owner()
        {
            Console.WriteLine("Welcome to the Magic Inventory System!(Owner)");
            Console.WriteLine("===========================================\n");
            Console.WriteLine("     1. Display All Stock Requests\n");
            Console.WriteLine("     2. Display Stock Requests (True / False)\n");
            Console.WriteLine("     3. Display All Product Lines\n");
            Console.WriteLine("     4. Return to Main Menu\n");
            Console.WriteLine("     5. Exit\n");

            int programCounter = 0;
            while (programCounter == 0)
            {
                int userInput = 0;
                Console.WriteLine("Enter an option");
                try
                {
                    userInput = Convert.ToInt32(Console.ReadLine());
                }
                catch (System.FormatException e)
                {
                    Console.WriteLine("Invalid input entered. input type must be integer.\n");
                }
                if (userInput == 1)
                {
                    programCounter++;
                }
                else if (userInput == 2)
                {
                    programCounter++;
                }
                else if (userInput == 3)
                {
                    programCounter++;
                }
                else if (userInput == 4)
                {
                    programCounter++;
                }

                else if(userInput == 5)
                {
                    programCounter++;
                }
                else if (userInput > 5 || userInput < 1)
                {
                    Console.WriteLine("Invalid input entered. please choose valid option.\n");
                }

            }
            Console.ReadKey();
        }

    }
}
