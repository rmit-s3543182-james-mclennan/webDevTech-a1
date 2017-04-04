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
        //Read Products from a file
        public List<Products> readProductsFile(string fileName)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Products>>(File.ReadAllText(fileName))
                ?? new List<Products>();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File does not exist.");
                return null;
            }
        }

        //Read stockRequestItem from a file
        public List<stockRequestItem> readRequestFile (string fileName)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<stockRequestItem>>(File.ReadAllText(fileName))
                ?? new List<stockRequestItem>();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File does not exist.");
                return null;
            }
}

        //Read Workshops from a file
        public List<Workshops> readWorkshopFile(string fileName)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Workshops>>(File.ReadAllText(fileName))
                ?? new List<Workshops>();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File does not exist.");
                return null;
            }
        }
    }
}
