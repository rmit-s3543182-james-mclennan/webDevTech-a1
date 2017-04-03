using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;

namespace MagicInventorySystem
{
    class LoadWorkshop : PageLoad
    {
        JsonProcessor reader = new JsonProcessor();
        public List<Workshops> allWorkshop { get; set; }
        public int bookingCompleted { get; set; }
        public int maxSeat { get; set; }
        public string workshopRef { get; set; }
        public string[] workshopCourse { get; set; }

        public string customerName { get; set; }
        //public string pattern { get; set; }
        //Regex regexCheck { get; set; }
        public int currentIndex { get; set; }


        public LoadWorkshop()
        {
            //pattern = @"(\w+)\s+(\w+)";
            //regexCheck = new Regex(pattern, RegexOptions.IgnoreCase);
            workshopCourse = new string[]
                {"ITM", "AMT", "HCM"};
            bookingCompleted = 0;
        }

        public int workshopConfirmation()
        {
            int choiceIndex;
            
            Console.WriteLine("Workshop Status at " + storeFileName);
            displayWorkshopTitle();
            allWorkshop = reader.readWorkshopFile(storeFileName);
            foreach (Workshops workshop in allWorkshop)
            {
                String workshopLine = String.Format("{0, -5} | {1, -25} | {2,  -30} | {3,  -10}", workshop.ID, workshop.Name, workshop.Date, workshop.Seat);
                Console.WriteLine(workshopLine);
            }
            Console.WriteLine("");
            Console.Write("Would you like to book into a workshop? ");
            choice = Console.ReadLine();
            if(choice == "Y" || choice == "y")
            {
                Console.WriteLine();
                Console.Write("Enter your name : ");
                // Try to add regex for valid name later if i have time to work on it
                customerName = Console.ReadLine();


                /* ask the customer to select workshop
                 * if workshop's available seat != 0, go to next step
                 * if the session has no space then print theres no more space
                 */ 
                Console.Write("\nChoose the workshop that you'd like to book");
                choice = Console.ReadLine();

                if(int.TryParse(choice, out choiceIndex)
                && choiceIndex > 0
                && choiceIndex <= allWorkshop.Count)
                {
                    /* if type is valid,
                     * display workshop reference number with name or stuff
                     */
                    Console.WriteLine("You have chosen " + allWorkshop[choiceIndex - 1].Name);
                    //if(allWorkshop[choiceIndex].Seat > )
                    
                     
                }




                bookingCompleted = 0;
            }
            else if(choice == "N" || choice == "n")
            {
                bookingCompleted = 1;
            }





            return bookingCompleted;
        }

        


        public void displayWorkshopTitle()
        {
            String titleLine = String.Format("\n{0, -5} | {1, -25} | {2, -30} | {3, -10}", "ID", "Name", "Date", "Available Seat");

            Console.WriteLine(titleLine);

            for (int i = 0; i < titleLine.Length; i++)
            {
                Console.Write("=");
                if (i + 1 == titleLine.Length)
                {
                    Console.Write("=\n");
                }
            }
        }



    }
}















