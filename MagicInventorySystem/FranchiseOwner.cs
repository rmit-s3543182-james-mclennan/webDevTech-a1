﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicInventorySystem
{
    class FranchiseOwner
    {
        JsonProcessor reader = new JsonProcessor();
        public string storeFileName { get; set; }
        
        //Assign storeFileName the store name that the user selects.
        public void identifyStore(String storeName)
        {
            storeFileName = "Melbourne_"+storeName + "_Inventory.json";
        }

        //addNewItem will enable a franchise owner to request stock
        //from the owners inventory. 
        public void addNewItem(string storeID, Boolean checkIfUnderThreshold, int threshold)
        {
            int selectedItem = 0;
            string continueOrExit;
            //Load current items in store
            List<Products> storeInventory = reader.readProductsFile(storeFileName);
            Products requestedItem;

            Console.WriteLine("Enter item ID to stock in store: ");
            String input = Console.ReadLine();
            if(int.TryParse(input, out selectedItem))
            {
                List<Products> allStock = reader.readProductsFile("owners_inventory.json");
                //Scan for the ID that user input in selectedItem
                foreach(Products item in allStock)
                {
                    if(item.ID == selectedItem)
                    {
                        requestedItem = item;
                        //Check if a threshold applies.
                        if (checkIfUnderThreshold)
                        {
                            //If it does, scan current store inventory.
                            foreach (Products shelfItem in storeInventory)
                            {
                                if(shelfItem.ID == requestedItem.ID)
                                {
                                    //Check if the item requested, that is currently in stock,
                                    //has a stockLevel less than the threshold.
                                    if (shelfItem.stockLevel <= threshold)
                                    {
                                        sendStockRequest(requestedItem, item.stockLevel, storeID);
                                    }
                                    //If threshold is greater than stockLevel (and thus, not required to restock)
                                    else
                                    {
                                        Console.WriteLine("The item you have requested is above the threshold. Do you wish to continue? (Y/N)");
                                        continueOrExit = Console.ReadLine();
                                        //Prompt user if they wish to continue.
                                        if (continueOrExit.ToLower().Equals("y") || continueOrExit.ToLower().Equals("yes"))
                                        {
                                            sendStockRequest(requestedItem, item.stockLevel, storeID);
                                            Console.Clear();
                                            Console.WriteLine("Processed Request. Please wait for the Owner to respond\n");
                                        }
                                        else if (continueOrExit.ToLower().Equals("n") || continueOrExit.ToLower().Equals("no"))
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Request aborted.\n");
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Invalid input. Please try again.");
                                        }
                                    }
                                }
                            }
                        }
                        //Threshold does not apply, process request regardless.
                        else
                        {
                            sendStockRequest(requestedItem, item.stockLevel, storeID);
                            Console.Clear();
                            Console.WriteLine("Processed Request. Please wait for the Owner to respond\n");
                        }
                        
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please try again.\n");
            }

        }
        //Display inventory from the store. Threshold = minimum number for a restock
        //filtered = Filter the list (true/false).
        public void displayInventory(int threshold, Boolean filtered)
        {
            //Load store inventory.
            List<Products> storeInventory = reader.readProductsFile(storeFileName);
            storeInventory = storeInventory.OrderBy(id => id.ID).ToList();

            //Formating.
            String titleLine = String.Format("\n{0, -5} | {1, -15} | {2, -10} | {3, -10}", "ID", "Name", "Stock Level", "Re-Stock");
            Boolean reStock;
            Console.WriteLine(titleLine);

            for (int i = 0; i < titleLine.Length; i++)
            {
                Console.Write("=");
                if (i + 1 == titleLine.Length)
                {
                    Console.Write("\n");
                }
            }
            
            //Search through inventory of store
            foreach (Products stockItem in storeInventory)
            {
                //Check if the stockLevel is less than the threshold.
                if(stockItem.stockLevel <= threshold)
                {
                    reStock = true;
                }
                else
                {
                    reStock = false;
                }
                //Check if the filter is on
                if (filtered)
                {
                    //If the filter is on, only display true options.
                    if (reStock)
                    {
                        String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10} | {3, -10}", stockItem.ID, stockItem.name, stockItem.stockLevel, reStock);
                        Console.WriteLine(productLine);
                    }
                }
                //If filter is off, display all stock, with true or false 'Restock' depending on 
                //threshold
                else
                {
                    String productLine = String.Format("{0, -5} | {1,  -15} | {2,  -10} | {3, -10}", stockItem.ID, stockItem.name, stockItem.stockLevel, reStock);
                    Console.WriteLine(productLine);
                }
            }
            Console.WriteLine();

        }
        //Ask user for threshold limit.
        public int promptThreshold()
        {
            int threshold = -1;

            string userInput = Console.ReadLine();
            try
            {
                if (int.TryParse(userInput, out threshold))
                {
                    return threshold;
                }
            } catch (Exception) { }
            return threshold;
        }

        //Send request to warehouse
        private void sendStockRequest(Products requestedItem, int currentStock, string storeID)
        {
            //Load file of stock requests.
            List<stockRequestItem> requestList = reader.readRequestFile("stockrequests.json");
            try
            {
                //Create a new request
                stockRequestItem addRequest = new stockRequestItem();
                //Add all details below.
                int listSize = requestList.Count + 1;
                addRequest.listID = listSize;
                addRequest.ID = requestedItem.ID;
                addRequest.currentStock = currentStock;
                addRequest.store = storeID;
                addRequest.itemName = requestedItem.name;
                addRequest.quantity = 5;

                //Check if stock is more than or equal to quantity.
                if (currentStock >= addRequest.quantity)
                {
                    //If yes, then stock is available to send
                    addRequest.availableStock = true;
                }
                else
                {
                    //If no, then stock cannot be sent.
                    addRequest.availableStock = false;
                }

                //Add request.
                requestList.Add(addRequest);

                reader.writeToStockFile("stockrequests.json", requestList);
            } catch (NullReferenceException)
            {
                Console.WriteLine("Error. Please contact system admin.");
            }
        }
        
    }
}
