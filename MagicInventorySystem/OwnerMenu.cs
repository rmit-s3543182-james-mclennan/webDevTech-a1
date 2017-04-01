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
                            Option = "Display All Stock Requests\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                Console.WriteLine("Display All Stock Requests: ");
                                access.displayAllStockRequests();
                                Console.Clear();
                                collection.ShowMenu(2);
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Display Stock Requests (True / False)\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.WriteLine("Need to implement function!");
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Display All Product Lines\n",
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
    }
}
