using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class CustomerMenu
    {
        JsonProcessor reader = new JsonProcessor();
        Owner displayProducts = new Owner();
        public MagicMenuCollection loadCustomerMenu(MagicMenuCollection collection)
        {
            collection.Menus.Add(new MagicMenuList()
            {
                MenuId = 4,
                MenuItems =
                    {
                        new MagicMenuItem()
                        {
                            Option = "Display Products\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                displayProducts.displayAllStock();
                                CustomerOrder();
                                Console.ReadKey();
                                Console.Clear();
                                collection.ShowMenu(4);
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Display Workshops\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.WriteLine("Need to implement function!");
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
            int isComplete = 0;
            while(isComplete == 0)
            {
                Console.WriteLine("[Legend: 'P' Next Page | 'R' Return to Menu | 'C' Complete Transaction]");
                Console.Write("Enter Item Number to purchase or Function: ");
                string choice = Console.ReadLine();
                if (choice == "P" || choice == "p")
                {
                    // Get to next product page
                    Console.Clear();
                    displayProducts.displayAllStock();
                    Console.WriteLine("got to next product page");
                    moveProductPage();
                    isComplete = 0;   
                }
                else if (choice == "R" || choice == "r")
                {
                    // Get back to previous product page
                    Console.Clear();
                    displayProducts.displayAllStock();
                    Console.WriteLine("Got back to previous page");
                    moveProductPage();
                    
                    isComplete = 0;
                }
                else if (choice == "C" || choice == "c")
                {
                    // Transaction done
                    Console.WriteLine("need to implement function");
                    isComplete = 1;
                }
                else
                {
                    // Invalid input. try again
                    Console.Clear();
                    displayProducts.displayAllStock();
                    Console.WriteLine("Invalid input - try again");
                    isComplete = 0;

                }
            }

        }

        public void moveProductPage()
        {
            /* need to implement function to paginate json file objects
             * to indicate 5 products in a page
             */
              
             
        }

    }
}
