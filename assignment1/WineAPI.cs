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
    {//Class to act as an interface between the program and the database. Because the connection
     //to the database could be down, every method is placed inside of try catch.

        //*********************************
        //Constructor
        //*********************************


        public WineAPI()
        {
        }

        //*********************************
        //Methods
        //*********************************

        /// <summary>
        /// Takes in the properties of a bevarage and adds it to the database
        /// </summary>
        /// <param name="id">string</param>
        /// <param name="name">string</param>
        /// <param name="pack">string</param>
        /// <param name="price">decimal</param>
        /// <param name="active">bool</param>
        public void AddNewItem(string id, string name, string pack, decimal price, bool active)
        {
            try
            {
                //Open the databse
                BeverageJMartinEntities beverageEntities = new BeverageJMartinEntities();
                //Create a new beverage
                Beverage addBevarage = new Beverage();
                addBevarage.id = id;
                addBevarage.name = name;
                addBevarage.pack = pack;
                addBevarage.price = price;
                addBevarage.active = active;
                //Add the new beverage with it's assigned properties to beverageEntites
                beverageEntities.Beverages.Add(addBevarage);
                //Save the changes to the databes made in this instance of beverageEntitites.
                beverageEntities.SaveChanges();
            }
            catch (Exception e)
            {
                UserInterface ui = new UserInterface();
                ui.ErrorCapture (e.ToString() + " " + e.StackTrace);
            }
            
        }

        public string[] CreateListString()
        {
            try
            {
                BeverageJMartinEntities beverageEntities = new BeverageJMartinEntities();
                int count = 0;
                foreach (Beverage beverage in beverageEntities.Beverages)
                {
                    count++;
                }
                string[] listString = new string[count];
                count = 0;
                foreach (Beverage beverage in beverageEntities.Beverages)
                {
                    listString[count] = FormatBeverageSting(beverage);
                    count++;
                }
                return listString;
            }
            catch (Exception e)
            {
                UserInterface ui = new UserInterface();
                ui.ErrorCapture(e.ToString() + " " + e.StackTrace);
                return null;
            }

        }

        public string[] CreateListStringOrderByName()
        {
            try
            {
                BeverageJMartinEntities beverageEntities = new BeverageJMartinEntities();
                List<Beverage> orderByName = (from bev in beverageEntities.Beverages
                                 orderby bev.name ascending
                                 select bev).ToList();

                string[] tempString = new string[orderByName.Count];
                int count = 0;
                foreach (Beverage beverage in orderByName)
                {
                    tempString[count] = FormatBeverageSting(beverage);
                    count++;
                }

                return tempString;
            }
            catch (Exception e)
            {
                UserInterface ui = new UserInterface();
                ui.ErrorCapture(e.ToString() + " " + e.StackTrace);
                return null;
            }
            
        }

        public string[] CreateListStringOrderByHighestPrice()
        {
            try
            {
                BeverageJMartinEntities beverageEntities = new BeverageJMartinEntities();
                List<Beverage> orderByHighestPrice = (from bev in beverageEntities.Beverages
                                              orderby bev.price descending
                                              select bev).ToList();

                string[] tempString = new string[orderByHighestPrice.Count];
                int count = 0;
                foreach (Beverage beverage in orderByHighestPrice)
                {
                    tempString[count] = FormatBeverageSting(beverage);
                    count++;
                }

                return tempString;
            }
            catch (Exception e)
            {
                UserInterface ui = new UserInterface();
                ui.ErrorCapture(e.ToString() + " " + e.StackTrace);
                return null;
            }
            
        }

        public string[] CreateListStringOrderByLowestPrice()
        {
            try
            {
                BeverageJMartinEntities beverageEntities = new BeverageJMartinEntities();
                List<Beverage> orderByLowestPrice = (from bev in beverageEntities.Beverages
                                                      orderby bev.price ascending
                                                      select bev).ToList();

                string[] tempString = new string[orderByLowestPrice.Count];
                int count = 0;
                foreach (Beverage beverage in orderByLowestPrice)
                {
                    tempString[count] = FormatBeverageSting(beverage);
                    count++;
                }

                return tempString;
            }
            catch (Exception e)
            {
                UserInterface ui = new UserInterface();
                ui.ErrorCapture(e.ToString() + " " + e.StackTrace);
                return null;
            }
           
        }


        private string FormatBeverageSting (Beverage beverage)
        {
            try
            {
                return $"{beverage.id, -7} {beverage.price:C}  {beverage.pack.Trim(), -19}  {beverage.name.Trim(), -52}  {beverage.active}";
            }
            catch (Exception e)
            {
                UserInterface ui = new UserInterface();
                ui.ErrorCapture(e.ToString() + " " + e.StackTrace);
                return " ";
            }
            
        }

        public string SearchByAndPossiblyDelete(string searchFor, string propertyName, bool delete)
        {//Method to search any of the Bevarage properties for the data specified by the user

            try
            {
                BeverageJMartinEntities beveageEntities = new BeverageJMartinEntities();
                List<Beverage> queryBeverages = null;

                bool found = false;
                string listString = "*************************************************************************************************" + Environment.NewLine;
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
                        listString += FormatBeverageSting(beverage) + Environment.NewLine;
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
                            listString += Environment.NewLine + "The list was deleted" + Environment.NewLine; 
                        }
                        else
                        {
                            listString = "";
                        }
                }
                listString += "*************************************************************************************************" + Environment.NewLine;

            
                }
                return listString;
            }
            catch (Exception e)
            {
                UserInterface ui = new UserInterface();
                ui.ErrorCapture(e.ToString() + " " + e.StackTrace);
                return null;
            }
            
        }

        public bool DelelteItem(List<Beverage> queryBeverages )
        {
            try
            {
                bool itemDeletedBool = false;
                int count = 0;
                BeverageJMartinEntities beveageEntities = new BeverageJMartinEntities();
                Beverage tempBeverage = new Beverage(); 
                foreach (Beverage beverage in queryBeverages)
                {
                    tempBeverage = beveageEntities.Beverages.Find(beverage.id);
                    beveageEntities.Beverages.Remove(tempBeverage);

                    //Check if the beverage just deleted is in the database 
                    tempBeverage = beveageEntities.Beverages.Find(beverage.id);
                    //If the bevarage is still in the database add one to the count
                    if (tempBeverage == null)
                    {
                        count++;
                    }
                }
                //If the count is not less than one, than at least one of the items was not succesffuly deleted
                if(count < 1)
                {
                    //All items successfully added so save the changes
                    beveageEntities.SaveChanges();
                    itemDeletedBool = true;
                }
                else
                {
                    //An item was not added correctly so do not save the changes and print out an error message.
                    UserInterface ui = new UserInterface();
                    ui.ErrorInDeleting();
                }
            
                return itemDeletedBool;
            }
            catch (Exception e)
            {
                UserInterface ui = new UserInterface();
                ui.ErrorCapture(e.ToString() + " " + e.StackTrace);
                return false;
            }
           
        }
    }
}
