using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace MagicInventorySystem
{

    class franchiseCustomer : PageLoad
    {
        FranchiseOwner access = new FranchiseOwner();
        public MagicMenuCollection loadFranchiseCustomerMenu(MagicMenuCollection collection)
        {
            collection.Menus.Add(new MagicMenuList()
            {
                MenuId = 6,
                MenuItems =
                            {
                new MagicMenuItem()
                {
                    Option = "Melbourne CBD\n",
                    CannotExecute = false,
                    Execute = () =>
                    {
                                Console.Clear();
                                CustomerOrder();
                                Console.ReadKey();
                                Console.Clear();
                                collection.ShowMenu(6);

                    }
                },
                        new MagicMenuItem()
                        {
                            Option = "Melbourne North\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                CustomerOrder();
                                Console.ReadKey();
                                Console.Clear();
                                collection.ShowMenu(6);
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Melbourne South\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                CustomerOrder();
                                Console.ReadKey();
                                Console.Clear();
                                collection.ShowMenu(6);
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Melbourne East\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                CustomerOrder();
                                Console.ReadKey();
                                Console.Clear();
                                collection.ShowMenu(6);
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Melbourne West\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                CustomerOrder();
                                Console.ReadKey();
                                Console.Clear();
                                collection.ShowMenu(6);
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

        public void CustomerOrder()
        {
            Console.Clear();
            Console.WriteLine("[!] Loading products from owners_inventory.json");
            displayTitle();
            currentPage = pageOne();
            isCompleted = 0;
            while (isCompleted == 0)
            {
                Console.WriteLine("Page " + currentPage + "/" + totalPage);
                Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
                Console.Write("Enter Item Number to purchase or Function(ID - Quantity) : ");
                choice = Console.ReadLine();
                try
                {
                    if (choice == "P" || choice == "p")
                    {
                        isCompleted = displayPageTwo();
                    }
                    else if (choice == "R" || choice == "r")
                    {
                        isCompleted = displayPageFour();
                    }
                    else if (choice == "C" || choice == "c")
                    {
                        Console.WriteLine("Transaction done!");
                        isCompleted = transactionComplete();
                    }
                    else if (Convert.ToInt32(choice) < allStock.Count
                    && Convert.ToInt32(choice) > 0)
                    {
                        foreach (Products getItem in allStock)
                        {
                            if (choice == Convert.ToString(getItem.ID)
                            && getItem.stockLevel > 0)
                            {
                                Console.Write("Choose the quantity of the product(current stock : "
                                    + getItem.stockLevel + ") : ");
                                choice = Console.ReadLine();
                            }
                            // out of stock
                            else if (choice == Convert.ToString(getItem.ID) && getItem.stockLevel <= 0)
                            {
                                outOfStock();
                            }
                        }
                    }
                    else
                    {
                        isCompleted = invalidInput();
                    }
                }
                catch (FormatException e)
                {
                    isCompleted = invalidInput();
                }
                /* compare product request with current stocklevel
                 * and then if stocklevel is > 0
                 * update current stock(-1)
                 * afterwards ask again to buy more stuff
                 * and book a workshop(if so, 10% discount of price)
                 */
            }

        }
    }
        
}

