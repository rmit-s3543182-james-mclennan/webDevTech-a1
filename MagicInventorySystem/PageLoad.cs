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
        public int pageIndex { get; set; }
        public int isCompleted { get; set; }      
        public int itemIndex { get; set; }
        public int currentItemIndex { get; set; }
        public int firstItem { get; set; }
        public int lastItem { get; set; }

        public string storeID { get; set; }
        public string choice { get; set; }
        public List<Products> allStock { get; set; }
        JsonProcessor reader = new JsonProcessor();
        
        public string storeFileName { get; set; }

        public PageLoad()
        {
            storeFileName = "owners_inventory.json";
            //allStock = reader.readFile(storeFileName);

            pageIndex = 1;
            isCompleted = 0;
            firstItem = 0;
            lastItem = 5;
        }



        public int purchaseItems(int choiceIndex)
        {
            currentItemIndex = choiceIndex;
            Console.WriteLine("You have chosen " + allStock[currentItemIndex].name + ".");
            Console.Write("Select the amount of the product : ");
            choice =  Console.ReadLine();
            //if(choice == )
            foreach(Products purchaseItem in allStock)
            {
                String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}"
                                    , allStock[currentItemIndex].ID, allStock[currentItemIndex].name, allStock[currentItemIndex].stockLevel);
                Console.WriteLine(productLine);
            }
            isCompleted = 0;
            return isCompleted;
        }

        public int firstPage()
        {
            allStock = JsonConvert.DeserializeObject<List<Products>>(File.ReadAllText(storeFileName));
            totalPage = allStock.Count / 5;
            if (allStock.Count % 5 != 0)
            {
                totalPage += 1;
            }
            displayTitle();
            try
            {
                for (itemIndex = firstItem; itemIndex < lastItem; itemIndex++)
                {
                    if (allStock[itemIndex].ID >= firstItem
                    && allStock[itemIndex].ID <= lastItem
                    && allStock.IndexOf(allStock[itemIndex]) <= allStock.Count)
                    {
                        String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}"
                                                            , allStock[itemIndex].ID
                                                            , allStock[itemIndex].name
                                                            , allStock[itemIndex].stockLevel);
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
            Console.WriteLine("Page " + pageIndex + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Product ID to purchase : ");
            choice = Console.ReadLine();
            return isCompleted;
        }

        public int previousPage()
        {
            if (pageIndex <= totalPage && pageIndex > 1)
            {
                pageIndex--;
            }
            Console.Clear();
            lastItem = firstItem;
            firstItem = lastItem - 5;
            displayTitle();
            displayProducts();
            if (itemIndex < 0)
            {
                firstItem = 0;
                itemIndex = firstItem;
                lastItem = firstItem + 5;

                firstPage();
            }
            else
            {
                Console.WriteLine("Page " + pageIndex + "/" + totalPage);
                Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
                Console.Write("Enter Product ID to purchase : ");
                choice = Console.ReadLine();
            }
            isCompleted = 0;
            return isCompleted;
        }

        public int lastPage()
        {
            if (pageIndex >= totalPage)
            {
                pageIndex = totalPage;
            }
            displayTitle();
            try
            {
                for (itemIndex = firstItem; itemIndex < lastItem; itemIndex++)
                {
                    if (allStock[itemIndex].ID >= firstItem
                    && allStock[itemIndex].ID <= lastItem
                    && allStock.IndexOf(allStock[itemIndex]) <= allStock.Count)
                    {
                        String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}"
                                                            , allStock[itemIndex].ID
                                                            , allStock[itemIndex].name
                                                            , allStock[itemIndex].stockLevel);
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
            Console.WriteLine("Page " + pageIndex + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Product ID to purchase : ");
            
            choice = Console.ReadLine();
            return isCompleted;
        }

        public int nextPage()
        {
            if (pageIndex < totalPage)
            {
                pageIndex++;
            }
            Console.Clear();
            firstItem = lastItem;
            lastItem = firstItem + 5;
            displayTitle();
            displayProducts();
            if (lastItem > allStock.Count)
            {
                lastItem = allStock.Count;
                firstItem = lastItem - 5;
                itemIndex = firstItem;

                lastPage();
            }
            else
            {
                Console.WriteLine("Page " + pageIndex + "/" + totalPage);
                Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
                Console.Write("Enter Product ID to purchase : ");
                choice = Console.ReadLine();
            }
            isCompleted = 0;
            return isCompleted;
        }

        public int currentPage()
        {
            displayTitle();
            displayProducts();
            Console.WriteLine("Page " + pageIndex + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Product ID to purchase : ");
            choice = Console.ReadLine();
            isCompleted = 0;
            return isCompleted;
        }


        public void displayProducts()
        {
            try
            {
                for (itemIndex = firstItem; itemIndex < lastItem; itemIndex++)
                {
                    if (allStock[itemIndex].ID >= firstItem
                    && allStock[itemIndex].ID <= lastItem
                    && allStock.IndexOf(allStock[itemIndex]) <= allStock.Count)
                    {
                        String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}"
                                                            , allStock[itemIndex].ID
                                                            , allStock[itemIndex].name
                                                            , allStock[itemIndex].stockLevel);
                        Console.WriteLine(productLine);
                    }
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine("No more pages exist!");
            }
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
            
            if(firstItem < 1)
            {
                firstPage();
            }
            else if(lastItem >= allStock.Count)
            {
                lastPage();
            }
            else
            {
                currentPage();
            }
            isCompleted = 0;
            //choice = Console.ReadLine();
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
