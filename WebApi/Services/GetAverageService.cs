using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public class GetAverageService : IGetAverageService
    {
        readonly ApplicationContext _applicationContext;

        public GetAverageService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public Dictionary<string, string> GetAverage()
        {
            var sum = 0;
            var count = 0;

            lock (_applicationContext)
            {
                sum = _applicationContext.Sum;
                count = _applicationContext.Counter;
            }

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

            var average = (float)sum / count;

            return new Dictionary<string, string>
            {
                { "average", average.ToString("F4") }
            };
        }
    }

    public interface IGetAverageService
    {
        Dictionary<string, string> GetAverage();
    }
}
