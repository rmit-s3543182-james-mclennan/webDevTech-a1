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
                return JsonConvert.DeserializeObject<List<Products>>(File.ReadAllText("JSONFiles/"+fileName))
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
                return JsonConvert.DeserializeObject<List<stockRequestItem>>(File.ReadAllText("JSONFiles/" + fileName))
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
                return JsonConvert.DeserializeObject<List<Workshops>>(File.ReadAllText("JSONFiles/" + fileName))
                ?? new List<Workshops>();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File does not exist.");
                return null;
            }
        }

        //Write to products file.
        public void writeToProductsFile(string fileName, List<Products> list)
        {
            File.WriteAllText("JSONFiles/" + fileName, JsonConvert.SerializeObject(list, Formatting.Indented));
        }

        //Write to stock file.
        public void writeToStockFile(string fileName, List<stockRequestItem> list)
        {
            File.WriteAllText("JSONFiles/"+fileName, JsonConvert.SerializeObject(list, Formatting.Indented));
        }

        //Write to workshop file.
        public void writeToWorkshopFIle(string fileName, List<Workshops> list)
        {
            File.WriteAllText("JSONFiles/" + fileName, JsonConvert.SerializeObject(list, Formatting.Indented));
        }
    }
}
