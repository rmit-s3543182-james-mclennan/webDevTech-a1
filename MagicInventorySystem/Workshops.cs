using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class Workshops
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public int availableSeat { get; set; }
        public int maxSeat { get; set; }
    }
}
