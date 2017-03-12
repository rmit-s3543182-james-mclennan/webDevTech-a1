using System;

namespace MagicInventorySystem
{
    internal class MainMenu
    {
        public void launchMenu()
        {
            Console.WriteLine("Welcome to the Magic Inventory System!");
            Console.WriteLine("===========================================\n");
            Console.WriteLine("     1. Owner\n");
            Console.WriteLine("     2. Franchise Owner\n");
            Console.WriteLine("     3. Customer\n");
            Console.WriteLine("     4. Quit\n");

            int programCounter = 0;
            while (programCounter == 0)
            {
                int userInput = 0;
                Console.WriteLine("Enter an option");
                try
                {
                    userInput = Convert.ToInt32(Console.ReadLine());
                }
                catch(System.FormatException e)
                {
                    Console.WriteLine("Invalid input entered. input type must be integer.\n");
                }
                if (userInput == 1)
                {
                    OwnerMenu owner = new OwnerMenu();
                    owner.Owner();
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
                else if(userInput > 4 || userInput < 1)
                {
                    Console.WriteLine("Invalid input entered. please choose valid option.\n");
                }

            }
            Console.ReadKey();

        }
    }
}