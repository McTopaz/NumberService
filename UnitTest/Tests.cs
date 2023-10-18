using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi;
using WebApi.Services;

namespace UnitTest
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TwoDifferentNumbers_OneAverage()
        {
            var dict1 = new Dictionary<string, int>()
            {
                { "number", 2 }
            };

            var dict2 = new Dictionary<string, int>()
            {
                { "number", 3 }
            };

            var ac = new ApplicationContext();
            var pns = new PostNumberService(ac);
            var gas = new GetAverageService(ac);

            pns.InsertNumber(dict1);
            pns.InsertNumber(dict2);
            var result = gas.GetAverage();

            Assert.AreEqual("2,5000", result.First().Value);
        }

        [TestMethod]
        public void TwoEqualNumbers_OneAverage()
        {
            var dict1 = new Dictionary<string, int>()
            {
                { "number", 2 }
            };

            var dict2 = new Dictionary<string, int>()
            {
                { "number", 2 }
            };

            var ac = new ApplicationContext();
            var pns = new PostNumberService(ac);
            var gas = new GetAverageService(ac);

            pns.InsertNumber(dict1);
            Assert.ThrowsException<Exception>(() => pns.InsertNumber(dict2));
            var result = gas.GetAverage();

            Assert.AreEqual("2,0000", result.First().Value);
        }

        [TestMethod]
        public void DifferentNumbers_OneAverageWithAllFourDecimals()
        {
            // (1 + 41 + 29) / 3 = 23,66666...666667.
            // Rounded to 23,6667.

            var dict1 = new Dictionary<string, int>()
            {
                { "number", 1 }
            };

            var dict2 = new Dictionary<string, int>()
            {
                { "number", 41 }
            };

            var dict3 = new Dictionary<string, int>()
            {
                { "number", 29 }
            };

            var ac = new ApplicationContext();
            var pns = new PostNumberService(ac);
            var gas = new GetAverageService(ac);

            pns.InsertNumber(dict1);
            pns.InsertNumber(dict2);
            pns.InsertNumber(dict3);
            var result = gas.GetAverage();

            Assert.AreEqual("23,6667", result.First().Value);
        }

        [TestMethod]
        public void OneNumber_OneAverage()
        {
            var dict1 = new Dictionary<string, int>()
            {
                { "number", 2 }
            };

            var ac = new ApplicationContext();
            var pns = new PostNumberService(ac);
            var gas = new GetAverageService(ac);

            pns.InsertNumber(dict1);
            var result = gas.GetAverage();

            Assert.AreEqual("2,0000", result.First().Value);
        }

        [TestMethod]
        public void NoNumbers_NoAverage()
        {
            var ac = new ApplicationContext();
            var gas = new GetAverageService(ac);
            var result = gas.GetAverage();

            Assert.AreEqual("0,0000", result.First().Value);
        }

        [TestMethod]
        public void TwoDifferentNumbersSimultaneous_OneAverage()
        {
            var dict1 = new Dictionary<string, int>()
            {
                { "number", 2 }
            };

            var dict2 = new Dictionary<string, int>()
            {
                { "number", 3 }
            };

            var ac = new ApplicationContext();
            var pns1 = new PostNumberService(ac);
            var pns2 = new PostNumberService(ac);
            var gas = new GetAverageService(ac);

            var thread1 = new Thread(() => pns1.InsertNumber(dict1));
            var thread2 = new Thread(() => pns2.InsertNumber(dict2));

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            var result = gas.GetAverage();

            Assert.AreEqual("2,5000", result.First().Value);
        }
    }
}
