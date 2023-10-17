using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
{
    public class ApplicationContext
    {
        public Dictionary<int, bool> Numbers { get; } = new Dictionary<int, bool>();
        public int NumberCount = 0;
        public long Sum = 0;

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
