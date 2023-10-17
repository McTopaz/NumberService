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

            if (!_applicationContext.Numbers.TryGetValue(number, out _))
            {
                _applicationContext.Numbers.Add(number, true);
                _applicationContext.Counter++;
                _applicationContext.Sum += number;
                return;
            }
            else
            {
                var msg = $"The number {number} is already posted and is rejected";
                throw new Exception(msg);
            }
        }
    }

    public interface IPostNumberService
    {
        void InsertNumber(Dictionary<string, int> dict);
    }
}
