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

        public string workshopDate { get; set; }
        public string workshopBranch { get; set; }
        public string customerName { get; set; }
        //public string pattern { get; set; }
        //Regex regexCheck { get; set; }
        public int currentIndex { get; set; }

        // Initialize variables in constructor
        public LoadWorkshop()
        {  
            //pattern = @"(\w+)\s+(\w+)";
            //regexCheck = new Regex(pattern, RegexOptions.IgnoreCase);
            workshopCourse = new string[]
                            {"ITM_", "AMT_", "HCM_"};
            bookingCompleted = 0;
        }

        // Workshop progresses begin
        public int workshopConfirmation()
        {
            allWorkshop = reader.readWorkshopFile(workshopBranch + "_Workshop.json");
            int choiceIndex;
            Console.WriteLine("Workshop Status at " + workshopBranch);
            displayWorkshopTitle();
            
            // Display all workshops in a store
            foreach (Workshops workshop in allWorkshop)
            {
                String workshopLine = String.Format("{0, -5} | {1, -25} | {2,  -30} | {3,  -20}", workshop.ID, workshop.Name, workshop.Date, workshop.availableSeat + " / " + workshop.maxSeat);
                Console.WriteLine(workshopLine);
            }
            Console.WriteLine("");
            Console.Write("Would you like to book into a workshop?( Y / N) ");
            choice = Console.ReadLine();
            if (choice == "Y" || choice == "y")
            {
                Console.WriteLine();
                Console.Write("Enter your name : ");
                // Try to add regex for valid name later if i have time to work on it
                customerName = Console.ReadLine();
                Console.Write("\nChoose the workshop that you'd like to book : ");
                choice = Console.ReadLine();

                if (int.TryParse(choice, out choiceIndex)
                && choiceIndex > 0
                && choiceIndex <= allWorkshop.Count)
                {
                    workshopAvailability(choiceIndex - 1);
                }
                else
                {
                    wrongInput();
                }

            }
            else if (choice == "N" || choice == "n")
            {
                Console.WriteLine("Bye!");
                bookingCompleted = 1;
            }
            else
            {
                wrongInput();
            }
            return bookingCompleted;
        }

        /* Once a workshop booking is completed
         * deduct availableSeat
         */
        private void deductAvailableSeat(int currentIndex)
        {
            allWorkshop[currentIndex].availableSeat -= 1;
            reader.writeToWorkshopFIle(workshopBranch + "_Workshop.json", allWorkshop);
        }

        // Display workshop title line
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

        // Called when invalid input is typed
        public void wrongInput()
        {
            Console.Clear();
            Console.WriteLine("Invalid input. Try again");
            bookingCompleted = 0;
        }

        // Check whether the chosen workshop has available seat
        public int workshopAvailability(int index)
        {
            
            if (allWorkshop[index].availableSeat >= 1)
            {
                Console.WriteLine("You have successfully booked into the " + allWorkshop[index].Name + " workshop!");                
                bookingCompleted = workshopBookingSummary(index);
            }
            else if (allWorkshop[index].availableSeat < 1)
            {
                Console.Clear();
                Console.WriteLine("The workshop you have chosen is now fully booked");
                bookingCompleted = 0;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Try again");
                Console.WriteLine("yay");
                bookingCompleted = 0;
                choice = Console.ReadLine();
            }
            return bookingCompleted;
        }

        // Display workshop summary
        public int workshopBookingSummary(int index)
        {
            int bookedSeat = 0;
            if (allWorkshop[index].availableSeat == allWorkshop[index].maxSeat)      // 10 == 10
            {
                bookedSeat = 1;
                deductAvailableSeat(index);
            }
            else if(allWorkshop[index].availableSeat < allWorkshop[index].maxSeat  // 0 < availableSeat(1 ~ 9) < 10
            && allWorkshop[index].availableSeat > 0)
            {
                bookedSeat = allWorkshop[index].maxSeat - allWorkshop[index].availableSeat + 1;
                deductAvailableSeat(index);
            }
            else if(allWorkshop[index].availableSeat == 0)
            {
                Console.WriteLine("The workshop you have chosen is now fully booked");
            }            
            Console.WriteLine("\n=============== Workshop Booking Summary ===============\n");
            Console.WriteLine(customerName + "'s booking summary is : \n");
            Console.WriteLine("Name : " + customerName);
            Console.WriteLine("Course Name : " + allWorkshop[index].Name);
            Console.WriteLine("Date : " + allWorkshop[index].Date);
            Console.WriteLine("Reference Number : " + workshopCourse[index] + workshopBranch + "_" + bookedSeat);
            Console.WriteLine("\n========================================================\n");
            return bookingCompleted = 1;
        }

    }
}















