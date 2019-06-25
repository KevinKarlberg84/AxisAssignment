using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxisProgrammingAssignment.Models
{
    
/// <summary>
/// DTO for the percipitation information needed
/// </summary>
   public class TotalPercipitationModel
    {
        public string MeasurementStartDate { get; set; }
        public string MeasurementEndDate { get; set; }
        public double TotalAmountOfWaterInMilimeters { get; set; }

        public TotalPercipitationModel(string measurementStartDate, string measurementEndDate, double totalAmountOfWaterInMilimeters)
        {
            this.MeasurementStartDate = measurementStartDate;
            this.MeasurementEndDate = measurementEndDate;
            this.TotalAmountOfWaterInMilimeters = totalAmountOfWaterInMilimeters;
        }
    }
}
