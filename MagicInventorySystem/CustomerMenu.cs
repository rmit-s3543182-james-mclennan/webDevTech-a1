using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    // Sub(Derived)class of PageLoad
    class CustomerMenu : PageLoad
    {

        public MagicMenuCollection loadCustomerPurchaseMenu(MagicMenuCollection collection)
        {
            collection.Menus.Add(new MagicMenuList()
            {
                MenuId = 6,
                MenuItems =
                {
                    new MagicMenuItem()
                    {
                        Option = "Melbourne CBD",
                        CannotExecute = false,
                        Execute = () =>
                        {
                            Console.Clear();
                            storeFileName = "Melbourne_CBD_Inventory.json";
                            workshopBranch = "Melbourne_CBD";
                            collection.ShowMenu(4);
                        }
                    },
                    new MagicMenuItem()
                    {
                        Option = "Melbourne North",
                        CannotExecute = false,
                        Execute = () =>
                        {
                            Console.Clear();
                            storeFileName = "Melbourne_North_Inventory.json";
                            workshopBranch = "Melbourne_North";
                            collection.ShowMenu(4);
                        }
                    },
                    new MagicMenuItem()
                    {
                        Option = "Melbourne South",
                        CannotExecute = false,
                        Execute = () =>
                        {
                            Console.Clear();
                            storeFileName = "Melbourne_South_Inventory.json";
                            workshopBranch = "Melbourne_South";
                            collection.ShowMenu(4);
                        }
                    },
                    new MagicMenuItem()
                    {
                        Option = "Melbourne East",
                        CannotExecute = false,
                        Execute = () =>
                        {
                            Console.Clear();
                            storeFileName = "Melbourne_East_Inventory.json";
                            workshopBranch = "Melbourne_East";
                            collection.ShowMenu(4);
                        }
                    },
                    new MagicMenuItem()
                    {
                        Option = "Melbourne West",
                        CannotExecute = false,
                        Execute = () =>
                        {
                            Console.Clear();
                            storeFileName = "Melbourne_West_Inventory.json";
                            workshopBranch = "Melbourne_West";
                            collection.ShowMenu(4);
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

        
        public MagicMenuCollection loadCustomerMenu(MagicMenuCollection collection)
        {
            collection.Menus.Add(new MagicMenuList()
            {
                MenuId = 4,
                MenuItems =
                    {
                        new MagicMenuItem()
                        {
                            Option = "Display Products",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                CustomerOrder();
                                Console.ReadKey();
                                Console.Clear();
                                collection.ShowMenu(4);
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Display Workshops",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                displayWorkshopConfirmation();
                                Console.ReadKey();
                                Console.Clear();
                                collection.ShowMenu(4);
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

        public void CustomerOrder()
        {
            int choiceIndex;
            Console.Clear();
            Console.WriteLine("Products at " + workshopBranch + " store");
            firstPage();
            /* The while loop works until the user presses "c"
             * or finishes products purchases
             */
            while (isCompleted == 0)
            {
                if (itemIndex == lastItem
                && (choice == "P" || choice == "p")
                && itemIndex <= allStock.Count)
                {
                    isCompleted = nextPage();
                }
                else if(itemIndex == lastItem
                && (choice == "B" || choice == "b")
                && itemIndex <= allStock.Count)
                {
                    isCompleted = previousPage();
                }
                else if (itemIndex == lastItem
                && (choice == "R" || choice == "r")
                && itemIndex <= allStock.Count)
                {
                    Console.WriteLine("Press any key to return to the menu.");
                    isCompleted = 1;
                }
                else if (itemIndex == lastItem
                && (choice == "C" || choice == "c")
                && itemIndex <= allStock.Count)
                {
                    isCompleted = transactionComplete();
                }

                else if (int.TryParse(choice, out choiceIndex)
                && choiceIndex > 0
                && choiceIndex <= allStock.Count)
                {
                    isCompleted = purchaseItems(choiceIndex - 1);
                }
                else
                {
                    isCompleted = invalidInput();
                }
            }
        }

        //public void bookingWorkshop()
        //{
        //    while (bookingCompleted == 0)
        //    {
        //        bookingCompleted = 
        //                    displayWorkshopConfirmation();
        //    }
        //    bookingCompleted = 0;
        //}
    }

}
