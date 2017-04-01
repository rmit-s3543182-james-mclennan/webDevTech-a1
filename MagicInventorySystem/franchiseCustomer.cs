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
            //displayTitle();
            //currentPage = pageOne();
            //isCompleted = 0;
            initialPage();
            while (isCompleted == 0)
            {
                if (itemIndex == lastItem
                && (choice == "P" || choice == "p")
                && itemIndex <= allStock.Count)
                {                    
                    isCompleted = nextPage();
                }
                else if (itemIndex == lastItem
                && (choice == "R" || choice == "r")
                && itemIndex <= allStock.Count)
                {
                    
                    isCompleted = previousPage();
                }
                else if (itemIndex > allStock.Count)
                {
                    isCompleted = invalidPage();
                    lastPage();
                }
                else if (itemIndex < 1)
                {
                    isCompleted = invalidPage();
                    initialPage();
                }
                else if (itemIndex == lastItem
                && (choice == "C" || choice == "c")
                && itemIndex <= allStock.Count)
                {
                    isCompleted = transactionComplete();
                }
                else
                {
                    isCompleted = invalidInput();
                }
                                //displayProductPage();
                //displayProductPage();
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

