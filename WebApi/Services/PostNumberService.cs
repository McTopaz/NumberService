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

        public void InsertNumber(int number)
        {
            if (_applicationContext.UsedNumbers.Contains(number))
            {
                var msg = $"The number {number} is already posted and is rejected";
                throw new Exception(msg);
            }

            _applicationContext.UsedNumbers.Add(number);
        }
    }

    public interface IPostNumberService
    {
        void InsertNumber(int number);
    }

}
