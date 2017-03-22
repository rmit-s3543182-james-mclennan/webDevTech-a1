using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UtilityLibrary;

namespace MagicInventorySystem
{
    class Owner
    {
        JsonProcessor reader = new JsonProcessor();
        public void displayAllStock()
        {
            List<Products> allStock = reader.readFile("owners_inventory.json");
            String test = String.Format("{0, 0}{1, 5}{2, 14}\n", "ID", "Name", "Stock Level");
            Console.WriteLine(test);
            foreach (Products stockItem in allStock)
            {
                String test2 = String.Format("{0, 0}{1, 8}{2, 16}", stockItem.ID, stockItem.name, stockItem.stockLevel);

                Console.WriteLine(test2);
            }
            Console.WriteLine();
        }

    }
}
