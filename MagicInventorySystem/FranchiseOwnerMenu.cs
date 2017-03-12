﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class FranchiseOwnerMenu
    {
        public MagicMenuCollection loadFranchiseOwnerMenu(MagicMenuCollection collection)
        {
            collection.Menus.Add(new MagicMenuSystem()
            {
                MenuId = 3,
                MenuItems =
                    {
                        new MagicMenuItem()
                        {
                            Option = "Display Inventory\n",
                            HasSubMenu = false,
                            Action = () =>
                            {
                                Console.WriteLine("Need to implement function!");
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Display Inventory (Threshold)\n",
                            HasSubMenu = false,
                            Action = () =>
                            {
                                Console.WriteLine("Need to implement function!");
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Add New Inventory Item\n",
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