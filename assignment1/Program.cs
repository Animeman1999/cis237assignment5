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
            //Constants
            //****************************
            const String CSV_FILE_PATH = "../../../datafiles/WineList.csv";//Holds path and file name of the csv file
            const int MAX_ARRAY_SIZE = 4000;

            //****************************
            //Class Variables
            //****************************

            WineItemCollection wineItemCollection = new WineItemCollection(MAX_ARRAY_SIZE);//Creates the array to hold WineItems by creating an instance of the class

            CSVProcessor loadRecords = new CSVProcessor();//Creates an instance of the CSVProcessor class to process the CSV file.

            UserInterface ui = new UserInterface();//Instance of the UserInterface class to run the menus'

            BeverageJMartinEntities beverageEntities = new BeverageJMartinEntities();

            foreach (Beverage beverage in beverageEntities.Beverages)
            {
                Console.WriteLine(beverage.id =beverage.id + " " + beverage.name.Trim() + " "  + beverage.pack.Trim() + " " + beverage.price + " " + beverage.active + Environment.NewLine);
            }

            // Logic for the Start Menu found in UserInterface.cs
            
                int choice = ui.GetUserInputMainMenu();

                while (choice != 4)
                {
                    switch (choice)
                    {
                        case 1:
                            ui.PrintOutput(wineItemCollection.CreateListString());
                            break;
                        case 2:
                            SearchForWine(wineItemCollection);
                            break;
                        default:
                            ui.AddWine(wineItemCollection);
                            break;
                    }
                    choice = ui.GetUserInputMainMenu();
                }
            
        }
        /// <summary>
        /// Logic used for the PrintSearchMenu found in UserInterface.cs
        /// </summary>
        /// <param name="WineCollection">WineItem[]</param>
        /// <param name="ExamineFile">CSVProcessor</param>
        static void SearchForWine(WineItemCollection WineCollection)
        {
            WineItem winItem = new WineItem("1", "2", "3");


            UserInterface ui = new UserInterface();
            int choice = ui.GetUserInputSearchMenu();
            while (choice != 5)
            {
                switch (choice)
                {
                    case 1:
                        ui.SearchBy(WineCollection, nameof(Beverage.id));
                        break;
                    case 2:
                        ui.SearchBy(WineCollection, nameof(Beverage.name));
                        break;
                    case 3:
                        ui.SearchBy(WineCollection, nameof(Beverage.pack));
                        break;
                    default:
                        ui.SearchBy(WineCollection, nameof(Beverage.price));
                        break;
                }
                choice = ui.GetUserInputSearchMenu();
            }
        }

    }
}