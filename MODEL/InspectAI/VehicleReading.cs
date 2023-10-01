using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
   public class VehicleReading
    {
        public string odometerReading { get; set; }
        public string vinReading { get; set; }
        public string fuelMeterReading { get; set; }
        public string licensePlateReading { get; set; }
        public string observations { get; set; }
        public string laborHoursEstimate { get; set; }
    }
}
