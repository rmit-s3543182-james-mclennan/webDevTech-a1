using System;

namespace MagicInventorySystem
{
    class MainMenu
    {
        public void loadMainMenu()
        {
            // build a collection of menus
            // can have as deep a structure as you like
            // give each menu a unique integer MenuId
            // link to other menus by setting HasSubMenu to true, and the SubMenuId to the MenuId of the menu you wish to link to
            // or, set HasSubMenu to false, and have an Action performed when the menuitem is selected

            MagicMenuCollection collection = new MagicMenuCollection()
            {
                Menus =
            {
                new MagicMenuList()
                {
                    MenuId = 1,
                    MenuItems =
                    {
                        //Owner Menu
                        new MagicMenuItem()
                        {
                            Option = "Owner",
                            CannotExecute = true,
                            SubMenuId = 2
                        },
                        //Franchise Owner Menu
                        new MagicMenuItem()
                        {
                            Option = "Franchise Owner",
                            CannotExecute = true,
                            SubMenuId = 5
                        },
                        //Customer Menu
                        new MagicMenuItem()
                        {
                            Option = "Customer",
                            CannotExecute = true,
                            SubMenuId = 6
                        },
                        //Exit option.
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
                }
            }
            };
            collection = loadAllLists(collection);

            collection.ShowMenu(1);
            Console.ReadLine();
        }
        //loadAllLists loads all menus into the collection.
        private MagicMenuCollection loadAllLists(MagicMenuCollection collection)
        {
            OwnerMenu ownerMenu = new OwnerMenu();
            FranchiseOwnerMenu franOwnerMenu = new FranchiseOwnerMenu();
            CustomerMenu CustomerMenu = new CustomerMenu();

            collection = ownerMenu.loadOwnerMenu(collection);
            collection = franOwnerMenu.loadFranchiseOwnerMenu(collection);
            collection = franOwnerMenu.validateFranchiseStore(collection);
            collection = CustomerMenu.loadCustomerMenu(collection);
            collection = CustomerMenu.loadCustomerPurchaseMenu(collection);

            return collection;
        }
    }

}