/* Jeffrey Martin
   CIS237 Advanced C#
   Assignment # 5
   11-22-2016
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
                //Assign values to addBeverage properties
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
                ui.OutputAString (e.ToString() + " " + e.StackTrace);
            }
            
        }

        /// <summary>
        /// Create a string array create from the data of the database
        /// </summary>
        /// <returns></returns>
        public string[] CreateListStringUnordered()
        {
            try
            {
                //Open the database
                BeverageJMartinEntities beverageEntities = new BeverageJMartinEntities();
                //Counter to be used for index of the string array
                int count = 0;
                //Create a string array the length of the number of beverages in the database
                string[] listString = new string[beverageEntities.Beverages.Count()];
                //For each bevarage in the database format the data from it into a printable sting
                //to be added to the listString array. An array is used due to the fact that the
                //amount of data to be output could be longer than a single string could hold.
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
                ui.OutputAString(e.ToString() + " " + e.StackTrace);
                return null;
            }

        }

        /// <summary>
        /// Creates a list of formated strings for each beverage in a list of Beverages
        /// </summary>
        /// <param name="beverageList">List</Beverages>List</param>
        /// <returns>string[]</returns>
        public string[] CreateListString(List<Beverage> beverageList)
        {
            //Create an array the length of the beverageList
            string[] tempString = new string[beverageList.Count];
            int count = 0;
            //For each bevarage in the database format the data from it into a printable sting
            //to be added to the listString array. An array is used due to the fact that the
            //amount of data to be output could be longer than a single string could hold.
            foreach (Beverage beverage in beverageList)
            {
                tempString[count] = FormatBeverageSting(beverage);
                count++;
            }
            return tempString;
        }

        /// <summary>
        /// Create a list of beverages ordered by Name
        /// </summary>
        /// <returns>string[]</returns>
        public string[] CreateListStringOrderByName()
        {
            try
            {
                //Open the database
                BeverageJMartinEntities beverageEntities = new BeverageJMartinEntities();
                //Create the list of beverages orderd by Name
                List<Beverage> orderByName = (from bev in beverageEntities.Beverages
                                 orderby bev.name ascending
                                 select bev).ToList();

                return CreateListString(orderByName);
            }
            catch (Exception e)
            {
                UserInterface ui = new UserInterface();
                ui.OutputAString(e.ToString() + " " + e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Create a list of beverages ordered by Highest Price
        /// </summary>
        /// <returns>string[]</returns>
        public string[] CreateListStringOrderByHighestPrice()
        {
            try
            {
                //Open the database
                BeverageJMartinEntities beverageEntities = new BeverageJMartinEntities();
                //Create the list of beverages orderd by Highest Price
                List<Beverage> orderByHighestPrice = (from bev in beverageEntities.Beverages
                                              orderby bev.price descending
                                              select bev).ToList();
                
                return CreateListString(orderByHighestPrice);
            }
            catch (Exception e)
            {
                UserInterface ui = new UserInterface();
                ui.OutputAString(e.ToString() + " " + e.StackTrace);
                return null;
            }
            
        }

        /// <summary>
        ///  Create a list of beverages ordered by Lowest Price
        /// </summary>
        /// <returns></returns>
        public string[] CreateListStringOrderByLowestPrice()
        {
            try
            {
                //Open the database
                BeverageJMartinEntities beverageEntities = new BeverageJMartinEntities();
                //Create the list of beverages orderd by lowest Price
                List<Beverage> orderByLowestPrice = (from bev in beverageEntities.Beverages
                                                      orderby bev.price ascending
                                                      select bev).ToList();

                return CreateListString(orderByLowestPrice);
            }
            catch (Exception e)
            {
                UserInterface ui = new UserInterface();
                ui.OutputAString(e.ToString() + " " + e.StackTrace);
                return null;
            }
           
        }

        /// <summary>
        /// Formats a single beverage into a string for output. Done once for reuse.
        /// </summary>
        /// <param name="beverage">Beverage</param>
        /// <returns>string</returns>
        private string FormatBeverageSting (Beverage beverage)
        {
            try
            {
                return $"{beverage.id, -7} {beverage.price:C}  {beverage.pack.Trim(), -19}  {beverage.name.Trim(), -52}  {beverage.active}";
            }
            catch (Exception e)
            {
                UserInterface ui = new UserInterface();
                ui.OutputAString(e.ToString() + " " + e.StackTrace);
                return " ";
            }
            
        }

        /// <summary>
        /// Method to search any of the Bevarage properties for the data specified by the user 
        /// and then delete those items if the user specifed deletion.
        /// </summary>
        /// <param name="searchFor">string</param>
        /// <param name="propertyName">string</param>
        /// <param name="delete">bool</param>
        /// <returns>string</returns>
        public string SearchByAndPossiblyDelete(string searchFor, string propertyName, bool delete)
        {
            try
            {
                //Open the database
                BeverageJMartinEntities beveageEntities = new BeverageJMartinEntities();
                //Create an empty list to hold the Beverages
                List<Beverage> queryBeverages = null;

                //Bool of if anything is found in the search starts out as false
                bool found = false;
                //Create the string to hold the found list
                string listString = "*************************************************************************************************" + Environment.NewLine;
                //From the property name create a search to be done
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
                        //create a decimal value from the string value for the search
                        if (Decimal.TryParse(searchFor, out searchForDec))
                        {
                            queryBeverages = beveageEntities.Beverages.Where(
                                            bev => bev.price == searchForDec
                                            ).ToList();
                        }
                        break;
                    case nameof(Beverage.active):
                        bool tempBool;
                        //Create a bool value from the string value to be used for the search                 
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

                //Find out if anything was found in the search
                if (queryBeverages != null)
                {
                    //As something was found, return the formated beverage information for each beverage found
                    //and make the found bool true.
                    foreach (Beverage beverage in queryBeverages)
                    {
                        listString += FormatBeverageSting(beverage) + Environment.NewLine;
                        found = true;
                    }
                }
                if (!found)
                {
                    //As nothing was found return searched for term was not found.
                    listString += searchFor + " was not found." + Environment.NewLine;
                }
                else
                {
                    //if somthing was found and you want to delete the items ask to confirm than
                    //delete the items.
                    if (delete)
                    {
                        //Make an instance of UserInterface
                        UserInterface ui = new UserInterface();
                        //Output to the user the list of items found
                        ui.OutputAString(listString);
                        //Find out if the user is sure to delete the list of items found.
                        if (ui.AreYouSure())
                        {
                            //Call the method to delete the list an inform the user if it was deleted or not.
                            if (DeleteItem(queryBeverages))
                            {
                                listString += Environment.NewLine + "The list was deleted" + Environment.NewLine; 
                            }
                            else
                            {
                                listString += Environment.NewLine + "The list was NOT deleted do to an error with the database!" + Environment.NewLine;
                            }
                           
                        }
                        else
                        {
                            //As the user does not want to delete the items exit out and return a blank list.
                            listString = "Not deleted";
                        }
                    }
                listString += "*************************************************************************************************" + Environment.NewLine;
                }
                return listString;
            }
            catch (Exception e)
            {
                UserInterface ui = new UserInterface();
                ui.OutputAString(e.ToString() + " " + e.StackTrace);
                return null;
            }
            
        }

        /// <summary>
        /// Used to delete the list of bevarages found in the SearchByAndPossiblyDelete method
        /// </summary>
        /// <param name="queryBeverages"></param>
        /// <returns>bool</returns>
        public bool DeleteItem(List<Beverage> queryBeverages )
        {
            try
            {
                //Bool to hold if the item(s) was successfully deleted of not
                bool itemDeletedBool = false;
                //Count used to find out how many times that the database did not correctly delete the beverage
                int count = 0;
                //Open the database
                BeverageJMartinEntities beverageEntities = new BeverageJMartinEntities();
                //Beverage to temporarily hold a beverage
                Beverage tempBeverage = new Beverage(); 

                foreach (Beverage beverage in queryBeverages)
                {
                    //hold the current beverage in the list than delete if from the list
                    tempBeverage = beverageEntities.Beverages.Find(beverage.id);
                    beverageEntities.Beverages.Remove(tempBeverage);

                    //Check if the beverage just deleted is in the database 
                    tempBeverage = beverageEntities.Beverages.Find(beverage.id);
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
                    beverageEntities.SaveChanges();
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
                ui.OutputAString(e.ToString() + " " + e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Updates a single wine
        /// </summary>
        /// <param name="id">string</param>
        public void UpdateWine(string id)
        {
            try
            {
                //Open database
                BeverageJMartinEntities beverageEntities = new BeverageJMartinEntities();
                //Create an instance of the UserInterface
                UserInterface ui = new UserInterface();
                //Create a beverage that will hold the one to be updated by the id passed in
                Beverage beverageToUpdate = beverageEntities.Beverages.Find(id);
            
                string inputString = " ";
                //Continue to loop until the user choose 5 to save and exit.
                while (inputString != "5")
                {
                    //Output the beveragtToUpdate to the user
                    ui.PrintLine();
                    ui.ColorLineNoEnter(FormatBeverageSting(beverageToUpdate));
                    ui.PrintLine();
                    //Ouput the Edit Menu
                    ui.ColorLineNoEnter(ui.PrintEditMenu(id));
                    //Get the users choice of which menu item to do
                    inputString = ui.InputCharReturnString();
                    //Loop until valid input from the user
                    while (inputString != "1" && inputString != "2" && inputString != "3" && inputString != "4" && inputString != "5")
                    {
                        ui.ColorLineNoEnter(ui.WriteInvalidEntry());
                        ui.ColorLineNoEnter(FormatBeverageSting(beverageToUpdate));
                        ui.ColorLineNoEnter(ui.PrintEditMenu(id));
                        inputString = ui.InputCharReturnString();
                    }
                    //Ouput a blank line
                    ui.OutputAString(" ");
                    //Get the information needed from the user to update the property of the 
                    //property type the user just chose.
                    switch (inputString)
                    {
                        case "1":
                            beverageToUpdate.name = ui.GetTheNameOfTheWine();
                            break;
                        case "2":
                            beverageToUpdate.pack = ui.GetTheWinePack();
                            break;
                        case "3":
                            beverageToUpdate.price = ui.GetThePrice();
                            break;
                        case "4":
                            beverageToUpdate.active = ui.GetIfWineIsActive();
                            break;
                    }
                }
                //Save the changes to the database
                beverageEntities.SaveChanges();
            }
            catch (Exception e)
            {
                UserInterface ui = new UserInterface();
                ui.OutputAString(e.ToString() + " " + e.StackTrace);
            }
        }
    }
}
