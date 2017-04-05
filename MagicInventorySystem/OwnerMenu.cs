using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class OwnerMenu
    {
        Owner access = new Owner();
        public MagicMenuCollection loadOwnerMenu(MagicMenuCollection collection)
        {
            collection.Menus.Add(new MagicMenuList()
            {
                MenuId = 2,
                MenuItems =
                    {
                        //Option 1 - Display all stock requests.
                        new MagicMenuItem()
                        {
                            Option = "Display All Stock Requests",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                int processStatus;
                                Console.Clear();
                                //Show all products in stock.
                                Console.WriteLine("Display All Stock Requests: ");
                                processStatus = access.displayStockRequests(true, true);
                                Console.Clear();
                                //If process worked
                                if(processStatus == 1)
                                {
                                    Console.WriteLine("Successfully processed order.\n");
                                }
                                //If user exited.
                                else if(processStatus == 0)
                                {
                                    Console.WriteLine("No request processed.\n");
                                }
                                //Invalid option
                                else
                                {
                                    Console.WriteLine("Failed to process order. Please ensure you selected a valid ID and there is enough stock to process order.\n");
                                }
                                collection.ShowMenu(2);
                            }
                        },
                        //Option 2 - Display Stock Requests that are True or False
                        new MagicMenuItem()
                        {
                            Option = "Display Stock Requests (True / False)",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                int processStatus;
                                Console.Clear();
                                //Ask user for true or false.
                                int type = access.promptTrueorFalse();
                                Console.Clear();
                                //Wrong input.
                                if(type == 0)
                                {
                                    Console.WriteLine("Invalid input. Please enter T/F or True/False");
                                    collection.ShowMenu(2);
                                }
                                Console.WriteLine("Display All Stock Requests: ");
                                //User requested "True"
                                if(type == 1)
                                {
                                    processStatus = access.displayStockRequests(false, true);
                                }
                                //User requested "False"
                                else
                                {
                                    processStatus = access.displayStockRequests(false, false);
                                }
                                Console.Clear();
                                //If process worked
                                if(processStatus == 1)
                                {
                                    Console.WriteLine("Successfully processed order.\n");
                                }
                                //If user exited.
                                else if(processStatus == 0)
                                {
                                    Console.WriteLine("No request processed.\n");
                                }
                                //Invalid option
                                else
                                {
                                    Console.WriteLine("Failed to process order. Please ensure you selected a valid ID and there is enough stock to process order.\n");
                                }
                                collection.ShowMenu(2);
                            }
                        },
                        //Display all product lines
                        new MagicMenuItem()
                        {
                            Option = "Display All Product Lines",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                //Print all items.
                                Console.WriteLine("Display All Products: ");
                                access.displayAllStock();
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                Console.Clear();
                                collection.ShowMenu(2);
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
