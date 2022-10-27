using MQTTnet.Server;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FunctionApp2
{
    
    public class PowerConsumption
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Time { get; set; } 
        public double Power { get; set; }
        public double Consumption { get; set; }

    }
}
