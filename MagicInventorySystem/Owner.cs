using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using UtilityLibrary;

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
            Console.WriteLine("Press any key to continue...");
        }

        public void updateStock(int index, int amountSentToStore)
        {
            List<Products> allStock = reader.readFile("owners_inventory.json");

            Products itemToReduce = allStock[index-1];

            itemToReduce.stockLevel = itemToReduce.stockLevel - amountSentToStore;

            File.WriteAllText("owners_inventory.json", JsonConvert.SerializeObject(allStock, Formatting.Indented));
        }

    }
}
