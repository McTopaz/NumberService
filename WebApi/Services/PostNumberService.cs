using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace WebApi.Services
{
    public class PostNumberService : IPostNumberService
    {
        readonly ApplicationContext _applicationContext;

        public PostNumberService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public (bool success, string message) InsertNumber(Dictionary<string, int> dict)
        {
            var number = dict.First().Value;

            lock (_applicationContext)
            {
                if (_applicationContext.Numbers.TryGetValue(number, out _))
                {
                    var msg = $"The number {number} is already posted and is rejected.";
                    return (false, msg);

                }
                else if (_applicationContext.Numbers.TryAdd(number, true))
                {
                    _applicationContext.Numbers.TryAdd(number, true);
                    _applicationContext.Counter++;
                    _applicationContext.Sum += number;
                    return (true, string.Empty);
                }
                else
                {
                    var msg = $"Unable to store the number {number}.";
                    return (false, msg);
                }
            }
        }
    }

    public interface IPostNumberService
    {
        (bool success, string message) InsertNumber(Dictionary<string, int> dict);
    }
}
