using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp2
{
    public class ConsumptionData
    {
        public DateTime Time { get; set; }
        public PowerData Power { get; set; }
    }

    public class PowerData
    {
        public string Meter_Number { get; set; }
        public double Total_in { get; set; }
        public double Power_curr { get; set; }

        public double Total_out { get; set; }

    }
}
