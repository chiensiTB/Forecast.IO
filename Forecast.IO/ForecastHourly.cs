using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace Forecast_IO
{
    public class ForecastHourly
    {
        public double ApparentTemperature { get; set; }
        public double CloudCover { get; set; }
        public double DewPoint { get; set; }
        public double Humidity { get; set; }
        public string Icon { get; set; }
        public double Ozone { get; set; }
        public double PrecipitationIntensity { get; set; }
        public double PrecipitationProbability { get; set; }
        //precipitation type?
        public double Pressure { get; set; }
        public string Summary {get;set;}
        public double Temperature { get; set; }
        public DateTimeOffset Time { get; set; }
        public LocalDateTime LocalTime { get; set; }
        public double Visibility { get; set; }
        public double WindBearing { get; set; }
        public double WindSpeed { get; set; }

    }
}
