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
        public String identifyStore(String storeName)
        {
            String fileName = storeName + "_Inventory.json";
            Console.WriteLine("[!] " + fileName + " is being loaded!\n");
            return fileName;
        }

        public void addNewItem(String storeID)
        {
            int selectedItem = 0;
            List<Products> storeInventory = reader.readFile(storeID);

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
                        Boolean isInStore = false;
                        Console.WriteLine("[!] ID Found in Owners Inventory!: " + selectedItem);
                        foreach(Products storeItems in storeInventory)
                        {
                            if(storeItems.ID == selectedItem)
                            {
                                Console.WriteLine("[!] Item is already in store! - Added 5 stock");
                                isInStore = true;
                                storeItems.stockLevel = storeItems.stockLevel + 5;
                                wareHouse.updateStock(storeItems.ID, 5);
                                File.WriteAllText(storeID, JsonConvert.SerializeObject(storeInventory, Formatting.Indented));
                            }
                        }

                        if (!isInStore)
                        {
                            Console.WriteLine("[!] Item is not in store - Added 5 stock");
                            item.stockLevel = 5;
                            storeInventory.Add(item);
                            wareHouse.updateStock(item.ID, 5);
                            File.WriteAllText(storeID, JsonConvert.SerializeObject(storeInventory, Formatting.Indented));
                        }
                    }
                }
            }

        }
    }
}
