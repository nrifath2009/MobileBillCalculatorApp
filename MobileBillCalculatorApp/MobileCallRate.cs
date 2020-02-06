using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBillCalculatorApp
{
    public class MobileCallRate
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Rate { get; set; }
        public DurationTypeEnum DurationType { set;get; }

    }
}
