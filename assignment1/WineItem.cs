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
{// Class to hold one record of a wine item.
    class WineItem
    {
        //*********************************
        //Backing Fields
        //*********************************
        private string _id;
        private string _description;
        private string _pack;

        //*********************************
        //Properties
        //*********************************

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string Pack
        {
            get { return _pack; }
            set { _pack = value; }
        }

        //*********************************
        //Constructors
        //*********************************
        public WineItem(string id, string description, string pack)
        {  // 3 Parameter Constructor
            this._id = id;
            this._description = description;
            this._pack = pack;
        }

        public WineItem()
        {
            //Default Parameter Constructor
        }

        //*********************************
        //Methods
        //*********************************

        /// <summary>
        /// Creates the overide string for each WineItem 
        /// </summary>
        /// <returns>string</returns>
        private string CreateOverideString()
        {
            string overideString = $"{this._description} Wine ID: {this._id}; Sold in: {this._pack}";
            return overideString;
        }
        /// <summary>
        /// Creates the overide of ToString() for each WineItem 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return CreateOverideString();
        }
    }
}
