using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace MagicInventorySystem
{

    class franchiseCustomer
    {
        FranchiseOwner access = new FranchiseOwner();
        private String storeID;
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
                        storeID = access.identifyStore("Melbourne_CBD");
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
                                storeID = access.identifyStore("Melbourne_North");
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
                                storeID = access.identifyStore("Melbourne_South");
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
                                storeID = access.identifyStore("Melbourne_East");
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
                                storeID = access.identifyStore("Melbourne_West");
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

