using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace MagicInventorySystem
{
    class PageLoad
    {
        public int totalPage { get; set; }
        public int currentPage { get; set; }
        public int isCompleted { get; set; }
        public string choice { get; set; }
        JsonProcessor reader = new JsonProcessor();

        public int displayPageOne()
        {
            // Display Page 1 and get input for next function
            Console.Clear();
            Console.WriteLine("[!] Loading products from owners_inventory.json");
            displayTitle();
            currentPage = pageOne();
            isCompleted = 0;
            Console.WriteLine("Page " + currentPage + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
            choice = Console.ReadLine();
            if (choice == "P" || choice == "p")
            {
               isCompleted = displayPageTwo();
            }
            else if(choice == "R" || choice == "r")
            {
                isCompleted = displayPageFour();
            }
            else if(choice == "C" || choice =="c")
            {
                isCompleted = transactionComplete();
            }
            else
            {
                invalidInput();
            }
            return isCompleted;
        }

        public int displayPageTwo()
        {
            // Display Page 2 and get input for next function
            Console.Clear();
            Console.WriteLine("[!] Loading products from owners_inventory.json");
            displayTitle();
            currentPage = pageTwo();
            isCompleted = 0;

            Console.WriteLine("Page " + currentPage + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
            choice = Console.ReadLine();
            if (choice == "P" || choice == "p")
            {
                isCompleted = displayPageThree();
            }
            else if(choice == "R" || choice == "r")
            {
                isCompleted = displayPageOne();
            }
            else if (choice == "C" || choice == "c")
            {
                isCompleted = transactionComplete();
            }
            else
            {
                invalidInput();
            }
            return isCompleted;
        }

        public int displayPageThree()
        {
            // Display Page 3 and get input for next function
            Console.Clear();
            Console.WriteLine("[!] Loading products from owners_inventory.json");
            displayTitle();
            currentPage = pageThree();
            isCompleted = 0;

            Console.WriteLine("Page " + currentPage + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
            choice = Console.ReadLine();
            if(choice == "P" || choice == "p")
            {
                isCompleted = displayPageFour();
            }
            else if (choice == "R" || choice == "r")
            {
                isCompleted = displayPageTwo();
            }
            else if (choice == "C" || choice == "c")
            {
                isCompleted = transactionComplete();
            }
            else
            {
                invalidInput();
            }
            return isCompleted;
        }

        public int displayPageFour()
        {
            // Display Page 1 and get input for next function
            Console.Clear();
            Console.WriteLine("[!] Loading products from owners_inventory.json");
            displayTitle();
            currentPage = pageFour();
            isCompleted = 0;
            Console.WriteLine("Page " + currentPage + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
            choice = Console.ReadLine();
            if (choice == "P" || choice == "p")
            {
                isCompleted = displayPageOne();
            }
            else if (choice == "R" || choice == "r")
            {
                isCompleted = displayPageThree();
            }
            else if (choice == "C" || choice == "c")
            {
                isCompleted = transactionComplete();
            }
            else
            {
                invalidInput();
            }
            return isCompleted;
        }

        public int invalidInput()
        {
            Console.Clear();
            displayTitle();

            if (currentPage == 1)
            {
                pageOne();
            }
            else if (currentPage == 2)
            {
                pageTwo();
            }
            else if (currentPage == 3)
            {
                pageThree();
            }

            isCompleted = 0;
            return isCompleted;
        }

        public int transactionComplete()
        {
            isCompleted = 1;
            return isCompleted;
        }

        public int pageOne()
        {
            // Display first page's products
            List<Products> allStock = JsonConvert.DeserializeObject<List<Products>>(File.ReadAllText("owners_inventory.json"));
            for (int currentItem = 0; currentItem < 5; currentItem++)
            {
                String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}", allStock[currentItem].ID, allStock[currentItem].name, allStock[currentItem].stockLevel);
                Console.WriteLine(productLine);
                
            }
            Console.WriteLine();
            currentPage = 1;
            return currentPage;
        }

        public int pageTwo()
        {
            // Display second page's products
            List<Products> allStock = JsonConvert.DeserializeObject<List<Products>>(File.ReadAllText("owners_inventory.json"));

            for (int currentItem = 5; currentItem < 10; currentItem++)
            {
                String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}", allStock[currentItem].ID, allStock[currentItem].name, allStock[currentItem].stockLevel);
                Console.WriteLine(productLine);
            }
            Console.WriteLine();
            currentPage = 2;
            return currentPage;
        }
        public int pageThree()
        {
            // Display third page's products
            List<Products> allStock = JsonConvert.DeserializeObject<List<Products>>(File.ReadAllText("owners_inventory.json"));
            for (int currentItem = 10; currentItem < 15; currentItem++)
            {
                String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}", allStock[currentItem].ID, allStock[currentItem].name, allStock[currentItem].stockLevel);
                Console.WriteLine(productLine);
            }
            Console.WriteLine();
            currentPage = 3;
            return currentPage;
        }

        public int pageFour()
        {
            List<Products> allStock = JsonConvert.DeserializeObject<List<Products>>(File.ReadAllText("owners_inventory.json"));
            for (int currentItem = 15; currentItem < 20; currentItem++)
            {
                try
                {
                    String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}", allStock[currentItem].ID, allStock[currentItem].name, allStock[currentItem].stockLevel);
                    Console.WriteLine(productLine);
                }
                catch(ArgumentOutOfRangeException e)
                {
                    Console.WriteLine();
                    currentPage = 4;
                }
            }
            return currentPage;
        }

        public void displayTitle()
        {
            // Display title line
            String titleLine = String.Format("\n{0, -5} | {1, -15} | {2, -10}", "ID", "Name", "Stock Level");

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
