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
            pageIndex = 1;
            firstItem = 0;
            lastItem = 5;
        }

        public int firstPage()
        {
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
                Console.WriteLine("This is the first page! firstPage");
                if (itemIndex < 0)
                {
                    firstItem = 0;
                    itemIndex = firstItem;
                    lastItem = firstItem + 5;
                }
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
            if (currentPage < totalPage && currentPage > 1)
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
                if (itemIndex < 0)
                {
                    firstItem = 0;
                    itemIndex = firstItem;
                    lastItem = firstItem + 5;
                }
                Console.Clear();
                Console.WriteLine("This is the first page! previousPage");
                firstPage();
            }
            isCompleted = 0;
            Console.WriteLine("Page " + currentPage + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
            choice = Console.ReadLine();
            return isCompleted;
        }

        
























        public int lastPage()
        {
            if (currentPage >= totalPage)
            {
                currentPage = totalPage;
            }
            //Console.Clear();
            //firstItem = allStock.Count - 5;
            //lastItem = firstItem + 5;
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
                Console.WriteLine("This is the last page! lastPage");
                if (lastItem > allStock.Count)
                {
                    lastItem = allStock.Count;
                    firstItem = lastItem - 5;
                    itemIndex = firstItem;
                }

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
            if (currentPage < totalPage)
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

                if (lastItem > allStock.Count)
                {
                    lastItem = allStock.Count;
                    firstItem = lastItem - 5;
                    itemIndex = firstItem;
                }
                Console.Clear();
                Console.WriteLine("This is the last page! nextPage");
                lastPage();
            }
            isCompleted = 0;
            Console.WriteLine("Page " + currentPage + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
            choice = Console.ReadLine();
            return isCompleted;
        }

        public int invalidPage()
        {
            Console.WriteLine("No more page!");
            Console.WriteLine("Page " + currentPage + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
            isCompleted = 0;
            if(itemIndex < 0)
            {
                itemIndex = 1;
            }
            else if(itemIndex > allStock.Count)
            {
                itemIndex = allStock.Count - 4;
            }
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
            choice = Console.ReadLine();
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
