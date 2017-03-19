using System;
using System.Collections.Generic;
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

        }
    }
}
