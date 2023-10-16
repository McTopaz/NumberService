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

namespace UnitTest
{
    [TestClass]
    public class Tests
    {
        private void ResetPostedNumbers()
        {
            ApplicationContext.UsedNumbers.Clear();
        }

        [TestMethod]
        public void TwoDifferentNumbers_OneAverage()
        {
            ResetPostedNumbers();

            var dict1 = new Dictionary<string, int>()
            {
                { "number", 2 }
            };

            var dict2 = new Dictionary<string, int>()
            {
                { "number", 3 }
            };

            var pnc = new PostNumberController();
            var gac = new GetAverageController();

            var resultPnc1 = pnc.Post(dict1);
            var resultPnc2 = pnc.Post(dict2);
            var result = gac.Get();

            Assert.IsInstanceOfType(resultPnc1, typeof(OkResult));
            Assert.IsInstanceOfType(resultPnc2, typeof(OkResult));
            Assert.AreEqual("2,5000", result.First().Value);
        }

        [TestMethod]
        public void TwoEqualNumbers_OneAverage()
        {
            ResetPostedNumbers();

            var dict1 = new Dictionary<string, int>()
            {
                { "number", 2 }
            };

            var dict2 = new Dictionary<string, int>()
            {
                { "number", 2 }
            };

            var pnc = new PostNumberController();
            var gac = new GetAverageController();

            var resultPnc1 = pnc.Post(dict1);
            var resultPnc2 = pnc.Post(dict2);
            var result = gac.Get();

            Assert.IsInstanceOfType(resultPnc1, typeof(OkResult));
            Assert.IsInstanceOfType(resultPnc2, typeof(BadRequestErrorMessageResult));
            Assert.AreEqual("2,0000", result.First().Value);
        }

        [TestMethod]
        public void DifferentNumbers_OneAverageWithAllFourDecimals()
        {
            // (1 + 41 + 29) / 3 = 23,66666...666667.
            // Rounnded to 23,6667.

            ResetPostedNumbers();

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

            var pnc = new PostNumberController();
            var gac = new GetAverageController();

            var resultPnc1 = pnc.Post(dict1);
            var resultPnc2 = pnc.Post(dict2);
            var resultPnc3 = pnc.Post(dict3);
            var result = gac.Get();

            Assert.IsInstanceOfType(resultPnc1, typeof(OkResult));
            Assert.IsInstanceOfType(resultPnc2, typeof(OkResult));
            Assert.IsInstanceOfType(resultPnc3, typeof(OkResult));
            Assert.AreEqual("23,6667", result.First().Value);
        }

        [TestMethod]
        public void OneNumber_OneAverage()
        {
            ResetPostedNumbers();

            var dict1 = new Dictionary<string, int>()
            {
                { "number", 2 }
            };

            var pnc = new PostNumberController();
            var gac = new GetAverageController();

            var resultPnc = pnc.Post(dict1);
            var result = gac.Get();

            Assert.IsInstanceOfType(resultPnc, typeof(OkResult));
            Assert.AreEqual("2,0000", result.First().Value);
        }

        [TestMethod]
        public void NoNumbers_NoAverage()
        {
            ResetPostedNumbers();

            var gac = new GetAverageController();
            var result = gac.Get();

            Assert.AreEqual("0,0000", result.First().Value);
        }
    }
}
