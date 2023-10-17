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
        public int Counter { get; set; } = 0;
        public int Sum { get; set; } = 0;
    }
}
