using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class stockRequestItem
    {
        public int ID { get; set; }
        public int listID { get; set; }
        public string store { get; set; }
        public string itemName { get; set; }
        public int quantity { get; set; }
        public int currentStock { get; set; }
        public Boolean availableStock { get; set; }
    }
}
