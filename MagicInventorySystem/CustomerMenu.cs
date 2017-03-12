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
            collection.Menus.Add(new MagicMenuSystem()
            {
                MenuId = 4,
                MenuItems =
                    {
                        new MagicMenuItem()
                        {
                            Option = "Display Products\n",
                            HasSubMenu = false,
                            Action = () =>
                            {
                                Console.WriteLine("Need to implement function!");
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Display Workshops\n",
                            HasSubMenu = false,
                            Action = () =>
                            {
                                Console.WriteLine("Need to implement function!");
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Return to Main Menu\n",
                            HasSubMenu = true,
                            SubMenuId = 1
                        },
                        new MagicMenuItem()
                        {
                            Option = "Exit\n",
                            HasSubMenu = false,
                            Action = () =>
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
