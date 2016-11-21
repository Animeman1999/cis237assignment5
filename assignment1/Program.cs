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
    class Program
    {//This file will handle the logic needed to run the menus in the userInterface.

        static void Main(string[] args)
        {
            //****************************
            //Class Variables
            //****************************

            //Create an instance of the WineAPI for use in program
            WineAPI wineItemCollection = new WineAPI();

            //Instance of the UserInterface class to run the menus'
            UserInterface ui = new UserInterface();

            //Start the User Interface and initialize it
            ui.StartUserInterface();

            //Start the main menu and wit for user input
            int choice = ui.GetUserInputMainMenu();

            //As long as the user does not choose 5 for exiting loop through the main menu
            while (choice != 6)
            {
                switch (choice)
                {
                    case 1://Go to the Print Wine List Menu in the User Interface
                        ui.GetUserInputPrintWineListMenu(wineItemCollection);
                        break;
                    case 2://Go to the method SearchForWineAndPossiblyDelete and choose search only
                        SearchForWineAndPossiblyDelete(wineItemCollection, false);
                        break;
                    case 3://Go to the method in the User Interface to add a wine item
                        ui.AddWine(wineItemCollection);
                        break;
                    case 4://Go to the method SearchForWineAndPossiblyDelete and choose search and delete
                        SearchForWineAndPossiblyDelete(wineItemCollection, true);
                    break;
                    case 5:
                        ui.GetUserInputToUpdateAWine(wineItemCollection);
                        break;
                }
                choice = ui.GetUserInputMainMenu();
            }
        }
   
        /// <summary>
        /// Method used to find out the choice of property the user wants to find or delete
        /// </summary>
        /// <param name="WineCollection">WineAPI</param>
        /// <param name="delete">bool</param>
        static void SearchForWineAndPossiblyDelete(WineAPI WineCollection, bool delete)
        {
            //Create an instance of the User Interface
            UserInterface ui = new UserInterface();
            //Output the Search or Delete Menu and wait for users choice
            int choice = ui.GetUserInputSearchMenu(delete);
            //Repeat until the user chooses 6 to exit
            while (choice != 6)
            {
                //Using the choice input by the user, get the input of the property type 
                //needed from the user.
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