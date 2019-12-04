using ConsoleAppIEnumerableExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTestProjectIEnumerableExtension
{
    [TestClass]
    public class UnitTestIEnumerable
    {
        /// <summary>
        /// method that tests if sum extenstion for a list of int is working properly
        /// </summary>
        [TestMethod]
        public void TestMethodIntSum()
        {
            List<int> intList = new List<int> { 1, 5, 4, 9, 12 };
            int expectedIntSum = 31;

            int currentIntSum = intList.Sum();

            Assert.AreEqual(expectedIntSum, currentIntSum);
        }
        /// <summary>
        /// tests if min for a list of int is ok
        /// </summary>
        [TestMethod]
        public void TestMethodIntMin()
        {
            List<int> intList = new List<int> { 1, 5, 4, 9, 12 };
            int expectedIntMin = 1;

            int currentIntMin = intList.Min();

            Assert.AreEqual(expectedIntMin, currentIntMin);
        }
        /// <summary>
        /// tests if max for a double list is working properly
        /// </summary>
        [TestMethod]
        public void TestMethodDoubleMax()
        {
            List<double> doubleList = new List<double> { 1.5, 5.9, 4.2, 9, 12.3 };
            double expectedDoubleMax = 12.3;

            double currentDoubleMax = doubleList.Max();

            Assert.AreEqual(expectedDoubleMax, currentDoubleMax);
        }
    }
}
