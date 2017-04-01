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
        public int itemIndex { get; set; }
        public int pageIndex { get; set; }
        public int firstItem { get; set; }
        public int lastItem { get; set; }
        public List<Products> allStock { get; set; }
        JsonProcessor reader = new JsonProcessor();
        public PageLoad()
        {
            allStock = JsonConvert.DeserializeObject<List<Products>>(File.ReadAllText("owners_inventory.json"));
            totalPage = allStock.Count / 5;
            if (allStock.Count % 5 != 0)
            {
                totalPage += 1;
            }
            currentPage = 1;
            isCompleted = 0;
            itemIndex = 1;
            pageIndex = 1;
            firstItem = 1;
            lastItem = 5;
        }

        public int initialPage()
        {
            displayTitle();
            try
            {
                for (itemIndex = firstItem - 1; itemIndex < lastItem; itemIndex++)
                {
                    if (allStock[itemIndex].ID >= firstItem
                    && allStock[itemIndex].ID <= lastItem
                    && allStock.IndexOf(allStock[itemIndex]) <= allStock.Count)
                    {
                        String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}", allStock[itemIndex].ID, allStock[itemIndex].name, allStock[itemIndex].stockLevel);
                        Console.WriteLine(productLine);

                    }
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine("This is the first page!");
            }
            isCompleted = 0;
            Console.WriteLine("Page " + currentPage + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
            choice = Console.ReadLine();
            return isCompleted;
        }

        public void displayProductPage()
        {
            displayTitle();
            for (itemIndex = firstItem - 1; itemIndex < lastItem; itemIndex++)
            {
                if (allStock[itemIndex].ID >= firstItem
                && allStock[itemIndex].ID <= lastItem
                && allStock.IndexOf(allStock[itemIndex]) <= allStock.Count)
                {
                    String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}", allStock[itemIndex].ID, allStock[itemIndex].name, allStock[itemIndex].stockLevel);
                    Console.WriteLine(productLine);



                }
            }
            Console.WriteLine("Page " + currentPage + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
            choice = Console.ReadLine();
            // Go to page 2
            if (itemIndex == lastItem && (choice == "P" || choice == "p"))
            {
                firstItem = lastItem;
                lastItem += firstItem;
                displayTitle();
                for (itemIndex = firstItem - 1; itemIndex < lastItem; itemIndex++)
                {
                    if (allStock[itemIndex].ID >= firstItem
                    && allStock[itemIndex].ID <= lastItem
                    && allStock.IndexOf(allStock[itemIndex]) <= allStock.Count)
                    {
                        String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}", allStock[itemIndex].ID, allStock[itemIndex].name, allStock[itemIndex].stockLevel);
                        Console.WriteLine(productLine);
                    }
                }
                Console.WriteLine("Page " + currentPage + "/" + totalPage);
                Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
                Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
                choice = Console.ReadLine();
            }

                // Go back to last page
                else if (itemIndex == lastItem && (choice == "R" || choice == "r"))
                            {
                firstItem = allStock.Count - 4;
                lastItem += firstItem;
                displayTitle();
                for (itemIndex = firstItem - 1; itemIndex < lastItem; itemIndex++)
                {
                    if (allStock[itemIndex].ID >= firstItem
                    && allStock[itemIndex].ID <= lastItem
                    && allStock.IndexOf(allStock[itemIndex]) <= allStock.Count)
                    {
                        String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}", allStock[itemIndex].ID, allStock[itemIndex].name, allStock[itemIndex].stockLevel);
                        Console.WriteLine(productLine);                        
                    }


                    if (itemIndex == lastItem)
                    {
                        pageIndex++;
                        firstItem = lastItem;
                        lastItem += firstItem;
                    }
                }
                Console.WriteLine("Page " + currentPage + "/" + totalPage);
                Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
                Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
                choice = Console.ReadLine();
            }
        }

        public int invalidPage()
        {
            Console.WriteLine("No more page!");
            Console.WriteLine("Page " + currentPage + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
            isCompleted = 0;
            choice = Console.ReadLine();
            return isCompleted;
        }

        public int lastPage()
        {
            if (currentPage <= totalPage)
            {
                currentPage = totalPage;
            }
            Console.Clear();
            currentPage = totalPage;
            firstItem = allStock.Count - 5;
            lastItem += firstItem;
            displayTitle();
            try
            {
                for (itemIndex = firstItem; itemIndex < lastItem; itemIndex++)
                {
                    if (allStock[itemIndex].ID >= firstItem
                    && allStock[itemIndex].ID <= lastItem
                    && allStock.IndexOf(allStock[itemIndex]) <= allStock.Count)
                    {
                        String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}", allStock[itemIndex].ID, allStock[itemIndex].name, allStock[itemIndex].stockLevel);
                        Console.WriteLine(productLine);
                    }
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine("This is the last page!");
            }
            
            isCompleted = 0;
            Console.WriteLine("Page " + currentPage + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
            choice = Console.ReadLine();
            return isCompleted;
        }

        public int nextPage()
        {
            if (currentPage <= totalPage)
            {
                currentPage++;
            }
            Console.Clear();
            firstItem = lastItem;
            lastItem = firstItem + 5;
            displayTitle();
            try
            {
                for (itemIndex = firstItem; itemIndex < lastItem; itemIndex++)
                {
                    if (allStock[itemIndex].ID >= firstItem
                    && allStock[itemIndex].ID <= lastItem
                    && allStock.IndexOf(allStock[itemIndex]) <= allStock.Count)
                    {
                        String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}", allStock[itemIndex].ID, allStock[itemIndex].name, allStock[itemIndex].stockLevel);
                        Console.WriteLine(productLine);
                    }
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine("This is the last page!");
            }
            
            isCompleted = 0;
            Console.WriteLine("Page " + currentPage + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
            choice = Console.ReadLine();
            return isCompleted;
        }

        public int previousPage()
        {
            if (currentPage <= totalPage)
            {
                currentPage--;
            }
            Console.Clear();
            lastItem = firstItem;
            firstItem = lastItem - 5;
            displayTitle();
            try
            {
                for (itemIndex = firstItem; itemIndex < lastItem; itemIndex++)
                {
                    if (allStock[itemIndex].ID >= firstItem
                    && allStock[itemIndex].ID <= lastItem
                    && allStock.IndexOf(allStock[itemIndex]) <= allStock.Count)
                    {
                        String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}", allStock[itemIndex].ID, allStock[itemIndex].name, allStock[itemIndex].stockLevel);
                        Console.WriteLine(productLine);
                    }
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("This is the first page!");
            }
            isCompleted = 0;
            Console.WriteLine("Page " + currentPage + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
            choice = Console.ReadLine();
            return isCompleted;
        }

        //public int outOfStock()
        //{
        //    Console.Clear();
        //    displayTitle();
        //    Console.WriteLine("The product is currently out of stock!");
        //    isCompleted = 0;
        //    return isCompleted;
        //}

        public int invalidInput()
        {
            Console.Clear();
            Console.WriteLine("Invalid input. Try again.");
            isCompleted = 0;
            return isCompleted;
        }

        public int transactionComplete()
        {
            Console.WriteLine("Transaction Done!");
            isCompleted = 1;
            return isCompleted;
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
