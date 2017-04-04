using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    interface IPageLoad
    {
        int firstPage();
        int previousPage();
        int lastPage();
        int nextPage();
        int currentPage();
        void displayProducts();
        int outOfStock();
        int invalidInput();
        int transactionComplete();
        void displayTitle();
        int purchaseItems(int choiceIndex);
        void purchaseProgress(int amount, int currentItemIndex);
        int displayPurchaseSummary();
    }
}
