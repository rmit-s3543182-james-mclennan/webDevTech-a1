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
        public void displayAllStock()
        {
            List<Products> allStock = reader.readFile("owners_inventory.json");

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

            foreach (Products stockItem in allStock)
            {
                String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10}", stockItem.ID, stockItem.name, stockItem.stockLevel);

                Console.WriteLine(productLine);
            }
            Console.WriteLine();
        }

        public void updateStockFromWarehouse(string name, int amountSentToStore)
        {
            List<Products> allStock = reader.readFile("owners_inventory.json");

            foreach(Products item in allStock)
            {
                if(item.name == name)
                {
                    item.stockLevel = item.stockLevel - amountSentToStore;
                }
            }
            
            File.WriteAllText("owners_inventory.json", JsonConvert.SerializeObject(allStock, Formatting.Indented));
        }

        public void sendStockFromWarehouse(stockRequestItem requestedItem, int amountSentToStore)
        {
            List<Products> storeInventory = reader.readFile("Melbourne_"+requestedItem.store+"_Inventory.json");
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
                storeInventory.Add(newItem);
            }

            File.WriteAllText("Melbourne_" + requestedItem.store + "_Inventory.json", 
                JsonConvert.SerializeObject(storeInventory, Formatting.Indented));
        }

        public void displayAllStockRequests()
        {
            List<stockRequestItem> stockRequests = reader.readRequestFile("stockrequests.json");

            int formatProductName = getLengthOfProduct(stockRequests);

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

            foreach (stockRequestItem requestItem in stockRequests)
            {
                String productLine = String.Format("{0, -5} | {1, -10} | {2, -" + formatProductName + "} | {3, -10} | {4, -15} | {5, -10}", 
                    requestItem.listID, requestItem.store, requestItem.itemName,
                    requestItem.quantity, requestItem.currentStock, requestItem.availableStock);

                Console.WriteLine(productLine);
            }
            Console.WriteLine();
            Console.WriteLine("Enter Request to Process: ");
            string stringID = Console.ReadLine();
            int id = 0;
            if(int.TryParse(stringID, out id))
            {
                stockRequestItem itemRequested = checkIDAndRemove(stockRequests, id);
                if (itemRequested != null)
                {
                    updateStockFromWarehouse(itemRequested.itemName, itemRequested.quantity);
                    sendStockFromWarehouse(itemRequested, itemRequested.quantity);
                }
                else
                {
                    Console.WriteLine("Request cannot be processed.");
                }
            }
            else
            {
                Console.WriteLine("Request cannot be processed.");
            }


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
                        File.WriteAllText("stockrequests.json", JsonConvert.SerializeObject(stockRequests, Formatting.Indented));
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

            return largest;
        }

    }
}
