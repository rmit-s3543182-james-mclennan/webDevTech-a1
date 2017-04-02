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
        public int purchaseItemIndex { get; set; }
        public int firstItem { get; set; }
        public int lastItem { get; set; }
        public int purchaseCount { get; set; }

        public string storeID { get; set; }
        public string storeFileName { get; set; }
        public string choice { get; set; }

        public List<Products> allStock { get; set; }
        public List<Products> soldItems { get; set; }
        JsonProcessor reader = new JsonProcessor();

        public PageLoad()
        {
            storeFileName = "owners_inventory.json";
            //allStock = reader.readFile(storeFileName);
            purchaseItemIndex = 0;
            purchaseCount = 0;
            pageIndex = 1;
            isCompleted = 0;
            firstItem = 0;
            lastItem = 5;
        }

        private void purchaseProgress(int amount, int currentItemIndex)
        {
            //allStock = reader.readFile(storeFileName);
            allStock[currentItemIndex].stockLevel -= amount;
            File.WriteAllText(storeFileName, JsonConvert.SerializeObject(allStock, Formatting.Indented));
            purchaseCount++;
        }

        public int displayPurchaseSummary()
        {
            Console.WriteLine("You have purchased : \n");
            try
            {
                for(int i = 0; i < purchaseItemIndex; i++)
                {
                    Console.WriteLine(soldItems[i].name + " " + soldItems[i].stockLevel + "ea");
                }
            }
            catch(ArgumentOutOfRangeException e)
            {

            }
            Console.WriteLine("Would you like to book a workshop?\n");
            choice = Console.ReadLine();
            if (choice == "Y" || choice == "y")
            {
                Console.WriteLine("The total price of your purchase has got 10% discount!");
                transactionComplete();
            }
            else if (choice == "N" || choice == "n")
            {
                transactionComplete();
            }
            else
            {
                invalidInput();
            }


            return isCompleted = 0;
        }

        public int purchaseItems(int choiceIndex)
        {
            currentItemIndex = choiceIndex;
            Console.WriteLine("You have chosen " + allStock[currentItemIndex].name + ".");
            Console.Write("Enter the amount of the product : ");
            choice =  Console.ReadLine();
            // if the item exists(true), ask how many to get
            if (int.TryParse(choice, out choiceIndex)
            && choiceIndex > 0
            && choiceIndex <= allStock[currentItemIndex].stockLevel)
            {
                /* Deduct the amount of the product from appropriate store json file
                 * and purchasedItem++
                 */
                purchaseProgress(choiceIndex, currentItemIndex);    // works
                Console.WriteLine("You have purchased " + choiceIndex + "ea of " + allStock[currentItemIndex].name);
                soldItems[purchaseItemIndex++].name = allStock[currentItemIndex].name;
                soldItems[purchaseItemIndex++].stockLevel = choiceIndex;


                // Ask again whether the customer wants to buy more products, if so purchaseItem++
                Console.WriteLine("Do you want to buy more products?(Y / N)");
                choice = Console.ReadLine();
                if(choice == "Y" || choice == "y")
                {
                    Console.Clear();
                    currentPage();
                }
                else if(choice == "N" || choice == "n")
                {
                    Console.Write("Do you want to pay for credit card?");
                    if(choice == "Y" || choice == "y")
                    {
                        Console.WriteLine("Credit card payment is being processed!");
                    }
                    else if(choice == "N" || choice == "n")
                    {
                        Console.WriteLine("Cash payment is being processed!");
                    }
                    displayPurchaseSummary();
                    transactionComplete();
                }
                else
                {
                    invalidInput();
                }

                /* if the user enters c for transaction complete
                 * , display summary of the purchase
                 * also ask him if he wants to book a workshop
                 */

                /* if so, keep the purchasedItem value
                 * and give him 10% discount by displaying in the summary
                 */
            }
            // else if the item is out of stock, print it is out of stock
            else if(allStock[currentItemIndex].stockLevel <= 0)
            {
                outOfStock();
            }
            // else input is invalid
            else
            {
                invalidInput();
            }
            isCompleted = 0;
            return isCompleted;
        }

        public int firstPage()
        {
            allStock = JsonConvert.DeserializeObject<List<Products>>(File.ReadAllText(storeFileName));
            soldItems = JsonConvert.DeserializeObject<List<Products>>(File.ReadAllText(storeFileName));
            totalPage = allStock.Count / 5;
            if (allStock.Count % 5 != 0)
            {
                totalPage += 1;
            }

            if(allStock.Count < 5)
            {
                lastItem = allStock.Count;
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
            
            if(lastItem < 5)
            {
                firstItem = 0;
            }
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
            if (allStock.Count < 5)
            {
                lastItem = allStock.Count;
            }
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

        public int outOfStock()
        {
            Console.Clear();
            currentPage();
            Console.WriteLine("The product is currently out of stock!");
            isCompleted = 0;
            return isCompleted;
        }

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
            return isCompleted;
        }

        public int transactionComplete()
        {
            Console.WriteLine("Transaction Done!");
            firstItem = 0;
            itemIndex = firstItem;
            lastItem = firstItem + 5;
            pageIndex = 1;
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
