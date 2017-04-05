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

        private void updateStockFromWarehouse(string name, int amountSentToStore)
        {
            List<Products> allStock = reader.readProductsFile("owners_inventory.json");

            foreach(Products item in allStock)
            {
                if(item.name == name)
                {
                    item.stockLevel = item.stockLevel - amountSentToStore;
                }
            }

            reader.writeToProductsFile("owners_inventory.json", allStock);
        }

        private void sendStockFromWarehouse(stockRequestItem requestedItem, int amountSentToStore)
        {
            List<Products> storeInventory = reader.readProductsFile("Melbourne_"+requestedItem.store+"_Inventory.json");
            Products newItem = new Products();
            Boolean inStore = false;
            foreach (Products item in storeInventory)
            {
                if (item.name == requestedItem.itemName)
                {
                    inStore = true;
                    item.stockLevel = item.stockLevel + amountSentToStore;
                }
            }

            if(!inStore)
            {
                newItem.name = requestedItem.itemName;
                newItem.ID = requestedItem.ID;
                newItem.stockLevel = requestedItem.quantity;
                newItem.price = 10;
                storeInventory.Add(newItem);
            }

            reader.writeToProductsFile("Melbourne_" + requestedItem.store + "_Inventory.json", storeInventory);
        }

        private void updateStockRequests(stockRequestItem fullfilledItem)
        {
            List<stockRequestItem> stockRequests = reader.readRequestFile("stockrequests.json");
            foreach (stockRequestItem item in stockRequests)
            {
                if(item.listID > fullfilledItem.listID)
                {
                    item.listID = item.listID - 1;
                }
                if (item.itemName == fullfilledItem.itemName)
                {
                    item.currentStock = item.currentStock - 5;
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
            reader.writeToStockFile("stockrequests.json", stockRequests);

        }

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

        private int processStockRequest(List<stockRequestItem> stockRequests)
        {
            Console.WriteLine("Enter 'No' to return to Owner Menu.\n");
            Console.WriteLine("Enter Request to Process: ");
            string stringID = Console.ReadLine();
            int id = 0;
            if (!stringID.ToUpper().Equals("NO"))
            {
                if (int.TryParse(stringID, out id))
                {
                    stockRequestItem itemRequested = checkIDAndRemove(stockRequests, id);
                    if (itemRequested != null)
                    {
                        updateStockFromWarehouse(itemRequested.itemName, itemRequested.quantity);
                        sendStockFromWarehouse(itemRequested, itemRequested.quantity);
                        updateStockRequests(itemRequested);
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 0;
            }
        }
        public int promptTrueorFalse()
        {
            Console.WriteLine("Please enter the stock requests you wish to view (True/False):");
            int type = 0;

            string input = Console.ReadLine().ToLower();

            if (input.Equals("t") || input.Equals("true"))
            {
                type = 1;
            }
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

        private stockRequestItem checkIDAndRemove(List<stockRequestItem> stockRequests, int stringid)
        {
            stockRequestItem requestedItem = null;

            foreach (stockRequestItem item in stockRequests)
            {
                if (item.availableStock)
                {
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
