/* Jeffrey Martin
   CIS237 Advanced C3
   9-20-20196
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1
{
    class WineAPI
    {//Class to hold the array of WineItems

        //*********************************
        //Constructor
        //*********************************


        public WineAPI()
        {
            

        }

        //*********************************
        //Methods
        //*********************************

        public void AddNewItem(string id, string description, string pack, decimal price, bool active)
        {
            BeverageJMartinEntities beveageEntities = new BeverageJMartinEntities();
            Beverage addBevarage = new Beverage();
            addBevarage.id = id;
            addBevarage.name = description;
            addBevarage.pack = pack;
            addBevarage.price = price;
            addBevarage.active = active;

            try
            {
                beveageEntities.Beverages.Add(addBevarage);
                beveageEntities.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString() + " " + e.StackTrace);
            }
            //WineCollection[wineCollectionLength] = new WineItem(id, description, pack);
            //wineCollectionLength++;
        }

        public string[] CreateListString()
        {
            //string[] listString = new string[wineCollectionLength];
            //int count = 0;

            //if (wineCollectionLength > 0)
            //{
            //    foreach (WineItem wineItem in WineCollection)
            //    {
            //        if (wineItem != null)
            //        {
            //            listString[count] = $"{wineItem.ID.ToString(),5} {wineItem.Description,-60} {wineItem.Pack,10}";
            //        }
            //        count++;
            //    }
            //}

            BeverageJMartinEntities beveageEntities = new BeverageJMartinEntities();

            int count = 0;
            foreach (Beverage beverage in beveageEntities.Beverages)
            {
                count++;
            }
            string[] listString = new string[count];
            count = 0;
            foreach (Beverage beverage in beveageEntities.Beverages)
            {
                listString[count] = FormatBevarageSting(beverage);
                count++;
            }
            return listString;
        }

        private string FormatBevarageSting (Beverage beverage)
        {
            return beverage.id + " " + beverage.pack.Trim() + " " +
                    beverage.price + " " + beverage.active +
                    " " + beverage.name.Trim();
        }

        public string SearchByAndPossiblyDelete(string searchFor, string propertyName, bool delete)
        {//Method to search any of the Bevarage properties for the data specified by the user

            BeverageJMartinEntities beveageEntities = new BeverageJMartinEntities();
            List<Beverage> queryBeverages = null;

            bool found = false;
            string listString = "*********************************************************************" + Environment.NewLine;
            switch (propertyName)
                {
                case nameof(Beverage.id):
                    queryBeverages = beveageEntities.Beverages.Where(
                                    bev => bev.id.ToLower() == searchFor.ToLower()
                                    ).ToList();
                    break;
                case nameof(Beverage.name):
                    queryBeverages = beveageEntities.Beverages.Where(
                                    bev => bev.name.ToLower().Contains(searchFor.ToLower())
                                    ).ToList();
                    break;
                case nameof(Beverage.pack):
                    queryBeverages = beveageEntities.Beverages.Where(
                                    bev => bev.pack.ToLower().Contains(searchFor.ToLower())
                                    ).ToList();
                    break;
                case nameof(Beverage.price):
                    decimal searchForDec = 0;
                    if (Decimal.TryParse(searchFor, out searchForDec))
                    {
                        queryBeverages = beveageEntities.Beverages.Where(
                                        bev => bev.price == searchForDec
                                        ).ToList();
                    }
                    break;
                case nameof(Beverage.active):
                    bool tempBool;
                    
                    if (searchFor == "True")
                    {
                        tempBool = true;
                    } 
                    else
                    {
                        tempBool = false;
                    }
                    queryBeverages = beveageEntities.Beverages.Where(
                        bev => bev.active == tempBool
                        ).ToList();
                    break;
                }

            if (queryBeverages != null)
            {
                foreach (Beverage beverage in queryBeverages)
                {
                    listString += FormatBevarageSting(beverage) + Environment.NewLine;
                    found = true;
                }
            }
            if (!found)
            {
                listString += searchFor + " was not found." + Environment.NewLine;
            }
            else
            {
                //When you want the deleted version of this method to be used.
                if (delete)
                {
                    UserInterface ui = new UserInterface();
                    ui.OutputAString(listString);
                    if (ui.AreYouSure())
                    {
                        DelelteItem(queryBeverages);
                        listString += Environment.NewLine + "The list was deleted"; 
                    }
                    else
                    {
                        listString = "";
                    }
            }
            listString += "*********************************************************************" + Environment.NewLine;

            
            }
            return listString;
        }

        public bool DelelteItem(List<Beverage> queryBeverages )
        {
            bool itemDeletedBool = false;
            int count = 0;
            BeverageJMartinEntities beveageEntities = new BeverageJMartinEntities();
            Beverage tempBeverage = new Beverage(); 
            foreach (Beverage beverage in queryBeverages)
            {
                tempBeverage = beveageEntities.Beverages.Find(beverage.id);
                beveageEntities.Beverages.Remove(tempBeverage);
                try
                {
                    tempBeverage = beveageEntities.Beverages.Find(beverage);
                    count++;
                }
                catch
                {

                }
                
            }
            if(count < 1)
            {
                beveageEntities.SaveChanges();
                itemDeletedBool = true;
                Console.WriteLine("Items Delleted at line 201 WineAPI");
            }
            
            return itemDeletedBool;
        }
    }
}
