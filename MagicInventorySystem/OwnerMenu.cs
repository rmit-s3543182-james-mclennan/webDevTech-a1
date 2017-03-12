﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class OwnerMenu
    {
        public MagicMenuCollection loadOwnerMenu(MagicMenuCollection collection)
        {
            collection.Menus.Add(new MagicMenuSystem()
            {
                MenuId = 2,
                MenuItems =
                    {
                        new MagicMenuItem()
                        {
                            Option = "Display All Stock Requests\n",
                            HasSubMenu = false,
                            Action = () =>
                            {
                                Console.WriteLine("Need to implement function!");
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Display Stock Requests (True / False)\n",
                            HasSubMenu = false,
                            Action = () =>
                            {
                                Console.WriteLine("Need to implement function!");
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Display All Product Lines\n",
                            HasSubMenu = false,
                            Action = () =>
                            {
                                Console.WriteLine("Need to implement function!");
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Return to Main Menu\n",
                            HasSubMenu = true,
                            SubMenuId = 1
                        },
                        new MagicMenuItem()
                        {
                            Option = "Exit\n",
                            HasSubMenu = false,
                            Action = () =>
                            {
                                System.Environment.Exit(1);
                            }
                        }

                    }
            });

            return collection;
        }
    }
}