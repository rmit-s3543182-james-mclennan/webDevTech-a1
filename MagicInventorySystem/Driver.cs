﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class Driver
    {
        static void Main(string[] args)
        {
            MainMenu menu = new MainMenu();
            menu.loadMainMenu();
        }
    }
}
