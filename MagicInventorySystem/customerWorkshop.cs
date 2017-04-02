using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class customerWorkshop : LoadWorkshop
    {
        FranchiseOwner access = new FranchiseOwner();

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
                            
                            confirmBooking(storeName[0], bookingRef[0], refNumMorningCount[0], workshopMorningMax[0], workshopAfternoonMax[0]);      // 50
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
                            
                            confirmBooking(storeName[1], bookingRef[1], refNumMorningCount[1], workshopMorningMax[1], workshopAfternoonMax[1]);      // 30
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
                            
                            confirmBooking(storeName[2], bookingRef[2], refNumMorningCount[2], workshopMorningMax[2], workshopAfternoonMax[2]);      // 20
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
                            
                            confirmBooking(storeName[3], bookingRef[3], refNumMorningCount[3], workshopMorningMax[3], workshopAfternoonMax[3]);      //30
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
                            
                            confirmBooking(storeName[4], bookingRef[4], refNumMorningCount[4], workshopMorningMax[4], workshopAfternoonMax[4]);      // 10
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
