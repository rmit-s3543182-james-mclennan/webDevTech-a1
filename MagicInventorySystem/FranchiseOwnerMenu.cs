﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class FranchiseOwnerMenu
    {
        FranchiseOwner access = new FranchiseOwner();

        private string storeID;
        public MagicMenuCollection loadFranchiseOwnerMenu(MagicMenuCollection collection)
        {
            collection.Menus.Add(new MagicMenuList()
            {
                MenuId = 3,
                MenuItems =
                    {
                        new MagicMenuItem()
                        {
                            Option = "Display Inventory\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                Console.WriteLine("Press enter value for re-stock threshold: ");
                                int threshold = access.promptThreshold();
                                Console.Clear();
                                if(threshold >= 0)
                                {
                                    Console.WriteLine("Inventory of " + storeID);
                                    access.displayInventory(threshold, false);
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                    collection.ShowMenu(3);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please try again.");
                                    collection.ShowMenu(3);
                                }
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Display Inventory (Threshold)\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                Console.WriteLine("Press enter value for re-stock threshold: ");
                                int threshold = access.promptThreshold();
                                Console.Clear();
                                if(threshold >= 0)
                                {
                                    Console.WriteLine("Inventory of " + storeID);
                                    access.displayInventory(threshold, true);
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                    collection.ShowMenu(3);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please try again.");
                                    collection.ShowMenu(3);
                                }
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Add New Inventory Item\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                Console.WriteLine("Add New Inventory Item: ");
                                access.addNewItem(storeID);
                                Console.Clear();
                                collection.ShowMenu(3);
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Return to Main Menu\n",
                            CannotExecute = true,
                            SubMenuId = 1
                        },
                        new MagicMenuItem()
                        {
                            Option = "Exit\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                System.Environment.Exit(1);
                            }
                        }

                    }
            });

            return collection;
        }

        public MagicMenuCollection validateFranchiseStore(MagicMenuCollection collection)
        {
            collection.Menus.Add(new MagicMenuList()
            {
                MenuId = 5,
                MenuItems =
                    {
                        new MagicMenuItem()
                        {
                            Option = "Melbourne CBD",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                access.identifyStore("CBD");
                                storeID="CBD";
                                collection.ShowMenu(3);

                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Melbourne North",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                access.identifyStore("North");
                                storeID="North";
                                collection.ShowMenu(3);
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Melbourne South",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                access.identifyStore("South");
                                storeID="South";
                                collection.ShowMenu(3);
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Melbourne East",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                access.identifyStore("East");
                                storeID="East";
                                collection.ShowMenu(3);
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Melbourne West",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                access.identifyStore("West");
                                storeID="West";
                                collection.ShowMenu(3);
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Return to Main Menu",
                            CannotExecute = true,
                            SubMenuId = 1
                        },
                        new MagicMenuItem()
                        {
                            Option = "Exit",
                            CannotExecute = false,
                            Execute = () =>
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
