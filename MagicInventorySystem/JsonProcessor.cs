using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MagicInventorySystem
{
    class JsonProcessor
    {
        /*Use the function readFile(pass the name of the file) and it will 
         * return a list of products.
         * 
         * Should only be used for products!
         */
        public List<Products> readFile(string fileName)
        {
            return JsonConvert.DeserializeObject<List<Products>>(File.ReadAllText(fileName))
                ?? new List<Products>();
        }

        public List<stockRequestItem> readRequestFile (string fileName)
        {
            return JsonConvert.DeserializeObject<List<stockRequestItem>>(File.ReadAllText(fileName))
                ?? new List<stockRequestItem>();
        }
    }
}
