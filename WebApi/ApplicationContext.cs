using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
{
    public class ApplicationContext
    {
        // Holder of global data and is using lock for thread safe.

        List<int> _usedNumbers = new List<int>();
        object _lock = new object();

        public List<int> UsedNumbers
        {
            get
            {
                lock (_lock)
                {
                    return _usedNumbers;
                }
            }
        }
    }
}
