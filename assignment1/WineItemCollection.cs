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
    class WineItemCollection
    {//Class to hold the array of WineItems

        //*********************************
        //Backing Fields
        //*********************************
        WineItem[] WineCollection;
        int wineCollectionLength;

        //*********************************
        //Constructor
        //*********************************


        public WineItemCollection(int size)
        {
            WineCollection = new WineItem[size];
            wineCollectionLength = 0;
        }

        //*********************************
        //Methods
        //*********************************

        public void AddNewItem(string id, string description, string pack)
        {
            WineCollection[wineCollectionLength] = new WineItem(id, description, pack);
            wineCollectionLength++;
        }

        public string[] CreateListString()
        {
            string[] listString = new string[wineCollectionLength];
            int count = 0;

            if (wineCollectionLength > 0)
            {
                foreach (WineItem wineItem in WineCollection)
                {
                    if (wineItem != null)
                    {
                        listString[count] = $"{wineItem.ID,5} {wineItem.Description,-60} {wineItem.Pack,10}";
                    }
                    count++;
                }
            }
            return listString;
        }

        public string SearchBy(string searchFor, string propertyName)
        {//Generic method to search any of the WineItem properties for the data specified by the user
            bool found = false;
            string listString = "*********************************************************************" + Environment.NewLine;
            foreach (WineItem wineItem in WineCollection)
            {

                if (wineItem != null)
                {
                    //String to hold the value held in the property converted to lowercase. The various Gets are used to enable any property to be passed in.
                    string propertyValue = wineItem.GetType().GetProperty(propertyName).GetValue(wineItem).ToString().ToLower();

                    //String to hold the value of the searchFor converted to lowercase.
                    string searchValue = searchFor.ToString();
                    if (propertyValue.Contains(searchValue))
                    {
                        found = true;
                        listString += wineItem + Environment.NewLine;
                    }
                }
            }
            if (!found)
            {
                listString += searchFor + " was not found." + Environment.NewLine;
            }
            listString += "*********************************************************************" + Environment.NewLine;
            return listString;
        }
    }
}
