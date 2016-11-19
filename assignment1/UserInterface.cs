/* Jeffrey Martin
   CIS237 Advanced C3
   9-20-20196
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
    class UserInterface
    {// Methods to handle all input and output for the program

        //****************************************Code to Maxamize the Console***********************
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
        /// Handles input and output of the Start Menu
        /// </summary>
        /// <returns>int</returns>
        public int GetUserStartMenu()
        {
            this.LoadMenu();
            ConsoleKeyInfo inputChar = Console.ReadKey();
            string inputString = inputChar.KeyChar.ToString();
            Console.WriteLine();
            while (inputString != "1" && inputString != "2")
            {
                Console.WriteLine(WriteInvalidEntry());
                this.LoadMenu();
                inputChar = Console.ReadKey();
                inputString = inputChar.KeyChar.ToString();
                Console.WriteLine();
            }
            return Int16.Parse(inputString);
        }
        /// <summary>
        /// Handles input and output of the Main Menu
        /// </summary>
        /// <returns></returns>
        public int GetUserInputMainMenu()
        {
            this.PrintMainMenu();
            ConsoleKeyInfo inputChar = Console.ReadKey();
            string inputString = inputChar.KeyChar.ToString();
            Console.WriteLine();
            while (inputString != "1" && inputString != "2" && inputString != "3" && inputString != "4" && inputString != "5")
            {
                Console.WriteLine(WriteInvalidEntry());
                this.PrintMainMenu();
                inputChar = Console.ReadKey();
                inputString = inputChar.KeyChar.ToString();
                Console.WriteLine();
            }
            return Int16.Parse(inputString);
        }

        public void GetUserInputPrintWineListMenu(WineAPI wineCollection)
        {
            this.PrintWineListMenu();
            ConsoleKeyInfo inputChar = Console.ReadKey();
            string inputString = inputChar.KeyChar.ToString();
            Console.WriteLine();
            while (inputString != "1" && inputString != "2" && inputString != "3" && inputString != "4" && inputString != "5")
            {
                Console.WriteLine(WriteInvalidEntry());
                this.PrintWineListMenu();
                inputChar = Console.ReadKey();
                inputString = inputChar.KeyChar.ToString();
                Console.WriteLine();
            }
            switch (inputString)
            {
                case "1":
                    PrintOutput(wineCollection.CreateListString());
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

            ConsoleKeyInfo inputChar = Console.ReadKey();
            string inputString = inputChar.KeyChar.ToString();
            Console.WriteLine();
            while (inputString != "1" && inputString != "2" && inputString != "3" && inputString != "4" && inputString != "5" && inputString != "6")
            {
                Console.WriteLine(WriteInvalidEntry());
                this.PrintSearchMenu(delete);
                inputChar = Console.ReadKey();
                inputString = inputChar.KeyChar.ToString();
                Console.WriteLine();
            }
            return Int16.Parse(inputString);
        }

        
        /// <summary>
        /// Generic invalid entry error message
        /// </summary>
        /// <returns>string</returns>
        private string WriteInvalidEntry()
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
                Console.Write($"Enter {propertyName}: ");
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
            Console.Write("Enter Wine ID: ");
            string idInput = Console.ReadLine();
            if (idInput == "")
            {
                Console.WriteLine(WriteInvalidSpecificEntry("Wine Id"));

            }
            else
            {
                string seachString = WineCollection.SearchByAndPossiblyDelete(idInput, nameof(Beverage.id), false);
                if (seachString.Contains("was not found"))
                {
                    Console.Write("Enter Wine Description: ");
                    string descriptionInput = Console.ReadLine();
                    while (descriptionInput == "")
                    {
                        Console.WriteLine(WriteInvalidSpecificEntry("Wine Description"));
                        Console.Write("Enter Wine Description: ");
                        descriptionInput = Console.ReadLine();
                    }


                    Console.Write("Enter Wine Pack: ");
                    string packInput = Console.ReadLine();
                    while (packInput == "")
                    {
                        Console.WriteLine(WriteInvalidSpecificEntry("Wine Pack"));
                        Console.Write("Enter Wine Pack: ");
                        packInput = Console.ReadLine();
                    }

                    decimal priceInputDec;
                    Console.Write("Enter Price: ");
                    string priceInput = Console.ReadLine();
                    while (priceInput == "" || !(Decimal.TryParse(priceInput, out priceInputDec)))
                    {
                        Console.WriteLine("Invalid Wine Price");
                        Console.Write("Enter Price: ");
                        priceInput = Console.ReadLine();
                    }

                    bool wineActive = BoolInput("Is this wine active");                    



                    //WineItem wineItemToAdd = new WineItem();
                    //wineItemToAdd.ID = idInput;
                    //wineItemToAdd.Description = descriptionInput;
                    //wineItemToAdd.Pack = packInput;

                    WineCollection.AddNewItem(idInput, descriptionInput, packInput, priceInputDec, wineActive);
                    

                    Console.WriteLine(WineCollection.SearchByAndPossiblyDelete(idInput, nameof(Beverage.id), false));
                }
                else
                {
                    Console.WriteLine(idInput + " is allready used as an ID. Each ID must be unique.");
                }
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
        /// <returns>ConsoleKeyInfo</returns>
        public string GetBoolInput(string YesNoQuestion)
        {
            Console.WriteLine();
            Console.WriteLine($"{YesNoQuestion}?");
            Console.WriteLine("Enter Yes or No.");
            string tempStringInfo = Console.ReadLine();
            Console.WriteLine();
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

            Console.BufferHeight = Int16.MaxValue - 1;
            for (int index = 0; index < printOutput.Length; index++)
            {
                Console.Write(Environment.NewLine + printOutput[index]);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Outputs the Load Menu to the console
        /// </summary>
        private void LoadMenu()
        {
            Console.WriteLine("Welcome to the wine list program.");
            Console.WriteLine("To start the program you must load the wine list.");
            Console.WriteLine();
            Console.WriteLine("#############-Load Menu-#############");
            Console.WriteLine("1) Load Wine List");
            Console.WriteLine("2) Exit the program");
            Console.WriteLine("#############-Load Menu-#############");
            Console.Write("Press the number of the menu item: ");
        }

        /// <summary>
        /// Outputs the Main Menu to the console
        /// </summary>
        private void PrintMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("#############-Main Menu-#############");
            Console.WriteLine("1) Print Wine List");
            Console.WriteLine("2) Search for Wine");
            Console.WriteLine("3) Add a new Wine");
            Console.WriteLine("4) Delete a Wine");
            Console.WriteLine("5) Exit the program");
            Console.WriteLine("#############-Main Menu-#############");
            Console.Write("Press the number of the menu item: ");
        }

        private void PrintWineListMenu()
        {
            Console.WriteLine();
            Console.WriteLine("#############-Print Wine List Menu-#############");
            Console.WriteLine("1) Print Wine List Unorderd");
            Console.WriteLine("2) Print Wine List by Name");
            Console.WriteLine("3) Print Wine List by Highest Price");
            Console.WriteLine("4) Print Wine List by Lowest Price");
            Console.WriteLine("5) Exit the program");
            Console.WriteLine("#############-Main Menu-#############");
            Console.Write("Press the number of the menu item: ");
        }

        /// <summary>
        /// Outputs the Search Menu to the console
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
            Console.WriteLine();
            Console.WriteLine($"############-{searchDelete} Menu-############");
            Console.WriteLine($"1) {searchDelete} by ID");
            Console.WriteLine($"2) {searchDelete} by Discription");
            Console.WriteLine($"3) {searchDelete} by Pack");
            Console.WriteLine($"4) {searchDelete} by Price");
            Console.WriteLine($"5) {searchDelete} by Active");

            Console.WriteLine("6) Return to Main Menu");
            Console.WriteLine($"############-{searchDelete} Menu-############");
            Console.Write($"Press the number of the menu item: ");
        }

                public void PrintFileLoadedMessage()
        {
            Console.WriteLine("********Wines have been loaded********");
        }

        public bool AreYouSure()
        {
           return BoolInput("Are you sure you wish to delete the listed wines");
        }

        public void OutputAString(string outputString)
        {
            Console.WriteLine(outputString);
        }

        public void ErrorInDeleting()
        {
            Console.WriteLine("*************There was an error in deleting from the database. Items not deleted*************");
        }

        public void ErrorCapture(string errorMessage)
        {
            Console.WriteLine(errorMessage);
        }
    }
}
