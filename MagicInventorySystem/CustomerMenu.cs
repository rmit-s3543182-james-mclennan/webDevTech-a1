using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class CustomerMenu
    {
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
                                Console.WriteLine("Need to implement function!");
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
    }
}
