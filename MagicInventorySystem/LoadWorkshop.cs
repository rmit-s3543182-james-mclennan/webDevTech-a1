using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class LoadWorkshop : PageLoad
    {
        public string[] confirmedRefNum { get; set; }     
        public string bookingRefMorning { get; set; }
        public string bookingRefAfternoon { get; set; } 
        public int[] refNumMorningCount { get; set; }
        public int[] refNumAfternoonCount { get; set; }
        public int workshopMembers { get; set; }
        public int[] workshopMorningMax { get; set; }
        public int[] workshopAfternoonMax { get; set; }
        public int[] workshopEachStore { get; set; }
        public int[] bookingRef { get; set; }
        public string[] storeName { get; set; }
         
        public LoadWorkshop()
        {
            confirmedRefNum = new string[100];
            bookingRefMorning = "05Apr17M_0";
            bookingRefAfternoon = "05Apr17A_0";
            
            workshopMembers = 0;
            storeName = new string[]
                        {"Melbourne_CBD", "Melbourne_North", "Melbourne_South", "Melbourne_East", "Melbounre_West"};
            workshopMorningMax = new int[]
                        {50, 30, 20, 10, 30 };
            workshopAfternoonMax = new int[]
                        {70, 30, 50, 30, 40};
            bookingRef = new int[]
                        {0, 0, 0, 0, 0};
            refNumMorningCount = new int[]
                        {0, 0, 0, 0, 0};
        }

        public int confirmBooking(string storeName, int bookingRef, int refNumCount, int workshopMorningMax, int workshopAfternoonMax)
        {
            int choiceIndex;
            Console.WriteLine("Workshop Booking Status at " + storeName + " :");
            Console.WriteLine("================================================");
            Console.WriteLine("Morning : " + refNumCount + " / " + workshopMorningMax);
            Console.WriteLine("Afternoon : " + )     
            Console.WriteLine("1. Morning");
            Console.WriteLine("2. Afternoon");
            Console.Write("Choose the booking session between Morning and Afternoon : ");
            choice = Console.ReadLine();
            if(int.TryParse(choice, out choiceIndex)
            && choiceIndex == 1)
            {
                /* if the customer select morning workshop
                 * the customer gets specific reference number
                 * as well as refNumMorningCount++
                 * and display current status of workshop booking
                 */
                refNumCount++;
                bookingRefMorning = storeName + "_" + bookingRefMorning;
                bookingRef = int.Parse(bookingRefMorning);
                bookingRefMorning = (bookingRef + refNumCount).ToString();
                confirmedRefNum[workshopMembers] = bookingRefMorning;
                bookingRefMorning = bookingRefMorning;
            }
            else if(int.TryParse(choice, out choiceIndex)
            && choiceIndex == 2)
            {
                /* else if the customer select afternoon workshop
                 * the customer gets specific reference number
                 * as well as refNumMorningCount++
                 * and display current status of workshop booking
                 */
                refNumCount++;
                bookingRef = int.Parse(bookingRefAfternoon);
                bookingRefAfternoon = (bookingRef + refNumCount).ToString();
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
