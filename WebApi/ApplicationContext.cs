using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
{
    public class ApplicationContext
    {
        public ConcurrentDictionary<int, bool> Numbers { get; } = new ConcurrentDictionary<int, bool>();
        public int Counter { get; set; } = 0;
        public int Sum { get; set; } = 0;
    }
}
