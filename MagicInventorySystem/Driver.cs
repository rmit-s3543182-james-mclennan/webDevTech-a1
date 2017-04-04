
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    //Designed and coded by
    //James McLennan - s3543182
    //Allen Cho - s<number>
    //Our implementation of the Magic Inventory System.
    class Driver
    {
        static void Main(string[] args)
        {
            MainMenu menu = new MainMenu();
            menu.loadMainMenu();
        }
    }
}
