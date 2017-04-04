using System;
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
        //Franchise Owner Menu
        public MagicMenuCollection loadFranchiseOwnerMenu(MagicMenuCollection collection)
        {
            collection.Menus.Add(new MagicMenuList()
            {
                MenuId = 3,
                MenuItems =
                    {
                        //Menu option 1 - Display Inventory
                        new MagicMenuItem()
                        {
                            Option = "Display Inventory",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                //Request value of minimum restock
                                Console.WriteLine("Press enter value for re-stock threshold: ");
                                try
                                {
                                    int threshold = access.promptThreshold();
                                    Console.Clear();
                                    if(threshold >= 0)
                                    {
                                        //Display inventory with restock dependant on threshold
                                        Console.WriteLine("Inventory of " + storeID);
                                        access.displayInventory(threshold, false);
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        Console.Clear();
                                        //Direct to menu 3.
                                        collection.ShowMenu(3);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input. Please try again.");
                                        collection.ShowMenu(3);
                                    }
                                }
                                catch (Exception e)
                                {
                                    //Catch exceptions
                                    Console.WriteLine("An error occured. Please seek admin assistance.");
                                    collection.ShowMenu(3);
                                }
                            }
                        },
                        //Menu option 2 - Display Inventory (Threshold)
                        new MagicMenuItem()
                        {
                            Option = "Display Inventory (Threshold)",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                try
                                {
                                    Console.Clear();
                                    Console.WriteLine("Press enter value for re-stock threshold: ");
                                    //Request value for minimum and only to display less than.
                                    int threshold = access.promptThreshold();
                                    Console.Clear();
                                    if(threshold >= 0)
                                    {
                                        //Display inventory with only the items that have less
                                        //stock than the user's input.
                                        Console.WriteLine("Inventory of " + storeID);
                                        access.displayInventory(threshold, true);
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        Console.Clear();
                                        //Direct to menu 3.
                                        collection.ShowMenu(3);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input. Please try again.");
                                        collection.ShowMenu(3);
                                    }
                                }
                                catch (Exception)
                                {
                                    //Catch exceptions
                                    Console.WriteLine("An error occured. Please seek admin assistance.");
                                    collection.ShowMenu(3);
                                }
                            }
                        },
                        //Menu option 3 - Add an item into inventory.
                        new MagicMenuItem()
                        {
                            Option = "Add New Inventory Item",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                try
                                {
                                    Console.Clear();
                                    Console.WriteLine("Add New Inventory Item: ");
                                    //Call add item.
                                    access.addNewItem(storeID);
                                    Console.Clear();
                                    collection.ShowMenu(3);
                                }
                                catch (Exception)
                                {
                                    //Catch exceptions
                                    Console.WriteLine("An error occured. Please seek admin assistance.");
                                    collection.ShowMenu(3);
                                }
                            }
                        },
                        //Menu option 4 - Return to main menu
                        new MagicMenuItem()
                        {
                            Option = "Return to Main Menu",
                            CannotExecute = true,
                            SubMenuId = 1
                        },
                        //Menu option 5 - Exit system.
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

        //Store Filter Menu
        public MagicMenuCollection validateFranchiseStore(MagicMenuCollection collection)
        {
            //All options below will simply indentify the store of the franchise owner.
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
