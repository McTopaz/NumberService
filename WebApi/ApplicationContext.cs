using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
{
    public static class ApplicationContext
    {
        // Holder of global data and is using lock for thread safe.

        static List<int> _usedNumbers = new List<int>();
        static object _lock = new object();

        public static List<int> UsedNumbers
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
