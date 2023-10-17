using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi;
using WebApi.Controllers;
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
            var pnc = new PostNumberService(ac);
            var gac = new GetAverageService(ac);

            pnc.InsertNumber(dict1);
            pnc.InsertNumber(dict2);
            pnc.InsertNumber(dict3);
            var result = gac.GetAverage();

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
            var pnc = new PostNumberService(ac);
            var gac = new GetAverageService(ac);

            pnc.InsertNumber(dict1);
            var result = gac.GetAverage();

            Assert.AreEqual("2,0000", result.First().Value);
        }

        [TestMethod]
        public void NoNumbers_NoAverage()
        {
            var ac = new ApplicationContext();
            var gac = new GetAverageService(ac);
            var result = gac.GetAverage();

            Assert.AreEqual("0,0000", result.First().Value);
        }
    }
}
