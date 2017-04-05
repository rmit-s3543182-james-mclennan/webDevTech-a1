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
        int displayPurchaseSummary();
        int outOfStock();
        int invalidInput();
        int transactionComplete();
        int purchaseItems(int choiceIndex);

        void displayTitle();
        void displayProducts();
        void purchaseProgress(int amount, int currentItemIndex);
    }
}
