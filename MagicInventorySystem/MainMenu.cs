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
            OwnerMenu ownerMenu = new OwnerMenu();
            FranchiseOwnerMenu franOwnerMenu = new FranchiseOwnerMenu();
            CustomerMenu customerMenu = new CustomerMenu();
            MagicMenuCollection collection = new MagicMenuCollection()
            {
                Menus =
            {
                new MagicMenuSystem()
                {
                    MenuId = 1,
                    MenuItems =
                    {
                        new MagicMenuItem()
                        {
                            Option = "Owner",
                            HasSubMenu = true,
                            SubMenuId = 2
                        },

                        new MagicMenuItem()
                        {
                            Option = "Franchise Owner",
                            HasSubMenu = true,
                            SubMenuId = 3
                        },

                        new MagicMenuItem()
                        {
                            Option = "Customer",
                            HasSubMenu = true,
                            SubMenuId = 4
                        },

                        new MagicMenuItem()
                        {
                            Option = "Exit",
                            HasSubMenu = false,
                            Action = () =>
                            {
                                System.Environment.Exit(1);
                            }
                        }
                    }
                }
            }
            };
            collection = ownerMenu.loadOwnerMenu(collection);
            collection = franOwnerMenu.loadFranchiseOwnerMenu(collection);
            collection = customerMenu.loadCustomerMenu(collection);
            collection.ShowMenu(1);
            Console.ReadLine();
        }
        
    }

}