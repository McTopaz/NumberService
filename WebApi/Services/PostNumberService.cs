using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public class PostNumberService : IPostNumberService
    {
        readonly ApplicationContext _applicationContext;

        public PostNumberService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void InsertNumber(Dictionary<string, int> dict)
        {
            var number = dict.First().Value;

            lock (_applicationContext)
            {
                if (_applicationContext.Numbers.TryGetValue(number, out _))
                {
                    var msg = $"The number {number} is already posted and is rejected.";
                    throw new Exception(msg);

                }
                else if (_applicationContext.Numbers.TryAdd(number, true))
                {
                    _applicationContext.Numbers.TryAdd(number, true);
                    _applicationContext.Counter++;
                    _applicationContext.Sum += number;
                }
                else
                {
                    var msg = $"Unable to store the number {number}.";
                    throw new Exception(msg);
                }
            }
        }
    }

    public interface IPostNumberService
    {
        void InsertNumber(Dictionary<string, int> dict);
    }
}
