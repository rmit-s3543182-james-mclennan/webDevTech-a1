using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class LoadWorkshop : PageLoad
    {
        public string[] confirmedRefNum { get; set; }     // form : 05Apr17M-1 , 05Apr17A-1
        public string bookingRefMorning { get; set; }
        public string bookingRefAfternoon { get; set; } 
        public int refNumCount { get; set; }
        public int workshopMembers { get; set; }
        public int[] workshopMorningMax { get; set; }
        public int[] workshopAfternoonMax { get; set; }
        public string[] storeName { get; set; }
         
        public LoadWorkshop()
        {
            confirmedRefNum = new string[100];
            bookingRefMorning = "05Apr17M-";
            bookingRefAfternoon = "05Apr17A-";
            refNumCount = 0;
            workshopMembers = 0;
            storeName = new string[]
                        {"Melbourne CBD", "Melbourne North", "Melbourne South", "Melbourne East", "Melbounre West"};
            workshopMorningMax = new int[]
                        {50, 30, 20, 10, 30 };
            workshopAfternoonMax = new int[]
                        {70, 30, 50, 30, 40};
        }

        public int confirmBooking(string storeName)
        {
            int choiceIndex;
            Console.WriteLine("Workshop Booking Status at " + storeName + " ");
            Console.WriteLine("================================================");
            Console.WriteLine("Choose the booking session between Morning and Afternoon : ");
            Console.WriteLine("1. Morning");
            Console.WriteLine("2. Afternoon");
            choice = Console.ReadLine();
            if(int.TryParse(choice, out choiceIndex)
            && choiceIndex == 1)
            {
                refNumCount++;
                bookingRefMorning += refNumCount;
                confirmedRefNum[workshopMembers] = bookingRefMorning;
            }
            else if(int.TryParse(choice, out choiceIndex)
            && choiceIndex == 2)
            {
                refNumCount++;
                bookingRefAfternoon += refNumCount;
                confirmedRefNum[workshopMembers] = bookingRefAfternoon;                  
            }
            else
            {
                invalidInput();
            }
            Console.WriteLine("Your booking reference number is : " + confirmedRefNum[workshopMembers]);
            workshopMembers++;
            isCompleted = 0;
            return isCompleted;
        }
    }
}
