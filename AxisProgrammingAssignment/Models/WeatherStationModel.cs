using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxisProgrammingAssignment.Models
{
    public class WeatherStationModel
    {
        /// <summary>
        /// DTO for the weather station information
        /// </summary>
        public DateTime DateOfMeasurement { get; set; }
        public string City { get; set; }
        public string Measurement { get; set; }

        public WeatherStationModel(DateTime dateofMeasurement, string city, string measurement)
        {
            this.DateOfMeasurement = dateofMeasurement;
            this.City = city;
            this.Measurement = measurement;
        }
    }
}
