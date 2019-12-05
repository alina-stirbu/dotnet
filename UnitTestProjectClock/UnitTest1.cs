using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleAppIClock;
using System;

namespace UnitTestProjectClock
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// test method to check if method CompareTo from BusinessDate is working properly
        /// </summary>
        [TestMethod]
        public void TestMethodClockCompare()
        {
            BusinessDate businessDate = new BusinessDate(new DateTime(2019, 12, 10));
            BusinessDate businessDate1 = new BusinessDate(new DateTime(2019, 12, 15));
            int expectedResult = -1;

            int actualResult = businessDate.CompareTo(businessDate1);

            Assert.AreEqual(expectedResult, actualResult);

        }
    }
}
