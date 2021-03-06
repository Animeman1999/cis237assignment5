﻿/* Jeffrey Martin
   CIS237 Advanced C#
   Assignment # 5
   11-22-2016
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; //Needed to maxamize the console
using System.Runtime.InteropServices; //Needed to maxamzie the console.

namespace assignment1
{
    /// <summary>
    ///  Methods to handle all input and output for the program
    /// </summary>
    class UserInterface
    {   //****************************************Code to Maxamize the Console***********************
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;

        /// <summary>
        /// Method to maxamize the console window
        /// </summary>
        private static void MaximizeConsole()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);
        }

        /// <summary>
        /// Adjust the console to increase the console buffer, change the title and maxamize the window
        /// </summary>
        public void StartUserInterface()
        {
            Console.BufferHeight = Int16.MaxValue - 1;
            Console.Title = "Wine list from a Database on a server";
            MaximizeConsole();
        }

        /// <summary>
        /// Handles input and output of the Main Menu
        /// </summary>
        /// <returns>Int16</returns>
        public int GetUserInputMainMenu()
        {
            this.PrintMainMenu();
            string inputString = InputCharReturnString();
            while (inputString != "1" && inputString != "2" && inputString != "3" && inputString != "4" && inputString != "5" && inputString != "6")
            {
                Console.WriteLine(WriteInvalidEntry());
                this.PrintMainMenu();
                inputString = InputCharReturnString();
            }
            return Int16.Parse(inputString);
        }

        /// <summary>
        /// Handles input and Output of the Print Wine List Menu
        /// </summary>
        /// <param name="wineCollection"></param>
        public void GetUserInputPrintWineListMenu(WineAPI wineCollection)
        {
            this.PrintWineListMenu();
            string inputString = InputCharReturnString();
            while (inputString != "1" && inputString != "2" && inputString != "3" && inputString != "4" && inputString != "5")
            {
                Console.WriteLine(WriteInvalidEntry());
                this.PrintWineListMenu();
                inputString = InputCharReturnString();
            }
            Console.WriteLine("Connecting to database, this may take a while.");

            //Output the list of wines by calling the appropriate sorting the user wants
            switch (inputString)
            {
                case "1":
                    PrintOutput(wineCollection.CreateListStringUnordered());
                break;
                case "2":
                    PrintOutput(wineCollection.CreateListStringOrderByName());
                    break;
                case "3":
                    PrintOutput(wineCollection.CreateListStringOrderByHighestPrice());
                    break;
                case "4":
                    PrintOutput(wineCollection.CreateListStringOrderByLowestPrice());
                    break;
            }
        }

        /// <summary>
        /// Handles the input and output of the Search Menu
        /// </summary>
        /// <returns>int</returns>
        public int GetUserInputSearchMenu(bool delete)
        {
            this.PrintSearchMenu(delete);

            string inputString = InputCharReturnString();
            while (inputString != "1" && inputString != "2" && inputString != "3" && inputString != "4" && inputString != "5" && inputString != "6")
            {
                Console.WriteLine(WriteInvalidEntry());
                this.PrintSearchMenu(delete);
                inputString = InputCharReturnString();
            }
            return Int16.Parse(inputString);
        }

        
        /// <summary>
        /// Generic invalid entry error message
        /// </summary>
        /// <returns>string</returns>
        public string WriteInvalidEntry()
        {
            string invalidEntry;
            invalidEntry = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" + Environment.NewLine +
                            "Not a valid entry, please make another choice" + Environment.NewLine +
                            "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" + Environment.NewLine;
            return invalidEntry;
        }

        /// <summary>
        /// Specific invalid entry error message
        /// </summary>
        /// <param name="propertyName">string</param>
        /// <returns>string</returns>
        private string WriteInvalidSpecificEntry(string propertyName)
        {
            string errorMessage = "XXXXXXXXXXXXXXXXXXXXXXXXXX" + Environment.NewLine +
                                    $"No {propertyName} entered" + Environment.NewLine +
                                    "XXXXXXXXXXXXXXXXXXXXXXXXXX";
            return errorMessage;
        }

        /// <summary>
        /// Searches the property for a value input by the user and outputs if found
        /// </summary>
        /// <param name="WineCollection">WineItem[]</param>
        /// <param name="propertyName">string</param>
        public void SearchBy(WineAPI WineCollection, string propertyName, bool delete)
        {
            string input;
            if (propertyName == "active")
            {
                input = BoolInput("Do you want the active wines").ToString();
            }
            else
            {
                ColorLineNoEnter($"Enter {propertyName}: ");
                input = Console.ReadLine();
            }
            
            if (input == "")
            {
                Console.WriteLine(WriteInvalidSpecificEntry(propertyName));
            }
            else
            {
                Console.WriteLine(WineCollection.SearchByAndPossiblyDelete(input, propertyName, delete));
            }
        }

        /// <summary>
        /// Input sequence to create a new WineItem that calls a method to add it to the WineItem array.
        /// </summary>
        /// <param name="WineCollection">WineItem[]</param>
        public void AddWine(WineAPI WineCollection)
        {
            string idInput = GetId();
            //Check if the id the user input is all ready in the database
            string seachString = WineCollection.SearchByAndPossiblyDelete(idInput, nameof(Beverage.id), false);
            if (seachString.Contains("was not found"))
            {
                //Get the Name of the wine
                string descriptionInput = GetTheNameOfTheWine();
                    
                //Get the Wine pack
                string packInput = GetTheWinePack();

                //Get the price
                decimal priceInputDec = GetThePrice();
                   
                //Get if the wine is active
                bool wineActive = BoolInput("Is this wine active");                    
                    
                //Now that all the input has been recieved by the user call the method to add the wine
                WineCollection.AddNewItem(idInput, descriptionInput, packInput, priceInputDec, wineActive);
                    
                //Output the wine that has been added
                Console.WriteLine(WineCollection.SearchByAndPossiblyDelete(idInput, nameof(Beverage.id), false));
            }
            else
            {
                Console.WriteLine(idInput + " is allready used as an ID. Each ID must be unique.");
            }
            
        }

        /// <summary>
        /// Generic method to test if a yes/no question was answered correctly and assign appropiate bool value 
        /// </summary>
        /// <param name="YesNoQuestion"></param>
        /// <returns>bool</returns>
        public bool BoolInput(string YesNoQuestion)
        {
            string inputKey;
            //Print out the YesNoQuestion and get the users response
            inputKey = GetBoolInput(YesNoQuestion);

            //Get user response until correct data input
            while (inputKey.ToLower().Trim() != "y" && inputKey.ToLower().Trim() != "yes" && inputKey.ToLower().Trim() != "n" && inputKey.ToLower().Trim() != "no")
            {
                ErrorMessage();
                inputKey = GetBoolInput(YesNoQuestion);
            }

            //Assign correct bool value
            if (inputKey.ToLower().Trim() == "y" || inputKey.ToLower().Trim() == "yes")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Generic method to print out the YesNoQuestion and get the users response
        /// </summary>
        /// <param name="YesNoQuestion"></param>
        /// <returns>string</returns>
        public string GetBoolInput(string YesNoQuestion)
        {
            string outputString = $"{YesNoQuestion}?" + Environment.NewLine;
            outputString+= "Enter Yes or No: ";
            ColorLineNoEnter(outputString);
            string tempStringInfo = Console.ReadLine();
            return tempStringInfo;
        }

        public void ErrorMessage()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine();
            Console.WriteLine(" Invalid Entry please try again.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Method to ouput any string to the console
        /// </summary>
        /// <param name="printOutput">string</param>
        public void PrintOutput(string[] printOutput)
        {
            //Go through the entire string array
            for (int index = 0; index < printOutput.Length; index++)
            {
                //Every 60 items output the header
                if (index % 60 == 0)
                {
                    Console.WriteLine();
                    PrintHeaders();
                }
                //Change the color every other line of wine for easier reading
                if (index % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.Write(Environment.NewLine + printOutput[index]);
               
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Outputs the headers
        /// </summary>
        private void PrintHeaders()
        {
            Console.WriteLine();
            string id = "ID";
            string price = "Price";
            string pack = "Pack";
            string name = "Name";
            string active = "Active";
            Console.WriteLine($"{id,-7} {price}   {pack,-19}  {name,-52}  {active}");
        }


        /// <summary>
        /// String to be output for the Main Menu 
        /// </summary>
        private void PrintMainMenu()
        {
            string outputString = Environment.NewLine;
            outputString += "#############-Main Menu-#############" + Environment.NewLine;
            outputString += "1) Print Wine List" + Environment.NewLine;
            outputString += "2) Search for Wine" + Environment.NewLine;
            outputString += "3) Add a new Wine" + Environment.NewLine;
            outputString += "4) Delete a Wine" + Environment.NewLine;
            outputString += "5) Update a wine in the list" + Environment.NewLine;
            outputString += "6) Exit the program" + Environment.NewLine;
            outputString += "#############-Main Menu-#############" + Environment.NewLine;
            outputString += "Enter the number of the menu item: ";
            ColorLineNoEnter(outputString);
        }

        /// <summary>
        /// String to be output for the Print Wine List Menu
        /// </summary>
        private void PrintWineListMenu()
        {
            string outputString = Environment.NewLine;
            outputString += "#############-Print Wine List Menu-#############" + Environment.NewLine;
            outputString += "1) Print Wine List Unorderd" + Environment.NewLine;
            outputString += "2) Print Wine List by Name" + Environment.NewLine;
            outputString += "3) Print Wine List by Highest Price" + Environment.NewLine;
            outputString += "4) Print Wine List by Lowest Price" + Environment.NewLine;
            outputString += "5) Exit the Print Wine List" + Environment.NewLine;
            outputString += "#############-Print Wine List Menu-#############" + Environment.NewLine;
            outputString += "Enter the number of the menu item: ";
            ColorLineNoEnter(outputString);
        }

        /// <summary>
        /// String to be output for the Search/Delete Menu
        /// </summary>
        private void PrintSearchMenu(bool delete)
        {
            string searchDelete;
            if (delete)
            {
                searchDelete = "Delete";
            }
            else
            {
                searchDelete = "Search";
            }
            string outputString = Environment.NewLine;
            outputString += $"############-{searchDelete} Menu-############" + Environment.NewLine;
            outputString += $"1) {searchDelete} by ID" + Environment.NewLine;
            outputString += $"2) {searchDelete} by Name" + Environment.NewLine;
            outputString += $"3) {searchDelete} by Pack" + Environment.NewLine;
            outputString += $"4) {searchDelete} by Price" + Environment.NewLine;
            outputString += $"5) {searchDelete} by Active" + Environment.NewLine;
            outputString += "6) Return to Main Menu" + Environment.NewLine;
            outputString += $"############-{searchDelete} Menu-############" + Environment.NewLine;
            outputString += $"Enter the number of the menu item: ";
            ColorLineNoEnter(outputString);
        }

        /// <summary>
        /// Method to be called from the WineAPI fo confirm the deleting of the wines found.
        /// </summary>
        /// <returns>bool</returns>
        public bool AreYouSure()
        {
           return BoolInput("Are you sure you wish to delete the listed wines");
        }

        /// <summary>
        /// Method to allow any other part of the program to output a string to the console
        /// </summary>
        /// <param name="outputString"></param>
        public void OutputAString(string outputString)
        {
            Console.WriteLine(outputString);
        }

        /// <summary>
        /// Error in Deleting message
        /// </summary>
        public void ErrorInDeleting()
        {
            Console.WriteLine("*************There was an error in deleting from the database. Items not deleted*************");
        }

        /// <summary>
        /// Controls the look of the output any string for unified color and easy update of color scheme
        /// </summary>
        /// <param name="outputString">string</param>
        public void ColorLineNoEnter(string outputString)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(outputString);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Get id from user of wine to update and launches UpdateWine in the WineAPI using the ID
        /// from the user.
        /// </summary>
        /// <param name="WineCollection">WineAPI</param>
        public void GetUserInputToUpdateAWine(WineAPI WineCollection)
        {
            string idInput = GetId();
            //Check if the id the user input is all ready in the database
            string seachString = WineCollection.SearchByAndPossiblyDelete(idInput, nameof(Beverage.id), false);
            if (seachString.Contains("was not found"))
            {
                Console.WriteLine(idInput + " Id was not found so can not be edited.");
            }
            else
            {
                //As the id was found to be used in the database launch the UpdateWine
                WineCollection.UpdateWine(idInput);
            }
            
        }
        
        /// <summary>
        /// Get the ID input from the user
        /// </summary>
        /// <returns>string</returns>
        public string GetId()
        {   
            ColorLineNoEnter("Enter Wine ID : ");
            string idInput = Console.ReadLine();
            while (idInput == "")
            {
                Console.WriteLine(WriteInvalidSpecificEntry("Wine Id"));
                ColorLineNoEnter("Enter Wine ID : ");
                idInput = Console.ReadLine();
            }
            return idInput;    
        }

        /// <summary>
        /// Get the Name of the wine from the user
        /// </summary>
        /// <returns>string</returns>
        public string GetTheNameOfTheWine()
        {
            ColorLineNoEnter("Enter Wine Name: ");
            string descriptionInput = Console.ReadLine();
            while (descriptionInput == "")
            {
                Console.WriteLine(WriteInvalidSpecificEntry("Wine Name"));
                ColorLineNoEnter("Enter Wine Name: ");
                descriptionInput = Console.ReadLine();
            }
            return descriptionInput;
        }

        /// <summary>
        /// Get the Wine pack from the user
        /// </summary>
        /// <returns>string</returns>
        public string GetTheWinePack()
        {
            ColorLineNoEnter("Enter Wine Pack: ");
            string packInput = Console.ReadLine();
            while (packInput == "")
            {
                Console.WriteLine(WriteInvalidSpecificEntry("Wine Pack"));
                ColorLineNoEnter("Enter Wine Pack: ");
                packInput = Console.ReadLine();
            }
            return packInput;
        }

        /// <summary>
        /// Get the price from the user
        /// </summary>
        /// <returns>Decimal</returns>
        public Decimal GetThePrice()
        {
            decimal priceInputDec;
            ColorLineNoEnter("Enter Price: ");
            string priceInput = Console.ReadLine();
            while (priceInput == "" || !(Decimal.TryParse(priceInput, out priceInputDec)))
            {
                Console.WriteLine("Invalid Wine Price");
                ColorLineNoEnter("Enter Price: ");
                priceInput = Console.ReadLine();
            }
            return priceInputDec;
        }

        /// <summary>
        /// Get user input if the wine is active
        /// </summary>
        /// <returns>bool</returns>
        public bool GetIfWineIsActive()
        {
            bool wineActive = BoolInput("Is this wine active");
            return wineActive;
        }

        /// <summary>
        /// Takes a single char and converts it into a string
        /// </summary>
        /// <returns>string</returns>
        public string InputCharReturnString()
        {
            //ConsoleKeyInfo inputChar = Console.ReadKey();
            //string inputString = inputChar.KeyChar.ToString();
            //Console.WriteLine();
            //return inputString;
            return Console.ReadLine();
        }

        /// <summary>
        /// Creates the Edit Menu as a string
        /// </summary>
        /// <param name="wineId">string</param>
        /// <returns>string</returns>
        public string PrintEditMenu(string wineId)
        {
            string outputString = Environment.NewLine;
            outputString += $"############-Edit for wine ID {wineId} Menu-############" + Environment.NewLine;
            outputString += $"1) Edit by Name" + Environment.NewLine;
            outputString += $"2) Edit by Pack" + Environment.NewLine;
            outputString += $"3) Edit by Price" + Environment.NewLine;
            outputString += $"4) Edit by Active" + Environment.NewLine;
            outputString += "5) Return to Main Menu" + Environment.NewLine;
            outputString += $"############-Edit for wine ID {wineId} Menu-############" + Environment.NewLine;
            outputString += $"Enter the number of the menu item: ";
            return outputString;
        }

        /// <summary>
        /// Outputs a single line of dashes
        /// </summary>
        public void PrintLine()
        {
            Console.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------------------------");
        }
    }
}
