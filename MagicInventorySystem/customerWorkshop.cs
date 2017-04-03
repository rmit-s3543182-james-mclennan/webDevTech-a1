﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class customerWorkshop : LoadWorkshop
    {
        //FranchiseOwner access = new FranchiseOwner();

        public MagicMenuCollection loadCustomerWorkshop(MagicMenuCollection collection)
        {
            collection.Menus.Add(new MagicMenuList()
            {
                MenuId = 7,
                MenuItems =
                {
                    new MagicMenuItem()
                    {
                        Option = "Melbourne CBD\n",
                        CannotExecute = false,
                        Execute = () =>
                        {
                            Console.Clear();
                            storeFileName = "Melbourne_CBD_Workshop.json";
                            workshopConfirmation();
                            Console.ReadKey();
                            Console.Clear();
                            collection.ShowMenu(4);

                        }
                    },
                    new MagicMenuItem()
                    {
                        Option = "Melbourne North\n",
                        CannotExecute = false,
                        Execute = () =>
                        {
                            Console.Clear();
                            storeFileName = "Melbourne_North_Workshop.json";
                            workshopConfirmation();
                            Console.ReadKey();
                            Console.Clear();
                            collection.ShowMenu(4);
                        }
                    },
                    new MagicMenuItem()
                    {
                        Option = "Melbourne South\n",
                        CannotExecute = false,
                        Execute = () =>
                        {
                            Console.Clear();
                            storeFileName = "Melbourne_South_Workshop.json";
                            workshopConfirmation();
                            Console.ReadKey();
                            Console.Clear();
                            collection.ShowMenu(4);
                        }
                    },
                    new MagicMenuItem()
                    {
                        Option = "Melbourne East\n",
                        CannotExecute = false,
                        Execute = () =>
                        {
                            Console.Clear();
                            storeFileName = "Melbourne_East_Workshop.json";
                            workshopConfirmation();
                            Console.ReadKey();
                            Console.Clear();
                            collection.ShowMenu(4);
                        }
                    },
                    new MagicMenuItem()
                    {
                        Option = "Melbourne West\n",
                        CannotExecute = false,
                        Execute = () =>
                        {
                            Console.Clear();
                            storeFileName = "Melbourne_West_Workshop.json";
                            workshopConfirmation();
                            Console.ReadKey();
                            Console.Clear();
                            collection.ShowMenu(4);
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

        public void bookingWorkshop()
        {
            
        }
    }
}
