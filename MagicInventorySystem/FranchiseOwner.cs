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
                        Boolean isInStore = false;
                        Console.WriteLine("[!] ID Found in Owners Inventory!: " + selectedItem);
                        foreach(Products storeItems in storeInventory)
                        {
                            if(storeItems.ID == requestedItem.ID)
                            {
                                Console.WriteLine("[!] Item is being restocked! Sending request!");
                                isInStore = true;
                                sendStockRequest(requestedItem, storeItems.stockLevel + 5, storeID);
                            }
                        }

                        if (!isInStore)
                        {
                            Console.WriteLine("[!] Item is out of your stock! Sending urgent request!");
                            Console.WriteLine(item.stockLevel);
                            sendStockRequest(requestedItem, item.stockLevel, storeID);

                        }
                    }
                }
            }

        }
           
        public void sendStockRequest(Products requestedItem, int currentStock, string storeID)
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
