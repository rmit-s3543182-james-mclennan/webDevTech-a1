using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

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
                            Option = "Display Products",
                            CannotExecute = true,
                            SubMenuId = 6
                        },
                        new MagicMenuItem()
                        {
                            Option = "Display Workshops",
                            CannotExecute = true,
                            SubMenuId = 7
                        },
                        new MagicMenuItem()
                        {
                            Option = "Return to Main Menu",
                            CannotExecute = true,
                            SubMenuId = 1
                        },
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
            });
            return collection;
        }
            

            
        

        
    }
    
}
