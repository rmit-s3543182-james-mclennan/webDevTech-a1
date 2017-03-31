using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace MagicInventorySystem
{
    class franchiseCustomer
    {
        JsonProcessor reader = new JsonProcessor();
        private Owner wareHouse = new Owner();
        public String identifyStore(String storeName)
        {
            String fileName = storeName + "_Inventory.json";
            Console.WriteLine("[!] " + fileName + " is being loaded!\n");
            return fileName;
        }
    }
}
