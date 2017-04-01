using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class FranchiseOwner
    {
        JsonProcessor reader = new JsonProcessor();
        private Owner wareHouse = new Owner();
        public string storeFileName { get; set; }
        
        public void identifyStore(String storeName)
        {
            storeFileName = "Melbourne_"+storeName + "_Inventory.json";
        }

        public void addNewItem(string storeID)
        {
            int selectedItem = 0;
            List<Products> storeInventory = reader.readFile(storeFileName);
            Products requestedItem;
            wareHouse.displayAllStock();

            Console.WriteLine("Enter item ID to stock in store: ");
            String input = Console.ReadLine();
            if(int.TryParse(input, out selectedItem))
            {
                List<Products> allStock = reader.readFile("owners_inventory.json");
                foreach(Products item in allStock)
                {
                    if(item.ID == selectedItem)
                    {
                        requestedItem = item;
                        Console.WriteLine(item.stockLevel);
                        sendStockRequest(requestedItem, item.stockLevel, storeID);
                    }
                }
            }

        }
          
        public void displayInventory(int threshold)
        {
            List<Products> storeInventory = reader.readFile(storeFileName);
            storeInventory = storeInventory.OrderBy(id => id.ID).ToList();

            String titleLine = String.Format("\n{0, -5} | {1, -15} | {2, -10} | {3, -10}", "ID", "Name", "Stock Level", "Re-Stock");
            Boolean reStock;
            Console.WriteLine(titleLine);

            for (int i = 0; i < titleLine.Length; i++)
            {
                Console.Write("=");
                if (i + 1 == titleLine.Length)
                {
                    Console.Write("\n");
                }
            }

            foreach (Products stockItem in storeInventory)
            {
                if(stockItem.stockLevel <= threshold)
                {
                    reStock = true;
                }
                else
                {
                    reStock = false;
                }
                String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10} | {3, -10}", stockItem.ID, stockItem.name, stockItem.stockLevel, reStock);

                Console.WriteLine(productLine);
            }

        }

        public int promptThreshold()
        {
            int threshold = -1;

            string userInput = Console.ReadLine();
            if(int.TryParse(userInput, out threshold))
            {
                return threshold;
            }
            return threshold;
        }
        private void sendStockRequest(Products requestedItem, int currentStock, string storeID)
        {
            List<stockRequestItem> requestList = reader.readRequestFile("stockrequests.json");

            stockRequestItem addRequest = new stockRequestItem();
            int listSize = requestList.Count + 1;
            addRequest.listID = listSize;
            addRequest.ID = requestedItem.ID;
            addRequest.currentStock = currentStock;
            addRequest.store = storeID;
            addRequest.itemName = requestedItem.name;
            addRequest.quantity = 5;

            if(currentStock >= addRequest.quantity)
            {
                addRequest.availableStock = true;
            }
            else
            {
                addRequest.availableStock = false;
            }

            requestList.Add(addRequest);

            File.WriteAllText("stockrequests.json", JsonConvert.SerializeObject(requestList, Formatting.Indented));
        }
        
    }
}
