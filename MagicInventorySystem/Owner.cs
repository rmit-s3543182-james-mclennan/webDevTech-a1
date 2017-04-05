using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class Owner
    {
        JsonProcessor reader = new JsonProcessor();

        //Display all stock - Prints all lines of inventory items.
        public void displayAllStock()
        {
            List<Products> allStock = reader.readProductsFile("owners_inventory.json");

            //Formating title.
            String titleLine = String.Format("\n{0, -5} | {1, -15} | {2, -10}", "ID", "Name", "Stock Level");

            Console.WriteLine(titleLine);
            
            for (int i = 0; i < titleLine.Length; i++)
            {
                Console.Write("=");
                if(i + 1 == titleLine.Length)
                {
                    Console.Write("=\n");
                }
            }

            //For each product, print the line.
            foreach (Products stockItem in allStock)
            {
                String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}", stockItem.ID, stockItem.name, stockItem.stockLevel);

                Console.WriteLine(productLine);
            }
            Console.WriteLine();
        }

        //updateStockFromWarehouse will remove quantity of items
        //from the owners inventory.
        private void updateStockFromWarehouse(string name, int amountSentToStore)
        {
            List<Products> allStock = reader.readProductsFile("owners_inventory.json");

            //Search list of items in stock.
            foreach(Products item in allStock)
            {
                if(item.name == name)
                {
                    //Remove "amountSentToStore" from the stock level.
                    item.stockLevel = item.stockLevel - amountSentToStore;
                }
            }

            //Save file.
            reader.writeToProductsFile("owners_inventory.json", allStock);
        }

        //updateStoreInventory will update a specific store's items 
        private void updateStoreInventory(stockRequestItem requestedItem, int amountSentToStore)
        {
            //Read store inventory that was selected by user.
            List<Products> storeInventory = reader.readProductsFile("Melbourne_"+requestedItem.store+"_Inventory.json");
            Products newItem = new Products();
            Boolean inStore = false;

            //Search for item in the stores inventory.
            foreach (Products item in storeInventory)
            {
                //If the item is found, update the stockLevel
                if (item.name == requestedItem.itemName)
                {
                    inStore = true;
                    item.stockLevel = item.stockLevel + amountSentToStore;
                }
            }

            //If the item is never found, then it is a new item.
            //Add the item into the store.
            if(!inStore)
            {
                newItem.name = requestedItem.itemName;
                newItem.ID = requestedItem.ID;
                newItem.stockLevel = requestedItem.quantity;
                newItem.price = 10;
                storeInventory.Add(newItem);
            }

            //Save file.
            reader.writeToProductsFile("Melbourne_" + requestedItem.store + "_Inventory.json", storeInventory);
        }

        //updateStockRequests will update items in stock request file
        //when the owner "approves" a stock request.
        //If multiple stores request stock, it will update on each approval.
        private void updateStockRequests(stockRequestItem fullfilledItem)
        {
            List<stockRequestItem> stockRequests = reader.readRequestFile("stockrequests.json");
            //Find item in stockRequests.
            foreach (stockRequestItem item in stockRequests)
            {
                //Decrease the ID in the list.
                if(item.listID > fullfilledItem.listID)
                {
                    item.listID = item.listID - 1;
                }
                if (item.itemName == fullfilledItem.itemName)
                {
                    //Update stock level
                    item.currentStock = item.currentStock - 5;
                    //Check if stock is still available and update.
                    if(item.currentStock >= item.quantity)
                    {
                        item.availableStock = true;
                    }
                    else
                    {
                        item.availableStock = false;
                    }
                }
            }
            //Save file.
            reader.writeToStockFile("stockrequests.json", stockRequests);

        }

        //displayStockRequests will show all stock requests.
        //If isAllStock, then show everything.
        //If StockToShow is true, only show true stock and vise versa.
        public int displayStockRequests(Boolean isAllStock, Boolean StockToShow)
        {
            List<stockRequestItem> stockRequests = reader.readRequestFile("stockrequests.json");

            int formatProductName = getLengthOfProduct(stockRequests);
            int processStatus = -1;

            String titleLine = String.Format("\n{0, -5} | {1, -10} | {2, -"+formatProductName+"} | {3, -10} | {4, -15} | {5, -10}", "ID", "Store", "Product", "Quantity", "Current Stock", "Stock Availability");

            Console.WriteLine(titleLine);

            for (int i = 0; i < titleLine.Length; i++)
            {
                Console.Write("=");
                if (i + 1 == titleLine.Length)
                {
                    Console.Write("=\n");
                }
            }

            if (isAllStock)
            {
                foreach (stockRequestItem requestItem in stockRequests)
                {
                    String productLine = String.Format("{0, -5} | {1, -10} | {2, -" + formatProductName + "} | {3, -10} | {4, -15} | {5, -10}",
                        requestItem.listID, requestItem.store, requestItem.itemName,
                        requestItem.quantity, requestItem.currentStock, requestItem.availableStock);

                    Console.WriteLine(productLine);
                }
            }
            else
            {
                foreach (stockRequestItem requestItem in stockRequests)
                {
                    if (StockToShow == requestItem.availableStock)
                    {
                        String productLine = String.Format("{0, -5} | {1, -10} | {2, -" + formatProductName + "} | {3, -10} | {4, -15} | {5, -10}",
                        requestItem.listID, requestItem.store, requestItem.itemName,
                        requestItem.quantity, requestItem.currentStock, requestItem.availableStock);
                        Console.WriteLine(productLine);
                    } 
                }
            }
            Console.WriteLine();
            processStatus = processStockRequest(stockRequests);
            return processStatus;

        }

        //ProcessStockRequest will take input for user.
        private int processStockRequest(List<stockRequestItem> stockRequests)
        {
            Console.WriteLine("Enter 'No' to return to Owner Menu.\n");
            Console.WriteLine("Enter Request to Process: ");
            string stringID = Console.ReadLine();
            int id = 0;
            //Check if stringID is not 'No'
            if (!stringID.ToUpper().Equals("NO"))
            {
                if (int.TryParse(stringID, out id))
                {
                    //check if the ID exists and remove from stock requests.
                    stockRequestItem itemRequested = checkIDAndRemove(stockRequests, id);
                    if (itemRequested != null)
                    {
                        //If the item exists, update throughout all files.
                        //(storeInventory, ownerInventory and stock requests).
                        updateStockFromWarehouse(itemRequested.itemName, itemRequested.quantity);
                        updateStoreInventory(itemRequested, itemRequested.quantity);
                        updateStockRequests(itemRequested);
                        return 1;
                    }
                    else
                    {
                        //Item does not exist or not enough stock.
                        return 2;
                    }
                }
                else
                {
                    //Item does not exist or not enough stock.
                    return 2;
                }
            }
            else
            {
                //Invalid input.
                return 0;
            }
        }

        //PromptTrueorFalse will ask the user for input
        //When Owner wishes to display inventory requests
        //by True or False.
        public int promptTrueorFalse()
        {
            Console.WriteLine("Please enter the stock requests you wish to view (True/False):");
            int type = 0;

            //Convert input to lower case
            string input = Console.ReadLine().ToLower();

            //Check if input is true, or t
            if (input.Equals("t") || input.Equals("true"))
            {
                type = 1;
            }
            
            //Check if input is false, or f
            else if(input.Equals("f") || input.Equals("false"))
            {
                type = 2;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter any combination of True/False (T/F)");
            }
            return type;
        }

        //checkIDAndRemove will remove items from stock request
        //when approved by owner.
        private stockRequestItem checkIDAndRemove(List<stockRequestItem> stockRequests, int stringid)
        {
            stockRequestItem requestedItem = null;

            foreach (stockRequestItem item in stockRequests)
            {
                //Check if stock is available
                if (item.availableStock)
                {
                    //If stock, remove item and begin updating data.
                    if (stringid == item.listID)
                    {
                        requestedItem = item;
                        stockRequests.Remove(item);
                        reader.writeToStockFile("stockrequests.json", stockRequests);
                        return requestedItem;
                    }
                }
            }
            return requestedItem;
        }

        //Formatting function to change size of format
        //depending on the product name's length.
        private int getLengthOfProduct(List<stockRequestItem> stockRequests)
        {
            int largest = 0;
            int current = 0;

            foreach(stockRequestItem item in stockRequests)
            {
                current = item.itemName.Length;
                if(current > largest)
                {
                    largest = current;
                }
            }

            if(largest < 9)
            {
                largest = 9;
            }

            return largest;
        }

        
    }
}
