using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class GetAverageController : ApiController
    {
        // GET http://localhost:9000/api/getaverage/
        public Dictionary<string, string> Get()
        {
            var count = ApplicationContext.UsedNumbers.Count();

            // When there are no numbers posted.
            // Prevent DivideByZeroException or NaN.
            // Return no average.
            if (count == 0)
            {
                return new Dictionary<string, string>
                {
                    { "average", "0,0000" }
                };
            }

            var sum = ApplicationContext.UsedNumbers.Sum();
            var average = (float)sum / count;

            return new Dictionary<string, string>
            {
                { "average", average.ToString("F4") }
            };
        }
    }
}
