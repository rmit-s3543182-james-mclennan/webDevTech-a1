using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace MagicInventorySystem
{
    // Super(Base)class PageLoad
    class PageLoad : IPageLoad
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
        public int multiplePurchases { get; set; }
        public int paymentMethod { get; set; }
        public int workshopConfirmation { get; set; }

        public double totalPrice { get; set; }

        public string storeFileName { get; set; }   
        public string choice { get; set; }

        public bool workshopBookingCheck { get; set; }

        public List<Products> allStock { get; set; }
        public List<Products> soldItems { get; set; }

        JsonProcessor reader = new JsonProcessor();

        // Initialize variables in constructor
        public PageLoad()
        {
            storeFileName = "owners_inventory.json";           
            isCompleted = 0;
            pageIndex = 1;
        }

        // Display first page
        public int firstPage()
        {
            /* Initialize variables everytime 
             * a customer chooses display product
             */
            purchaseItemIndex = 0;
            purchaseCount = 0;
            firstItem = 0;
            lastItem = 5;
            totalPrice = 0;

            allStock = reader.readProductsFile(storeFileName);
            soldItems = reader.readProductsFile(storeFileName);
            totalPage = allStock.Count / 5;

            /* Creates product pages
             * by comparing with the amount of products
             */
            if (allStock.Count % 5 != 0)
            {
                totalPage += 1;
            }
            if (allStock.Count < 5)
            {
                lastItem = allStock.Count;
            }

            // Display 5 products from the appropriate json file
            displayTitle();
            try
            {
                for (itemIndex = firstItem; itemIndex < lastItem; itemIndex++)
                {
                    if (allStock[itemIndex].ID >= firstItem
                    && allStock[itemIndex].ID <= lastItem
                    && allStock.IndexOf(allStock[itemIndex]) <= allStock.Count)
                    {
                        String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10} | {3,  -10}"
                                                            , allStock[itemIndex].ID
                                                            , allStock[itemIndex].name
                                                            , allStock[itemIndex].stockLevel
                                                            , allStock[itemIndex].price);
                        Console.WriteLine(productLine);

                    }
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                // If index is out of range, variables are reset
                Console.Clear();
                if (itemIndex < 0)
                {
                    firstItem = 0;
                    itemIndex = firstItem;
                    lastItem = firstItem + 5;
                }
            }
            isCompleted = 0;
            displayBottomLine();
            return isCompleted;
        }

        // Moves to previous page
        public int previousPage()
        {
            if (pageIndex <= totalPage && pageIndex > 1)
            {
                pageIndex--;
            }
            Console.Clear();
            
            // Variables get next values to display next 5 products
            lastItem = firstItem;
            firstItem = lastItem - 5;
            displayTitle();
            displayProducts();

            // itemIndex gets the first index
            if (itemIndex < 0)
            {
                firstItem = 0;
                itemIndex = firstItem;
                lastItem = firstItem + 5;
                firstPage();
            }
            else
            {
                displayBottomLine();
            }
            isCompleted = 0;
            return isCompleted;
        }

        // Display last page
        public int lastPage()
        {
            if (pageIndex >= totalPage)
            {
                pageIndex = totalPage;
            }
            displayTitle();
            
            /* If a store has less than 5 items
             * firstItem's index would be 0
             */
            if (lastItem < 5)
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
                        String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10} | {3,  -10}"
                                                            , allStock[itemIndex].ID
                                                            , allStock[itemIndex].name
                                                            , allStock[itemIndex].stockLevel
                                                            , allStock[itemIndex].price);
                        Console.WriteLine(productLine);
                    }
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                /* If lastItem's index is greater than allStock,
                 * variables are reset with appropriate values
                 */
                Console.Clear();
                if (lastItem > allStock.Count)
                {
                    lastItem = allStock.Count;
                    firstItem = lastItem - 5;
                    itemIndex = firstItem;
                }
            }
            isCompleted = 0;
            displayBottomLine();
            return isCompleted;
        }

        // Moves to next page
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
                if(firstItem >= lastItem)
                {
                    /* Compute to display proper item list in a page
                     * e.g. 8 items -> 5 items in page 1, 3 items in page 2
                     */
                    if(lastItem % 5 != 0)
                    {
                        firstItem = lastItem - (lastItem % 5);
                        itemIndex = firstItem;
                    }
                    else
                    {
                        firstItem = lastItem - 5;
                        itemIndex = firstItem;
                    }
                }
                else if((firstItem % 5) == 0 && firstItem > 5)
                {
                    itemIndex = firstItem;
                }
                lastPage();
            }
            else
            {
                displayBottomLine();
            }
            isCompleted = 0;
            return isCompleted;
        }

        // Display current page's products
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
                        String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10} | {3,  -10}"
                                                            , allStock[itemIndex].ID
                                                            , allStock[itemIndex].name
                                                            , allStock[itemIndex].stockLevel
                                                            , allStock[itemIndex].price);
                        Console.WriteLine(productLine);
                    }
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
            }
        }

        // Display current page when invalid input is typed
        public int currentPage()
        {
            if (allStock.Count < 5)
            {
                lastItem = allStock.Count;
            }
            displayTitle();
            displayProducts();
            displayBottomLine();
            isCompleted = 0;
            return isCompleted;
        }


        // Called when the product is out of stock
        public int outOfStock()
        {
            Console.Clear();
            Console.WriteLine("The product is currently out of stock!");
            currentPage();
            isCompleted = 0;
            return isCompleted;
        }

        // Called when invalid input is typed
        public int invalidInput()
        {
            Console.Clear();
            Console.WriteLine("Invalid input. Try again.\n");

            if (firstItem < 1)
            {
                firstPage();
            }
            else if (lastItem >= allStock.Count)
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

        // Called when products purchases are done
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

        // Display title line
        public void displayTitle()
        {
            String titleLine = String.Format("\n{0, -5} | {1, -15} | {2, -10} | {3,  -10}"
                                            ,"ID", "Name", "Stock Level","Price");
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

        // Products purchases progress 
        public int purchaseItems(int choiceIndex)
        {    
            currentItemIndex = choiceIndex;
            Console.WriteLine("\nYou have chosen " + allStock[currentItemIndex].name + ".\n");
            Console.Write("Enter the amount of the product : ");
            choice = Console.ReadLine();

            // Check if the chosen product's stock is available now
            if (int.TryParse(choice, out choiceIndex)
            && choiceIndex > 0
            && choiceIndex <= allStock[currentItemIndex].stockLevel)
            {
                // If so, deduct product's amount from the appropriate json file
                purchaseProgress(choiceIndex, currentItemIndex);
                Console.WriteLine("\nYou have purchased " + choiceIndex + "ea of " + allStock[currentItemIndex].name + "\n");

                // Assign current index's name and price into another list for multiple purchases
                soldItems[purchaseItemIndex].name = allStock[currentItemIndex].name;
                soldItems[purchaseItemIndex].price = allStock[currentItemIndex].price;
                
                // Assign the price to calculate total price
                soldItems[purchaseItemIndex].price *= choiceIndex;
                Console.WriteLine("The price of purchases is " + soldItems[purchaseItemIndex].price);
                try
                {
                    soldItems[purchaseItemIndex].stockLevel = choiceIndex;
                    purchaseItemIndex++;
                    multiplePurchases = 0;
                    paymentMethod = 0;
                    workshopConfirmation = 0;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Choose the item from the list.");
                }

                // Ask the customer whether s/he wants to buy more items
                while (multiplePurchases == 0)
                {
                    Console.Write("\nDo you want to buy more products?(Y / N) : ");
                    choice = Console.ReadLine();
                    if (choice == "Y" || choice == "y")
                    {
                        Console.Clear();
                        multiplePurchases = 1;
                        currentPage();
                    }
                    else if (choice == "N" || choice == "n")
                    {
                        while (paymentMethod == 0)
                        {
                            Console.Write("\nDo you want to pay for credit card?(Y / N) : ");
                            choice = Console.ReadLine();
                            if (choice == "Y" || choice == "y")
                            {
                                Console.WriteLine("\nCredit card payment is being processed!\n");
                                displayPurchaseSummary();
                                paymentMethod = 1;
                                multiplePurchases = 1;
                            }
                            else if (choice == "N" || choice == "n")
                            {
                                Console.WriteLine("\nCash payment is being processed!\n");
                                displayPurchaseSummary();
                                paymentMethod = 1;
                                multiplePurchases = 1;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Try again.\n");
                                paymentMethod = 0;
                                multiplePurchases = 0;
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Try again.\n");
                        multiplePurchases = 0;
                        isCompleted = 0;
                    }
                }

            }
            else if (allStock[currentItemIndex].stockLevel <= 0)
            {
                outOfStock();
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

        /* Deduct the amount of stocks from appropriate store json page 
         * purchaseCount is to check whether the customer buys item,
         * if so the customer will get 10% discount of the total price
         * of their purchases
         */
        public void purchaseProgress(int amount, int currentItemIndex)
        {
            allStock[currentItemIndex].stockLevel -= amount;
            reader.writeToProductsFile(storeFileName, allStock);
            purchaseCount++;
        }

        // Display what the customer bought
        public int displayPurchaseSummary()
        {
            while(workshopConfirmation == 0)
            {
                Console.Write("Would you like to book into a workshop?(Y / N) : ");
                choice = Console.ReadLine();
                if ((choice == "Y" || choice == "y") && purchaseItemIndex > 0)
                {
                    Console.WriteLine("The total price of your purchases have got 10% discount!\n");
                    workshopBookingCheck = true;
                    workshopConfirmation = 1;
                }
                else if (choice == "N" || choice == "n")
                {
                    workshopBookingCheck = false;
                    workshopConfirmation = 1;
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.\n");
                    workshopConfirmation = 0;
                }
            }
            
            // Display purchases summary
            Console.Clear();
            Console.WriteLine("\n=============== Purchase Summary ===============\n");
            Console.WriteLine("You have purchased : \n");
            try
            {
                for(int i = 0; i < purchaseItemIndex; i++)
                {
                    Console.WriteLine(soldItems[i].name + " " + soldItems[i].stockLevel + "ea, price : " + soldItems[i].price);

                    // compute each item's original price only
                    totalPrice += soldItems[i].price;
                }
                
                // If the customer booked into a workshop, 10% discount applied
                if(workshopBookingCheck == true)
                {
                    totalPrice -= (totalPrice * 0.1);
                    Console.WriteLine("Workshop booked, 10% discount of total price.\n");
                    Console.WriteLine("The total price of purchased items is : " + totalPrice);
                }
                else if(workshopBookingCheck == false)
                {
                    Console.WriteLine("The total price of purchased items is : " + totalPrice);
                }
                Console.WriteLine("\n================================================\n");
            }
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Array index out of range");
            }
            transactionComplete();
            return isCompleted;
        }

        public void displayBottomLine()
        {
            Console.WriteLine("Page " + pageIndex + "/" + totalPage);
            Console.WriteLine("[Legend: 'P' Next Page | 'B' Previous Page | 'R' Return to Menu | 'C' Complete Transaction]");
            Console.Write("Enter Product ID to purchase : ");
            choice = Console.ReadLine();
        }
    }
}
