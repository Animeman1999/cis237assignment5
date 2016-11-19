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
    class Program
    {//This file will handle the logic needed to run the menus in the userInterface.

        static void Main(string[] args)
        {
            //****************************
            //Class Variables
            //****************************

            WineAPI wineItemCollection = new WineAPI();

            UserInterface ui = new UserInterface();//Instance of the UserInterface class to run the menus'

            BeverageJMartinEntities beverageEntities = new BeverageJMartinEntities();
            
            int choice = ui.GetUserInputMainMenu();

            while (choice != 5)
            {
                switch (choice)
                {
                    case 1:
                        ui.PrintOutput(wineItemCollection.CreateListString());
                        break;
                    case 2:
                        SearchForWine(wineItemCollection, false);
                        break;
                    case 3:
                        ui.AddWine(wineItemCollection);
                        break;
                    case 4:
                        SearchForWine(wineItemCollection, true);
                    break;
                }
                choice = ui.GetUserInputMainMenu();
            }
            
        }
   

        static void SearchForWine(WineAPI WineCollection, bool delete)
        {
            UserInterface ui = new UserInterface();
            int choice = ui.GetUserInputSearchMenu(delete);
            while (choice != 6)
            {
                switch (choice)
                {
                    case 1:
                        ui.SearchBy(WineCollection, nameof(Beverage.id), delete);
                        break;
                    case 2:
                        ui.SearchBy(WineCollection, nameof(Beverage.name), delete);
                        break;
                    case 3:
                        ui.SearchBy(WineCollection, nameof(Beverage.pack), delete);
                        break;
                    case 4:
                        ui.SearchBy(WineCollection, nameof(Beverage.price), delete);
                        break;
                    case 5:
                        ui.SearchBy(WineCollection, nameof(Beverage.active), delete);
                        break;
                }
                choice = ui.GetUserInputSearchMenu(delete);
            }
        }
    }
}