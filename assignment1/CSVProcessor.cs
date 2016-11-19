///* Jeffrey Martin
//   CIS237 Advanced C3
//   9-20-20196
//*/
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;

//namespace assignment1
//{
//    class CSVProcessor
//    {//This class will do all the manipulation being done to the WineItem array in conjuntion with the CSV file.

//        /// <summary>
//        /// Reads data from CSV file and loads it into the array
//        /// </summary>
//        /// <param name="csvFilePath">string</param>
//        /// <param name="wineCollection">WineItem[]</param>
//        public void ReadFile(string csvFilePath, WineItemCollection wineCollection)
//        {// Method to place data into the WineItem array.

//            StreamReader streamReader = null;

//            try
//            {
//                string inputString;

//                streamReader = new StreamReader(csvFilePath);

//                int counter = 0;

//                while ((inputString = streamReader.ReadLine()) != null)
//                {
//                    processRecord(inputString, wineCollection, counter++);
//                }

//            }

//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//                Console.WriteLine();
//                Console.WriteLine(e.StackTrace);
//            }

//            finally
//            {
//                if (streamReader != null)
//                {
//                    streamReader.Close();
//                }
//            }
//        }

//        /// <summary>
//        /// Creates a single record and adds it into the array
//        /// </summary>
//        /// <param name="inputString">string</param>
//        /// <param name="wineCollection">WineItem</param>
//        /// <param name="index">int</param>
//        //static void processRecord(string inputString, WineItemCollection wineCollection, int index)
//        //{// Internal method used for tacking a single record from the CSV file and placing into the array
//        //    string[] inputParts = inputString.Split(',');

//        //    string id = inputParts[0];
//        //    string description = inputParts[1];
//        //    string pack = inputParts[2];

//        //    wineCollection.AddNewItem(id, description, pack);
//        //}
//    }
//}
