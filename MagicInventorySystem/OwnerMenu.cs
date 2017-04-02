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
                        new MagicMenuItem()
                        {
                            Option = "Display All Stock Requests",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Boolean processStatus;
                                Console.Clear();
                                Console.WriteLine("Display All Stock Requests: ");
                                processStatus = access.displayStockRequests(true, true);
                                Console.Clear();
                                if(processStatus)
                                {
                                    Console.WriteLine("Successfully processed order.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to process order. Please ensure you selected a valid ID and there is enough stock to process order.\n");
                                }
                                collection.ShowMenu(2);
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Display Stock Requests (True / False)",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Boolean processStatus;
                                Console.Clear();
                                int type = access.promptTrueorFalse();
                                Console.Clear();
                                if(type == 0)
                                {
                                    Console.WriteLine("Invalid input. Please enter T/F or True/False");
                                    collection.ShowMenu(2);
                                }
                                Console.WriteLine("Display All Stock Requests: ");
                                if( type == 1)
                                {
                                    processStatus = access.displayStockRequests(false, true);
                                }
                                else
                                {
                                    processStatus = access.displayStockRequests(false, false);
                                }
                                Console.Clear();
                                if(processStatus)
                                {
                                    Console.WriteLine("Successfully processed order.");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to process order. Please ensure you selected a valid ID and there is enough stock to process order.\n");
                                }
                                collection.ShowMenu(2);
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Display All Product Lines",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
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
