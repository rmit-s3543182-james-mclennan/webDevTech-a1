using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class FranchiseOwner
    {
        JsonProcessor reader = new JsonProcessor();
        public void identifyStore(String storeName)
        {
            String fileName = storeName + "_Inventory.json";
            Console.WriteLine("[!] " + fileName + " is being loaded!\n");
        }
    }
}
