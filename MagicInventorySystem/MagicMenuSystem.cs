using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    /*MenuItem is a single line/option in a menu. It contains:
     * Option = The string for the line
     * HasSubMenu = A boolean to say if there is a menu or is this the leaf of the menu
     * SubMenuId = The ID for a specific submenu
     * Action = The action that will take place, if this line is a leaf
     */
    class MagicMenuItem
    {
        public string Option { get; set; }
        public bool HasSubMenu { get; set; }
        public int? SubMenuId { get; set; }
        public Action Action { get; set; }
    }

    /*Menu is an entire Menu display or a list of MenuItem's. It contains:
     * MenuId = ID for current menu
     * MenuItems = A list of MagicMenuItem's
     * Title = a title for the menu
     */
    class MagicMenuSystem
    {
        public MagicMenuSystem()
        {
            MenuItems = new List<MagicMenuItem>();
        }

        public int MenuId { get; set; }
        public List<MagicMenuItem> MenuItems { get; set; }
        public string Title { get; set; }

        //Prints all items in the MagicMenu, with index on the left side.
        public void PrintToConsole()
        {
            Console.WriteLine("Welcome to the Magic Inventory System!");
            Console.WriteLine("======================================\n");
            foreach (MagicMenuItem item in MenuItems)
            {
                Console.WriteLine("     " + (MenuItems.IndexOf(item) + 1 ) + ". " + item.Option + "\n");
            }
        }
    }

    /*MagicMenuCollection is all menus in the Magic Inventory System.
     * It collects all menus, and processes user input for the navigation.
     */
    class MagicMenuCollection
    {
        public MagicMenuCollection()
        {
            Menus = new List<MagicMenuSystem>();
        }

        public List<MagicMenuSystem> Menus { get; set; }

        public void ShowMenu(int id)
        {
            //Obtain menu that is requested and display to console.
            var currentMenu = Menus.Where(m => m.MenuId == id).Single();
            currentMenu.PrintToConsole();

            //Request user input
            Console.WriteLine("Enter an option:");
            string choice = Console.ReadLine();
            int choiceIndex;

            //Ensure input is an integer and within range of options.
            //If not then show an error message and re-display the menu
            if (!int.TryParse(choice, out choiceIndex) || currentMenu.MenuItems.Count < choiceIndex || choiceIndex < 0)
            {
                Console.Clear();
                Console.WriteLine("Invalid selection - try again");
                ShowMenu(id);
            }
            else
            {
                //If the selection is good, then option assign the item to menuItemSelected 
                //choiceIndex is minus 1, in order to show a list index from 1 (as it starts at 0).
                var menuItemSelected = currentMenu.MenuItems[choiceIndex - 1];

                //Check if the item has a sub menu, if it does, show this menu.
                if (menuItemSelected.HasSubMenu)
                {
                    Console.Clear();
                    ShowMenu(menuItemSelected.SubMenuId.Value);
                }
                //If item does not have a sub menu, then execute action.
                else
                {
                    menuItemSelected.Action();
                }
            }
        }
    }

}
