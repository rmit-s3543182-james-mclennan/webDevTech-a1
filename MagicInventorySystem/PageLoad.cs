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
        public List<Products> pageDistributor { get; set; }
        //public IEnumerable<Products> pageDistributor { get; set; }
        JsonProcessor reader = new JsonProcessor();
        
        public PageLoad()
        {
            allStock = JsonConvert.DeserializeObject<List<Products>>(File.ReadAllText("owners_inventory.json"));
            pageDistributor = new List<Products>(allStock.Count / 5 + 1);
            totalPage = allStock.Count / 5;
            if(allStock.Count % 5 != 0)
            {
                totalPage += 1;
            }
            currentPage = 1;
            isCompleted = 0;
            pageIndex = 1;
            firstItem = 1;
            lastItem = 5;
        }

        //public int outOfStock()
        //{
        //    Console.Clear();
        //    displayTitle();

        //    if (currentPage == 1)
        //    {
        //        pageOne();
        //    }
        //    else if (currentPage == 2)
        //    {
        //        pageTwo();
        //    }
        //    else if (currentPage == 3)
        //    {
        //        pageThree();
        //    }
        //    Console.WriteLine("The product is currently out of stock!");
        //    isCompleted = 0;
        //    return isCompleted;
        //}

        //public int invalidInput()
        //{
        //    Console.Clear();
        //    displayTitle();

        //    if (currentPage == 1)
        //    {
        //        pageOne();
        //    }
        //    else if (currentPage == 2)
        //    {
        //        pageTwo();
        //    }
        //    else if (currentPage == 3)
        //    {
        //        pageThree();
        //    }
        //    Console.WriteLine("Invalid input. Try again.");
        //    isCompleted = 0;
        //    return isCompleted;
        //}

        //public int transactionComplete()
        //{
        //    isCompleted = 1;
        //    return isCompleted;
        //}

        public void displayProductPage()
        {
            // Page 1
            initialPage();

            // Go to page 2
            if (itemIndex == lastItem
            && (choice == "P" || choice == "p")
            && itemIndex <= allStock.Count)
            {
                nextPage();
                // Go to next page (page 3)
                if (itemIndex == lastItem
                    && (choice == "P" || choice == "p")
                    && itemIndex <= allStock.Count)
                {
                    nextPage();
                    // Go to next page and check if the last page
                    if(itemIndex == lastItem
                    && (choice == "P" || choice == "p")
                    && itemIndex <= allStock.Count)
                    {
                        nextPage();
                    }
                    else if ((choice == "P" || choice == "p")
                    && itemIndex > allStock.Count)
                    {
                        Console.WriteLine("Dang!");
                        Console.WriteLine("This is the last page!");
                    }
                }
                else if((choice == "P" || choice == "p")
                && itemIndex > allStock.Count)
                {
                    Console.WriteLine("This is the last page!");
                }
                // Go back to page (page 1)
                else if(itemIndex == lastItem
                && (choice == "R" || choice == "r")
                && itemIndex <= allStock.Count)
                {
                    previousPage();
                }
            }

            else if ((choice == "P" || choice == "p")
                && itemIndex > allStock.Count)
            {
                Console.Clear();
                Console.WriteLine("This is the last page!");
            }
            // Go back to last page
            else if(itemIndex == lastItem
            && (choice == "R" || choice == "r")
            && itemIndex <= allStock.Count)
            {
                lastPage();
            }
        }

        public void initialPage()
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
            Console.WriteLine("Page " + currentPage + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
            choice = Console.ReadLine();
        }

        public void lastPage()
        {
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
            }catch(ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine("This is the last page!");
            }

            Console.WriteLine("Page " + currentPage + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
            choice = Console.ReadLine();
        }

        public void nextPage()
        {
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
            }catch(ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine("This is the last page!");
            }

            Console.WriteLine("Page " + currentPage + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
            choice = Console.ReadLine();
        }

        public void previousPage()
        {
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
            Console.WriteLine("Page " + currentPage + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
            choice = Console.ReadLine();
        }

        public void displayTitle()
        {
            //Console.WriteLine("itemIndex : " + itemIndex);
            //Console.WriteLine("firstItem : " + firstItem);
            //Console.WriteLine("lastItem : " + lastItem);

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

//public int pageTwo()
//{
//    // Display second page's products
//    for (int currentItem = 5; currentItem < 10; currentItem++)
//    {
//        String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}", allStock[currentItem].ID, allStock[currentItem].name, allStock[currentItem].stockLevel);
//        Console.WriteLine(productLine);
//    }
//    Console.WriteLine();
//    currentPage = 2;
//    return currentPage;
//}
//public int pageThree()
//{
//    // Display third page's products
//    for (int currentItem = 10; currentItem < 15; currentItem++)
//    {
//        String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}", allStock[currentItem].ID, allStock[currentItem].name, allStock[currentItem].stockLevel);
//        Console.WriteLine(productLine);
//    }
//    Console.WriteLine();
//    currentPage = 3;
//    return currentPage;
//}

//public int pageFour()
//{
//    for (int currentItem = 15; currentItem < 20; currentItem++)
//    {
//        try
//        {
//            String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}", allStock[currentItem].ID, allStock[currentItem].name, allStock[currentItem].stockLevel);
//            Console.WriteLine(productLine);
//        }
//        catch(ArgumentOutOfRangeException e)
//        {
//            Console.WriteLine();
//            currentPage = 4;
//        }
//    }
//    return currentPage;
//}

//public int pageOne()
//{
//    // Display first page's products
//    for (int currentItem = 0; currentItem < 5; currentItem++)
//    {
//        String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}", allStock[currentItem].ID, allStock[currentItem].name, allStock[currentItem].stockLevel);
//        Console.WriteLine(productLine);

//    }
//    Console.WriteLine();
//    currentPage = 1;
//    return currentPage;
//}
//public int displayPageOne()
//{
//    // Display Page 1 and get input for next function

//    Console.Clear();
//    Console.WriteLine("[!] Loading products from owners_inventory.json");
//    displayTitle();
//    currentPage = pageOne();
//    isCompleted = 0;
//    Console.WriteLine("Page " + currentPage + "/" + totalPage);
//    Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
//    Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
//    choice = Console.ReadLine();
//    try
//    {
//        if (choice == "P" || choice == "p")
//        {
//            isCompleted = displayPageTwo();
//        }
//        else if (choice == "R" || choice == "r")
//        {
//            isCompleted = displayPageFour();
//        }
//        else if (choice == "C" || choice == "c")
//        {
//            Console.WriteLine("Transaction done!");
//            isCompleted = transactionComplete();
//        }
//        else if (Convert.ToInt32(choice) < allStock.Count
//        && Convert.ToInt32(choice) > 0)
//        {
//            foreach (Products getItem in allStock)
//            {
//                if (choice == Convert.ToString(getItem.ID)
//                && getItem.stockLevel > 0)
//                {
//                    Console.Write("Choose the quantity of the product(current stock : "
//                        + getItem.stockLevel + ") : ");
//                    choice = Console.ReadLine();
//                }
//                // out of stock
//                else if (choice == Convert.ToString(getItem.ID) && getItem.stockLevel <= 0)
//                {
//                    outOfStock();                        }
//            }
//        }
//        else
//        {
//            isCompleted = invalidInput();
//        }
//    }
//    catch (FormatException e)
//    {
//        isCompleted = invalidInput();
//    }
//    return isCompleted;
//}

//public int displayPageTwo()
//{
//    // Display Page 2 and get input for next function
//    Console.Clear();
//    Console.WriteLine("[!] Loading products from owners_inventory.json");
//    displayTitle();
//    currentPage = pageTwo();
//    isCompleted = 0;

//    Console.WriteLine("Page " + currentPage + "/" + totalPage);
//    Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
//    Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
//    choice = Console.ReadLine();
//    try
//    {
//        if (choice == "P" || choice == "p")
//        {
//            isCompleted = displayPageThree();
//        }
//        else if (choice == "R" || choice == "r")
//        {
//            isCompleted = displayPageOne();
//        }
//        else if (choice == "C" || choice == "c")
//        {
//            Console.WriteLine("Transaction done!");
//            isCompleted = transactionComplete();
//        }
//        else if (Convert.ToInt32(choice) < allStock.Count
//        && Convert.ToInt32(choice) > 0)
//        {
//            foreach (Products getItem in allStock)
//            {
//                if (choice == Convert.ToString(getItem.ID)
//                && getItem.stockLevel > 0)
//                {
//                    Console.Write("Choose the quantity of the product(current stock : "
//                        + getItem.stockLevel + ") : ");
//                    choice = Console.ReadLine();
//                }
//                // out of stock
//                else if (choice == Convert.ToString(getItem.ID) && getItem.stockLevel <= 0)
//                {
//                    outOfStock();                        }
//            }
//        }
//        else
//        {
//            isCompleted = invalidInput();
//        }
//    }
//    catch (FormatException e)
//    {
//        isCompleted = invalidInput();
//    }
//    return isCompleted;
//}

//public int displayPageThree()
//{
//    // Display Page 3 and get input for next function
//    Console.Clear();
//    Console.WriteLine("[!] Loading products from owners_inventory.json");
//    displayTitle();
//    currentPage = pageThree();
//    isCompleted = 0;

//    Console.WriteLine("Page " + currentPage + "/" + totalPage);
//    Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
//    Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
//    choice = Console.ReadLine();
//    try
//    {
//        if (choice == "P" || choice == "p")
//        {
//            isCompleted = displayPageFour();
//        }
//        else if (choice == "R" || choice == "r")
//        {
//            isCompleted = displayPageTwo();
//        }
//        else if (choice == "C" || choice == "c")
//        {
//            Console.WriteLine("Transaction done!");
//            isCompleted = transactionComplete();
//        }
//        else if (Convert.ToInt32(choice) < allStock.Count
//        && Convert.ToInt32(choice) > 0)
//        {
//            foreach (Products getItem in allStock)
//            {
//                if (choice == Convert.ToString(getItem.ID)
//                && getItem.stockLevel > 0)
//                {
//                    Console.Write("Choose the quantity of the product(current stock : "
//                        + getItem.stockLevel + ") : ");
//                    choice = Console.ReadLine();
//                }
//                // out of stock
//                else if (choice == Convert.ToString(getItem.ID) && getItem.stockLevel <= 0)
//                {
//                    outOfStock();
//                }
//            }
//        }
//        else
//        {
//            isCompleted = invalidInput();
//        }
//    }
//    catch (FormatException e)
//    {
//        isCompleted = invalidInput();
//    }
//    return isCompleted;
//}

//public int displayPageFour()
//{
//    // Display Page 1 and get input for next function
//    Console.Clear();
//    Console.WriteLine("[!] Loading products from owners_inventory.json");
//    displayTitle();
//    currentPage = pageFour();
//    isCompleted = 0;
//    Console.WriteLine("Page " + currentPage + "/" + totalPage);
//    Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
//    Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
//    choice = Console.ReadLine();
//    try
//    {
//        if (choice == "P" || choice == "p")
//        {
//            isCompleted = displayPageOne();
//        }
//        else if (choice == "R" || choice == "r")
//        {
//            isCompleted = displayPageThree();
//        }
//        else if (choice == "C" || choice == "c")
//        {
//            Console.WriteLine("Transaction done!");
//            isCompleted = transactionComplete();
//        }
//        else if (Convert.ToInt32(choice) < allStock.Count
//        && Convert.ToInt32(choice) > 0)
//        {
//            foreach (Products getItem in allStock)
//            {
//                if (choice == Convert.ToString(getItem.ID)
//                && getItem.stockLevel > 0)
//                {
//                    Console.Write("Choose the quantity of the product(current stock : "
//                        + getItem.stockLevel + ") : ");
//                    choice = Console.ReadLine();
//                }
//                // out of stock
//                else if (choice == Convert.ToString(getItem.ID) && getItem.stockLevel <= 0)
//                {
//                    Console.WriteLine("The product is currently out of stock!");
//                }
//            }
//        }
//        else
//        {
//            isCompleted = invalidInput();
//        }
//    }
//    catch (FormatException e)
//    {
//        isCompleted = invalidInput();
//    }
//    return isCompleted;
//}