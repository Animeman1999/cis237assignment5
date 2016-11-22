/* Jeffrey Martin
   CIS237 Advanced C#
   Assignment # 5
   11-22-2016
*/

using NUnit.Framework;
using assignment1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace assignment1.Tests
{
    [TestFixture()]
    public class WineAPITests
    {
        [Test()]
        public void WineAPITest()
        {
            WineAPI testWineAPI = new WineAPI();
            Assert.IsNotNull(testWineAPI);
        }

        [Test()]
        public void AddNewItemTest()
        {
            bool active = true;
            string id = "XXXXXX";
            string name = "white winie";
            string pack = "54 ml";
            Decimal price = 12.12M;

            BeverageJMartinEntities BevEntitites = new BeverageJMartinEntities();
            WineAPI testWineAPI = new WineAPI();
            Beverage testBeverage = new Beverage();
            testBeverage.active = active;
            testBeverage.id = id;
            testBeverage.name = name;
            testBeverage.pack = pack;
            testBeverage.price = price;
            testWineAPI.AddNewItem(id, name, pack, price, active);

            Beverage addedBeverage = BevEntitites.Beverages.Find(id);
            Assert.AreEqual(testBeverage.active, addedBeverage.active);
            Assert.AreEqual(testBeverage.id, addedBeverage.id);
            Assert.AreEqual(testBeverage.name.Trim(), addedBeverage.name.Trim());
            Assert.AreEqual(testBeverage.pack.Trim(), addedBeverage.pack.Trim());
            Assert.AreEqual(testBeverage.price, addedBeverage.price);

            BevEntitites.Beverages.Remove(addedBeverage);
            BevEntitites.SaveChanges();

        }

        [Test()]
        public void CreateListStringUnorderedTest()
        {
            BeverageJMartinEntities BevEntitites = new BeverageJMartinEntities();
            WineAPI testWineAPI = new WineAPI();
            bool active = true;
            string id = "00000";
            string id2 = "XXXXX";
            string name = "white winie";
            string pack = "54 ml";
            Decimal price = 12.12M;
            testWineAPI.AddNewItem(id, name, pack, price, active);
            testWineAPI.AddNewItem(id2, name, pack, price, active);
            string[] testString = testWineAPI.CreateListStringUnordered();

            Beverage addedBeverage = BevEntitites.Beverages.Find(id);
            BevEntitites.Beverages.Remove(addedBeverage);
            addedBeverage = BevEntitites.Beverages.Find(id2);
            BevEntitites.Beverages.Remove(addedBeverage);
            BevEntitites.SaveChanges();

            StringAssert.Contains(id, testString[0]);
            StringAssert.Contains(id2, testString[testString.Count() -1]);
           
        }

        [Test()]
        public void CreateListStringTest()
        {
            BeverageJMartinEntities BevEntitites = new BeverageJMartinEntities();
            WineAPI testWineAPI = new WineAPI();
            bool active = true;
            string id = "00000";
            string id2 = "XXXXX";
            string name = "white winie";
            string pack = "54 ml";
            Decimal price = 12.12M;
            Beverage testBev1 = new Beverage();
            Beverage testBev2 = new Beverage();
            testBev1.active = active;
            testBev1.id = id;
            testBev1.name = name;
            testBev1.pack = pack;
            testBev1.price = price;

            testBev2.active = active;
            testBev2.id = id2;
            testBev2.name = name;
            testBev2.pack = pack;
            testBev2.price = price;

            List<Beverage> testBevList = new List<Beverage> { testBev1, testBev2 };
            string[] testString = testWineAPI.CreateListString(testBevList);
            StringAssert.Contains(id, testString[0]);
            StringAssert.Contains(id2, testString[1]);
        }

        [Test()]
        public void CreateListStringOrderByNameTest()
        {
            BeverageJMartinEntities BevEntitites = new BeverageJMartinEntities();
            WineAPI testWineAPI = new WineAPI();
            bool active = true;
            string id = "00000";
            string id2 = "XXXXX";
            string name = "000000 white ";
            string name2 = "ZZZZZZZ White";
            string pack = "54 ml";
            Decimal price = 12.12M;
            testWineAPI.AddNewItem(id, name, pack, price, active);
            testWineAPI.AddNewItem(id2, name2, pack, price, active);
            string[] testString = testWineAPI.CreateListStringOrderByName();

            Beverage addedBeverage = BevEntitites.Beverages.Find(id);
            BevEntitites.Beverages.Remove(addedBeverage);
            addedBeverage = BevEntitites.Beverages.Find(id2);
            BevEntitites.Beverages.Remove(addedBeverage);
            BevEntitites.SaveChanges();

            StringAssert.Contains(id, testString[0]);
            StringAssert.Contains(id2, testString[testString.Count() - 1]);
            
        }

        [Test()]
        public void CreateListStringOrderByHighestPriceTest()
        {
            BeverageJMartinEntities BevEntitites = new BeverageJMartinEntities();
            WineAPI testWineAPI = new WineAPI();
            bool active = true;
            string id = "00000";
            string id2 = "XXXXX";
            string name = "000000 white ";
            string name2 = "ZZZZZZZ White";
            string pack = "54 ml";
            Decimal price = -12.12M;
            Decimal price2 = 120000.12M;
            testWineAPI.AddNewItem(id, name, pack, price, active);
            testWineAPI.AddNewItem(id2, name2, pack, price2, active);
            string[] testString = testWineAPI.CreateListStringOrderByHighestPrice();

            Beverage addedBeverage = BevEntitites.Beverages.Find(id);
            BevEntitites.Beverages.Remove(addedBeverage);
            addedBeverage = BevEntitites.Beverages.Find(id2);
            BevEntitites.Beverages.Remove(addedBeverage);
            BevEntitites.SaveChanges();

            StringAssert.Contains(id2, testString[0]);
            StringAssert.Contains(id, testString[testString.Count() - 1]);
            
           
        }

        [Test()]
        public void CreateListStringOrderByLowestPriceTest()
        {
            BeverageJMartinEntities BevEntitites = new BeverageJMartinEntities();
            WineAPI testWineAPI = new WineAPI();
            bool active = true;
            string id = "00000";
            string id2 = "XXXXX";
            string name = "000000 white ";
            string name2 = "ZZZZZZZ White";
            string pack = "54 ml";
            Decimal price = 0M;
            Decimal price2 = 120000.12M;
            testWineAPI.AddNewItem(id, name, pack, price, active);
            testWineAPI.AddNewItem(id2, name2, pack, price2, active);
            string[] testString = testWineAPI.CreateListStringOrderByLowestPrice();


            Beverage addedBeverage = BevEntitites.Beverages.Find(id);
            BevEntitites.Beverages.Remove(addedBeverage);
            addedBeverage = BevEntitites.Beverages.Find(id2);
            BevEntitites.Beverages.Remove(addedBeverage);
            BevEntitites.SaveChanges();


            StringAssert.Contains(id, testString[0]);
            StringAssert.Contains(id2, testString[testString.Count() - 1]);
           
        }

        [Test()]
        public void SearchByAndPossiblyDeleteTest()
        {
            bool delete1 = true;
            BeverageJMartinEntities BevEntitites = new BeverageJMartinEntities();
            WineAPI testWineAPI = new WineAPI();
            Beverage addBev = new Beverage();
            
            bool active = true;
            string id = "TTTTTT";
            string id2 = "TTTTTA";
            string id3 = "TTTTTB";
            string id4 = "TTTTTC";
            string id5 = "TTTTTD";
            string name = "white winie";
            string pack = "54 ml";
            Decimal price = 12.12M;
            StringBuilder actualOutput = replaceConsole();
            string inputString = StringRead("y{0}");

            testWineAPI.AddNewItem(id, name, pack, price, active);
            string idTest = testWineAPI.SearchByAndPossiblyDelete(id, nameof(Beverage.id), delete1);
           
            inputString = StringRead("y{0}");
            testWineAPI.AddNewItem(id2, name, pack, price, active);
            string nameTest = testWineAPI.SearchByAndPossiblyDelete(name, nameof(Beverage.name), delete1);
            
            inputString = StringRead("y{0}");
            testWineAPI.AddNewItem(id3, name, pack, price, active);
            string packTest = testWineAPI.SearchByAndPossiblyDelete(pack, nameof(Beverage.pack), delete1);
            
            inputString = StringRead("y{0}");
            testWineAPI.AddNewItem(id4, name, pack, price, active);
            string priceTest = testWineAPI.SearchByAndPossiblyDelete(price.ToString(), nameof(Beverage.price), delete1);
            
            inputString = StringRead("n{0}6{0}");
            testWineAPI.AddNewItem(id5, name, pack, price, active);
            string activeTest = testWineAPI.SearchByAndPossiblyDelete(active.ToString(), nameof(Beverage.active), delete1);
            addBev = BevEntitites.Beverages.Find(id5);
            BevEntitites.Beverages.Remove(addBev);
            BevEntitites.SaveChanges();

            StringAssert.Contains(id, idTest);
            StringAssert.Contains(name, nameTest);
            StringAssert.Contains(pack, packTest);
            StringAssert.Contains(price.ToString(), priceTest);
            StringAssert.Contains("Not deleted", activeTest);
        }

        [Test()]
        public void DeleteItemTest()
        {
            BeverageJMartinEntities BevEntitites = new BeverageJMartinEntities();
            WineAPI testWineAPI = new WineAPI();
            bool active = true;
            string id = "00000";
            string name = "white winie";
            string pack = "54 ml";
            Decimal price = 12.12M;
            testWineAPI.AddNewItem(id, name, pack, price, active);
            string[] testString = testWineAPI.CreateListStringUnordered();

            Beverage addedBeverage = BevEntitites.Beverages.Find(id);
            List<Beverage> testBevList = new List<Beverage> { addedBeverage };
            bool itemAdded = false;
            if (addedBeverage != null)
            {
                itemAdded = true;
            }
            bool itemDeleted =  testWineAPI.DeleteItem(testBevList);
            addedBeverage = BevEntitites.Beverages.Find(id);
           
            Assert.IsTrue(itemAdded);
            Assert.IsTrue(itemDeleted);
        }

        [Test()]
        public void UpdateWineTest()
        {
            StringBuilder actualOutput = replaceConsole();

            BeverageJMartinEntities BevEntitites = new BeverageJMartinEntities();
            WineAPI testWineAPI = new WineAPI();
            bool active = true;
            string id = "VVVVV";
            string name = "white winie";
            string pack = "54 ml";
            Decimal price = 12.12M;
            testWineAPI.AddNewItem(id, name, pack, price, active);

            string inputString = StringRead("1{0}Red winie{0}2{0}53 ml{0}3{0}11.11{0}4{0}n{0}5{0}6{0}");

            testWineAPI.UpdateWine(id);

            Beverage changedBeverage = BevEntitites.Beverages.Find(id);
            string nameActual = changedBeverage.name;
            string packActual = changedBeverage.pack;
            decimal priceActual = changedBeverage.price;
            bool activeActual = changedBeverage.active;

            BevEntitites.Beverages.Remove(changedBeverage);
            BevEntitites.SaveChanges();

            Assert.AreEqual("Red winie", nameActual.Trim());
            Assert.AreEqual("53 ml", packActual.Trim());
            Assert.AreEqual(11.11, priceActual);
            Assert.IsFalse(activeActual);
        }
        private static StringBuilder replaceConsole()
        {
            StringBuilder actualOutput = new StringBuilder();
            StringWriter sw = new StringWriter(actualOutput);
            Console.SetOut(sw);
            return actualOutput;
        }
        private static string StringRead(string inputString)
        {
            StringReader sr = new StringReader(string.Format(inputString, Environment.NewLine));
            Console.SetIn(sr);
            return inputString;
        }

    }
}