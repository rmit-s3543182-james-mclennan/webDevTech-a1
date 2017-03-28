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
        public MagicMenuCollection loadFranchiseOwnerMenu(MagicMenuCollection collection)
        {
            collection.Menus.Add(new MagicMenuList()
            {
                MenuId = 3,
                MenuItems =
                    {
                        new MagicMenuItem()
                        {
                            Option = "Display Inventory\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.WriteLine("Need to implement function!");
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Display Inventory (Threshold)\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.WriteLine("Need to implement function!");
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Add New Inventory Item\n",
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

        public MagicMenuCollection validateFranchiseStore(MagicMenuCollection collection)
        {
            collection.Menus.Add(new MagicMenuList()
            {
                MenuId = 5,
                MenuItems =
                    {
                        new MagicMenuItem()
                        {
                            Option = "Melbourne CBD\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                access.identifyStore("Melbourne_CBD");
                                collection.ShowMenu(3);

                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Melbourne North\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                access.identifyStore("Melbourne_North");
                                collection.ShowMenu(3);
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Melbourne South\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                access.identifyStore("Melbourne_South");
                                collection.ShowMenu(3);
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Melbourne East\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                access.identifyStore("Melbourne_East");
                                collection.ShowMenu(3);
                            }
                        },
                        new MagicMenuItem()
                        {
                            Option = "Melbourne West\n",
                            CannotExecute = false,
                            Execute = () =>
                            {
                                Console.Clear();
                                access.identifyStore("Melbourne_West");
                                collection.ShowMenu(3);
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
